using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class PreCheckoutQueryCondiment : CondimentBase<PreCheckoutQuery>
    {
        /// <inheritdoc/>
        public PreCheckoutQueryCondiment(PreCheckoutQuery inComing, FlamingoCore flamingo) 
            : base(inComing, flamingo)
        { }

        /// <inheritdoc/>
        public override User Sender => InComing.From;
    }
}
