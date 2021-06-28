using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build a filter for updates of type <see cref="ShippingQuery"/>
    /// </summary>
    public class ShippingQueryFilter : FilterBase<ICondiment<ShippingQuery>>
    {
        /// <summary>
        /// Build a filter for updates of type <see cref="ShippingQuery"/>
        /// </summary>
        public ShippingQueryFilter(Func<ICondiment<ShippingQuery>, bool> filter)
            : base(filter)
        { }

        /// <summary>
        /// Build a filter for updates of type <see cref="ShippingQuery"/>
        /// </summary>
        public ShippingQueryFilter(Func<ShippingQueryCondiment, bool> filter)
        : base(x => filter(x as ShippingQueryCondiment))
        { }
    }
}
