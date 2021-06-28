using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build a filter for updates of type <see cref="ChosenInlineResult"/>
    /// </summary>
    public class ChosenInlineResultFilter : FilterBase<ICondiment<ChosenInlineResult>>
    {
        /// <summary>
        /// Build a filter for updates of type <see cref="ChosenInlineResult"/>
        /// </summary>
        public ChosenInlineResultFilter(Func<ICondiment<ChosenInlineResult>, bool> filter) 
            : base(filter)
        { }

        /// <summary>
        /// Build a filter for updates of type <see cref="ChosenInlineResult"/>
        /// </summary>
        public ChosenInlineResultFilter(Func<ChosenInlineResultCondiment, bool> filter)
            : base(x => filter(x as ChosenInlineResultCondiment))
        { }
    }
}
