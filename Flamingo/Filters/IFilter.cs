using System;

namespace Flamingo.Filters
{
    /// <summary>
    /// Base interface to make filters
    /// </summary>
    /// <typeparam name="T">Obj type to filter</typeparam>
    public interface IFilter<T>
    {
        /// <summary>
        /// A callback function that takes T as parameter
        /// and returns true if this filter passed otherwise returns false.
        /// </summary>
        public Func<T, bool> Filter { get; }

        /// <summary>
        /// Just calls <see cref="Filter"/> and returns its result.
        /// </summary>
        /// <param name="inComing">Incoming object of type T</param>
        /// <returns>returns true if this filter passed otherwise returns false</returns>
        public bool IsPassed(T inComing);
    }
}
