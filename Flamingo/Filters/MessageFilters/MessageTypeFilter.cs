using Flamingo.Condiments;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Filters.MessageFilters
{
    /// <summary>
    /// Filter messages based on their type
    /// </summary>
    public class MessageTypeFilter : FilterBase<ICondiment<Message>>
    {
        /// <summary>
        /// Filter messages based on their type
        /// </summary>
        public MessageTypeFilter(MessageType messageType) 
            : base(x=> x.InComing.Type == messageType)
        { }
    }
}
