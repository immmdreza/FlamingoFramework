using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Telegram.Bot.Types;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// You can create your handler for <c>ChatMemberUpdated</c> by inheriting from this
    /// </summary>
    public abstract class InComingChatMember : InComingFish<ChatMemberUpdated>
    {
        /// <summary>
        /// You can create your handler for <c>ChatMemberUpdated</c> by inheriting from this
        /// </summary>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingChatMember(IFilter<ICondiment<ChatMemberUpdated>> filter = null,
            IFilterAsync<ICondiment<ChatMemberUpdated>> filterAsync = null) 
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public ChatMemberCondiment RealCdmt
        {
            get
            {
                if (Cdmt is ChatMemberCondiment cdmt)
                {
                    return cdmt;
                }

                return null;
            }
        }
    }
}
