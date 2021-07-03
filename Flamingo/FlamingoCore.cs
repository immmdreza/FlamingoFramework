using Flamingo.Attributes;
using Flamingo.Attributes.Filters;
using Flamingo.Attributes.Filters.Async;
using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Flamingo.Fishes;
using Flamingo.Fishes.Awaitables;
using Flamingo.Fishes.InComingFishes;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Flamingo
{
    /// <summary>
    /// Main class to setup handler, run bot and more ...
    /// </summary>
    public class FlamingoCore
    {
        private char _callbackDataSpliter;

        private TelegramBotClient _botClient;

        private User _botInfo;

        /// <summary>
        /// Telegram bot that is suppose to run
        /// </summary>
        public TelegramBotClient BotClient => _botClient;

        private readonly HttpClient _httpClient;

        private readonly string _baseUrl;

        private readonly InComingManager _inComingManager;

        private readonly InComingManager _inComingAwaitableManager;

        private readonly CancellationTokenSource _cancellationTokenSource;

        /// <summary>
        /// The this of allowed updates based on InComings you've added
        /// </summary>
        public List<UpdateType> AllowedUpdates => _allowedUpdates;

        /// <summary>
        /// CancellationTokenSource to do cancellation stuff
        /// </summary>
        public CancellationTokenSource CancellationTokenSource => _cancellationTokenSource;

        /// <summary>
        /// User object of bot itself ( Filled when calling <see cref="InitBot(string, char)"/> )
        /// </summary>
        public User BotInfo => _botInfo;

        /// <summary>
        /// Create you Flamingo
        /// </summary>
        /// <param name="httpClient"></param>
        /// <param name="baseUrl"></param>
        public FlamingoCore(HttpClient httpClient = null, string baseUrl = null)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _httpClient ??= new HttpClient();
            _inComingManager = new InComingManager();
            _inComingAwaitableManager = new InComingManager();
            _cancellationTokenSource = new CancellationTokenSource();
            _allowedUpdates = new List<UpdateType>();

            var found = AddAttributedInComings();
            Console.WriteLine($"{found} Attributed inComing found!");
        }

        /// <summary>
        /// Initialize your bot!
        /// </summary>
        /// <param name="botToken">Bot token from @BotFather!</param>
        /// <param name="callbackDataSpliter">char that uses to split call back data</param>
        /// <returns>The flamingo core instance itself</returns>
        public async Task<FlamingoCore> InitBot(string botToken, char callbackDataSpliter = '_')
        {
            _callbackDataSpliter = callbackDataSpliter;
            _botClient = new TelegramBotClient(botToken, _httpClient, _baseUrl);
            _botInfo = await _botClient.GetMeAsync();

            return this;
        }

        private void InAttributedProcess<T>(
            MethodInfo methodInfo,
            Attribute[] attributes,
            int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false)
        {
            var filters = attributes
                .Where(x => x is IFilterAttribute<T>)
                .Cast<IFilterAttribute<T>>()
                .Select(x => x.Filter)
                .ToList();

            var asyncFilters = attributes
                .Where(x => x is IFilterAsyncAttribute<T>)
                .Cast<IFilterAsyncAttribute<T>>()
                .Select(x => x.Filter)
                .ToList();

            var inComing = new SimpleInComing<T>(
                methodInfo.CreateDelegate(
                    typeof(Func<ICondiment<T>, Task<bool>>))
                        as Func<ICondiment<T>, Task<bool>>,
                    FilterBase<ICondiment<T>>.Combine(filters),
                    FilterBaseAsync<ICondiment<T>>.Combine(asyncFilters));

            AddInComing(inComing, group, isEdited, isChannelPost, isMine);
        }

        /// <summary>
        /// This method will search the project for InComing attributes
        /// Then adds them to the FlamingoCore using <see cref="AddInComing{T}(IFish{T}, int, bool, bool, bool)"/>
        /// </summary>
        /// <returns>Count of found incoming attributes</returns>
        private int AddAttributedInComings()
        {
            int foundCount = 0;
            foreach (var item in Assembly.GetEntryAssembly().GetTypes().Where(x => x.IsClass))
            {
                foreach (var m in item.GetMethods())
                {
                    var allAttr = Attribute.GetCustomAttributes(m);

                    bool added = false;
                    foreach (var target in allAttr)
                    {
                        if (target is InComingMessageAttribute inComingMessage)
                        {
                            InAttributedProcess<Message>(m, allAttr,
                                group: inComingMessage.Group,
                                isEdited: inComingMessage.IsEdited,
                                isChannelPost: inComingMessage.IsChannelPost);
                            foundCount++;
                            added = true;
                            break;
                        }
                        else if (target is InComingCallbackQueryAttribute inComingCallback)
                        {
                            InAttributedProcess<CallbackQuery>(m, allAttr,
                                group: inComingCallback.Group);
                            foundCount++;
                            added = true;
                            break;
                        }
                        else if (target is InComingChatMemberAttribute inComingChatMember)
                        {
                            InAttributedProcess<ChatMemberUpdated>(m, allAttr,
                                group: inComingChatMember.Group,
                                isMine: inComingChatMember.IsMine);
                            foundCount++;
                            added = true;
                            break;
                        }
                        else if (target is InComingInlineQueryAttribute inComingInline)
                        {
                            InAttributedProcess<InlineQuery>(m, allAttr,
                                group: inComingInline.Group);
                            foundCount++;
                            added = true;
                            break;
                        }
                        else if (target is InComingChosenInlineResultAttribute inComing)
                        {
                            InAttributedProcess<ChosenInlineResult>(m, allAttr,
                                group: inComing.Group);
                            foundCount++;
                            added = true;
                            break;
                        }
                        else if (target is InComingPollAttribute inComingPoll)
                        {
                            InAttributedProcess<Poll>(m, allAttr,
                                group: inComingPoll.Group);
                            foundCount++;
                            added = true;
                            break;
                        }
                        else if (target is InComingPollAnswerAttribute inComingPollAnswer)
                        {
                            InAttributedProcess<PollAnswer>(m, allAttr,
                                group: inComingPollAnswer.Group);
                            foundCount++;
                            added = true;
                            break;
                        }
                        else if (target is InComingShippingQueryAttribute inComingShippingQuery)
                        {
                            InAttributedProcess<ShippingQuery>(m, allAttr,
                                group: inComingShippingQuery.Group);
                            foundCount++;
                            added = true;
                            break;
                        }
                        else if (target is InComingPreCheckoutQueryAttribute inComingPreCheckoutQuery)
                        {
                            InAttributedProcess<PreCheckoutQuery>(m, allAttr,
                                group: inComingPreCheckoutQuery.Group);
                            foundCount++;
                            added = true;
                            break;
                        }
                    }

                    if (added) break;
                }
            }
            return foundCount;
        }

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
        public void AddInComing<T>(IFish<T> fish, int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false)
        {
            if(typeof(T) == typeof(Message))
            {
                AddInComingMessage(fish as IFish<Message>, isEdited, isChannelPost, group);
            }
            else if(typeof(T) == typeof(CallbackQuery))
            {
                _inComingManager.SafeAdd(new GroupedInComing<CallbackQuery>(fish as IFish<CallbackQuery>, group));

                if (!_allowedUpdates.Contains(UpdateType.CallbackQuery))
                {
                    _allowedUpdates.Add(UpdateType.CallbackQuery);
                }
            }
            else if (typeof(T) == typeof(InlineQuery))
            {
                _inComingManager.SafeAdd(new GroupedInComing<InlineQuery>(fish as IFish<InlineQuery>, group));

                if (!_allowedUpdates.Contains(UpdateType.InlineQuery))
                {
                    _allowedUpdates.Add(UpdateType.InlineQuery);
                }
            }
            else if (typeof(T) == typeof(ChosenInlineResult))
            {
                _inComingManager.SafeAdd(new GroupedInComing<ChosenInlineResult>(
                    fish as IFish<ChosenInlineResult>, group));

                if (!_allowedUpdates.Contains(UpdateType.ChosenInlineResult))
                {
                    _allowedUpdates.Add(UpdateType.ChosenInlineResult);
                }
            }
            else if (typeof(T) == typeof(Poll))
            {
                _inComingManager.SafeAdd(new GroupedInComing<Poll>(fish as IFish<Poll>, group));

                if (!_allowedUpdates.Contains(UpdateType.Poll))
                {
                    _allowedUpdates.Add(UpdateType.Poll);
                }
            }
            else if (typeof(T) == typeof(PollAnswer))
            {
                _inComingManager.SafeAdd(new GroupedInComing<PollAnswer>(fish as IFish<PollAnswer>, group));

                if (!_allowedUpdates.Contains(UpdateType.PollAnswer))
                {
                    _allowedUpdates.Add(UpdateType.PollAnswer);
                }
            }
            else if (typeof(T) == typeof(ShippingQuery))
            {
                _inComingManager.SafeAdd(new GroupedInComing<ShippingQuery>(fish as IFish<ShippingQuery>, group));

                if (!_allowedUpdates.Contains(UpdateType.ShippingQuery))
                {
                    _allowedUpdates.Add(UpdateType.ShippingQuery);
                }
            }
            else if (typeof(T) == typeof(PreCheckoutQuery))
            {
                _inComingManager.SafeAdd(new GroupedInComing<PreCheckoutQuery>(fish as IFish<PreCheckoutQuery>, group));

                if (!_allowedUpdates.Contains(UpdateType.PreCheckoutQuery))
                {
                    _allowedUpdates.Add(UpdateType.PreCheckoutQuery);
                }
            }
            else if (typeof(T) == typeof(ChatMemberUpdated))
            {
                _inComingManager.SafeAdd(new GroupedInComing<ChatMemberUpdated>(fish as IFish<ChatMemberUpdated>, group));

                if (isMine)
                {
                    if (!_allowedUpdates.Contains(UpdateType.MyChatMember))
                    {
                        _allowedUpdates.Add(UpdateType.MyChatMember);
                    }
                }
                else
                {
                    if (!_allowedUpdates.Contains(UpdateType.ChatMember))
                    {
                        _allowedUpdates.Add(UpdateType.ChatMember);
                    }
                }
            }
        }


        /// <summary>
        /// (Not recommended) If you are not using <see cref="WaitForInComing{T}(IFisherAwaits{T})"/> 
        /// and you want to create await-able handlers manually, then use this to add an await-able
        /// incoming handler to the Flamingo! and then call <see cref="IFisherAwaits{T}.Wait(Dictionary{IFish{T}, int})"/>
        /// to wait for respond.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fish"></param>
        /// <param name="group">Grouping rank if required</param>
        /// <returns></returns>
        public GroupedInComing<T> AddInComingAwaitable<T>(IFisherAwaits<T> fish, int group = 0)
        {
            var toAdd = new GroupedInComing<T>(fish as IFish<T>, group);
            _inComingAwaitableManager.SafeAdd(toAdd);
            return toAdd;
        }

        private void AddInComingMessage(
            IFish<Message> fish, bool isEdited = false,
            bool isChannelPost = false, int group = 0)
        {
            _inComingManager.SafeAdd(new GroupedInComing<Message>(fish, group));

            if(isChannelPost)
            {
                if (isEdited)
                {
                    if (!_allowedUpdates.Contains(UpdateType.EditedChannelPost))
                    {
                        _allowedUpdates.Add(UpdateType.EditedChannelPost);
                    }
                }
                else
                {
                    if (!_allowedUpdates.Contains(UpdateType.ChannelPost))
                    {
                        _allowedUpdates.Add(UpdateType.ChannelPost);
                    }
                }
            }

            if (isEdited)
            {
                if (!_allowedUpdates.Contains(UpdateType.EditedMessage))
                {
                    _allowedUpdates.Add(UpdateType.EditedMessage);
                }
            }
            else
            {
                if (!_allowedUpdates.Contains(UpdateType.Message))
                {
                    _allowedUpdates.Add(UpdateType.Message);
                }
            }
        }

        private List<UpdateType> _allowedUpdates;

        /// <summary>
        /// Processes await-able incomings here 
        /// </summary>
        /// <remarks>If this method return true, 
        /// then normal handlers should not be processed for this update.</remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="condiment"></param>
        /// <returns></returns>
        public async Task<bool> ProcessAwaitables<T>(ICondiment<T> condiment)
        {
            var manager = _inComingAwaitableManager.GetInComingList<T>();

            if(manager.Any())
            {
                foreach (var inComing in manager)
                {
                    if (await inComing.InComingFish.ShouldEatAsync(condiment))
                    {
                        var fisher = inComing.InComingFish as IFisherAwaits<T>;
                        fisher.SetCdmt(condiment);
                        (inComing.InComingFish as IFisherAwaits<T>).AwaitFor();
                        return true;
                    }
                }
            }

            return false;
        }

        private async Task ProcessInComings<T>(
            SortedSet<GroupedInComing<T>> _inComingMessages,
            ICondiment<T> condiment)
        {
            if (await ProcessAwaitables(condiment)) return;

            foreach (var inComing in _inComingMessages
                .Where(x=> !(x.InComingFish is IFisherAwaits<T>)))
            {
                if (await inComing.InComingFish.ShouldEatAsync(condiment))
                {
                    if (!await inComing.InComingFish.GetEaten(condiment))
                    {
                        break;
                    }
                }
            }
        }

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
        public async Task ProcessInComings<T>(
            ICondiment<T> condiment)
        {
            if (await ProcessAwaitables(condiment)) return;

            await foreach (var handler in PassedHandlersAsync(condiment))
            {
                if (!await handler.GetEaten(condiment))
                {
                    break;
                }
            }
        }

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
        public async IAsyncEnumerable<IFish<T>> PassedHandlersAsync<T>(ICondiment<T> condiment)
        {
            var _inComingMessages = _inComingManager.GetInComingList<T>();

            if (_inComingMessages == null) yield break;

            foreach (var inComing in _inComingMessages
                .Where(x => !(x.InComingFish is IFisherAwaits<T>)))
            {
                if (await inComing.InComingFish.ShouldEatAsync(condiment))
                {
                    yield return inComing.InComingFish;
                }
            }
        }

        /// <summary>
        /// Call this to cancel everything that exists
        /// </summary>
        public void TriggerCancell()
        {
            Console.WriteLine("Cancellation triggered!");
            _cancellationTokenSource.Cancel();
        }

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
        /// 
        /// Don't forget to call <see cref="AddAttributedInComings"/> if you are using attributes
        /// and customized update receiver.
        /// </remarks>
        /// <param name="update">Update you want to process</param>
        public async Task UpdateRedirector(Update update)
        {
            switch (update)
            {
                case { Message: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingMessages,
                            new MessageCondiment(update.Message, this));

                        break;
                    }

                case { ChannelPost: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingMessages,
                            new MessageCondiment(update.ChannelPost, this, true));

                        break;
                    }

                case { EditedMessage: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingMessages,
                            new MessageCondiment(update.EditedMessage, this, isEdited: true));

                        break;
                    }

                case { EditedChannelPost: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingMessages,
                            new MessageCondiment(update.EditedChannelPost, this, true, true));

                        break;
                    }

                case { CallbackQuery: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingCallbackQueries,
                            new CallbackQueryCondiment(
                                update.CallbackQuery, this, _callbackDataSpliter));

                        break;
                    }

                case { InlineQuery: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingInlineQueries,
                            new InlineQueryCondiment(
                                update.InlineQuery, this));

                        break;
                    }

                case { ChosenInlineResult: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingInlineResultChosen,
                            new ChosenInlineResultCondiment(
                                update.ChosenInlineResult, this));

                        break;
                    }

                case { ChatMember: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingChatMembers,
                            new ChatMemberCondiment(
                                update.ChatMember, this, false));

                        break;
                    }

                case { MyChatMember: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingChatMembers,
                            new ChatMemberCondiment(
                                update.MyChatMember, this, true));

                        break;
                    }

                case { Poll: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingPoll,
                            new PollCondiment(
                                update.Poll, this));

                        break;
                    }

                case { PollAnswer: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingPollAnswer,
                            new PollAnswerCondiment(
                                update.PollAnswer, this));

                        break;
                    }

                case { PreCheckoutQuery: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingPreCheckoutQuery,
                            new PreCheckoutQueryCondiment(
                                update.PreCheckoutQuery, this));

                        break;
                    }

                case { ShippingQuery: { } }:
                    {
                        await ProcessInComings(
                            _inComingManager.InComingShippingQuery,
                            new ShippingQueryCondiment(
                                update.ShippingQuery, this));

                        break;
                    }
            }
        }

        /// <summary>
        /// Time has come to fly! Start handlers by calling this.
        /// </summary>
        /// <param name="clearQueue">If you don't care about old updates and unprocessed</param>
        /// <param name="errorHandler">a callback function to handle errors</param>
        /// <returns></returns>
        public async Task Fly(
            bool clearQueue = false,
            Func<FlamingoCore, Exception, Task> errorHandler = null)
        {
            int messageOffset = 0;
            Update[] emptyUpdates = Array.Empty<Update>();

            if (clearQueue)
            {
                _ = await _botClient.MakeRequestAsync(new GetUpdatesRequest()
                {
                    Offset = -1
                }, _cancellationTokenSource.Token).ConfigureAwait(false);
            }

            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                int timeout = (int)_botClient.Timeout.TotalSeconds;
                Update[] updates = emptyUpdates;
                try
                {
                    updates = await _botClient.MakeRequestAsync(new GetUpdatesRequest()
                    {
                        Offset = messageOffset,
                        Timeout = timeout,
                        AllowedUpdates = _allowedUpdates,
                    }, _cancellationTokenSource.Token).ConfigureAwait(false);
                }
                catch (OperationCanceledException)
                {
                    // Ignore
                }
                catch (Exception e)
                {
                    if (errorHandler != null)
                    {
                        await errorHandler(this, e);
                    }
                }

                try
                {
                    foreach (Update update in updates)
                    {
                        _ = Task.Run(async () =>
                        {
                            try
                            {
                                await UpdateRedirector(update);
                            }
                            catch(Exception e) 
                            {
                                if (errorHandler != null)
                                {
                                    await errorHandler(this, e);
                                }
                            }
                        }, _cancellationTokenSource.Token);

                        messageOffset = update.Id + 1;
                    }
                }
                catch { }
            }
        }


        /// <summary>
        /// This is a method provided by "Telegram.Bot.Extensions.Polling" to receive updates
        /// </summary>
        /// <param name="onError">Function that called on errors</param>
        public void StartReceiving(
            Func<ITelegramBotClient, Exception, CancellationToken, Task> onError)
        {
            BotClient.StartReceiving(
                new DefaultUpdateHandler(
                    HandleUpdateLongPollingAsync,
                    onError),
                _cancellationTokenSource.Token);
        }


        /// <summary>
        /// This is a method provided by "Telegram.Bot.Extensions.Polling" to receive updates
        /// </summary>
        /// <param name="onError">Function that called on errors</param>
        public async Task ReceiveAsync(
            Func<ITelegramBotClient, Exception, CancellationToken, Task> onError)
        {
            await BotClient.ReceiveAsync(
                new DefaultUpdateHandler(
                    HandleUpdateLongPollingAsync,
                    onError),
                _cancellationTokenSource.Token);
        }

        async Task HandleUpdateLongPollingAsync(
            ITelegramBotClient _,
            Update update,
            CancellationToken __)
        {
            await UpdateRedirector(update);
        }

        #region Helpers

        /// <summary>
        /// Waits for a await-able incoming handler ( <see cref="SimpleAwaitableInComing{T}"/> )
        /// </summary>
        /// <remarks>Use await! this method will ( and Should ) block and wait for a respond to come.
        /// Or timeout!</remarks>
        /// <typeparam name="T">Type of update</typeparam>
        /// <param name="inComingfish">InComing handler of type <see cref="IFisherAwaits{T}"/></param>
        /// <returns>Returns <see cref="AwaitableResult{T}"/></returns>
        public async Task<AwaitableResult<T>> WaitForInComing<T>(IFisherAwaits<T> inComingfish)
        {
            var added = AddInComingAwaitable(inComingfish);
            return await inComingfish.Wait(this, added);
        }

        /// <summary>
        /// Safely remove an await-able handler
        /// </summary>
        public void RemoveAwaitable<T>(GroupedInComing<T> groupedIn)
        {
            var manger = _inComingAwaitableManager.GetInComingList<T>();

            _inComingAwaitableManager.SafeRemove(manger, groupedIn);
        }

        #endregion
    }
}
