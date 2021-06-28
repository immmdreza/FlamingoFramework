using System;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Attributes
{
    /// <summary>
    /// InComing handler for PreCheckoutQuery
    /// </summary>
    public class InComingPreCheckoutQueryAttribute : Attribute, IFishAttribute<PreCheckoutQuery>
    {
        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
