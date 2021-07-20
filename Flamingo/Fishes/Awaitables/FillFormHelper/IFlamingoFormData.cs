namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    /// <summary>
    /// Base interface for flamingo form attributes
    /// </summary>
    public interface IFlamingoFormData
    {
        /// <summary>
        /// If the data is Required
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// The name of property when asking for it
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A text to ask this data from user. like "Please enter the name"
        /// </summary>
        public string AskText { get; set; }

        /// <summary>
        /// A text that should be sent if input string cannot be converted to target type
        /// </summary>
        public string InvalidTypeText { get; set; }
    }
}
