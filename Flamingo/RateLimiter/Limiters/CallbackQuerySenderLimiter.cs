using Flamingo.RateLimiter.Limits;
using System;
using Telegram.Bot.Types;

namespace Flamingo.RateLimiter.Limiters
{
    /// <summary>
    /// Create a limit for callback query sender
    /// </summary>
    public class CallbackQuerySenderLimiter : BaseLimiter<CallbackQuery, User>
    {
        /// <summary>
        /// Create a limit for callback query sender
        /// </summary>
        public CallbackQuerySenderLimiter(long userId, ILimit limit) 
            : base(new User { Id = userId }, x=> x.From, limit, (x, y)=> x.Id == y.Id)
        { }

        /// <summary>
        /// Create a limit for callback query sender
        /// </summary>
        public CallbackQuerySenderLimiter(
            long userId,
            TimeSpan timeSpan,
            bool waitForLimit = false)
            : base(new User { Id = userId },
                  x => x.From, new TimeLimit(timeSpan, waitForLimit),
                  (x, y) => x.Id == y.Id)
        { }

        /// <summary>
        /// Create a limit for callback query sender
        /// </summary>
        public CallbackQuerySenderLimiter(
            TimeSpan timeSpan,
            bool waitForLimit = false)
            : base(new User { Id = 0 },
                  x => x.From, new TimeLimit(timeSpan, waitForLimit),
                  (x, y) => x.Id == y.Id)
        { }
    }
}
