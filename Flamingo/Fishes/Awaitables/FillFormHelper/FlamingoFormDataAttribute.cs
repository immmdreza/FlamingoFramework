using System;

namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    /// <summary>
    /// Place this on constructor parameters
    /// </summary>
    /// <remarks>
    /// Constructor should have <see cref="FlamingoFormConstructorAttribute"/> on it
    /// </remarks>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FlamingoFormDataAttribute : Attribute, IFlamingoFormData
    {
        /// <inheritdoc/>
        public string AskText { get; set; }

        /// <inheritdoc/>
        public string InvalidTypeText { get; set; }
    }
}
