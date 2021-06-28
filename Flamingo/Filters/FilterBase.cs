using System;
using System.Collections.Generic;
using System.Linq;

namespace Flamingo.Filters
{
    /// <summary>
    /// Base class to build filters of Type T
    /// </summary>
    /// <typeparam name="T">Type of obj to filter</typeparam>
    public class FilterBase<T> : IFilter<T>
    {
        /// <summary>
        /// Base class to build filters of Type T
        /// </summary>
        /// <param name="filter">A function that takes T as parameter
        /// and returns true if this filter passed otherwise returns false.</param>
        public FilterBase(Func<T, bool> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public Func<T, bool> Filter { get; }

        /// <inheritdoc/>
        public bool IsPassed(T inComing)
        {
            return Filter(inComing);
        }

        /// <summary>
        /// Combines a set of filters
        /// </summary>
        /// <param name="filters">A set of filters</param>
        /// <returns>A filter that is a combination of inComing set</returns>
        public static FilterBase<T> Combine(IEnumerable<FilterBase<T>> filters)
        {
            if (!filters.Any()) return null;

            var main = filters.ElementAt(0);

            foreach (var filter in filters.Skip(1))
            {
                main &= filter;
            }

            return main;
        }

        /// <summary>
        /// It is AND operator
        /// </summary>
        public static FilterBase<T> operator &(FilterBase<T> a, FilterBase<T> b)
        {
            return new FilterBase<T>(x => a.IsPassed(x) && b.IsPassed(x));
        }

        /// <summary>
        /// It is OR operator
        /// </summary>
        public static FilterBase<T> operator |(FilterBase<T> a, FilterBase<T> b)
        {
            return new FilterBase<T>(x => a.IsPassed(x) || b.IsPassed(x));
        }

        /// <summary>
        /// It is NOT operator
        /// </summary>
        public static FilterBase<T> operator ~(FilterBase<T> a)
        {
            return new FilterBase<T>(x => !a.IsPassed(x));
        }
    }
}
