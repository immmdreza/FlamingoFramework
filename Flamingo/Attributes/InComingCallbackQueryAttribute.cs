using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes
{
    /// <summary>
    /// Attribute inComing for callback Queries
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InComingCallbackQueryAttribute : Attribute, IFishAttribute<CallbackQuery>
    {
        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
