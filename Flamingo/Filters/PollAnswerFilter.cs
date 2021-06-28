using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build a filter for updates of type <see cref="PollAnswer"/>
    /// </summary>
    public class PollAnswerFilter : FilterBase<ICondiment<PollAnswer>>
    {
        /// <summary>
        /// Build a filter for updates of type <see cref="PollAnswer"/>
        /// </summary>
        public PollAnswerFilter(Func<ICondiment<PollAnswer>, bool> filter)
            : base(filter)
        { }

        /// <summary>
        /// Build a filter for updates of type <see cref="PollAnswer"/>
        /// </summary>
        public PollAnswerFilter(Func<PollAnswerCondiment, bool> filter)
            : base(x => filter(x as PollAnswerCondiment))
        { }
    }
}
