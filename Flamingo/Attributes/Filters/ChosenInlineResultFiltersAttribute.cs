using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base class to build filter attributes for chosen in-line results
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class ChosenInlineResultFiltersAttribute : Attribute, IFilterAttribute<ChosenInlineResult>
    {
        /// <summary>
        /// Base class to build filter attributes for chosen in-line results
        /// </summary>
        protected ChosenInlineResultFiltersAttribute(FilterBase<ICondiment<ChosenInlineResult>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<ChosenInlineResult>> Filter { get; }
    }
}
