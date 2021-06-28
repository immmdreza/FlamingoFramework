using Telegram.Bot.Types;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class ChosenInlineResultCondiment : CondimentBase<ChosenInlineResult>
    {
        /// <inheritdoc/>
        public ChosenInlineResultCondiment(
            ChosenInlineResult inComing, FlamingoCore flamingo) : base(inComing, flamingo)
        { }

        /// <inheritdoc/>
        public override Chat Chat => null;

        /// <inheritdoc/>
        public override User Sender => InComing.From;

        /// <inheritdoc/>
        public override string StringQuery => InComing.Query;
    }
}
