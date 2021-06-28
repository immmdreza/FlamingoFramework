using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Telegram.Bot.Types;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// InComing handler for Polls
    /// </summary>
    public class InComingPoll : InComingFish<Poll>
    {
        /// <summary>
        /// InComing handler for Polls
        /// </summary>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingPoll(
            IFilter<ICondiment<Poll>> filter = null,
            IFilterAsync<ICondiment<Poll>> filterAsync = null) 
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public PollCondiment RealCdmt => Cdmt as PollCondiment;
    }
}
