using System;
using System.Collections.Generic;
using System.Linq;

namespace Flamingo.Fishes.Advanced
{
    public class Requirement: IDisposable
    {
        public Requirement(Type type)
        {
            NeededType = type;
        }

        private object _createdObj; 

        private object[] GetRequirements()
        {
            return _requirements.Select(x => x.CreateYourSelf()).ToArray();
        }

        public object CreateYourSelf()
        {
            if(_requirements == null)
            {
                _createdObj = Activator.CreateInstance(NeededType);
            }
            else
            {
                _createdObj = Activator.CreateInstance(NeededType, GetRequirements());
            }

            return _createdObj;
        }

        public Type NeededType { get; }

        public Requirement Require<T>()
        {
            if (_requirements == null)
                _requirements = new List<Requirement>();

            _requirements.Add(new Requirement(typeof(T)));
            return this;
        }

        public void Dispose()
        {
            if(_createdObj != null)
            {
                if(_createdObj is IDisposable disposable)
                {
                    disposable.Dispose();
                    _createdObj = null;
                }
            }
        }

        public List<Requirement> Requirements => _requirements;

        private List<Requirement> _requirements;
    }
}
