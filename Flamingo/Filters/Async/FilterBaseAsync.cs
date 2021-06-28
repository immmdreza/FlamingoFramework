using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flamingo.Filters.Async
{
    public class FilterBaseAsync<T> : IFilterAsync<T>
    {
        public FilterBaseAsync(Func<T, Task<bool>> filter)
        {
            Filter = filter;
        }

        public Func<T, Task<bool>> Filter { get; }

        public async Task<bool> IsPassed(T inComing)
        {
            return await Filter(inComing);
        }

        public static FilterBaseAsync<T> Combine(IEnumerable<FilterBaseAsync<T>> filters)
        {
            if (!filters.Any()) return null;

            var main = filters.ElementAt(0);

            foreach (var filter in filters.Skip(1))
            {
                main = main & filter;
            }

            return main;
        }

        public static FilterBaseAsync<T> operator &(FilterBaseAsync<T> a, FilterBaseAsync<T> b)
        {
            return new FilterBaseAsync<T>(async x => await a.IsPassed(x) && await b.IsPassed(x));
        }

        public static FilterBaseAsync<T> operator |(FilterBaseAsync<T> a, FilterBaseAsync<T> b)
        {
            return new FilterBaseAsync<T>(async x => await a.IsPassed(x) || await b.IsPassed(x));
        }

        public static FilterBaseAsync<T> operator ~(FilterBaseAsync<T> a)
        {
            return new FilterBaseAsync<T>(async x => !await a.IsPassed(x));
        }
    }
}
