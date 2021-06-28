using System;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Attributes
{
    /// <summary>
    /// InComing handler attribute for ShippingQuery
    /// </summary>
    public class InComingShippingQueryAttribute : Attribute, IFishAttribute<ShippingQuery>
    {
        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
