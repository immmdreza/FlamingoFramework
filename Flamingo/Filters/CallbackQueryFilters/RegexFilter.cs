using System.Text.RegularExpressions;
using Telegram.Bot.Types;

namespace Flamingo.Filters.CallbackQueryFilters
{
    public class RegexFilter : BaseRegexFilter<CallbackQuery>
    {
        public RegexFilter(
            string pattern, RegexOptions regexOptions = RegexOptions.None) 
            : base(x=> x.InComing.Data, pattern, regexOptions)
        {
        }
    }
}
