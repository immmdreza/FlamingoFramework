using Flamingo.Condiments;
using System;
using System.Threading.Tasks;

namespace Flamingo.Filters.Async
{
    public class SimpleFilterAsync<T> : FilterBaseAsync<ICondiment<T>>
    {
        /// <summary>
        /// Use this class to quickly create an async filter of update type T
        /// </summary>
        /// <param name="filter">A function that gets an update of type T and return true to pass</param>
        public SimpleFilterAsync(Func<ICondiment<T>, Task<bool>> filter) : base(filter)
        {
        }
    }
}
