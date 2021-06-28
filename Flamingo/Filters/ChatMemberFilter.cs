using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build filters for updates of type <see cref="ChatMemberUpdated"/>
    /// </summary>
    public class ChatMemberFilter : FilterBase<ICondiment<ChatMemberUpdated>>
    {
        /// <summary>
        /// Build filters for updates of type <see cref="ChatMemberUpdated"/>
        /// </summary>
        public ChatMemberFilter(Func<ChatMemberCondiment, bool> filter) 
            : base(x=> filter(x as ChatMemberCondiment))
        { }

        /// <summary>
        /// Build filters for updates of type <see cref="ChatMemberUpdated"/>
        /// </summary>
        public ChatMemberFilter(Func<ICondiment<ChatMemberUpdated>, bool> filter) : base(filter)
        { }
    }
}
