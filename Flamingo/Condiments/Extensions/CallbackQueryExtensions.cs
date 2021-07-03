using Flamingo.Condiments.HotCondiments;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Condiments.Extensions
{
    /// <summary>
    /// A batch of useful methods for <see cref="ICondiment{T}"/>
    /// where T is <see cref="CallbackQuery"/> 
    /// </summary>
    public static class CallbackQueryExtensions
    {
        /// <summary>
        /// Extension method to delete the message which carries buttons
        /// </summary>
        public static async Task DeleteMessage(this ICondiment<CallbackQuery> Cdmt,
            CancellationToken cancellationToken = default)
        {
            await Cdmt.Flamingo.BotClient.DeleteMessageAsync(
                Cdmt.Chat.Id, Cdmt.InComing.Message.MessageId, cancellationToken);
        }

        /// <summary>
        /// Extension method to answer a callback query
        /// </summary>
        public static async Task Answer(this ICondiment<CallbackQuery> Cdmt,
            string text = null,
            bool showAlert = true,
            string url = null,
            int cacheTime = 0,
            CancellationToken cancellationToken = default)
        {
            await Cdmt.Flamingo.BotClient.AnswerCallbackQueryAsync(
                Cdmt.InComing.Id, text, showAlert, url, cacheTime, cancellationToken);
        }

        /// <summary>
        /// Extension method to edit the text of a message
        /// </summary>
        public static async Task<ICondiment<Message>> EditText(this ICondiment<CallbackQuery> Cdmt,
            string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            var message = await Cdmt.Flamingo.BotClient.EditMessageTextAsync(
                Cdmt.Chat.Id, Cdmt.InComing.Message.MessageId, text,
                parseMode, entities, disableWebPreview,
                replyMarkup, cancellationToken);

            return new MessageCondiment(message, Cdmt.Flamingo);
        }

        /// <summary>
        /// Extension method to edit inline keyboard of a message
        /// </summary>
        public static async Task<ICondiment<Message>> EditButtons(this ICondiment<CallbackQuery> Cdmt,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            var message = await Cdmt.Flamingo.BotClient.EditMessageReplyMarkupAsync(
                Cdmt.Chat.Id, Cdmt.InComing.Message.MessageId,
                replyMarkup, cancellationToken);

            return new MessageCondiment(message, Cdmt.Flamingo);
        }
    }
}
