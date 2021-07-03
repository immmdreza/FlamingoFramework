using System.Text.RegularExpressions;

namespace Flamingo.Attributes.Filters.InlineQueries
{
    /// <summary>
    /// Regex filter attribute for in-line queries
    /// </summary>
    public class RegexFilterAttribute : InlineQueryFiltersAttribute
    {
        /// <summary>
        /// Regex filter attribute for in-line queries
        /// </summary>
        public RegexFilterAttribute(string pattern, RegexOptions regexOptions)
            : base(new Flamingo.Filters.InlineQueryFilters.RegexFilter(
                pattern, regexOptions))
        { }
    }
}
