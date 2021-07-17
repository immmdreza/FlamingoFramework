using System;

namespace Flamingo.Fishes.Advanced.Attributes
{
    /// <summary>
    /// If the message should be a channel post
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class IsChannelPostAttribute: Attribute
    { }
}
