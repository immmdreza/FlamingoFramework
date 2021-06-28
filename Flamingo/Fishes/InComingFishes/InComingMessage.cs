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
    /// You can create your handler for <c>Message</c> by inheriting from this
    /// </summary>
    /// <remarks>This is also for: <c>EditedMessage</c>, <c>ChannlePost</c>, <c>EditedChannelPost</c></remarks>
    public abstract class InComingMessage : InComingFish<Message>
    {
        /// <summary>
        /// You can create your handler for <c>Message</c> by inheriting from this
        /// </summary>
        /// <remarks>This is also for: <c>EditedMessage</c>, <c>ChannlePost</c>, <c>EditedChannelPost</c></remarks>
        /// <param name="filter">Add your Optional sync filter based on incoming update</param>
        /// <param name="filterAsync">Add your Optional async filter based on incoming update</param>
        public InComingMessage(
            IFilter<ICondiment<Message>> filter = null,
            IFilterAsync<ICondiment<Message>> filterAsync = null) 
            : base(filter, filterAsync)
        { }

        /// <summary>
        /// Complete version of Cdmt based on incoming update
        /// </summary>
        public MessageCondiment RealCdmt => Cdmt as MessageCondiment;

        #region Helpers

        protected Chat Chat => Cdmt.Chat;

        protected User Sender => Cdmt.Sender;

        protected async Task<Message> RespondText(
            string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            bool disableNofication = true,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            return await Cdmt.RespondText(text,
                parseMode, entities, disableWebPreview,
                disableNofication,
                replyMarkup, cancellationToken);
        }

        protected async Task<Message> ReplyText(
            string text,
            ParseMode parseMode = ParseMode.Default,
            IEnumerable<MessageEntity> entities = null,
            bool disableWebPreview = true,
            bool disableNofication = true,
            IReplyMarkup replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            return await Cdmt.ReplyText(text,
                parseMode, entities, disableWebPreview,
                disableNofication,
                replyMarkup, cancellationToken);
        }

        #endregion
    }
}
