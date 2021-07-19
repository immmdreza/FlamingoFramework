using System;

namespace Flamingo.Fishes.Advanced.Attributes
{
    /// <summary>
    /// If this handler is for edited messages
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IsEditedMessageAttribute: Attribute
    { }
}
