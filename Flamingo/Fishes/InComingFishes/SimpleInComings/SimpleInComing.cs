using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Threading.Tasks;

namespace Flamingo.Fishes.InComingFishes.SimpleInComings
{
    /// <summary>
    /// A class to quickly create an update handler with filters and callback function
    /// </summary>
    /// <typeparam name="T">Type of incoming update</typeparam>
    public class SimpleInComing<T> : InComingFish<T>
    {
        /// <summary>
        /// A class to quickly create an update handler with filters and callback function
        /// </summary>
        /// <param name="getEatenCallback">
        /// Callback function that takes an <see cref="ICondiment{T}"/> and returns boolean
        /// if it returns true then the incoming update will be handle in other handlers that passed filters
        /// for this update and they are added after this handler.
        /// </param>
        /// <param name="filter">Pass your sync filter or a combination of them</param>
        /// <param name="filterAsync">Pass your async filter or a combination of them</param>
        public SimpleInComing(Func<ICondiment<T>, Task<bool>> getEatenCallback,
            IFilter<ICondiment<T>> filter = null,
            IFilterAsync<ICondiment<T>> filterAsync = null)
            : base(filter, filterAsync, getEatenCallback)
        { }
    }
}
