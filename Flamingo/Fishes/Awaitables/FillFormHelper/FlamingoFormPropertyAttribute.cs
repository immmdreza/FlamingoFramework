using System;

namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FlamingoFormPropertyAttribute: Attribute, IFlamingoFormData
    {
        public string AskText { get; set; }

        // Failure
        public string InvalidTypeText { get; set; }
    }
}
