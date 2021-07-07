namespace Flamingo.RateLimiter.Limits
{
    /// <summary>
    /// Limit to send after a specified amount of things
    /// </summary>
    public class CountLimit : ILimit
    {
        /// <summary>
        /// Limit to send after a specified amount of things
        /// </summary>
        public CountLimit(int afterCount)
        {
            _afterCount = afterCount;
            _sentCount = 0;
        }

        /// <summary>
        /// One step closer to limit
        /// </summary>
        public void Sent(int increament = 1)
        {
            _sentCount += increament;
        }

        private int _afterCount;

        private int _sentCount;

        /// <inheritdoc/>
        public bool IsYet
        {
            get
            {
                return _sentCount >= _afterCount;
            }
        }
    }
}
