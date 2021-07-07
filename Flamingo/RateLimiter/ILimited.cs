using System;
using System.Linq.Expressions;

namespace Flamingo.RateLimiter
{
    public interface ILimited<T, TResult>
    {
        /// <summary>
        /// Creates a limit for incoming obj
        /// </summary>
        public ILimited<T, TResult> CreateYourSelf(TResult obj);

        /// <summary>
        /// Limit that applied on this limited obj
        /// </summary>
        public ILimit Limit { get; }

        /// <summary>
        /// Get the property value of selector
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public TResult GetValue(T obj)
        {
            var func = Selector.Compile();
            return func(obj);
        }

        /// <summary>
        /// Check equality
        /// </summary>
        public bool IsMyTResult(TResult result)
        {
            if (Comparer != null)
            {
                return Comparer(Limited, result);
            }
            else
            {
                return Limited.Equals(result);
            }
        }

        /// <summary>
        /// Function to check if this is what you want
        /// </summary>
        public Func<TResult, TResult, bool> Comparer { get; }

        /// <summary>
        /// Obj which is limited
        /// </summary>
        public TResult Limited { get; }

        /// <summary>
        /// Check limit
        /// </summary>
        public bool LimitedYet(T result);

        /// <summary>
        /// Selector for <see cref="Limited"/>
        /// </summary>
        public Expression<Func<T, TResult>> Selector { get; }
    }

    public interface ILimited<T>
    {
        /// <summary>
        /// Limit that applied on this limited obj
        /// </summary>
        public ILimit Limit { get; }


        /// <summary>
        /// Obj which is limited
        /// </summary>
        public T Limited { get; }

        /// <summary>
        /// Check limit
        /// </summary>
        public bool LimitedYet(T result);
    }
}
