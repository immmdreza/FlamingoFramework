using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.BotExtensions
{
    /// <summary>
    /// Messenger helps you sent your message
    /// </summary>
    public class Messenger
    {
        /// <summary>
        /// Sends your message
        /// </summary>
        public async Task<ICondiment<Message>> Send(
            ChatId chatId, int replyToMessageId = 0,
            CancellationToken cancellationToken = default)
        {
            if(_messageMode.HasFlag(MessageMode.Text))
            {
                var message = await _client.BotClient.SendTextMessageAsync(chatId,
                    _text, _parseMode,
                    _messageEntities, _disableWebPreview, _disableNotification,
                    replyToMessageId, _allowSendWithoutReply, _replyMarkup,
                    cancellationToken);

                return new MessageCondiment(message, _client);
            }
            else if(_messageMode.HasFlag(MessageMode.Photo))
            {

            }

            throw new System.Exception();
        }

        /// <summary>
        /// Messenger helps you sent your message
        /// </summary>
        public Messenger(
            IFlamingoCore client,
            IEnumerable<MessageEntity> messageEntities = null,
            bool disableWebPreview = false,
            bool withNotification = false,
            bool allowSendWithoutReply = true)
        {
            _client = client;
            _messageEntities = messageEntities;
            _disableWebPreview = disableWebPreview;
            _disableNotification = !withNotification;
            _allowSendWithoutReply = allowSendWithoutReply;

            _messageMode = MessageMode.Empty;
        }

        private IReplyMarkup _replyMarkup;

        private readonly bool _allowSendWithoutReply;

        private readonly bool _disableWebPreview;

        private IEnumerable<MessageEntity> _messageEntities; 

        private MessageMode _messageMode;

        private readonly IFlamingoCore _client;

        private readonly bool _disableNotification;

        private string _text;

        private const ParseMode _parseMode = ParseMode.Html;

        /// <summary>
        /// Append text to your message
        /// </summary>
        public Messenger AppendText(string text)
        {
            if(_text == null)
                _text = text;
            else
                _text += text;

            _text = Markdig.Markdown.ToHtml(_text);
            _messageMode &= MessageMode.Text;
            return this;
        }

        private List<FileToSend> _photos;

        /// <summary>
        /// Append photo to your message
        /// </summary>
        public Messenger AppendPhoto(string input)
        {
            if (_photos == null)
                _photos = new List<FileToSend>();

            if(Regex.IsMatch(input, 
                @"[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)"))
            {
                _photos.Add(new FileToSend(input, InputType.Url));
            }
            else if(Regex.IsMatch(input,@"^(.+)\/([^\/]+)$"))
            {
                _photos.Add(new FileToSend(input, InputType.Path));
            }
            else
            {
                _photos.Add(new FileToSend(input, InputType.Path));
            }

            _messageMode &= MessageMode.Photo;
            return this;
        }
    }
}
