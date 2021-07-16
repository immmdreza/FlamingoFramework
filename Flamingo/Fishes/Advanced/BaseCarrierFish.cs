using Flamingo.Attributes.Filters;
using Flamingo.Attributes.Filters.Async;
using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Flamingo.Fishes.Advanced
{
    public class BaseCarrierFish<T, U> : 
        IDisposable, 
        ICarrier<U>, 
        IAdvFish<T> where U: IAdvFish<T>
    {
        public BaseCarrierFish(Carrier<U> carrier,
            IFilter<ICondiment<T>> filter = null,
            IFilterAsync<ICondiment<T>> filterAsync = null)
        {
            Carrier = carrier;

            var attr = typeof(U).GetCustomAttributes(false);

            if(filter == null)
            {
                if (!attr.Any())
                    return;

                var filters = attr
                    .Where(x => x is IFilterAttribute<T>)
                    .Cast<IFilterAttribute<T>>()
                    .Select(x => x.Filter)
                    .ToList();

                Filter = FilterBase<ICondiment<T>>.Combine(filters);
            }
            else
            {
                Filter = filter;
            }

            if(filterAsync == null)
            {
                if (!attr.Any())
                    return;

                var asyncFilters = attr
                    .Where(x => x is IFilterAsyncAttribute<T>)
                    .Cast<IFilterAsyncAttribute<T>>()
                    .Select(x => x.Filter)
                    .ToList();

                FilterAsync = FilterBaseAsync<ICondiment<T>>.Combine(asyncFilters);
            }
            else
            {
                FilterAsync = filterAsync;
            } 
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
