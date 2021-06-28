using Flamingo.Filters.MessageFilters;
using System.Text.RegularExpressions;

namespace Flamingo.Attributes.Filters.Messages
{

    /// <inheritdoc/>
    public class RegexFilterAttribute : MessageFiltersAttribute
    {
        /// <inheritdoc/>
        /// <param name="pattern">Pattern for regex to check</param>
        /// <param name="regexOptions">Optional regex options to use</param>
        public RegexFilterAttribute(string pattern, RegexOptions regexOptions = default) 
            : base(new RegexFilter(pattern, regexOptions))
        { }
    }
}
