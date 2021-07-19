using System;

namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class FlamingoFormDataAttribute : Attribute, IFlamingoFormData
    {
        public string AskText { get; set; }

        public string InvalidTypeText { get; set; }
    }
}
