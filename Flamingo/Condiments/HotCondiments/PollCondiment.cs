using Telegram.Bot.Types;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class PollCondiment : CondimentBase<Poll>
    {
        /// <inheritdoc/>
        public PollCondiment(Poll inComing, FlamingoCore flamingo) : base(inComing, flamingo)
        { }
    }
}
