using System;

namespace Flamingo.Fishes.Advanced.Attributes
{
    /// <summary>
    /// Add this attribute on a constructor of incoming handler which should be used
    /// to create an instance of handler
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public class AdvancedHandlerConstructorAttribute: Attribute
    { }
}
