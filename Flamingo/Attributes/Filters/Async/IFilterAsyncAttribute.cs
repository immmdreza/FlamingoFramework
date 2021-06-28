using Flamingo.Condiments;
using Flamingo.Filters.Async;

namespace Flamingo.Attributes.Filters.Async
{
    /// <summary>
    /// Base interface for async filter attributes
    /// </summary>
    /// <typeparam name="T">Type of incoming update</typeparam>
    public interface IFilterAsyncAttribute<T>
    {
        /// <summary>
        /// The async filter that this attribute carries
        /// </summary>
        FilterBaseAsync<ICondiment<T>> Filter { get; }
    }
}
