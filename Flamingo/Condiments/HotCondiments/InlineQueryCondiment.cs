using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class InlineQueryCondiment : CondimentBase<InlineQuery>
    {
        /// <inheritdoc/>
        public InlineQueryCondiment(InlineQuery inComing, IFlamingoCore flamingo) 
            : base(inComing, flamingo)
        { }

        /// <inheritdoc/>
        public override Chat Chat => new Chat { Type = InComing.ChatType?? ChatType.Private};

        /// <inheritdoc/>
        public override string StringQuery => InComing.Query;
    }
}
