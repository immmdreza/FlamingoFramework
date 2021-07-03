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
        public CallbackQueryCondiment RealCdmt
        {
            get
            {
                if (Cdmt is CallbackQueryCondiment cdmt)
                {
                    return cdmt;
                }

                return null;
            }
        }

        #region Helpers

        /// <inheritdoc/>
        protected Chat Chat => Cdmt.Chat;

        /// <inheritdoc/>
        protected User Sender => Cdmt.Sender;

        protected async Task Answer(string text,
            bool showAlert = true,
            string url = null,
            int cacheTime = 0,
            CancellationToken cancellationToken = default)
        {
            await Cdmt.Answer(text, showAlert, url, cacheTime, cancellationToken);
        }

        protected async Task<ICondiment<Message>> EditText(string text,
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

        /// <summary>
        /// Extension method to delete the message which carries buttons
        /// </summary>
        protected async Task DeleteMessage(CancellationToken cancellationToken = default)
        {
            await Cdmt.Flamingo.BotClient.DeleteMessageAsync(
                Cdmt.Chat.Id, Cdmt.InComing.Message.MessageId, cancellationToken);
        }

        /// <summary>
        /// Extension method to edit inline keyboard of a message
        /// </summary>
        protected async Task<ICondiment<Message>> EditButtons(InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            var message = await Cdmt.Flamingo.BotClient.EditMessageReplyMarkupAsync(
                Cdmt.Chat.Id, Cdmt.InComing.Message.MessageId,
                replyMarkup, cancellationToken);

            return new MessageCondiment(message, Cdmt.Flamingo);
        }

        #endregion
    }
}
