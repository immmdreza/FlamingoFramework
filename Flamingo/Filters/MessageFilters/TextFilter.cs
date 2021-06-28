namespace Flamingo.Filters.MessageFilters
{
    /// <summary>
    /// Filter messages with a text
    /// </summary>
    public class TextFilter : MessageFilter
    {
        /// <summary>
        /// Filter messages with a text
        /// </summary>
        public TextFilter()
            : base(x=> x.InComing.Text != null || x.InComing.Caption != null)
        { }
    }
}
