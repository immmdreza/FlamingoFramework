using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base class to build filter attributes for in-line queries
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class InlineQueryFiltersAttribute : Attribute, IFilterAttribute<InlineQuery>
    {
        /// <summary>
        /// Base class to build filter attributes for in-line queries
        /// </summary>
        protected InlineQueryFiltersAttribute(FilterBase<ICondiment<InlineQuery>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<InlineQuery>> Filter { get; }
    }
}
