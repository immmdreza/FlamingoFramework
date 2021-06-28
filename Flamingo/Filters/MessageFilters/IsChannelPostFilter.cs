namespace Flamingo.Filters.MessageFilters
{
    /// <summary>
    /// Check if this message is channel post
    /// </summary>
    public class IsChannelPostFilter: MessageFilter
    {
        /// <summary>
        /// Check if this message is channel post
        /// </summary>
        public IsChannelPostFilter()
            : base(x=> x.IsChannelPost)
        { }
    }
}
