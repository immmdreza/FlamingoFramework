using Flamingo.Condiments.HotCondiments;
using Flamingo.Helpers.Types.Enums;

namespace Flamingo.Filters.ChatMemberFilters
{
    /// <summary>
    /// Checks whatever a user joined a chat
    /// </summary>
    public class JoinedChatFilter : ChatMemberFilter
    {
        /// <summary>
        /// Checks whatever a user joined a chat
        /// </summary>
        public JoinedChatFilter()
            : base(x=>
            {
                return x.OldChatMemberFlamingoStatus.HasFlag(
                    ChatMemberFlamingoStatus.Kicked | ChatMemberFlamingoStatus.Left) &&

                    x.NewChatMemberFlamingoStatus == ChatMemberFlamingoStatus.Member;
            })
        {
        }
    }
}
