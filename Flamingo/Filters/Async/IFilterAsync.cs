using System;
using System.Threading.Tasks;

namespace Flamingo.Filters.Async
{
    public interface IFilterAsync<T>
    {
        public Func<T, Task<bool>> Filter { get; }

        public Task<bool> IsPassed(T inComing);
    }
}
