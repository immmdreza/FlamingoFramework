using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Telegram.Bot.Types;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// InComing handler for ChosenInlineResult update
    /// </summary>
    public class InComingChosenInlineResult : InComingFish<ChosenInlineResult>
    {
        /// <summary>
        /// InComing handler for ChosenInlineResult update
        /// </summary>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingChosenInlineResult(
            IFilter<ICondiment<ChosenInlineResult>> filter = null,
            IFilterAsync<ICondiment<ChosenInlineResult>> filterAsync = null) 
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public ChosenInlineResultCondiment RealCdmt => Cdmt as ChosenInlineResultCondiment;
    }
}
