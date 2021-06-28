namespace Flamingo.Attributes
{
    /// <summary>
    /// Base interface for InComing hanlers in attribute
    /// </summary>
    /// <typeparam name="T">InComing update type</typeparam>
    public interface IFishAttribute<T>
    {
        /// <summary>
        /// Handling group of this handler
        /// </summary>
        public int Group { get; set; }
    }
}
