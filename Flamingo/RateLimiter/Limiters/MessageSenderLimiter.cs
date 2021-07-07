using Flamingo.RateLimiter.Limits;
using System;
using Telegram.Bot.Types;

namespace Flamingo.RateLimiter.Limiters
{
    /// <summary>
    /// Limit base on message sender id
    /// </summary>
    public class MessageSenderLimiter : BaseLimiter<Message, User>
    {
        /// <summary>
        /// Limit base on message sender id
        /// </summary>
        /// <param name="senderId">Message sender id to limit</param>
        /// <param name="limit">Limit to be applied</param>
        public MessageSenderLimiter(long senderId, ILimit limit) 
            : base(new User { Id = senderId }, x=> x.From,
                  limit, (x, y) => x.Id == x.Id)
        { }

        /// <summary>
        /// Limit base on message sender id
        /// </summary>
        /// <param name="senderId">Message sender id to limit</param>
        /// <param name="timeSpan">Time span for a timed limit</param>
        /// <param name="waitForLimit">Block and wait for limit</param>
        public MessageSenderLimiter(long senderId, TimeSpan timeSpan, bool waitForLimit = false)
            : base(new User { Id = senderId }, x => x.From,
                  new TimeLimit(timeSpan, waitForLimit), (x, y) => x.Id == x.Id)
        { }
    }
}
