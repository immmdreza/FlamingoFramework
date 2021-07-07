using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Telegram.Bot.Types;
using Flamingo.RateLimiter.Limits;
using Flamingo.RateLimiter.Limiters;

namespace Flamingo.RateLimiter.Tests
{
    [TestClass()]
    public class RateLimitManagerTests
    {
        private readonly RateLimitManager manager = new RateLimitManager();

        [TestMethod()]
        public void RateLimitManagerTest()
        {
            var limit = new BaseLimiter<Message, User>(
                new User { Id = 1000 },
                x => x.From,
                new TimeLimit(TimeSpan.FromSeconds(10)),
                (x, y) => x.Id == y.Id);

            manager.AddLimit(limit);

            var limits = manager.GetLimiteds<Message, User>();

            var message = new Message
            {
                From = new User
                {
                    Id = 2000
                }
            };

            foreach (var l in limits)
            {
                var res = l.LimitedYet(message);
            }
        }

        [TestMethod()]
        public void MessageSenderLimitsTest()
        {
            manager.AddLimit(new MessageSenderLimiter(1000, TimeSpan.FromSeconds(10)));

            var message = new Message
            {
                From = new User
                {
                    Id = 1000
                }
            };

            var l = manager.IsLimited(message);
        }

        [TestMethod()]
        public void MessageSenderLimitsTest2()
        {
            var lim = new MessageSenderLimiter(1000, TimeSpan.FromSeconds(10));

            var message = new Message
            {
                From = new User
                {
                    Id = 1000
                }
            };


            manager.AddAutoLimit(new AutoLimitBuilder<Message, User>(lim));

            manager.CheckAutoBuilders(message);

            manager.CheckAutoBuilders(message);
        }
    }
}