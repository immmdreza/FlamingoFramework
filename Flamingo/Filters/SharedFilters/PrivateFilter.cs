using Flamingo.Condiments;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Filters.SharedFilters
{
    /// <summary>
    /// Filter updates from private chats
    /// </summary>
    /// <typeparam name="T">Update type</typeparam>
    public class PrivateFilter<T> : FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Filter updates from private chats
        /// </summary>
        public PrivateFilter()
            : base(x =>
            {
                if (x.Chat == null) return false;

                return x.Chat.Type == ChatType.Private;
            })
        { }
    }
}
