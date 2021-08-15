using System;
using System.Text.RegularExpressions;

namespace Flamingo.Fishes.Awaitables.FillFormHelper.FromDataChecks
{
    /// <summary>
    /// Checks an string input based on regex pattern
    /// </summary>
    public class StringRegexAttribute : Attribute, IFlamingoFormDataCheck<string>
    {
        /// <inheritdoc/>
        public string FailureMessage { get; set; }

        private readonly string _pattern;

        private readonly RegexOptions _regexOptions;

        /// <summary>
        /// Checks an string input based on regex pattern
        /// </summary>
        public StringRegexAttribute(string pattern, RegexOptions regexOptions = RegexOptions.None)
        {
            _pattern = pattern;
            _regexOptions = regexOptions;
        }

        /// <inheritdoc/>
        public bool Check(string input)
        {
            if (string.IsNullOrEmpty(input)) return false;

            var matches = Regex.Matches(input, _pattern, _regexOptions);

            if (matches.Count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
