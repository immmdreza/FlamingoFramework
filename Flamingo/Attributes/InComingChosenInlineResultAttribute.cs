using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes
{
    /// <summary>
    /// inComing Attribute for ChosenInlineResult
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InComingChosenInlineResultAttribute : Attribute, IFishAttribute<ChosenInlineResult>
    {
        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
