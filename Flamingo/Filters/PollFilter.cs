using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build a filter for updates of type <see cref="Poll"/>
    /// </summary>
    public class PollFilter : FilterBase<ICondiment<Poll>>
    {
        /// <summary>
        /// Build a filter for updates of type <see cref="Poll"/>
        /// </summary>
        public PollFilter(Func<ICondiment<Poll>, bool> filter)
            : base(filter)
        { }

        /// <summary>
        /// Build a filter for updates of type <see cref="Poll"/>
        /// </summary>
        public PollFilter(Func<PollCondiment, bool> filter)
            : base(x => filter(x as PollCondiment))
        { }
    }
}
