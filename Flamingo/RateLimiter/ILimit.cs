namespace Flamingo.RateLimiter
{
    /// <summary>
    /// Base interface for limits
    /// </summary>
    public interface ILimit
    {
        /// <summary>
        /// Indicates that a limit is still valid
        /// </summary>
        public bool IsYet { get; }
    }
}
