using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// InComing handler for ShippingQueries
    /// </summary>
    public abstract class InComingShippingQuery : InComingFish<ShippingQuery>
    {
        /// <summary>
        /// InComing handler for ShippingQueries
        /// </summary>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingShippingQuery(
            IFilter<ICondiment<ShippingQuery>> filter = null,
            IFilterAsync<ICondiment<ShippingQuery>> filterAsync = null) 
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public ShippingQueryCondiment RealCdmt
        {
            get
            {
                if (Cdmt is ShippingQueryCondiment cdmt)
                {
                    return cdmt;
                }

                return null;
            }
        }
    }
}
