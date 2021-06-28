using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes
{
    /// <summary>
    /// InComing attribute for inline queries
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InComingInlineQueryAttribute: Attribute, IFishAttribute<InlineQuery>
    {
        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
