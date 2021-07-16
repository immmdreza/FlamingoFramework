using Flamingo.Fishes.Advanced.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Flamingo.Fishes.Advanced
{
    public class Requirement: IDisposable
    {
        private bool _attributed = false;

        public Requirement(Type type)
        {
            NeededType = type;

            if(HasAttribute(type) is ConstructorInfo constructor)
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

        private ConstructorInfo HasAttribute(Type t)
        {
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
            return Require(typeof(T));
        }

        public Requirement Require(Type type)
        {
            if (_attributed)
                throw new Exception("This requirement is attributed");

            if (_requirements == null)
                _requirements = new List<Requirement>();

            _requirements.Add(new Requirement(type));
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
