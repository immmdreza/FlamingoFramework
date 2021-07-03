using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base abstract class for Filter Attribute that works on Messages
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class MessageFiltersAttribute : Attribute, IFilterAttribute<Message>
    {
        /// <summary>
        /// Base class for Filter Attribute that works on Messages
        /// </summary>
        /// <param name="filter">The filter that this attribute carries</param>
        public MessageFiltersAttribute(FilterBase<ICondiment<Message>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<Message>> Filter { get; }
    }
}
