using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build a filter for updates of type <see cref="PreCheckoutQuery"/>
    /// </summary>
    public class PreCheckoutQueryFilter : FilterBase<ICondiment<PreCheckoutQuery>>
    {
        /// <summary>
        /// Build a filter for updates of type <see cref="PreCheckoutQuery"/>
        /// </summary>
        public PreCheckoutQueryFilter(Func<ICondiment<PreCheckoutQuery>, bool> filter)
            : base(filter)
        { }

        /// <summary>
        /// Build a filter for updates of type <see cref="PreCheckoutQuery"/>
        /// </summary>
        public PreCheckoutQueryFilter(Func<PreCheckoutQueryCondiment, bool> filter)
        : base(x => filter(x as PreCheckoutQueryCondiment))
        { }
    }
}
