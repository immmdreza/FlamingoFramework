namespace Flamingo.RateLimiter
{
    /// <summary>
    /// Add you limiter here to check and add it automatically
    /// </summary>
    public class AutoLimitBuilder<T, TResult>
    {
        /// <summary>
        /// Add you limiter here to check and add it automatically
        /// </summary>
        public AutoLimitBuilder(ILimited<T, TResult> limited)
        {
            Limited = limited;
        }

        /// <summary>
        /// Get automatically created limiter
        /// </summary>
        public ILimited<T, TResult> GetNew => Limited.CreateYourSelf(Result);

        /// <summary>
        /// Limiter you saved
        /// </summary>
        public ILimited<T, TResult> Limited { get; }

        /// <summary>
        /// Sets incoming obj
        /// </summary>
        /// <param name="obj"></param>
        public void SetResult(T obj)
        {
            _result = Limited.Selector.Compile()(obj);
        }

        /// <summary>
        /// Gets the target property to limit
        /// </summary>
        public TResult Result => _result;

        private TResult _result;
    }
}
