﻿using Flamingo.Attributes;
using Flamingo.Attributes.Filters;
using Flamingo.Attributes.Filters.Async;
using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Exceptions;
using Flamingo.Filters;
using Flamingo.Filters.Async;
using Flamingo.Fishes;
using Flamingo.Fishes.Advanced;
using Flamingo.Fishes.Advanced.Attributes;
using Flamingo.Fishes.Advanced.CarrierFishes;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using Flamingo.RateLimiter;
using Flamingo.RateLimiter.Limiters;
using Flamingo.RateLimiter.Limits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class FlamingoCore : IFlamingoCore
    {
        /// <summary>
        /// splitter char for callback query data
        /// </summary>
        protected char _callbackDataSpliter;

        /// <summary>
        /// Telegram bot instance
        /// </summary>
        protected TelegramBotClient _botClient;

        /// <summary>
        /// Bot user info
        /// </summary>
        protected User _botInfo;

        /// <inheritdoc/>
        public TelegramBotClient BotClient => _botClient;

        private readonly HttpClient _httpClient;

        private readonly string _baseUrl;

        private readonly InComingManager _inComingManager;

        private readonly InComingManager _inComingAwaitableManager;

        private RateLimitManager _rateLimitManager;

        private readonly CancellationTokenSource _cancellationTokenSource;

        /// <inheritdoc/>
        public List<UpdateType> AllowedUpdates => _allowedUpdates;

        /// <inheritdoc/>
        public CancellationTokenSource CancellationTokenSource => _cancellationTokenSource;

        /// <inheritdoc/>
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

            _callbackDataSpliter = '_';
            var found = AddAttributedInComings();
            Console.WriteLine($"{found} Attributed inComing found!");
        }

        /// <summary>
        /// You can add configurations here
        /// </summary>
        public FlamingoCore Config(Action<FlamingoCore> action)
        {
            action(this);
            return this;
        }

        /// <inheritdoc/>
        public async Task<FlamingoCore> InitBot(string botToken)
        {
            _botClient = new TelegramBotClient(botToken, _httpClient, _baseUrl);
            _botInfo = await _botClient.GetMeAsync();

            return this;
        }

        /// <inheritdoc/>
        public FlamingoCore InitBot(string botToken, bool getMe)
        {
            _botClient = new TelegramBotClient(botToken, _httpClient, _baseUrl);

            if (getMe)
                _botInfo = _botClient.GetMeAsync().GetAwaiter().GetResult();

            return this;
        }

        /// <inheritdoc/>
        public async Task<FlamingoCore> SetBotInfo()
        {
            _botInfo = await _botClient.GetMeAsync();
            return this;
        }

        /// <inheritdoc/>
        public FlamingoCore SetCallbackDataSpliter(char callbackDataSpliter = '_')
        {
            _callbackDataSpliter = callbackDataSpliter;
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

        private bool CheckCallbackMethod<T>(MethodInfo methodInfo)
        {
            var param = methodInfo.GetParameters();

            if (param.Length > 1) return false;

            var type = param[0].ParameterType;

            if (type != typeof(ICondiment<T>)) return false;

            var returnType = methodInfo.ReturnType;

            if (returnType != typeof(Task<bool>)) return false;

            return true;
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

                    foreach (var target in allAttr)
                    {
                        if (target is InComingMessageAttribute inComingMessage)
                        {
                            if (!CheckCallbackMethod<Message>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<Message>(m, allAttr,
                                group: inComingMessage.Group,
                                isEdited: inComingMessage.IsEdited,
                                isChannelPost: inComingMessage.IsChannelPost);

                            foundCount++;
                        }
                        else if (target is InComingCallbackQueryAttribute inComingCallback)
                        {
                            if (!CheckCallbackMethod<CallbackQuery>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<CallbackQuery>(m, allAttr,
                                group: inComingCallback.Group);

                            foundCount++;
                        }
                        else if (target is InComingChatMemberAttribute inComingChatMember)
                        {

                            if (!CheckCallbackMethod<ChatMemberUpdated>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<ChatMemberUpdated>(m, allAttr,
                                group: inComingChatMember.Group,
                                isMine: inComingChatMember.IsMine);

                            foundCount++;
                        }
                        else if (target is InComingInlineQueryAttribute inComingInline)
                        {
                            if (!CheckCallbackMethod<InlineQuery>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<InlineQuery>(m, allAttr,
                                group: inComingInline.Group);

                            foundCount++;
                        }
                        else if (target is InComingChosenInlineResultAttribute inComing)
                        {
                            if (!CheckCallbackMethod<ChosenInlineResult>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<ChosenInlineResult>(m, allAttr,
                                group: inComing.Group);

                            foundCount++;
                        }
                        else if (target is InComingPollAttribute inComingPoll)
                        {
                            if (!CheckCallbackMethod<Poll>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<Poll>(m, allAttr,
                                group: inComingPoll.Group);

                            foundCount++;
                        }
                        else if (target is InComingPollAnswerAttribute inComingPollAnswer)
                        {

                            if (!CheckCallbackMethod<PollAnswer>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<PollAnswer>(m, allAttr,
                                group: inComingPollAnswer.Group);

                            foundCount++;
                        }
                        else if (target is InComingShippingQueryAttribute inComingShippingQuery)
                        {
                            if (!CheckCallbackMethod<ShippingQuery>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<ShippingQuery>(m, allAttr,
                                group: inComingShippingQuery.Group);

                            foundCount++;
                        }
                        else if (target is InComingPreCheckoutQueryAttribute inComingPreCheckoutQuery)
                        {
                            if (!CheckCallbackMethod<PreCheckoutQuery>(m))
                            {
                                throw new NotACallbackFunc(m.Name);
                            }

                            InAttributedProcess<PreCheckoutQuery>(m, allAttr,
                                group: inComingPreCheckoutQuery.Group);

                            foundCount++;
                        }
                    }
                }
            }
            return foundCount;
        }

        /// <inheritdoc/>
        public void AddInComing<T>(IFish<T> fish,
            int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false)
        {
            var updateType = Extensions.AsUpdateType<T>(isEdited, isChannelPost, isMine);

            _inComingManager.SafeAdd(new GroupedInComing<T>(fish, group));

            if (!_allowedUpdates.Contains(updateType))
            {
                _allowedUpdates.Add(updateType);
            }
        }

        /// <inheritdoc/>
        public void AddAdvancedInComing<T, U>(BaseCarrierFish<T, U> carrierFish,
            int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false) where U : IAdvFish<T>
        {
            AddInComing(carrierFish, group, isEdited, isChannelPost, isMine);
        }

        /// <inheritdoc/>
        public Carrier<U> AddAdvancedInComing<T, U>(
            IFilter<ICondiment<T>> filter = null,
            IFilterAsync<ICondiment<T>> filterAsync = null,
            int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false) where U : IAdvFish<T>
        {
            var carrier = new BaseCarrierFish<T, U>(
                filter,
                filterAsync);

            AddInComing(carrier, group, isEdited, isChannelPost, isMine);
            return carrier.Carrier;
        }

        /// <inheritdoc/>
        public GroupedInComing<T> AddInComingAwaitable<T>(IFisherAwaits<T> fish,
            int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false)
        {
            var updateType = Extensions.AsUpdateType<T>(
                isEdited, isChannelPost, isMine);

            var toAdd = new GroupedInComing<T>(fish as IFish<T>, group);
            _inComingAwaitableManager.SafeAdd(toAdd);

            if (!_allowedUpdates.Contains(updateType))
            {
                _allowedUpdates.Add(updateType);
            }

            return toAdd;
        }

        private List<UpdateType> _allowedUpdates;

        /// <inheritdoc/>
        public void AddAllowedUpdateType(UpdateType updateType)
        {
            _allowedUpdates.Add(updateType);
        }

        /// <inheritdoc/>
        public async Task<bool> ProcessAwaitables<T>(ICondiment<T> condiment)
        {
            var manager = _inComingAwaitableManager.GetInComingList<T>();

            if (manager.Any())
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

        private void CheckAdvInComings<T>(IFish<T> fish)
        {
            var t = fish.GetType();

            var interfaces = t.GetInterfaces();

            var carrier = t.GetInterfaces().FirstOrDefault(x =>
              x.IsGenericType &&
              x.GetGenericTypeDefinition() == typeof(ICarrier<>));

            if (carrier != null)
            {
                var m = carrier.GetMethod("SetupFish");

                m.Invoke(fish, null);
            }
        }

        private async Task ProcessInComings<T>(
            SortedSet<GroupedInComing<T>> _inComingMessages,
            ICondiment<T> condiment,
            Func<FlamingoCore, Exception, Task> errorHandler)
        {
            if (await ProcessAwaitables(condiment)) return;

            foreach (var inComing in _inComingMessages
                .Where(x => !(x.InComingFish is IFisherAwaits<T>)))
            {
                if (await inComing.InComingFish.ShouldEatAsync(condiment))
                {
                    CheckAdvInComings(inComing.InComingFish);

                    try
                    {
                        if (!await inComing.InComingFish.GetEaten(condiment))
                        {
                            break;
                        }
                    }
                    catch(Exception e)
                    {
                        if(errorHandler != null)
                        {
                            await errorHandler(this, e);
                        }
                    }
                    finally
                    {
                        if(inComing.InComingFish is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                    }
                }
            }
        }

        /// <inheritdoc/>
        public async Task ProcessInComings<T>(
            ICondiment<T> condiment,
            Func<FlamingoCore, Exception, Task> errorHandler = null)
        {
            if (await ProcessAwaitables(condiment)) return;

            await foreach (var handler in PassedHandlersAsync(condiment))
            {
                CheckAdvInComings(handler);

                try
                {
                    if (!await handler.GetEaten(condiment))
                    {
                        break;
                    }
                }
                catch (Exception e)
                {
                    if (errorHandler != null)
                    {
                        await errorHandler(this, e);
                    }
                }
                finally
                {
                    if (handler is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void TriggerCancell()
        {
            Console.WriteLine("Cancellation triggered!");
            _cancellationTokenSource.Cancel();
        }

        private bool CheckLimiters<T>(T update)
        {
            if (!RateLimitManager.CheckAutoBuilders(update))
            {
                if (RateLimitManager.IsLimited(update))
                {
                    return true;
                }
            }

            return false;
        }

        /// <inheritdoc/>
        public async Task UpdateRedirector(
            Update update,
            Func<FlamingoCore, Exception, Task> errorHandler = null)
        {
            switch (update)
            {
                case { Message: { } }:
                    {
                        if (CheckLimiters(update.Message))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingMessages,
                            new MessageCondiment(update.Message, this),
                            errorHandler);

                        break;
                    }

                case { ChannelPost: { } }:
                    {
                        if (CheckLimiters(update.ChannelPost))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingMessages,
                            new MessageCondiment(update.ChannelPost, this, true),
                            errorHandler);

                        break;
                    }

                case { EditedMessage: { } }:
                    {
                        if (CheckLimiters(update.EditedMessage))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingMessages,
                            new MessageCondiment(update.EditedMessage, this, isEdited: true),
                            errorHandler);

                        break;
                    }

                case { EditedChannelPost: { } }:
                    {
                        if (CheckLimiters(update.EditedChannelPost))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingMessages,
                            new MessageCondiment(update.EditedChannelPost, this, true, true),
                            errorHandler);

                        break;
                    }

                case { CallbackQuery: { } }:
                    {
                        if (CheckLimiters(update.CallbackQuery))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingCallbackQueries,
                            new CallbackQueryCondiment(
                                update.CallbackQuery, this, _callbackDataSpliter),
                            errorHandler);

                        break;
                    }

                case { InlineQuery: { } }:
                    {
                        if (CheckLimiters(update.InlineQuery))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingInlineQueries,
                            new InlineQueryCondiment(
                                update.InlineQuery, this),
                            errorHandler);

                        break;
                    }

                case { ChosenInlineResult: { } }:
                    {
                        if (CheckLimiters(update.ChosenInlineResult))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingInlineResultChosen,
                            new ChosenInlineResultCondiment(
                                update.ChosenInlineResult, this),
                            errorHandler);

                        break;
                    }

                case { ChatMember: { } }:
                    {
                        if (CheckLimiters(update.ChatMember))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingChatMembers,
                            new ChatMemberCondiment(
                                update.ChatMember, this, false),
                            errorHandler);

                        break;
                    }

                case { MyChatMember: { } }:
                    {
                        if (CheckLimiters(update.MyChatMember))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingChatMembers,
                            new ChatMemberCondiment(
                                update.MyChatMember, this, true),
                            errorHandler);

                        break;
                    }

                case { Poll: { } }:
                    {
                        if (CheckLimiters(update.Poll))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingPoll,
                            new PollCondiment(
                                update.Poll, this),
                            errorHandler);

                        break;
                    }

                case { PollAnswer: { } }:
                    {
                        if (CheckLimiters(update.PollAnswer))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingPollAnswer,
                            new PollAnswerCondiment(
                                update.PollAnswer, this),
                            errorHandler);

                        break;
                    }

                case { PreCheckoutQuery: { } }:
                    {
                        if (CheckLimiters(update.PreCheckoutQuery))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingPreCheckoutQuery,
                            new PreCheckoutQueryCondiment(
                                update.PreCheckoutQuery, this),
                            errorHandler);

                        break;
                    }

                case { ShippingQuery: { } }:
                    {
                        if (CheckLimiters(update.ShippingQuery))
                            return;

                        await ProcessInComings(
                            _inComingManager.InComingShippingQuery,
                            new ShippingQueryCondiment(
                                update.ShippingQuery, this),
                            errorHandler);

                        break;
                    }
            }
        }

        /// <inheritdoc/>
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
                                await UpdateRedirector(update, errorHandler);
                            }
                            catch (Exception e)
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


        /// <inheritdoc/>
        public void StartReceiving(
            Func<ITelegramBotClient, Exception, CancellationToken, Task> onError)
        {
            BotClient.StartReceiving(
                new DefaultUpdateHandler(
                    HandleUpdateLongPollingAsync,
                    onError),
                _cancellationTokenSource.Token);
        }


        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public async Task<AwaitableResult<T>> WaitForInComing<T>(IFisherAwaits<T> inComingfish)
        {
            var added = AddInComingAwaitable(inComingfish);
            return await inComingfish.Wait(this, added);
        }

        /// <inheritdoc/>
        public void RemoveAwaitable<T>(GroupedInComing<T> groupedIn)
        {
            var manger = _inComingAwaitableManager.GetInComingList<T>();

            _inComingAwaitableManager.SafeRemove(manger, groupedIn);
        }

        /// <inheritdoc/>
        public RateLimitManager RateLimitManager
        {
            get
            {
                if (_rateLimitManager == null)
                    _rateLimitManager = new RateLimitManager();

                return _rateLimitManager;
            }
        }

        /// <summary>
        /// Add an auto limit builder to rate manager
        /// </summary>
        public bool AddAutoLimit<T, TResult>(AutoLimitBuilder<T, TResult> autoLimitBuilder)
        {
            return RateLimitManager.AddAutoLimit(autoLimitBuilder);
        }

        /// <summary>
        /// Add a timed limit
        /// </summary>
        public bool AddTimedLimit<T, TResult>(
            TResult limited,
            Expression<Func<T, TResult>> selsctor,
            TimeSpan duration,
            bool waitForLimit = false,
            Func<TResult, TResult, bool> comparer = null)
        {
            return RateLimitManager.AddLimit(
                new BaseLimiter<T, TResult>(
                    limited,
                    selsctor,
                    new TimeLimit(duration, waitForLimit),
                    comparer));
        }

        /// <summary>
        /// Add an auto message sender limit to rate manager advanced
        /// </summary>
        /// <param name="timeSpan">Time span to wait till next message</param>
        /// /// <param name="waitForLimit">Block and wait for limit</param>
        public bool AddAutoMessageSenderLimit(TimeSpan timeSpan, bool waitForLimit = false)
        {
            return AddAutoLimit(new AutoLimitBuilder<Message, User>(
                    new MessageSenderLimiter(0, timeSpan, waitForLimit)));
        }

        /// <summary>
        /// Add an auto callback query sender limit to rate manager
        /// </summary>
        /// <param name="timeSpan">Time span to wait till next message</param>
        /// /// <param name="waitForLimit">Block and wait for limit</param>
        public bool AddAutoCallbackQuerySenderLimit(
            TimeSpan timeSpan, bool waitForLimit = false)
        {
            return AddAutoLimit(new AutoLimitBuilder<CallbackQuery, User>(
                    new CallbackQuerySenderLimiter(timeSpan, waitForLimit)));
        }

        private void AddFishObj(object obj,
            Type type,
            int group = 0,
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false)
        {
            var updateType = Extensions.AsUpdateType(
                type, isEdited, isChannelPost, isMine);

            switch (updateType)
            {
                case UpdateType.Message:
                case UpdateType.EditedMessage:
                case UpdateType.ChannelPost:
                case UpdateType.EditedChannelPost:
                    AddInComing(obj as IFish<Message>, group, isEdited, isChannelPost, isMine);
                    break;
                case UpdateType.ChatMember:
                case UpdateType.MyChatMember:
                    AddInComing(obj as IFish<ChatMemberUpdated>, group, isEdited, isChannelPost, isMine);
                    break;
                case UpdateType.InlineQuery:
                    AddInComing(obj as IFish<InlineQuery>, group, isEdited, isChannelPost, isMine);
                    break;
                case UpdateType.ChosenInlineResult:
                    AddInComing(obj as IFish<ChosenInlineResult>, group, isEdited, isChannelPost, isMine);
                    break;
                case UpdateType.CallbackQuery:
                    AddInComing(obj as IFish<CallbackQuery>, group, isEdited, isChannelPost, isMine);
                    break;
                case UpdateType.ShippingQuery:
                    AddInComing(obj as IFish<ShippingQuery>, group, isEdited, isChannelPost, isMine);
                    break;
                case UpdateType.PreCheckoutQuery:
                    AddInComing(obj as IFish<PreCheckoutQuery>, group, isEdited, isChannelPost, isMine);
                    break;
                case UpdateType.Poll:
                    AddInComing(obj as IFish<Poll>, group, isEdited, isChannelPost, isMine);
                    break;
                case UpdateType.PollAnswer:
                    AddInComing(obj as IFish<PollAnswer>, group, isEdited, isChannelPost, isMine);
                    break;
            }
        }

        /// <inheritdoc/>
        public FlamingoCore AutoAddInComings(bool notify = false)
        {
            var entry = Assembly.GetEntryAssembly();

            var name = entry.GetName().Name;

            var inComingsNamespace = name + ".InComings";

            var typesNamespaces = new Dictionary<Type, string>
            {
                {typeof(Message), inComingsNamespace + ".Messages" },
                {typeof(CallbackQuery), inComingsNamespace + ".CallbackQueries" },
                {typeof(InlineQuery), inComingsNamespace + ".InlineQueries" },
                {typeof(ChosenInlineResult), inComingsNamespace + ".ChosenInlineResults" },
                {typeof(ChatMemberUpdated), inComingsNamespace + ".ChatMemberUpdates" },
                {typeof(Poll), inComingsNamespace + ".Polls" },
                {typeof(PollAnswer), inComingsNamespace + ".PollAnswers" },
                {typeof(ShippingQuery), inComingsNamespace + ".ShippingQueries" },
                {typeof(PreCheckoutQuery), inComingsNamespace + ".PreCheckoutQueries" }
            };

            var types = entry.GetTypes();

            var c1 = typeof(BaseCarrierFish<,>);

            foreach (var ns in typesNamespaces)
            {
                var handlers = types.Where(
                    x => x.Namespace == ns.Value && x.IsClass && x.IsPublic);

                if (!handlers.Any())
                    continue;

                foreach (var handler in handlers)
                {
                    var attrs = handler.GetCustomAttributes();

                    bool edited = false;
                    bool channelPost = false;
                    bool mine = false;
                    int group = 0;

                    foreach (var attr in attrs)
                    {
                        if (attr is HandlingGroupAttribute groupAttribute)
                        {
                            group = groupAttribute.Group;
                            continue;
                        }

                        if (ns.Key == typeof(Message))
                        {
                            if (attr is IsEditedMessageAttribute)
                            {
                                edited = true;
                                continue;
                            }

                            if (attr is IsChannelPostAttribute)
                            {
                                channelPost = true;
                                continue;
                            }
                        }

                        if (attr is IsMyChatMemberAttribute)
                        {
                            mine = true;
                            continue;
                        }
                    }

                    var interfaces = handler.GetInterfaces();
                    var advFish = interfaces.FirstOrDefault(x =>
                      x.IsGenericType &&
                      x.GetGenericTypeDefinition() == typeof(IAdvFish<>));

                    if(advFish != null)
                    {
                        var gArgs = advFish.GetGenericArguments();
                        if(gArgs[0] == ns.Key)
                        {
                            var c2 = c1.MakeGenericType(ns.Key, handler);

                            var carrier = Activator.CreateInstance(c2, 
                                args: new object[] { null, null });

                            AddFishObj(carrier, ns.Key, group, edited, channelPost, mine);

                            if(notify)
                                Console.WriteLine($"Added advanced incoming: '{handler.Name}' from '{ns.Value}'\n" +
                                $"\tGroup: {group}, IsEdited: {edited}, IsChannelPost: {channelPost}, IsMine: {mine}");
                            continue;
                        }
                    }

                    var fish = interfaces.FirstOrDefault(x =>
                      x.IsGenericType &&
                      x.GetGenericTypeDefinition() == typeof(IFish<>));

                    if (fish != null)
                    {
                        if (handler.GetConstructor(Array.Empty<Type>()) is ConstructorInfo constructor)
                        {
                            var carrier = constructor.Invoke(Array.Empty<object>());

                            AddFishObj(carrier, ns.Key, group, edited, channelPost, mine);

                            if (notify)
                                Console.WriteLine($"Added incoming: '{handler.Name}' from '{ns.Value}'\n" +
                                $"\tGroup: {group}, IsEdited: {edited}, IsChannelPost: {channelPost}, IsMine: {mine}");
                            continue;
                        }
                    }
                }
            }

            return this;
        }

        #endregion
    }
}
