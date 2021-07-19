using Flamingo.Condiments;
using System;
using System.Threading.Tasks;

namespace Flamingo.Fishes.Advanced
{
    public interface IAdvFish<T> : IFish<T>
    {
        /// <summary>
        /// Here you can choose what you want to do with incoming update
        /// </summary>
        public Func<ICondiment<T>, Task<bool>> GetEaten { get; }
    }
}
