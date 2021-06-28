using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Flamingo.Filters.MessageFilters
{
    public class RegexFilter : BaseRegexFilter<Message>
    {
        public RegexFilter(string pattern, RegexOptions regexOptions = default)
            : base(x=>
            {
                return x.InComing switch
                {
                    { Text: { } } => x.InComing.Text,
                    { Caption: { } } => x.InComing.Caption,
                    _=> null
                };
            }, pattern, regexOptions)
        { }
    }
}
