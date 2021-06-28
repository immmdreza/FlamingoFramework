using Flamingo.Condiments;
using Telegram.Bot.Types;

namespace Flamingo.Filters.MessageFilters
{
    /// <summary>
    /// Filter messages that replied to another message
    /// </summary>
    public class RepliedFilter : FilterBase<ICondiment<Message>>
    {
        /// <summary>
        /// Filter messages that replied to another message
        /// </summary>
        public RepliedFilter() 
            : base(x=> x.InComing.ReplyToMessage != null)
        { }
    }
}
