using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes
{
    /// <summary>
    /// InComing handler attribute for PollAnswer
    /// </summary>
    public class InComingPollAnswerAttribute : Attribute, IFishAttribute<PollAnswer>
    {
        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
