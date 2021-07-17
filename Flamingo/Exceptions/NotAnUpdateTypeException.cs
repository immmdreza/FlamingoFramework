using System;

namespace Flamingo.Exceptions
{
    /// <summary>
    /// The type you're using should be one of update types
    /// </summary>
    public class NotAnUpdateTypeException<T> : FlamingoUnExpected
    {
        /// <summary>
        /// The type you're using should be one of update types
        /// </summary>
        public NotAnUpdateTypeException() 
            : base($"Type {typeof(T)} is not an update type!")
        { }
    }

    /// <summary>
    /// The type you're using should be one of update types
    /// </summary>
    public class NotAnUpdateTypeException : FlamingoUnExpected
    {
        /// <summary>
        /// The type you're using should be one of update types
        /// </summary>
        public NotAnUpdateTypeException(Type type)
            : base($"Type {type} is not an update type!")
        { }
    }
}
