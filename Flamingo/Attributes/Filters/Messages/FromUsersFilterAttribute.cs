using Flamingo.Filters.SharedFilters;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters.Messages
{
    /// <summary>
    /// From users Filter attribute for messages
    /// </summary>
    public class FromUsersFilterAttribute : MessageFiltersAttribute
    {
        /// <summary>
        /// From users Filter attribute for messages
        /// </summary>
        public FromUsersFilterAttribute(params long[] ids)
            : base(new FromUsersFilter<Message>(ids))
        { }

        /// <summary>
        /// From users Filter attribute for messages
        /// </summary>
        public FromUsersFilterAttribute(params string[] usernames)
            : base(new FromUsersFilter<Message>(usernames))
        { }
    }
}
