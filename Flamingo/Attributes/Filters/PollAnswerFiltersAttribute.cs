using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base class to build filter attributes for poll answers
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class PollAnswerFiltersAttribute : Attribute, IFilterAttribute<PollAnswer>
    {
        /// <summary>
        /// Base class to build filter attributes for poll answers
        /// </summary>
        protected PollAnswerFiltersAttribute(FilterBase<ICondiment<PollAnswer>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<PollAnswer>> Filter { get; }
    }
}
