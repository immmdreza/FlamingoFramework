using Flamingo.Condiments;
using Flamingo.Filters;

namespace Flamingo.Attributes.Filters
{
    /// <summary>
    /// Base interface for filter attributes
    /// </summary>
    /// <typeparam name="T">Type of update to filter</typeparam>
    public interface IFilterAttribute<T>
    {
        /// <summary>
        /// The filter that this attribute carries
        /// </summary>
        FilterBase<ICondiment<T>> Filter { get; }
    }
}
