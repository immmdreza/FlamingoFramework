using Flamingo.Filters.CallbackQueryFilters;
using Flamingo.Filters.SharedFilters;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Flamingo.Filters.Ninja
{
    /// <summary>
    /// A Collection of useful filters for <see cref="CallbackQuery"/>.
    /// </summary>
    public static class CallbackQueryNinja
    {
        /// <summary>
        /// Filter callback queries data based on a regex pattern
        /// </summary>
        public static RegexFilter Regex(
            string pattern, RegexOptions regexOptions = default)
            => new RegexFilter(pattern, regexOptions);

        /// <summary>
        /// Filter the sender of callback query based on user id
        /// </summary>
        public static FromUsersFilter<CallbackQuery> FromUsers(params long[] ids)
            => new FromUsersFilter<CallbackQuery>(ids);

        /// <summary>
        /// Filter the chat of callback query based on chat id
        /// </summary>
        public static FromChatsFilter<CallbackQuery> FromChats(params long[] ids)
            => new FromChatsFilter<CallbackQuery>(ids);

        /// <summary>
        /// Filter the sender of callback query based on username if exists
        /// </summary>
        public static FromUsersFilter<CallbackQuery> FromUsers(params string[] ids)
            => new FromUsersFilter<CallbackQuery>(ids);

        /// <summary>
        /// Filter the chat of callback query based on username if exists
        /// </summary>
        public static FromChatsFilter<CallbackQuery> FromChats(params string[] ids)
            => new FromChatsFilter<CallbackQuery>(ids);
    }
}
