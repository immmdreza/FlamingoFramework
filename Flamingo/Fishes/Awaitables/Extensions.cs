using Flamingo.Condiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;

namespace Flamingo.Fishes.Awaitables
{
    /// <summary>
    /// A set of extension methods related to await-able handlers
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Extension method to wait for a user text respond in private chat!
        /// </summary>
        public static async Task<AwaitableResult<Message>> WaitForTextRespond<T>(
            this ICondiment<T> cdmt,
            long userId,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitTextRespond(
                userId, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a callback query incoming!
        /// </summary>
        public static async Task<AwaitableResult<CallbackQuery>> WaitForCallbackQuery<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<CallbackQuery>> filter = null,
            IFilterAsync<ICondiment<CallbackQuery>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitCallbackQuery(
                filter, filterAsync, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a message incoming!
        /// </summary>
        public static async Task<AwaitableResult<Message>> WaitForMessage<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<Message>> filter = null,
            IFilterAsync<ICondiment<Message>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitMessage(
                filter, filterAsync, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a in-line query incoming!
        /// </summary>
        public static async Task<AwaitableResult<InlineQuery>> WaitForInlineQuery<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<InlineQuery>> filter = null,
            IFilterAsync<ICondiment<InlineQuery>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitInlineQuery(
                filter, filterAsync, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a chosen in-line result incoming!
        /// </summary>
        public static async Task<AwaitableResult<ChosenInlineResult>> WaitForChosenInlineResult<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<ChosenInlineResult>> filter = null,
            IFilterAsync<ICondiment<ChosenInlineResult>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitChosenInlineResult(
                filter, filterAsync, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a chat member updated incoming!
        /// </summary>
        public static async Task<AwaitableResult<ChatMemberUpdated>> WaitForChatMember<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<ChatMemberUpdated>> filter = null,
            IFilterAsync<ICondiment<ChatMemberUpdated>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitChatMemberUpdated(
                filter, filterAsync, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a poll incoming!
        /// </summary>
        public static async Task<AwaitableResult<Poll>> WaitForPoll<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<Poll>> filter = null,
            IFilterAsync<ICondiment<Poll>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitPoll(
                filter, filterAsync, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a poll answer incoming!
        /// </summary>
        public static async Task<AwaitableResult<PollAnswer>> WaitForPollAnswer<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<PollAnswer>> filter = null,
            IFilterAsync<ICondiment<PollAnswer>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitPollAnswer(
                filter, filterAsync, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a shipping query incoming!
        /// </summary>
        public static async Task<AwaitableResult<ShippingQuery>> WaitForShippingQuery<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<ShippingQuery>> filter = null,
            IFilterAsync<ICondiment<ShippingQuery>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitShippingQuery(
                filter, filterAsync, timeOut, cancellationToken));
        }

        /// <summary>
        /// Extension method to wait for a pre checkout query incoming!
        /// </summary>
        public static async Task<AwaitableResult<PreCheckoutQuery>> WaitForPreCheckoutQuery<T>(
            this ICondiment<T> cdmt,
            IFilter<ICondiment<PreCheckoutQuery>> filter = null,
            IFilterAsync<ICondiment<PreCheckoutQuery>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await cdmt.WaitFor(Awaiters.AwaitPreCheckoutQuery(
                filter, filterAsync, timeOut, cancellationToken));
        }
    }
}
