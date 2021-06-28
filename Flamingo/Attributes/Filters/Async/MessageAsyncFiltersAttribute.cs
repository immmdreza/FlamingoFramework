using Flamingo.Condiments;
using Flamingo.Filters.Async;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters.Async
{
    /// <summary>
    /// Base class for async Message filter
    /// </summary>
    public class MessageAsyncFiltersAttribute : Attribute, IFilterAsyncAttribute<Message>
    {
        /// <summary>
        /// Base class for async Message filter
        /// </summary>
        /// <param name="filter">The that may carry</param>
        public MessageAsyncFiltersAttribute(FilterBaseAsync<ICondiment<Message>> filter)
        {
            Filter = filter;
        }

        /// <summary>
        /// The filter that this class applies
        /// </summary>
        public FilterBaseAsync<ICondiment<Message>> Filter { get; }
    }
}
