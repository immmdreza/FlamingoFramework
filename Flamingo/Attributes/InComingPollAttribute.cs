using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes
{
    /// <summary>
    /// InComing handler attribute for Polls
    /// </summary>
    public class InComingPollAttribute : Attribute, IFishAttribute<Poll>
    {
        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
