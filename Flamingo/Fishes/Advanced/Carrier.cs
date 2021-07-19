using Flamingo.Fishes.Advanced.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Flamingo.Fishes.Advanced
{
    /// <summary>
    /// Carries is the holder for your handler and its requirements
    /// </summary>
    /// <typeparam name="T">Handler itself</typeparam>
    public class Carrier<T> : IDisposable
    {
        private bool _attributed = false;

        /// <summary>
        /// Carries is the holder for your handler and its requirements
        /// </summary>
        public Carrier()
        {
            if(HasAttribute() is ConstructorInfo constructor)
            {
                FillRequireFromAttributed(constructor);
                _attributed = true;
            }
        }

        private void FillRequireFromAttributed(ConstructorInfo constructor)
        {
            foreach (var para in constructor.GetParameters())
            {
                Require(para.ParameterType);
            }
        }

        private ConstructorInfo HasAttribute()
        {
            var t = typeof(T);

            var cnts = t.GetConstructors();

            foreach (var cnt in cnts)
            {
                var attr = cnt.GetCustomAttributes(false);

                if (attr.Any(x => x is AdvancedHandlerConstructorAttribute))
                {
                    return cnt;
                }
            }

            return null;
        }

        /// <summary>
        /// Add a require type for this handler
        /// </summary>
        /// <remarks>Flamingo will look for a constructor that has all require parameters</remarks>
        /// <typeparam name="U">The type of requirement class</typeparam>
        /// <returns>The created requirement</returns>
        public Requirement Require<U>()
        {
            return Require(typeof(U));
        }

        public Requirement Require(Type type)
        {
            if (_attributed)
                throw new Exception("This carrier is attributed");

            if (_requirements == null)
                _requirements = new List<Requirement>();

            var req = new Requirement(type);
            _requirements.Add(req);
            return req;
        }

        private List<Requirement> _requirements;

        private bool disposedValue;

        private object[] GetRequirements()
        {
            if (_requirements == null)
                return Array.Empty<object>();

            return _requirements.Select(x => x.CreateYourSelf()).ToArray();
        }

        private T _mainObj;

        /// <summary>
        /// Creates handler with it requirements
        /// </summary>
        public T Create()
        {
            var req = GetRequirements();

            _mainObj = (T)Activator.CreateInstance(typeof(T), req);
            return _mainObj;
        }

        /// <summary>
        /// Dispose the handler and all of it's Disposable requirements
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if(_mainObj != null)
                    {
                        if (_requirements != null)
                        {
                            foreach (var req in _requirements)
                            {
                                req.Dispose();
                            }
                        }

                        if(_mainObj is IDisposable disposable)
                        {
                            disposable.Dispose();
                        }
                    }
                }

                _mainObj = default;
                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose the handler and all of it's Disposable requirements
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
