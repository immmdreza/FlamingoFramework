using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base class to build filter attributes for callback queries
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class CallbackQueryFiltersAttribute : Attribute, IFilterAttribute<CallbackQuery>
    {
        /// <summary>
        /// Base class to build filter attributes for callback queries
        /// </summary>
        public CallbackQueryFiltersAttribute(FilterBase<ICondiment<CallbackQuery>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<CallbackQuery>> Filter { get; }
    }
}
