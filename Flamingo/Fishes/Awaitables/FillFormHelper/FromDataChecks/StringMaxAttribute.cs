using System;

namespace Flamingo.Fishes.Awaitables.FillFormHelper.FromDataChecks
{
    /// <summary>
    /// Check string length
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class StringLengthAttribute : Attribute, IFamingoFormDataCheck<string>
    {
        /// <inheritdoc/>
        public string FailureMessage { get; set; }

        private readonly int _length;

        private readonly int _minLength;

        /// <summary>
        /// Check string length
        /// </summary>
        public StringLengthAttribute(int maxLength, int minLength = 0)
        {
            _length = maxLength;
            _minLength = minLength;
        }

        /// <inheritdoc/>
        public bool Check(string input)
        {
            if(!string.IsNullOrEmpty(input))
            {
                return input.Length >= _minLength && input.Length <= _length;
            }

            return false;
        }
    }
}
