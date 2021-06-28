using Flamingo.Condiments;
using System;

namespace Flamingo.Filters
{
    /// <summary>
    /// Use this class to quickly create a filter of update type T
    /// </summary>
    public class SimpleFilter<T> : FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Use this class to quickly create a filter of update type T
        /// </summary>
        /// <param name="filter">A function that gets an update of type T and return true to pass</param>
        public SimpleFilter(Func<ICondiment<T>, bool> filter) : base(filter)
        { }
    }
}
