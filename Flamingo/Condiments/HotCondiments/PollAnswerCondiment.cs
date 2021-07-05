using Telegram.Bot.Types;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class PollAnswerCondiment : CondimentBase<PollAnswer>
    {
        /// <inheritdoc/>
        public PollAnswerCondiment(PollAnswer inComing, IFlamingoCore flamingo) 
            : base(inComing, flamingo)
        { }

        /// <inheritdoc/>
        public override User Sender => InComing.User;
    }
}
