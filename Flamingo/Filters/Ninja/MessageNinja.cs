using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.SharedFilters;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Filters.Ninja
{
    /// <summary>
    /// A Collection of useful filters for <see cref="Message"/>.
    /// </summary>
    public static class MessageNinja
    {
        /// <summary>
        /// Filter incoming messages based on their type - text, photo, video ...
        /// </summary>
        /// <param name="messageType">Message type you want</param>
        public static MessageTypeFilter MessageTypeFilter(MessageType messageType)
        {
            return new MessageTypeFilter(messageType);
        }

        /// <summary>
        /// Check if this update is about an edited message
        /// </summary>
        public static IsEditedFilter IsEdited => new IsEditedFilter();

        /// <summary>
        /// Check if this message is channel post
        /// </summary>
        public static IsChannelPostFilter IsChannelPost => new IsChannelPostFilter();

        /// <summary>
        /// Check if the message sent into a private chat
        /// </summary>
        public static PrivateFilter<Message> Private => new PrivateFilter<Message>();

        /// <summary>
        /// Checks message text or caption based on a regex pattern
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="regexOptions"></param>
        /// <returns></returns>
        public static RegexFilter Regex(
            string pattern, RegexOptions regexOptions = default)
            => new RegexFilter(pattern, regexOptions);

        /// <summary>
        /// Filter the sender of message based on user id
        /// </summary>
        public static FromUsersFilter<Message> FromUsers(params long[] ids) 
            => new FromUsersFilter<Message>(ids);

        /// <summary>
        /// Filter the chat of message based on chat id
        /// </summary>
        public static FromChatsFilter<Message> FromChats(params long[] ids)
            => new FromChatsFilter<Message>(ids);

        /// <summary>
        /// Filter the sender of message based on username if exists
        /// </summary>
        public static FromUsersFilter<Message> FromUsers(params string[] ids)
            => new FromUsersFilter<Message>(ids);

        /// <summary>
        /// Filter the chat of message based on chat username if exists
        /// </summary>
        public static FromChatsFilter<Message> FromChats(params string[] ids)
            => new FromChatsFilter<Message>(ids);
    }
}
