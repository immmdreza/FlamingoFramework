using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Helpers.Types.Enums
{
    /// <summary>
    /// Flaged alias of <see cref="ChatMemberStatus"/>
    /// </summary>
    /// <remarks>
    /// This enum can help you filter <see cref="ChatMemberUpdated"/> easily using flags and filters
    /// </remarks>
    [Flags]
    public enum ChatMemberFlamingoStatus
    {
        /// <summary>
        /// User is restricted from chat
        /// </summary>
        Restricted = 2,

        /// <summary>
        /// User kicked from chat
        /// </summary>
        Kicked = 4,

        /// <summary>
        /// User left the caht
        /// </summary>
        Left = 8,

        /// <summary>
        /// User is a normal member of chat
        /// </summary>
        Member = 16,

        /// <summary>
        /// User is an admin of chat
        /// </summary>
        Admin = 32,

        /// <summary>
        /// User is the current owner of the chat
        /// </summary>
        Creator = 64
    }
}
