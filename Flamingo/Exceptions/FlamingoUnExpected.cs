using System;

namespace Flamingo.Exceptions
{
    /// <summary>
    /// Something unexpected happened in Flamingo
    /// </summary>
    public abstract class FlamingoUnExpected : Exception
    {
        /// <summary>
        /// Something unexpected happened in Flamingo
        /// </summary>
        protected FlamingoUnExpected(string message) : base(message)
        { }
    }
}
