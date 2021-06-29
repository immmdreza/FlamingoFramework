using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Telegram.Bot.Types;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// InComing handler for PollAnswers
    /// </summary>
    public abstract class InComingPollAnswer : InComingFish<PollAnswer>
    {
        /// <summary>
        /// InComing handler for PollAnswers
        /// </summary>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingPollAnswer(
            IFilter<ICondiment<PollAnswer>> filter = null,
            IFilterAsync<ICondiment<PollAnswer>> filterAsync = null)
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public PollAnswerCondiment RealCdmt
        {
            get
            {
                if (Cdmt is PollAnswerCondiment cdmt)
                {
                    return cdmt;
                }

                return null;
            }
        }
    }
}
