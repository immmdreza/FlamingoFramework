using Flamingo.Filters.CallbackQueryFilters;
using Flamingo.Filters.SharedFilters;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Flamingo.Filters.Ninja
{
    public static class CallbackQueryNinja
    {
        public static RegexFilter Regex(
            string pattern, RegexOptions regexOptions = default)
            => new RegexFilter(pattern, regexOptions);

        public static FromUsersFilter<CallbackQuery> FromUsers(params long[] ids)
            => new FromUsersFilter<CallbackQuery>(ids);

        public static FromChatsFilter<CallbackQuery> FromChats(params long[] ids)
            => new FromChatsFilter<CallbackQuery>(ids);

        public static FromUsersFilter<CallbackQuery> FromUsers(params string[] ids)
            => new FromUsersFilter<CallbackQuery>(ids);

        public static FromChatsFilter<CallbackQuery> FromChats(params string[] ids)
            => new FromChatsFilter<CallbackQuery>(ids);
    }
}
