namespace Flamingo.Filters.MessageFilters
{
    /// <summary>
    /// Check if this is an edited message 
    /// </summary>
    public class IsEditedFilter : MessageFilter
    {
        /// <summary>
        /// Check if this is an edited message 
        /// </summary>
        public IsEditedFilter() 
            : base(x=> x.IsEdited)
        { }
    }
}
