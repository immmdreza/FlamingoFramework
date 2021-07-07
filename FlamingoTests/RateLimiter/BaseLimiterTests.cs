using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Telegram.Bot.Types;
using Flamingo.RateLimiter.Limits;
using System.Threading;

namespace Flamingo.RateLimiter.Tests
{
    [TestClass()]
    public class BaseLimiterTests
    {
        [TestMethod()]
        public void BaseLimiterTest()
        {
            var limit = new BaseLimiter<Message, long>(
                1000,
                x => x.From.Id,
                new TimeLimit(TimeSpan.FromSeconds(10)));

            Thread.Sleep(9000);

            Assert.IsTrue(limit.LimitedYet(new Message
            {
                From = new User
                { Id = 1000 }
            }));
        }
    }
}