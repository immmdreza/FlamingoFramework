using System;

namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    /// <summary>
    /// Use this on a class property to be include in flamingo form
    /// </summary>
    /// <remarks>You can't use <see cref="FlamingoFormConstructorAttribute"/>
    /// and <see cref="FlamingoFormPropertyAttribute"/> at the same class.
    /// You can actually but one of them will be ignored!</remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public class FlamingoFormPropertyAttribute: Attribute, IFlamingoFormData
    {
        /// <inheritdoc/>
        public string AskText { get; set; }

        /// <inheritdoc/>
        public string InvalidTypeText { get; set; }
    }
}
