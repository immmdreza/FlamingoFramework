using System;

namespace Flamingo.Fishes.Advanced.Attributes
{
    /// <summary>
    /// Chose handling group for this handler
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class HandlingGroupAttribute : Attribute
    {
        /// <summary>
        /// Handling group rank
        /// </summary>
        public int Group { get; set; }
    }
}
