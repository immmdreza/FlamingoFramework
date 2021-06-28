using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Condiments.Extensions
{
    public static class MessageExtensions
    {
        public static Chat Chat(this ICondiment<Message> Cdmt) => Cdmt.InComing.Chat;

        public static User Sender(this ICondiment<Message> Cdmt) => Cdmt.InComing.From;

        public static async Task<Message> RespondText(this ICondiment<Message> Cdmt,
            string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            bool disableNofication = true,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            return await Cdmt.Flamingo.BotClient.SendTextMessageAsync(
                Cdmt.Chat().Id, text,
                parseMode, entities, disableWebPreview,
                disableNofication, 0, true,
                replyMarkup, cancellationToken);
        }

        public static async Task<Message> ReplyText(this ICondiment<Message> Cdmt,
            string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            bool disableNofication = true,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            return await Cdmt.Flamingo.BotClient.SendTextMessageAsync(
                Cdmt.Chat().Id, text,
                parseMode, entities, disableWebPreview,
                disableNofication, Cdmt.InComing.MessageId, true,
                replyMarkup, cancellationToken);
        }
    }
}
