using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Flamingo.Filters.InlineQueryFilters
{
    /// <summary>
    /// Filter inline queries base on Query
    /// </summary>
    public class RegexFilter : BaseRegexFilter<InlineQuery>
    {
        /// <inheritdoc/>
        public RegexFilter(string pattern, RegexOptions regexOptions = RegexOptions.None) 
            : base(x=> x.InComing.Query, pattern, regexOptions)
        { }
    }
}
