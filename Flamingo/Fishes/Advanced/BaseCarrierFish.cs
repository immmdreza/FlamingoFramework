using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Threading.Tasks;

namespace Flamingo.Fishes.Advanced
{
    public class BaseCarrierFish<T, U> : 
        IDisposable, 
        ICarrier<U>, 
        IAdvFish<T> where U: IAdvFish<T>
    {
        public BaseCarrierFish(Carrier<U> carrier,
            IFilter<ICondiment<T>> filter,
            IFilterAsync<ICondiment<T>> filterAsync)
        {
            Carrier = carrier;
            Filter = filter;
            FilterAsync = filterAsync;
        }

        /// <inheritdoc/>
        public void SetupFish()
        {
            var c = Carrier.Create();

            _getEaten = c.GetEaten;
        }

        /// <inheritdoc/>
        public void ClearFish()
        {
            Carrier.Dispose();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            ClearFish();
        }

        /// <inheritdoc/>
        public Carrier<U> Carrier { get; }


        private Func<ICondiment<T>, Task<bool>> _getEaten;


        /// <inheritdoc/>
        public IFilter<ICondiment<T>> Filter { get; }

        /// <inheritdoc/>
        public IFilterAsync<ICondiment<T>> FilterAsync { get; }

        /// <inheritdoc/>
        public Func<ICondiment<T>, Task<bool>> GetEaten => _getEaten;
    }
}
