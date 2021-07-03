using System.Text.RegularExpressions;

namespace Flamingo.Attributes.Filters.CallbackQueries
{
    /// <summary>
    /// Regex filter attribute for callback queries
    /// </summary>
    public class RegexFilterAttribute : CallbackQueryFiltersAttribute
    {
        /// <summary>
        /// Regex filter attribute for callback queries
        /// </summary>
        public RegexFilterAttribute(string pattern, RegexOptions regexOptions)
            : base(new Flamingo.Filters.CallbackQueryFilters.RegexFilter(
                pattern, regexOptions))
        { }
    }
}
