using Flamingo.Filters.Async.SharedFilters;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters.Async.Messages
{
    /// <summary>
    /// An async filter to check if sender is an admin of chat
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AdminsOnlyFilterAttribute : MessageAsyncFiltersAttribute
    {
        /// <summary>
        /// An async filter to check if sender is an admin of chat
        /// </summary>
        public AdminsOnlyFilterAttribute() 
            : base(new AdminsOnlyFilter<Message>())
        { }
    }
}
