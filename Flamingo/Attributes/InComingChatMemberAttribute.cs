using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes
{
    /// <summary>
    /// InComing attribute for ChatMember and MyChatMember
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InComingChatMemberAttribute: Attribute, IFishAttribute<ChatMemberUpdated>
    {
        /// <summary>
        /// If this update is MyChatMember (and not ChatMember)
        /// </summary>
        public bool IsMine { get; set; }

        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
