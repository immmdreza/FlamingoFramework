using System;
using System.Threading;

namespace Flamingo.RateLimiter.Limits
{
    /// <summary>
    /// Timed Limit
    /// </summary>
    public class TimeLimit : ILimit
    {
        /// <summary>
        /// Timed Limit
        /// </summary>
        public TimeLimit(TimeSpan duration, bool waitForLimit = false)
        {
            _started = DateTime.UtcNow;
            Duration = duration;
            _wait = waitForLimit;
        }

        private bool _wait;

        /// <summary>
        /// When time limit started
        /// </summary>
        private DateTime _started;

        /// <summary>
        /// Duration of a time limit
        /// </summary>
        public TimeSpan Duration { get; }

        private int _inSleep = 0;

        /// <inheritdoc/>
        public bool IsYet
        {
            get
            {
                if(_started + Duration > DateTime.UtcNow)
                {
                    if(_wait)
                    {
                        if(_inSleep > 10)
                        {
                            return true;
                        }

                        _inSleep++;
                        Thread.Sleep(
                            (int)(_started + (Duration*_inSleep) - DateTime.UtcNow)
                                .TotalMilliseconds);

                        _inSleep--;
                        _started = DateTime.UtcNow;
                        return false;
                    }

                    return true;
                }
                else
                {
                    _started = DateTime.UtcNow;
                    return false;
                }
            }
        }
    }
}
