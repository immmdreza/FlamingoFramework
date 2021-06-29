using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// InComing handler for PreCheckoutQueries
    /// </summary>
    public abstract class InComingPreCheckoutQuery : InComingFish<PreCheckoutQuery>
    {
        /// <summary>
        /// InComing handler for PreCheckoutQueries
        /// </summary>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingPreCheckoutQuery(
            IFilter<ICondiment<PreCheckoutQuery>> filter = null,
            IFilterAsync<ICondiment<PreCheckoutQuery>> filterAsync = null) 
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public PreCheckoutQueryCondiment RealCdmt
        {
            get
            {
                if (Cdmt is PreCheckoutQueryCondiment cdmt)
                {
                    return cdmt;
                }

                return null;
            }
        }
    }
}
