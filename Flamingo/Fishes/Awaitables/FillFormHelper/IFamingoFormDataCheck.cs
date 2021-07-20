namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    /// <summary>
    /// Base interface to validate a data in flamingo forms
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public interface IFamingoFormDataCheck<T>
    {
        /// <summary>
        /// A function to check if data is valid
        /// </summary>
        public bool Check(T input);

        /// <summary>
        /// Message to send if data is invalid
        /// </summary>
        public string FailureMessage { get; set; }
    }
}
