using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Threading.Tasks;

namespace Flamingo.Fishes
{
    /// <summary>
    /// Base interface to build inComing handlers
    /// </summary>
    /// <typeparam name="T">Type of incoming update</typeparam>
    public interface IFish<T>
    {
        /// <summary>
        /// This is a sync filter that filters incoming updates
        /// </summary>
        public IFilter<ICondiment<T>> Filter { get; }

        /// <summary>
        /// This is a async filter that filters incoming updates
        /// </summary>
        public IFilterAsync<ICondiment<T>> FilterAsync { get; }

        /// <summary>
        /// Here you can choose what you want to do with incoming update
        /// </summary>
        public Func<ICondiment<T>, Task<bool>> GetEaten { get; }

        /// <summary>
        /// Check if this should be processed based on <see cref="Filter"/> and <see cref="FilterAsync"/>
        /// </summary>
        /// <param name="inComing">InComing ICondiment of update</param>
        /// <returns>True if filters passed</returns>
        public async Task<bool> ShouldEatAsync(ICondiment<T> inComing)
        {
            if(_ShouldEat(inComing))
            {
                return await _ShouldEatAsync(inComing);
            }

            return false;
        }

        private async Task<bool> _ShouldEatAsync(ICondiment<T> inComing)
        {
            if (FilterAsync == null) return true;

            return await FilterAsync.IsPassed(inComing);
        }

        private bool _ShouldEat(ICondiment<T> inComing)
        {
            if (Filter == null) return true;

            return Filter.IsPassed(inComing);
        }
    }
}
