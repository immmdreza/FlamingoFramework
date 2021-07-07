using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.RateLimiter
{
    /// <summary>
    /// Manage rate limiter here
    /// </summary>
    public class RateLimitManager
    {
        private readonly ConcurrentDictionary<UpdateType, List<object>> _objectes;

        private readonly ConcurrentDictionary<UpdateType, List<object>> _autoBuilders;


        private bool _isSet = false;

        /// <summary>
        /// Manage rate limiter here
        /// </summary>
        public RateLimitManager()
        {
            _objectes = new ConcurrentDictionary<UpdateType, List<object>>();
            _autoBuilders = new ConcurrentDictionary<UpdateType, List<object>>();
        }

        /// <summary>
        /// Automatically add limitation if its not there based on auto limit builders
        /// </summary>
        /// <typeparam name="T">Type of incoming update</typeparam>
        /// <param name="obj">Value of incoming update</param>
        public bool CheckAutoBuilders<T>(T obj)
        {
            if (!_isSet) return true;

            foreach (var item in IterAutoLimitObjects<T>())
            {
                item.Item2.GetMethod("SetResult").Invoke(
                    item.Item1, new object[] { obj });

                var result = item.Item2.GetProperty("Result").GetValue(item.Item1);

                var type = item.Item2.GetGenericArguments()[1];

                if(!HasLimit<T>(result, type))
                {
                    var limited = item.Item2.GetProperty("GetNew").GetValue(item.Item1);

                    AddLimit<T>(limited);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a selector has limit
        /// </summary>
        public bool HasLimit<T, TResult>(TResult result)
        {
            foreach (var item in IterILimitedObjects<T, TResult>())
            {
                if ((bool)item.Item2.GetMethod("IsMyTResult")
                    .Invoke(item.Item1, new object[] { result }))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if a selector has limit
        /// </summary>
        public bool HasLimit<T>(object result, Type type)
        {
            foreach (var item in IterILimitedObjects<T>(type))
            {
                var m = item.Item2.GetMethod("IsMyTResult");

                var b = m.Invoke(item.Item1, new object[]{ result });

                if ((bool)b)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if current limit is exists
        /// </summary>
        public bool Exists<T, TResult>(ILimited<T, TResult> limited)
        {
            return Exists<T, TResult>(limited.Limited);
        }

        /// <summary>
        /// Check if current limit is exists
        /// </summary>
        public bool Exists<T, TResult>(TResult limited)
        {
            return GetLimiteds<T, TResult>().Any(x => x.IsMyTResult(limited));
        }

        private IEnumerable<(object, Type)> IterILimitedObjects<T>()
        {
            var updateType = Extensions.AsUpdateType<T>();

            if (!_objectes.ContainsKey(updateType))
                yield break;

            var limits = _objectes[updateType];

            foreach (var lim in limits)
            {
                var t = lim.GetType();

                var limited = t.GetInterfaces().FirstOrDefault(x =>
                  x.IsGenericType &&
                  x.GetGenericTypeDefinition() == typeof(ILimited<,>));

                if (limited != null)
                {
                    var args = limited.GetGenericArguments();

                    if (args.Length == 2)
                    {
                        if (args[0] == typeof(T))
                        {
                            yield return (lim, limited);
                        }
                    }
                }
            }
        }

        private IEnumerable<(object, Type)> IterILimitedObjects<T, TResult>()
        {
            var updateType = Extensions.AsUpdateType<T>();

            if (!_objectes.ContainsKey(updateType))
                yield break;

            var limits = _objectes[updateType];

            foreach (var lim in limits)
            {
                var t = lim.GetType();

                var limited = t.GetInterfaces().FirstOrDefault(x =>
                  x.IsGenericType &&
                  x.GetGenericTypeDefinition() == typeof(ILimited<,>));

                if (limited != null)
                {
                    var args = limited.GetGenericArguments();

                    if (args.Length == 2)
                    {
                        if (args[0] == typeof(T) && args[1] == typeof(TResult))
                        {
                            yield return (lim, limited);
                        }
                    }
                }
            }
        }

        private IEnumerable<(object, Type)> IterILimitedObjects<T>(Type type)
        {
            var updateType = Extensions.AsUpdateType<T>();

            if (!_objectes.ContainsKey(updateType))
                yield break;

            var limits = _objectes[updateType];

            foreach (var lim in limits)
            {
                var t = lim.GetType();

                var limited = t.GetInterfaces().FirstOrDefault(x =>
                  x.IsGenericType &&
                  x.GetGenericTypeDefinition() == typeof(ILimited<,>));

                if (limited != null)
                {
                    var args = limited.GetGenericArguments();

                    if (args.Length == 2)
                    {
                        if (args[0] == typeof(T) && args[1] == type)
                        {
                            yield return (lim, limited);
                        }
                    }
                }
            }
        }

        private IEnumerable<(object, Type)> IterAutoLimitObjects<T>()
        {
            var updateType = Extensions.AsUpdateType<T>();

            if (!_autoBuilders.ContainsKey(updateType))
                yield break;

            var limits = _autoBuilders[updateType];

            foreach (var lim in limits)
            {
                var t = lim.GetType();

                if (t.IsGenericType &&
                    t.GetGenericTypeDefinition() == typeof(AutoLimitBuilder<,>))
                {
                    var args = t.GetGenericArguments();

                    if (args.Length == 2)
                    {
                        if (args[0] == typeof(T))
                        {
                            yield return (lim, t);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Check if any limits applied!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="incoming"></param>
        /// <returns></returns>
        public bool IsLimited<T>(T incoming)
        {
            if (!_isSet) return false;

            foreach (var lim in IterILimitedObjects<T>())
            {
                if((bool)lim.Item2.GetMethod("LimitedYet")
                    .Invoke(lim.Item1, new object[] { incoming }))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get limits based on message senders
        /// </summary>
        public IEnumerable<ILimited<Message, User>> MessageSenderLimits()
        {
            return GetLimiteds<Message, User>();
        }

        /// <summary>
        /// Get limits for an update type
        /// </summary>
        public IEnumerable<ILimited<T, TResult>> GetLimiteds<T, TResult>()
        {
            var updateType = Extensions.AsUpdateType<T>();

            var limiteds = _objectes[updateType];

            foreach (var limited in limiteds)
            {
                if(limited is ILimited<T, TResult> result)
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// Get limits for an update type
        /// </summary>
        public IEnumerable<ILimited<T>> GetLimiteds<T>()
        {
            var updateType = Extensions.AsUpdateType<T>();

            var limiteds = _objectes[updateType];

            foreach (var limited in limiteds)
            {
                if (limited is ILimited<T> result)
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// Add a limit to list
        /// </summary>
        public bool AddLimit<T, TResult>(ILimited<T, TResult> limited)
        {
            var updateType = Extensions.AsUpdateType<T>();

            _isSet = true;
            if (_objectes.ContainsKey(updateType))
            {
                _objectes[updateType].Add(limited);
                return true;
            }
            else
            {
                return _objectes.TryAdd(updateType, new List<object> { limited });
            }
        }

        /// <summary>
        /// Add an auto limit builder
        /// </summary>
        public bool AddAutoLimit<T, TResult>(AutoLimitBuilder<T, TResult> autoLimitBuilder)
        {
            var updateType = Extensions.AsUpdateType<T>();

            _isSet = true;
            if (_autoBuilders.ContainsKey(updateType))
            {
                _autoBuilders[updateType].Add(autoLimitBuilder);
                return true;
            }
            else
            {
                return _autoBuilders.TryAdd(updateType, new List<object> { autoLimitBuilder });
            }
        }

        private bool AddLimit<T>(object limited)
        {
            var updateType = Extensions.AsUpdateType<T>();

            _isSet = true;
            if (_objectes.ContainsKey(updateType))
            {
                _objectes[updateType].Add(limited);
                return true;
            }
            else
            {
                return _objectes.TryAdd(updateType, new List<object> { limited });
            }
        }

        /// <summary>
        /// Add a limit to list
        /// </summary>
        public bool AddLimit<T>(ILimited<T> limited)
        {
            var updateType = Extensions.AsUpdateType<T>();

            _isSet = true;
            if (_objectes.ContainsKey(updateType))
            {
                _objectes[updateType].Add(limited);
                return true;
            }
            else
            {
                return _objectes.TryAdd(updateType, new List<object> { limited });
            }
        }
    }
}
