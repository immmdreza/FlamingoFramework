using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System;
using Telegram.Bot.Types;

namespace Flamingo.Filters
{
    /// <summary>
    /// Build a filter for updates of type <see cref="CallbackQuery"/>
    /// </summary>
    public class CallbackQueryFilter : FilterBase<ICondiment<CallbackQuery>>
    {
        /// <summary>
        /// Build a filter for updates of type <see cref="CallbackQuery"/>
        /// </summary>
        public CallbackQueryFilter(Func<ICondiment<CallbackQuery>, bool> filter) : base(filter)
        { }

        /// <summary>
        /// Build a filter for updates of type <see cref="CallbackQuery"/>
        /// </summary>
        public CallbackQueryFilter(Func<CallbackQueryCondiment, bool> filter) 
            : base(x=> filter(x as CallbackQueryCondiment))
        { }
    }
}
