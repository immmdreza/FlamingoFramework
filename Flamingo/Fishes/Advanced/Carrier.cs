using System;
using System.Collections.Generic;
using System.Linq;

namespace Flamingo.Fishes.Advanced
{
    /// <summary>
    /// Carries is the holder for your handler and its requirements
    /// </summary>
    /// <typeparam name="T">Handler itself</typeparam>
    public class Carrier<T> : IDisposable
    {
        /// <summary>
        /// Carries is the holder for your handler and its requirements
        /// </summary>
        public Carrier()
        { }

        /// <summary>
        /// Add a require type for this handler
        /// </summary>
        /// <remarks>Flamingo will look for a constructor that has all require parameters</remarks>
        /// <typeparam name="U">The type of requirement class</typeparam>
        /// <returns>The created requirement</returns>
        public Requirement Require<U>()
        {
            if (_requirements == null)
                _requirements = new List<Requirement>();

            var req = new Requirement(typeof(U));
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
                        foreach (var req in _requirements)
                        {
                            req.Dispose();
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
