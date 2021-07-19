using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base class to build filter attributes for shipping queries
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class ShippingQueryFiltersAttribute : Attribute, IFilterAttribute<ShippingQuery>
    {
        /// <summary>
        /// Base class to build filter attributes for shipping queries
        /// </summary>
        protected ShippingQueryFiltersAttribute(FilterBase<ICondiment<ShippingQuery>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<ShippingQuery>> Filter { get; }
    }
}
