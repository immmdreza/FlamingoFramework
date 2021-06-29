using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Telegram.Bot.Types;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// InComing update handler for InlineQuery
    /// </summary>
    public abstract class InComingInlineQuery : InComingFish<InlineQuery>
    {
        /// <summary>
        /// InComing update handler for InlineQuery
        /// </summary>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingInlineQuery(IFilter<ICondiment<InlineQuery>> filter = null,
            IFilterAsync<ICondiment<InlineQuery>> filterAsync = null)
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public InlineQueryCondiment RealCdmt
        {
            get
            {
                if (Cdmt is InlineQueryCondiment cdmt)
                {
                    return cdmt;
                }

                return null;
            }
        }
    }
}
