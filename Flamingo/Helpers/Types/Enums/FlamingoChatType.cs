using System;

namespace Flamingo.Helpers.Types.Enums
{
    /// <summary>
    /// A mapped enum for ChatType to use flags
    /// </summary>
    [Flags]
    public enum FlamingoChatType
    {
        /// <summary>
        /// Has no chat
        /// </summary>
        NoChat = 2,

        /// <summary>
        /// It is a private chat with the bot
        /// </summary>
        Private = 4,

        /// <summary>
        /// Sender for inline queries
        /// </summary>
        Sender = 8,

        /// <summary>
        /// It's a normal group chat
        /// </summary>
        Group = 16,

        /// <summary>
        /// It's a super group chat
        /// </summary>
        SuperGroup = 32,

        /// <summary>
        /// Update comes from channel
        /// </summary>
        Channel = 64
    }
}
