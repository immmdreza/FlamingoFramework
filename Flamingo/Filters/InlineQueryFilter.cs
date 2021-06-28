using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build a filter for updates of type <see cref="InlineQuery"/>
    /// </summary>
    public class InlineQueryFilter : FilterBase<ICondiment<InlineQuery>>
    {
        /// <summary>
        /// Build a filter for updates of type <see cref="InlineQuery"/>
        /// </summary>
        public InlineQueryFilter(Func<ICondiment<InlineQuery>, bool> filter) : base(filter)
        { }

        /// <summary>
        /// Build a filter for updates of type <see cref="InlineQuery"/>
        /// </summary>
        public InlineQueryFilter(Func<InlineQueryCondiment, bool> filter)
            : base(x => filter(x as InlineQueryCondiment))
        { }
    }
}
