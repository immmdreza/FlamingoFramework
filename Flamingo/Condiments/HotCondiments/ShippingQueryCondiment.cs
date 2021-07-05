using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class ShippingQueryCondiment : CondimentBase<ShippingQuery>
    {
        /// <inheritdoc/>
        public ShippingQueryCondiment(ShippingQuery inComing, IFlamingoCore flamingo) 
            : base(inComing, flamingo)
        { }

        /// <inheritdoc/>
        public override User Sender => InComing.From;
    }
}
