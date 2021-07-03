using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base class to build filter attributes for polls
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class PollFiltersAttribute : Attribute, IFilterAttribute<Poll>
    {
        /// <summary>
        /// Base class to build filter attributes for polls
        /// </summary>
        protected PollFiltersAttribute(FilterBase<ICondiment<Poll>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<Poll>> Filter { get; }
    }
}
