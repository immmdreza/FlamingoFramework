using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.SharedFilters;
using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Flamingo.Filters.Ninja
{
    public static class MessageNinja
    {
        public static IsEditedFilter IsEdited => new IsEditedFilter();

        public static IsChannelPostFilter IsChannelPost => new IsChannelPostFilter();

        public static PrivateFilter<Message> Private => new PrivateFilter<Message>();

        public static RegexFilter Regex(
            string pattern, RegexOptions regexOptions = default)
            => new RegexFilter(pattern, regexOptions);

        public static FromUsersFilter<Message> FromUsers(params long[] ids) 
            => new FromUsersFilter<Message>(ids);

        public static FromChatsFilter<Message> FromChats(params long[] ids)
            => new FromChatsFilter<Message>(ids);

        public static FromUsersFilter<Message> FromUsers(params string[] ids)
            => new FromUsersFilter<Message>(ids);

        public static FromChatsFilter<Message> FromChats(params string[] ids)
            => new FromChatsFilter<Message>(ids);
    }
}
