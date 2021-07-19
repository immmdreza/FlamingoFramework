using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Fishes.Advanced.InComingHandlers
{
    public abstract class AdvInComingMessage : AdvInComingFish<Message>
    {
        public AdvInComingMessage() : base()
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public MessageCondiment RealCdmt
        {
            get
            {
                if (Cdmt is MessageCondiment cdmt)
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

        /// <summary>
        /// Extension method to edit the text of a message
        /// </summary>
        public async Task<ICondiment<Message>> EditText(string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            var message = await Cdmt.Flamingo.BotClient.EditMessageTextAsync(
                Cdmt.Chat.Id, Cdmt.InComing.MessageId, text,
                parseMode, entities, disableWebPreview,
                replyMarkup, cancellationToken);

            return new MessageCondiment(message, Cdmt.Flamingo);
        }

        /// <summary>
        /// Extension method to edit inline keyboard of a message
        /// </summary>
        public async Task<ICondiment<Message>> EditButtons(InlineKeyboardMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            var message = await Cdmt.Flamingo.BotClient.EditMessageReplyMarkupAsync(
                Cdmt.Chat.Id, Cdmt.InComing.MessageId,
                replyMarkup, cancellationToken);

            return new MessageCondiment(message, Cdmt.Flamingo);
        }

        /// <summary>
        /// Extension method to pin the message
        /// </summary>
        public async Task Pin(bool disableNotify = true,
            CancellationToken cancellationToken = default)
        {
            await Cdmt.Flamingo.BotClient.PinChatMessageAsync(
                Cdmt.Chat.Id, Cdmt.InComing.MessageId,
                disableNotify, cancellationToken);
        }

        /// <summary>
        /// Extension method to delete the message
        /// </summary>
        public async Task Delete(CancellationToken cancellationToken = default)
        {
            await Cdmt.Flamingo.BotClient.DeleteMessageAsync(
                Cdmt.Chat.Id, Cdmt.InComing.MessageId, cancellationToken);
        }

        /// <summary>
        /// Quickly respond to a message with a text message
        /// </summary>
        public async Task<ICondiment<Message>> RespondText(string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            bool disableNofication = true,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            var message = await Cdmt.Flamingo.BotClient.SendTextMessageAsync(
                Cdmt.Chat.Id, text,
                parseMode, entities, disableWebPreview,
                disableNofication, 0, true,
                replyMarkup, cancellationToken);

            return new MessageCondiment(message, Cdmt.Flamingo);
        }

        /// <summary>
        /// Quickly forward a message with a text message
        /// </summary>
        public async Task<ICondiment<Message>> Forward(Chat toChat,
            bool disableNofication = true,
            CancellationToken cancellationToken = default)
        {
            var message = await Cdmt.Flamingo.BotClient.ForwardMessageAsync(
                toChat, Cdmt.Chat.Id, Cdmt.InComing.MessageId,
                disableNofication, cancellationToken);

            return new MessageCondiment(message, Cdmt.Flamingo);
        }

        /// <summary>
        /// Quickly reply to a message with a text message
        /// </summary>
        public async Task<ICondiment<Message>> ReplyText(string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            bool disableNofication = true,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            var message = await Cdmt.Flamingo.BotClient.SendTextMessageAsync(
                Cdmt.Chat.Id, text,
                parseMode, entities, disableWebPreview,
                disableNofication, Cdmt.InComing.MessageId, true,
                replyMarkup, cancellationToken);

            return new MessageCondiment(message, Cdmt.Flamingo);
        }

        #endregion
    }
}
