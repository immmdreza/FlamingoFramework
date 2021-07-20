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

        private readonly bool _hasConstructor = false;

        private bool _asked = false;

        /// <summary>
        /// This class helps you fill a form class properties
        /// </summary>
        public FillFormRequest(IFlamingoCore flamingoCore)
        {
            _flamingoCore = flamingoCore;

            var constructor = typeof(T).GetConstructors()
                .FirstOrDefault(c => c.CustomAttributes.Any(
                    a => a.AttributeType == typeof(FlamingoFormConstructorAttribute)));

            if(constructor != null)
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

        private U GetAttribute<U>(IEnumerable<Attribute> attributes) where U: Attribute
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
                return GetAttribute<FlamingoFormDataAttribute>(param.GetCustomAttributes());

            }
            else
            {
                var prop = _propertyInfos.ElementAt(index);
                return GetAttribute<FlamingoFormPropertyAttribute>(prop.GetCustomAttributes());
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

        private string ValueChecks(Attribute attribute, Type type, object obj, string name = null)
        {
            var interfaces = attribute.GetType().GetInterfaces();
            foreach (var i in interfaces)
            {
                if (!i.IsGenericType)
                    continue;

                var g = i.GetGenericTypeDefinition();
                if (g == typeof(IFamingoFormDataCheck<>))
                {
                    var a = i.GetGenericArguments();
                    if (a[0] == type)
                    {
                        var m = i.GetMethod("Check");
                        var result = (bool)m.Invoke(attribute, new[] { obj });

                        if (!result)
                        {
                            var text = (string)i.GetProperty("FailureMessage").GetValue(attribute);
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

        private string PropertyName(int index)
        {
            if (_hasConstructor)
            {
                return _parameterInfos.ElementAt(index).Name;

            }
            else
            {
                return _propertyInfos.ElementAt(index).Name;
            }
        }


        /// <summary>
        /// Start asking for form data
        /// </summary>
        /// <param name="userid">User id to ask to</param>
        /// <param name="triesOnFailure">How much time we should try if failed</param>
        /// <param name="cancellationTokenSource">To cancel the job</param>
        /// <param name="timeOut">Time out in seconds</param>
        /// <param name="onTimeOutMessage">A message sent when wait is timed out</param>
        /// <param name="cancellInputPattern">Regex pattern to cancel asking</param>
        /// <returns></returns>
        public async Task Ask(long userid,
            int triesOnFailure = 0,
            int timeOut = 30,
            string onTimeOutMessage = null,
            string cancellInputPattern = null,
            CancellationTokenSource cancellationTokenSource = null)
        {
            triesOnFailure++;
            var cleanKeys = false;
            foreach (var types in _dataTypes.Select((x, i) => (i, x)))
            {
                var formDataInfo = GetFormDataInfo(types.i);

                IReplyMarkup markup = null;
                if (types.x.IsEnum)
                {
                    markup = new ReplyButton(types.x.GetEnumNames()).Markup();
                    cleanKeys = true;
                }
                else if(cleanKeys)
                {
                    markup = new ReplyKeyboardRemove();
                    cleanKeys = false;
                }

                await _flamingoCore.BotClient.SendTextMessageAsync(
                    userid,
                    formDataInfo.AskText?? $"Please send value for {formDataInfo.Name?? PropertyName(types.i)}",
                    replyMarkup: markup);

                int tries = 0;
                bool succeeded = false;
                while (tries < triesOnFailure)
                {
                    var respond = await _flamingoCore.WaitForInComing(
                        new AwaitInComingText(userid, timeOut, cancellationTokenSource));

                    if (respond.Succeeded)
                    {
                        if(cancellInputPattern != null)
                        {
                            if (Regex.IsMatch(respond.TextRespond, cancellInputPattern))
                            {
                                if(formDataInfo.Required)
                                {
                                    _canceled = true;
                                    return;
                                }

                                _formObjs.Add(null);
                                succeeded = true;
                                break;
                            }
                        }


                        if (respond.TextRespond.TryConvert(
                            types.x, out object obj))
                        {
                            var valueCheck = CheckFunction(
                                types.i, types.x, obj, formDataInfo.Name ?? PropertyName(types.i));

                            if(!string.IsNullOrEmpty(valueCheck))
                            {
                                await _flamingoCore.BotClient.SendTextMessageAsync(
                                    userid, valueCheck);
                                tries++;
                                continue;
                            }

                            _formObjs.Add(obj);
                            succeeded = true;
                            break;
                        }
                        else
                        {
                            await _flamingoCore.BotClient.SendTextMessageAsync(
                                userid, formDataInfo.InvalidTypeText?? $"Invalid input type for {formDataInfo.Name ?? PropertyName(types.i)}");
                        }
                    }
                    else if(respond.TimedOut)
                    {
                        await _flamingoCore.BotClient.SendTextMessageAsync(
                            userid, onTimeOutMessage?? "You're time out!");
                    }
                    else
                    {
                        throw new TaskCanceledException();
                    }

                    tries++;
                }

                if (!succeeded)
                    return;
            }

            _asked = true;
        }
    }
}
