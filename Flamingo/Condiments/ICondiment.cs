using Flamingo.Condiments.Extensions;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Flamingo.Fishes;
using Flamingo.Helpers;
using Flamingo.Helpers.Types.Enums;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Condiments
{
    /// <summary>
    /// Interface to create condiments
    /// </summary>
    /// <typeparam name="T">InComing Update Type</typeparam>
    public interface ICondiment<T>
    {
        /// <summary>
        /// Update type of this condiment
        /// </summary>
        public UpdateType UpdateType { get; }

        /// <summary>
        /// InComing Update!
        /// </summary>
        public T InComing { get; }

        /// <summary>
        /// The engaged instance of FlamingoCore
        /// </summary>
        public IFlamingoCore Flamingo { get; }

        /// <summary>
        /// The engaged instance of ITelegramBotClient
        /// </summary>
        public ITelegramBotClient Bot => Flamingo.BotClient;

        /// <summary>
        /// The main string query that update may carry
        /// </summary>
        /// <example>message.Text or message.Caption for Message</example>
        /// <example>callback.Data for CallbackQuery</example>
        public string StringQuery { get; }

        /// <summary>
        /// Query arguments fetched by splitting string query ( mostly with ' ' )
        /// </summary>
        /// <remarks>Consider using <c>GetRequireArgs</c> to check args safe and quickly</remarks>
        public string[] QueryArgs { get; }


        #region Extra attr

        /// <summary>
        /// Flamingo
        /// </summary>
        public FlamingoChatType FlamingoChatType
        {
            get
            {
                if (Chat != null)
                    return Chat.Type.ToFlamingoChatType();
                else
                    return FlamingoChatType.NoChat;
            }
            
        }

        /// <summary>
        /// User id for sender of update
        /// </summary>
        public long SenderId { get; }

        /// <summary>
        /// The sender of update ( mostly <c>update.From</c> )
        /// </summary>
        public User Sender { get; }

        /// <summary>
        /// The container chat of update ( mostly <c>update.Chat</c> )
        /// </summary>
        public Chat Chat { get; }

        /// <summary>
        /// You have this if you have a RegexFilter and at least a matched result
        /// </summary>
        public MatchCollection MatchCollection { get; set; }

        /// <summary>
        /// This is the query that comes after /command
        /// If any Command Filter is available
        /// </summary>
        /// <remark>
        /// <c>/command CommandQuery</c>
        /// </remark>
        public string CommandQuery { get; set; } 

        #endregion


        #region Extensions

        /// <summary>
        /// Shorthand method for <see cref="FlamingoCore.WaitForInComing{U}(IFisherAwaits{U})"/>
        /// </summary>
        /// <typeparam name="U">Update type</typeparam>
        /// <param name="fisherAwaits">Await-able incoming handler</param>
        /// <returns>return <see cref="AwaitableResult{U}"/></returns>
        public async Task<AwaitableResult<U>> WaitFor<U>(IFisherAwaits<U> fisherAwaits)
        {
            return await Flamingo.WaitForInComing(fisherAwaits);
        }

        /// <summary>
        /// Waits for an update that matches the filters
        /// </summary>
        /// <typeparam name="U">InComing update type</typeparam>
        /// <param name="filter">normal filters</param>
        /// <param name="filterAsync">async filters</param>
        /// <param name="timeOut">time in second to wait</param>
        /// <param name="cancellationToken">token to cancel waiting</param>
        public async Task<AwaitableResult<U>> WaitFor<U>(
            IFilter<ICondiment<U>> filter = null,
            IFilterAsync<ICondiment<U>> filterAsync = null,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await Flamingo.WaitForInComing(filter, filterAsync, timeOut, cancellationToken);
        }

        /// <summary>
        /// Waits for an update that matches the filters
        /// </summary>
        /// <typeparam name="U">InComing update type</typeparam>
        /// <param name="filter">expression to filter incoming update</param>
        /// <param name="timeOut">time in second to wait</param>
        /// <param name="cancellationToken">token to cancel waiting</param>
        public async Task<AwaitableResult<U>> WaitFor<U>(
            Func<ICondiment<U>, bool> filter,
            int timeOut = 30,
            CancellationTokenSource cancellationToken = default)
        {
            return await Flamingo.WaitForInComing(
                new FilterBase<ICondiment<U>>(filter), null, timeOut, cancellationToken);
        }


        /// <summary>
        /// Tries to get a argument
        /// </summary>
        /// <typeparam name="Type">Type of arg</typeparam>
        /// <param name="index">It's index</param>
        /// <param name="outArg">Fetched arg</param>
        /// <param name="toEnd">Get to the end of line</param>
        /// <param name="joiner">join them using which char?</param>
        /// <returns></returns>
        public bool TryGetStrArg<Type>(
            int index, out Type outArg, bool toEnd = false, char joiner = ' ')
        {
            var arg = this.GetStrArg(index, toEnd, joiner);
            if (arg != null)
            {
                try
                {
                    if (arg.TryConvert(out Type output))
                    {
                        outArg = output;
                        return true;
                    }

                    outArg = default;
                    return true;
                }
                catch
                {
                    outArg = default;
                    return false;
                }
            }
            else
            {
                outArg = default;
                return false;
            }
        }

        #endregion
    }
}
