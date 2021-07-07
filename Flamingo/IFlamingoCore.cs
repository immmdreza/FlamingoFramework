using Flamingo.Condiments;
using Flamingo.Fishes;
using Flamingo.Fishes.Awaitables;
using Flamingo.Fishes.InComingFishes;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using Flamingo.RateLimiter;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo
{
    public interface IFlamingoCore
    {
        /// <summary>
        /// The this of allowed updates based on InComings you've added
        /// </summary>
        List<UpdateType> AllowedUpdates { get; }

        /// <summary>
        /// Telegram bot that is suppose to run
        /// </summary>
        TelegramBotClient BotClient { get; }

        /// <summary>
        /// User object of bot itself ( Filled when calling <see cref="InitBot(string, char)"/> )
        /// </summary>
        User BotInfo { get; }

        /// <summary>
        /// CancellationTokenSource to do cancellation stuff
        /// </summary>
        CancellationTokenSource CancellationTokenSource { get; }

        /// <summary>
        /// You can force flamingo to receive update type of your choice
        /// </summary>
        /// <remarks>Flamingo will setup allowed updates based on your handlers automatically
        /// and you may not need this</remarks>
        /// <param name="updateType"></param>
        void AddAllowedUpdateType(UpdateType updateType);

        /// <summary>
        /// Add your inComings (handlers) to the Flamingo 🦩
        /// </summary>
        /// <remarks>
        /// This inComing handler could be a <see cref="SimpleInComing{T}"/>
        /// or one of ready to use Incoming handlers for every update you may like see following list:
        /// <list type="bullet">
        /// <item>
        /// <see cref="InComingMessage"/> for every type of message including edited or channel posts
        /// </item>
        /// <item><see cref="InComingCallbackQuery"/> for callback queries.</item>
        /// <item><see cref="InComingInlineQuery"/> for in-line queries</item>
        /// <item><see cref="InComingChosenInlineResult"/></item> for chosen in-line results
        /// <item><see cref="InComingChatMember"/></item> for both ChatMember and MyChatMember
        /// <item><see cref="InComingPoll"/></item> for updates about polls
        /// <item><see cref="InComingPollAnswer"/></item> for updates about polls answer
        /// <item><see cref="InComingShippingQuery"/> and <see cref="InComingPreCheckoutQuery"/>
        /// for shipping stuff</item>
        /// </list>
        /// </remarks>
        /// <typeparam name="T">Type of inComing update</typeparam>
        /// <param name="fish">Your incoming handler</param>
        /// <param name="group">Process group number. (Lower group processes sooner)</param>
        /// <param name="isEdited">(For Messages only) if it's for edited messages</param>
        /// <param name="isChannelPost">(For Messages only) if it's for channel messages</param>
        /// <param name="isMine">(For ChatMemberUpdated only) is it's MyChatMember</param>
        void AddInComing<T>(
            IFish<T> fish,
            int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false);

        /// <summary>
        /// (Not recommended) If you are not using <see cref="WaitForInComing{T}(IFisherAwaits{T})"/> 
        /// and you want to create await-able handlers manually, then use this to add an await-able
        /// incoming handler to the Flamingo! and then call <see cref="IFisherAwaits{T}.AwaitFor"/>
        /// to wait for respond.
        /// </summary>
        GroupedInComing<T> AddInComingAwaitable<T>(
            IFisherAwaits<T> fish,
            int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false);

        /// <summary>
        /// Time has come to fly! Start handlers by calling this.
        /// </summary>
        /// <param name="clearQueue">If you don't care about old updates and unprocessed</param>
        /// <param name="errorHandler">a callback function to handle errors</param>
        /// <returns></returns>
        Task Fly(
            bool clearQueue = false,
            Func<FlamingoCore, Exception, Task> errorHandler = null);

        /// <summary>
        /// Initialize your bot!
        /// </summary>
        /// <param name="botToken">Bot token from @BotFather!</param>
        /// <param name="callbackDataSpliter">char that uses to split call back data</param>
        /// <param name="getMe">If you don't like to fill bot user info here</param>
        /// <returns>The flamingo core instance itself</returns>
        FlamingoCore InitBot(
            string botToken,
            bool getMe = false,
            char callbackDataSpliter = '_');

        /// <summary>
        /// Initialize your bot!
        /// </summary>
        /// <param name="botToken">Bot token from @BotFather!</param>
        /// <param name="callbackDataSpliter">char that uses to split call back data</param>
        /// <returns>The flamingo core instance itself</returns>
        Task<FlamingoCore> InitBot(string botToken, char callbackDataSpliter = '_');

        /// <summary>
        /// Wanna go even deeper? This is used when you have your own update receiver
        /// And you have also an update redirector! Using this you can pass an <see cref="ICondiment{T}"/>
        /// of your choice that is customized the way you like for every single update.
        /// </summary>
        /// <remarks>
        /// This is useful when you need to pass your own properties to the update Condiment
        /// and use them when handling update. and you can also control their life cycle.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="condiment"></param>
        /// <returns>Get an async Enumerable of InComing handlers that passed filters</returns>
        IAsyncEnumerable<IFish<T>> PassedHandlersAsync<T>(ICondiment<T> condiment);

        /// <summary>
        /// Processes await-able incomings here 
        /// </summary>
        /// <remarks>If this method return true, 
        /// then normal handlers should not be processed for this update.</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="condiment"></param>
        /// <returns></returns>
        Task<bool> ProcessAwaitables<T>(ICondiment<T> condiment);

        /// <summary>
        /// Wanna go even deeper? This is used when you have your own update receiver
        /// And you have also an update redirector! Using this you can pass an <see cref="ICondiment{T}"/>
        /// of your choice that is customized the way you like for every single update.
        /// </summary>
        /// <remarks>
        /// This is useful when you need to pass your own properties to the update Condiment
        /// and use them when handling update. and you can also control their life cycle.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="condiment"></param>
        Task ProcessInComings<T>(ICondiment<T> condiment);

        /// <summary>
        /// This is a method provided by "Telegram.Bot.Extensions.Polling" to receive updates
        /// </summary>
        /// <param name="onError">Function that called on errors</param>
        Task ReceiveAsync(
            Func<ITelegramBotClient, Exception, CancellationToken, Task> onError);

        /// <summary>
        /// Safely remove an await-able handler
        /// </summary>
        void RemoveAwaitable<T>(GroupedInComing<T> groupedIn);

        /// <summary>
        /// Set bot user info if you didn't
        /// </summary>
        Task<FlamingoCore> SetBotInfo();


        /// <summary>
        /// This is a method provided by "Telegram.Bot.Extensions.Polling" to receive updates
        /// </summary>
        /// <param name="onError">Function that called on errors</param>
        void StartReceiving(
            Func<ITelegramBotClient, Exception, CancellationToken, Task> onError);

        /// <summary>
        /// Call this to cancel everything that exists
        /// </summary>
        void TriggerCancell();

        /// <summary>
        /// This method is the very first thing that flamingo dose when handling
        /// </summary>
        /// <remarks>
        /// Use this method only if you have a customized update receiver.
        /// If you don't the use one of following:
        /// <see cref="Fly(bool, Func{FlamingoCore, Exception, Task})"/> From this package
        /// Or <see cref="StartReceiving(Func{ITelegramBotClient, Exception, CancellationToken, Task})"/>
        /// and <see cref="ReceiveAsync(Func{ITelegramBotClient, Exception, CancellationToken, Task})"/>
        /// from Polling extension.
        /// </remarks>
        /// <param name="update">Update you want to process</param>
        Task UpdateRedirector(Update update);

        /// <summary>
        /// Waits for a await-able incoming handler ( <see cref="SimpleAwaitableInComing{T}"/> )
        /// </summary>
        /// <remarks>Use await! this method will ( and Should ) block and wait for a respond to come.
        /// Or timeout!</remarks>
        /// <typeparam name="T">Type of update</typeparam>
        /// <param name="inComingfish">InComing handler of type <see cref="IFisherAwaits{T}"/></param>
        /// <returns>Returns <see cref="AwaitableResult{T}"/></returns>
        Task<AwaitableResult<T>> WaitForInComing<T>(IFisherAwaits<T> inComingfish);

        /// <summary>
        /// Rate limit manager
        /// </summary>
        public RateLimitManager RateLimitManager { get; }
    }
}