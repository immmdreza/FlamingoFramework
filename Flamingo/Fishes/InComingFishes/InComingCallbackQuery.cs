using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Fishes.InComingFishes
{
    /// <summary>
    /// InComing update handler for CallbackQuery
    /// </summary>
    public abstract class InComingCallbackQuery : InComingFish<CallbackQuery>
    {
        /// <summary>
        /// InComing update handler for CallbackQuery
        /// </summary>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingCallbackQuery(IFilter<ICondiment<CallbackQuery>> filter = null, 
            IFilterAsync<ICondiment<CallbackQuery>> filterAsync = null) 
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public CallbackQueryCondiment RealCdmt => Cdmt as CallbackQueryCondiment;

        #region Helpers

        protected Chat Chat => Cdmt.Chat;

        protected User Sender => Cdmt.Sender;

        protected async Task Answer(string text,
            bool showAlert = true,
            string url = null,
            int cacheTime = 0,
            CancellationToken cancellationToken = default)
        {
            await Cdmt.Answer(text, showAlert, url, cacheTime, cancellationToken);
        }

        protected async Task<Message> EditText(string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            return await Cdmt.EditText(text,
                parseMode, entities, disableWebPreview,
                replyMarkup, cancellationToken);
        }

        #endregion
    }
}
