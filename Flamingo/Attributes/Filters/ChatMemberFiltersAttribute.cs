using Flamingo.Condiments;
using Flamingo.Filters;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base class to build filter attributes for Chat member updates
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public abstract class ChatMemberFiltersAttribute : Attribute,
        IFilterAttribute<ChatMemberUpdated>
    {
        /// <summary>
        /// Base class to build filter attributes for Chat member updates
        /// </summary>
        protected ChatMemberFiltersAttribute(FilterBase<ICondiment<ChatMemberUpdated>> filter)
        {
            Filter = filter;
        }

        /// <inheritdoc/>
        public FilterBase<ICondiment<ChatMemberUpdated>> Filter { get; }
    }
}
