using System;

namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    /// <summary>
    /// Place this on a constructor that should be use in flamingo form
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor)]
    public class FlamingoFormConstructorAttribute: Attribute
    { }
}
