using Flamingo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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
            

        private string GetAskText(int index)
        {
            if(_hasConstructor)
            {
                var param = _parameterInfos.ElementAt(index);
                return GetAttribute<FlamingoFormDataAttribute>(param.GetCustomAttributes())
                    .AskText ?? $"Please send me {param.Name}";
            }
            else
            {
                var prop = _propertyInfos.ElementAt(index);
                return GetAttribute<FlamingoFormPropertyAttribute>(prop.GetCustomAttributes())
                    .AskText ?? $"Please send me {prop.Name}";
            }
        }

        private string GetTypeFailureText(int index)
        {
            if (_hasConstructor)
            {
                var param = _parameterInfos.ElementAt(index);
                return GetAttribute<FlamingoFormDataAttribute>(param.GetCustomAttributes())
                    .InvalidTypeText ?? $"Invalid input for {param.Name}";
            }
            else
            {
                var prop = _propertyInfos.ElementAt(index);
                return GetAttribute<FlamingoFormPropertyAttribute>(prop.GetCustomAttributes())
                    .InvalidTypeText ?? $"Invalid input for {prop.Name}";
            }
        }

        private readonly List<object> _formObjs;

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

                    // return (T)Activator.CreateInstance(typeof(T), _formObjs);
                }

                var _instance = (T)Activator.CreateInstance(typeof(T));

                for (int i = 0; i < _propertyInfos.Count(); i++)
                {
                    _propertyInfos.ElementAt(i).SetValue(_instance, _formObjs[i]);
                }

                return _instance;
            }
        }

        /// <summary>
        /// Start asking for form data
        /// </summary>
        /// <param name="userid">User id to ask to</param>
        /// <param name="triesOnTypeFailure">How much time we should try if failed</param>
        /// <param name="cancellationTokenSource">To cancel the job</param>
        /// <returns></returns>
        public async Task Ask(long userid,
            int triesOnTypeFailure = 0,
            CancellationTokenSource cancellationTokenSource = null)
        {
            triesOnTypeFailure++;

            foreach (var types in _dataTypes.Select((x, i) => (i, x)))
            {
                var askText = GetAskText(types.i);

                await _flamingoCore.BotClient.SendTextMessageAsync(
                    userid, askText);

                int tries = 0;
                bool succeeded = false;
                while (tries < triesOnTypeFailure)
                {
                    var respond = await _flamingoCore.WaitForInComing(
                        new AwaitInComingText(userid, cancellationToken: cancellationTokenSource));

                    if (respond.Succeeded)
                    {
                        if (respond.TextRespond.TryConvert(
                            types.x, out object obj))
                        {
                            _formObjs.Add(obj);
                            succeeded = true;
                            break;
                        }
                        else
                        {
                            await _flamingoCore.BotClient.SendTextMessageAsync(
                                userid, GetTypeFailureText(types.i));
                        }
                    }
                    else if(respond.TimedOut)
                    {
                        // Do something on time out
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
