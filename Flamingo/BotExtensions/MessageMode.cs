using System;

namespace Flamingo.BotExtensions
{
    /// <summary>
    /// Message mode flag of this Messenger
    /// </summary>
    [Flags]
    public enum MessageMode
    {
        /// <summary>
        /// Nothing yet
        /// </summary>
        Empty = 0,

        /// <summary>
        /// Message has text
        /// </summary>
        Text = 2,

        /// <summary>
        /// Message contains photo
        /// </summary>
        Photo = 4,
    }
}
