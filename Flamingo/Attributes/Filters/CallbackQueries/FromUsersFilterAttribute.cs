using Flamingo.Filters.SharedFilters;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters.CallbackQueries
{
    /// <summary>
    /// From users Filter attribute for callback queries
    /// </summary>
    public class FromUsersFilterAttribute : CallbackQueryFiltersAttribute
    {
        /// <summary>
        /// From users Filter attribute for callback queries
        /// </summary>
        public FromUsersFilterAttribute(params long[] ids)
            : base(new FromUsersFilter<CallbackQuery>(ids))
        { }

        /// <summary>
        /// From users Filter attribute for callback queries
        /// </summary>
        public FromUsersFilterAttribute(params string[] usernames)
            : base(new FromUsersFilter<CallbackQuery>(usernames))
        { }
    }
}
