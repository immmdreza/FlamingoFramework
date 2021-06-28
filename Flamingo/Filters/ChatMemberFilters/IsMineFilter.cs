namespace Flamingo.Filters.ChatMemberFilters
{
    /// <summary>
    /// Filter to handle updates of type MyChatMember!
    /// </summary>
    public class IsMineFilter : ChatMemberFilter
    {
        /// <summary>
        /// Filter to handle updates of type <c>MyChatMember!</c>
        /// </summary>
        public IsMineFilter()
            : base(x =>x.IsMine)
        { }
    }
}
