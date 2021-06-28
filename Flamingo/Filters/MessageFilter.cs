using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build filters for updates of type <see cref="Message"/>
    /// </summary>
    public class MessageFilter : FilterBase<ICondiment<Message>>
    {
        /// <summary>
        /// Build filters for updates of type <see cref="Message"/>
        /// </summary>
        public MessageFilter(Func<ICondiment<Message>, bool> filter) : base(filter)
        { }

        /// <summary>
        /// Build filters for updates of type <see cref="Message"/>
        /// </summary>
        public MessageFilter(Func<MessageCondiment, bool> filter) 
            : base(x=> filter(x as MessageCondiment))
        { }
    }
}
