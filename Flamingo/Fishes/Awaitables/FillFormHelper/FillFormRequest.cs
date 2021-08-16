using Flamingo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Fishes.Awaitables.FillFormHelper
{
    /// <summary>
    /// This class helps you fill a form class properties
    /// </summary>
    /// <typeparam name="T">Form class to fill</typeparam>
    public class FillFormRequest<T>
    {
        private readonly IEnumerable<PropertyInfo> _propertyInfos;

        private readonly IEnumerable<ParameterInfo> _parameterInfos;

        private readonly IEnumerable<Type> _dataTypes;

        private readonly IFlamingoCore _flamingoCore;

        /// <summary>
        /// Flamingo instance
        /// </summary>
        public IFlamingoCore Flamingo => _flamingoCore;

        private Func<FillFormRequest<T>, FlamingoFormStatus, IFlamingoFormData, bool> _statusChanged;

        private readonly bool _hasConstructor = false;

        private bool _asked = false;

        private readonly List<object> _formObjs;

        /// <summary>
        /// If asking was successful
        /// </summary>
        public bool Succeeded => _asked;

        private bool _canceled = false;

        /// <summary>
        /// If user sends cancel text
        /// </summary>
        public bool Canceled => _canceled;

        private bool _timedOut = false;

        /// <summary>
        /// Indicates if the ask process terminated due to timeout
        /// in the latest question.
        /// </summary>
        public bool TimedOut => _timedOut;

        private bool _terminated = false;

        /// <summary>
        /// Indicates if the ask process terminated manually
        /// </summary>
        public bool Terminated => _terminated;

        private long _userId = 0;

        /// <summary>
        /// User who we are asking from.
        /// </summary>
        public long UserId => _userId;

        private CancellationTokenSource _cancellationToken;

        /// <summary>
        /// This class helps you fill a form class properties
        /// </summary>
        public FillFormRequest(
            IFlamingoCore flamingoCore,
            Func<FillFormRequest<T>, FlamingoFormStatus, IFlamingoFormData, bool> statusChanged = null,
            CancellationTokenSource cancellationTokenSource = null)
        {
            _flamingoCore = flamingoCore;
            _statusChanged = statusChanged ?? ((_, __, ___) => true);
            _cancellationToken = cancellationTokenSource ?? new CancellationTokenSource();

            var constructor = typeof(T).GetConstructors()
                .FirstOrDefault(c => c.CustomAttributes.Any(
                    a => a.AttributeType == typeof(FlamingoFormConstructorAttribute)));

            if (constructor != null)
            {
                _parameterInfos = constructor.GetParameters()
                    .Where(x => x.CustomAttributes.Any(
                        a => a.AttributeType == typeof(FlamingoFormDataAttribute)));

                _dataTypes = _parameterInfos.Select(x => x.ParameterType);
                _hasConstructor = true;
            }
            else
            {
                _propertyInfos = typeof(T).GetProperties()
                    .Where(x => x.CanWrite)
                    .Where(x => x.CustomAttributes.Any(
                        a => a.AttributeType == typeof(FlamingoFormPropertyAttribute)));

                _dataTypes = _propertyInfos.Select(x => x.PropertyType);
            }

            _formObjs = new List<object>();
        }

        /// <summary>
        /// Created instance of form
        /// </summary>
        public T Instance
        {
            get
            {
                if (!_asked)
                    return default;

                if (_hasConstructor)
                {
                    var cnstr = typeof(T).GetConstructor(_dataTypes.ToArray());

                    return (T)cnstr.Invoke(_formObjs.ToArray());
                }

                var _instance = (T)Activator.CreateInstance(typeof(T));

                for (int i = 0; i < _propertyInfos.Count(); i++)
                {
                    _propertyInfos.ElementAt(i).SetValue(_instance, _formObjs[i]);
                }

                return _instance;
            }
        }

        private U GetAttribute<U>(IEnumerable<Attribute> attributes) where U : Attribute
        {
            var attr = (U)attributes
                .FirstOrDefault(x => x is U);

            return attr;
        }

        private IFlamingoFormData GetFormDataInfo(int index)
        {
            if (_hasConstructor)
            {
                var param = _parameterInfos.ElementAt(index);
                var data = GetAttribute<FlamingoFormDataAttribute>(
                    param.GetCustomAttributes());

                data.Name ??= param.Name;
                data.AskText ??= $"Please send value for {data.Name}";
                data.InvalidTypeText ??= $"Invalid input type for {data.Name}";

                return data;
            }
            else
            {
                var prop = _propertyInfos.ElementAt(index);
                var data = GetAttribute<FlamingoFormPropertyAttribute>(
                    prop.GetCustomAttributes());

                data.Name ??= prop.Name;
                data.AskText ??= $"Please send value for {data.Name}";
                data.InvalidTypeText ??= $"Invalid input type for {data.Name}";

                return data;
            }
        }

        private string CheckFunction(int index, Type type, object obj, string name = null)
        {
            if (_hasConstructor)
            {
                foreach (var item in _parameterInfos.ElementAt(index)
                    .GetCustomAttributes())
                {
                    var check = ValueChecks(item, type, obj, name);
                    if (!string.IsNullOrEmpty(check))
                    {
                        return check;
                    }
                }

                return null;
            }
            else
            {
                foreach (var item in _propertyInfos.ElementAt(index)
                    .GetCustomAttributes())
                {
                    var check = ValueChecks(item, type, obj, name);
                    if (!string.IsNullOrEmpty(check))
                    {
                        return check;
                    }
                }

                return null;
            }
        }

        private string ValueChecks(
            Attribute attribute, Type type, object obj, string name = null)
        {
            var interfaces = attribute.GetType().GetInterfaces();
            foreach (var i in interfaces)
            {
                if (!i.IsGenericType)
                    continue;

                var g = i.GetGenericTypeDefinition();
                if (g == typeof(IFlamingoFormDataCheck<>))
                {
                    var a = i.GetGenericArguments();
                    if (a[0] == type)
                    {
                        var m = i.GetMethod("Check");
                        var result = (bool)m.Invoke(attribute, new[] { obj });

                        if (!result)
                        {
                            var text = (string)i.GetProperty("FailureMessage")
                                .GetValue(attribute);

                            return text ?? $"Value check failed " + name ?? "";
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Start asking for form data
        /// </summary>
        /// <param name="userid">User id to ask to</param>
        /// <param name="triesOnFailure">How much time we should try if failed</param>
        /// <param name="timeOut">Time out in seconds</param>
        /// <param name="onTimeOutMessage">A message sent when wait is timed out</param>
        /// <param name="cancellInputPattern">Regex pattern to cancel asking</param>
        /// <returns></returns>
        public async Task Ask(long userid,
            int triesOnFailure = 0,
            int timeOut = 30,
            string onTimeOutMessage = null,
            Regex cancellInputPattern = null)
        {
            _userId = userid;
            triesOnFailure++;
            var cleanKeys = false;
            foreach (var types in _dataTypes.Select((x, i) => (i, x)))
            {
                if (_cancellationToken.IsCancellationRequested)
                {
                    _terminated = true;
                    return;
                }

                var formDataInfo = GetFormDataInfo(types.i);

                IReplyMarkup markup = null;
                if (types.x.IsEnum)
                {
                    markup = new ReplyButton(types.x.GetEnumNames()).Markup();
                    cleanKeys = true;
                }
                else if (cleanKeys)
                {
                    markup = new ReplyKeyboardRemove();
                    cleanKeys = false;
                }

                await _flamingoCore.BotClient.SendTextMessageAsync(
                    userid,
                    formDataInfo.AskText,
                    replyMarkup: markup);

                int tries = 0;
                bool succeeded = false;
                while (tries < triesOnFailure)
                {
                    if (_cancellationToken.IsCancellationRequested)
                    {
                        _terminated = true;
                        return;
                    }

                    _timedOut = false;
                    var respond = await _flamingoCore.WaitForInComing(
                        new AwaitInComingText(userid, timeOut, _cancellationToken));

                    if (respond.Succeeded)
                    {
                        if (cancellInputPattern != null)
                        {
                            if (cancellInputPattern.IsMatch(respond.TextRespond))
                            {
                                if (formDataInfo.Required)
                                {
                                    _canceled = true;
                                    _statusChanged(
                                        this,
                                        FlamingoFormStatus.Cancelled,
                                        formDataInfo);
                                    return;
                                }

                                _statusChanged(
                                    this,
                                    FlamingoFormStatus.RecoverableCancelled,
                                    formDataInfo);
                                _formObjs.Add(null);
                                succeeded = true;
                                break;
                            }
                        }


                        if (respond.TextRespond.TryConvert(
                            types.x, out object obj))
                        {
                            var valueCheck = CheckFunction(
                                types.i, types.x, obj, formDataInfo.Name);

                            if (!string.IsNullOrEmpty(valueCheck))
                            {
                                if (_statusChanged(
                                    this,
                                    FlamingoFormStatus.ValidatingFailed,
                                    formDataInfo))
                                {
                                    await _flamingoCore.BotClient.SendTextMessageAsync(
                                        userid, valueCheck);
                                }

                                tries++;
                                continue;
                            }

                            _statusChanged(
                                this,
                                FlamingoFormStatus.Answered,
                                formDataInfo);
                            _formObjs.Add(obj);
                            succeeded = true;
                            break;
                        }
                        else
                        {
                            if (_statusChanged(
                                this,
                                FlamingoFormStatus.ValidatingFailed,
                                formDataInfo))
                            {
                                await _flamingoCore.BotClient.SendTextMessageAsync(
                                    userid, formDataInfo.InvalidTypeText);
                            }
                        }
                    }
                    else if (respond.TimedOut)
                    {
                        if (_statusChanged(
                            this,
                            FlamingoFormStatus.TimedOut,
                            formDataInfo))
                        {
                            await _flamingoCore.BotClient.SendTextMessageAsync(
                                userid, onTimeOutMessage ?? "You're time out!");
                        }
                        _timedOut = true;
                    }
                    else
                    {
                        _terminated = true;
                        return;
                    }

                    tries++;
                }

                if (!succeeded)
                {
                    if (formDataInfo.Required)
                        return;
                    else
                    {
                        _statusChanged(
                            this,
                            _timedOut ?
                                FlamingoFormStatus.RecoverableTimedOut :
                                FlamingoFormStatus.RecoverableFailure,
                            formDataInfo);
                        _formObjs.Add(null);
                        succeeded = true;
                    }
                }
            }

            _asked = true;
        }

        /// <summary>
        /// Sends cancell request to filler
        /// </summary>
        public void Terminate()
        {
            _cancellationToken.Cancel();
        }
    }
}
