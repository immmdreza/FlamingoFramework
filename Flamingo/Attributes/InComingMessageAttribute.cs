using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes
{
    /// <summary>
    /// inComing Attribute for messages
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class InComingMessageAttribute : Attribute, IFishAttribute<Message>
    { 
        /// <summary>
        /// If this handler is for Edited messages!
        /// </summary>
        public bool IsEdited { get; set; }

        /// <summary>
        /// If this handler is for Channel Posts!
        /// </summary>
        public bool IsChannelPost { get; set; }

        /// <inheritdoc/>
        public int Group { get; set; }
    }
}
