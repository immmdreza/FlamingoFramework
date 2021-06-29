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
    /// A Set of useful extension for <see cref="ICondiment{T}"/> and <see cref="MessageCondiment"/>.
    /// Where T is <see cref="Message"/>.
    /// </summary>
    public static class MessageExtensions
    {
        /// <summary>
        /// Quickly respond to a message with a text message
        /// </summary>
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
                Cdmt.Chat.Id, text,
                parseMode, entities, disableWebPreview,
                disableNofication, 0, true,
                replyMarkup, cancellationToken);
        }

        /// <summary>
        /// Quickly respond to a message with a text message
        /// </summary>
        public static async Task<Message> RespondText(this MessageCondiment Cdmt,
            string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            bool disableNofication = true,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            return await Cdmt.Flamingo.BotClient.SendTextMessageAsync(
                Cdmt.Chat.Id, text,
                parseMode, entities, disableWebPreview,
                disableNofication, 0, true,
                replyMarkup, cancellationToken);
        }

        /// <summary>
        /// Quickly reply to a message with a text message
        /// </summary>
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
                Cdmt.Chat.Id, text,
                parseMode, entities, disableWebPreview,
                disableNofication, Cdmt.InComing.MessageId, true,
                replyMarkup, cancellationToken);
        }

        /// <summary>
        /// Quickly reply to a message with a text message
        /// </summary>
        public static async Task<Message> ReplyText(this MessageCondiment Cdmt,
            string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            bool disableNofication = true,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            return await Cdmt.Flamingo.BotClient.SendTextMessageAsync(
                Cdmt.Chat.Id, text,
                parseMode, entities, disableWebPreview,
                disableNofication, Cdmt.InComing.MessageId, true,
                replyMarkup, cancellationToken);
        }
    }
}
