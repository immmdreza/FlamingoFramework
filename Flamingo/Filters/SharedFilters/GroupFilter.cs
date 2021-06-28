using Flamingo.Condiments;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Filters.SharedFilters
{
    /// <summary>
    /// Filter updates from groups
    /// </summary>
    /// <typeparam name="T">Update type</typeparam>
    public class GroupFilter<T> : FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Filter updates from groups
        /// </summary>
        public GroupFilter() 
            : base(x=>
            {
                if (x.Chat != null) return false;

                return x.Chat.Type == ChatType.Group || x.Chat.Type == ChatType.Supergroup;
            })
        {
        }
    }
}
