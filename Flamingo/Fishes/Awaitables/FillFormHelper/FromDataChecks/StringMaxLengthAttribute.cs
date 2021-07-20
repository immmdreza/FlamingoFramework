using System;

namespace Flamingo.Fishes.Awaitables.FillFormHelper.FromDataChecks
{
    /// <summary>
    /// Check string length
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class StringMaxLengthAttribute : Attribute, IFamingoFormDataCheck<string>
    {
        /// <inheritdoc/>
        public string FailureMessage { get; set; }

        private readonly int _length;

        /// <summary>
        /// Check string length
        /// </summary>
        public StringMaxLengthAttribute(int maxLength)
        {
            _length = maxLength;
        }

        /// <inheritdoc/>
        public bool Check(string input)
        {
            if(!string.IsNullOrEmpty(input))
            {
                return input.Length <= _length;
            }

            return false;
        }
    }
}
