using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Condiments.Extensions
{
    public static class CallbackQueryExtensions
    {
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

        public static async Task<Message> EditText(this ICondiment<CallbackQuery> Cdmt,
            string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            return await Cdmt.Flamingo.BotClient.EditMessageTextAsync(
                Cdmt.Chat.Id, Cdmt.InComing.Message.MessageId, text,
                parseMode, entities, disableWebPreview,
                replyMarkup, cancellationToken);
        }
    }
}
