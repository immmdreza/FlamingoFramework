using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System;
using System.Threading.Tasks;

namespace Flamingo.Fishes.InComingFishes.SimpleInComings
{
    public class SimpleInComing<T> : InComingFish<T>
    {
        public SimpleInComing(Func<ICondiment<T>, Task<bool>> getEatenCallback,
            IFilter<ICondiment<T>> filter = null,
            IFilterAsync<ICondiment<T>> filterAsync = null)
            : base(filter, filterAsync, getEatenCallback)
        { }
    }
}
