using System.Collections.Generic;
using Telegram.Bot.Types;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class CallbackQueryCondiment : CondimentBase<CallbackQuery>
    {
        private char _spliter;

        /// <inheritdoc/>
        public CallbackQueryCondiment(
            CallbackQuery inComing,
            FlamingoCore flamingo,
            char spliterChar)
            : base(inComing, flamingo)
        {
            _spliter = spliterChar;
        }

        /// <inheritdoc/>
        public override User Sender => InComing.From;

        /// <inheritdoc/>
        public override Chat Chat => InComing.Message.Chat;

        /// <inheritdoc/>
        public override string StringQuery => InComing.Data;

        /// <inheritdoc/>
        public override IEnumerable<string> QueryArgs
        {
            get
            {
                return InComing.Data.Split(_spliter);
            }
        }
    }
}
