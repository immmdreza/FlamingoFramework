using System;
using System.Linq.Expressions;

namespace Flamingo.RateLimiter
{
    /// <summary>
    /// Base class to limit things
    /// </summary>
    /// <typeparam name="T">Object type to check limit on its property</typeparam>
    /// <typeparam name="TResult">Property of object to be limited</typeparam>
    public class BaseLimiter<T, TResult> : ILimited<T, TResult>
    {
        /// <summary>
        /// Base class to limit things
        /// </summary>
        /// <param name="limited">Object that should be limited</param>
        /// <param name="selector">Expression to select the limited property</param>
        /// <param name="limit">The limit you like</param>
        /// <param name="comparer">It's how we compare two objects</param>
        public BaseLimiter(
            TResult limited,
            Expression<Func<T, TResult>> selector,
            ILimit limit,
            Func<TResult, TResult, bool> comparer = null)
        {
            Limited = limited;
            Limit = limit;
            Selector = selector;
            Comparer = comparer;
        }

        /// <inheritdoc/>
        public ILimit Limit { get; }

        /// <inheritdoc/>
        public Expression<Func<T, TResult>> Selector { get; }

        /// <inheritdoc/>
        public TResult Limited { get; }

        /// <inheritdoc/>
        public Func<TResult, TResult, bool> Comparer { get; }

        /// <inheritdoc/>
        public ILimited<T, TResult> CreateYourSelf(TResult obj)
        {
            return new BaseLimiter<T, TResult>(obj, Selector, Limit, Comparer);
        }

        /// <inheritdoc/>
        public bool LimitedYet(T result)
        {
            var asLimited = this as ILimited<T, TResult>;
            var value = asLimited.GetValue(result);

            if(asLimited.IsMyTResult(value))
            {
                return Limit.IsYet;
            }

            return false;
        }
    }

    /// <summary>
    /// Base class to limit things
    /// </summary>
    /// <typeparam name="T">Type of the thing</typeparam>
    public class BaseLimiter<T> : ILimited<T>
    {
        /// <summary>
        /// Base class to limit things
        /// </summary>
        /// <param name="limited">Object to be limited</param>
        /// <param name="limit">The Limit you want</param>
        public BaseLimiter(
            T limited,
            ILimit limit)
        {
            Limited = limited;
            Limit = limit;
        }

        /// <inheritdoc/>
        public ILimit Limit { get; }

        /// <inheritdoc/>
        public T Limited { get; }

        /// <inheritdoc/>
        public bool LimitedYet(T result)
        {
            if (Limited.Equals(result))
            {
                return Limit.IsYet;
            }

            return false;
        }
    }
}
