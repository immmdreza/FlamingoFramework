using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types.Payments;
namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base class to build filter attributes for pre-checkout queries
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class PreCheckoutQueryFiltersAttribute : Attribute, IFilterAttribute<PreCheckoutQuery>
    {
        /// <summary>
        /// Base class to build filter attributes for pre-checkout queries
        /// </summary>
        protected PreCheckoutQueryFiltersAttribute(FilterBase<ICondiment<PreCheckoutQuery>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<PreCheckoutQuery>> Filter { get; }
    }
}
