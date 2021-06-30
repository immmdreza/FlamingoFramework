using Flamingo.Filters.SharedFilters;
using Flamingo.Helpers.Types.Enums;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters.Messages
{
    /// <summary>
    /// Filter updates based on chat type - private, group ...
    /// </summary>
    public class ChatTypeFilterAttribute : MessageFiltersAttribute
    {
        /// <summary>
        /// Filter updates based on chat type - private, group ...
        /// </summary>
        public ChatTypeFilterAttribute(FlamingoChatType chatType) 
            : base(new ChatTypeFilter<Message>(chatType))
        { }
    }
}
