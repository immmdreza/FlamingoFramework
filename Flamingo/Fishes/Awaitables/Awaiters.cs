using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System.Threading;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Fishes.Awaitables
{
    /// <summary>
    /// Quickly create an await-able incoming handler for any update type
    /// </summary>
    public static class Awaiters
    {
        /// <summary>
        /// Awaits for an incoming <see cref="Message"/>
        /// </summary>
        public static SimpleAwaitableInComing<Message> AwaitMessage(
            IFilter<ICondiment<Message>> filter,
            IFilterAsync<ICondiment<Message>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<Message>(
                filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Awaits for an incoming <see cref="CallbackQuery"/>
        /// </summary>
        public static SimpleAwaitableInComing<CallbackQuery> AwaitCallbackQuery(
            IFilter<ICondiment<CallbackQuery>> filter,
            IFilterAsync<ICondiment<CallbackQuery>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<CallbackQuery>(
                filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Awaits for an incoming <see cref="InlineQuery"/>
        /// </summary>
        public static SimpleAwaitableInComing<InlineQuery> AwaitInlineQuery(
            IFilter<ICondiment<InlineQuery>> filter,
            IFilterAsync<ICondiment<InlineQuery>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<InlineQuery>(
                filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Awaits for an incoming <see cref="ChosenInlineResult"/>
        /// </summary>
        public static SimpleAwaitableInComing<ChosenInlineResult> AwaitChosenInlineResult(
            IFilter<ICondiment<ChosenInlineResult>> filter,
            IFilterAsync<ICondiment<ChosenInlineResult>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<ChosenInlineResult>(
                filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Awaits for an incoming <see cref="ChatMemberUpdated"/>
        /// </summary>
        public static SimpleAwaitableInComing<ChatMemberUpdated> AwaitChatMemberUpdated(
            IFilter<ICondiment<ChatMemberUpdated>> filter,
            IFilterAsync<ICondiment<ChatMemberUpdated>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<ChatMemberUpdated>(
                filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Awaits for an incoming <see cref="Poll"/>
        /// </summary>
        public static SimpleAwaitableInComing<Poll> AwaitPoll(
            IFilter<ICondiment<Poll>> filter,
            IFilterAsync<ICondiment<Poll>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<Poll>(
                filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Awaits for an incoming <see cref="PollAnswer"/>
        /// </summary>
        public static SimpleAwaitableInComing<PollAnswer> AwaitPollAnswer(
            IFilter<ICondiment<PollAnswer>> filter,
            IFilterAsync<ICondiment<PollAnswer>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<PollAnswer>(
                filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Awaits for an incoming <see cref="ShippingQuery"/>
        /// </summary>
        public static SimpleAwaitableInComing<ShippingQuery> AwaitShippingQuery(
            IFilter<ICondiment<ShippingQuery>> filter,
            IFilterAsync<ICondiment<ShippingQuery>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<ShippingQuery>(
                filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Awaits for an incoming <see cref="PreCheckoutQuery"/>
        /// </summary>
        public static SimpleAwaitableInComing<PreCheckoutQuery> AwaitPreCheckoutQuery(
            IFilter<ICondiment<PreCheckoutQuery>> filter,
            IFilterAsync<ICondiment<PreCheckoutQuery>> filterAsync,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return new SimpleAwaitableInComing<PreCheckoutQuery>(
                filter, filterAsync, timeOut, cancellationToken);
        }
    }
}
