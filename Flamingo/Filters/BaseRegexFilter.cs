using Flamingo.Condiments;
using System;
using System.Text.RegularExpressions;

namespace Flamingo.Filters
{
    /// <summary>
    /// Base class to build regex filters
    /// </summary>
    /// <typeparam name="T">Type of update</typeparam>
    public abstract class BaseRegexFilter<T>: FilterBase<ICondiment<T>>
    {
        /// <summary>
        /// Base class to build regex filters
        /// </summary>
        public BaseRegexFilter(
            Func<ICondiment<T>, string> getText,
            string pattern,
            RegexOptions regexOptions = default)
            : base(x =>
            {
                var text = getText(x);

                if (string.IsNullOrEmpty(text)) return false;

                var matches = Regex.Matches(text, pattern, regexOptions);

                if(matches.Count > 0)
                {
                    x.MatchCollection = matches;
                    return true;
                }

                return false;
            })
        { }
    }
}
