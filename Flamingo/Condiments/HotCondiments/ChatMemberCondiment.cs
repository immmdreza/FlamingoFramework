using Flamingo.Helpers;
using Flamingo.Helpers.Types.Enums;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class ChatMemberCondiment : CondimentBase<ChatMemberUpdated>
    {
        /// <inheritdoc/>
        public ChatMemberCondiment(
            ChatMemberUpdated inComing,
            FlamingoCore flamingo,
            bool isMine)
            : base(inComing, flamingo)
        {
            IsMine = isMine;
        }


        /// <summary>
        /// Converted New <see cref="ChatMemberStatus"/> to <see cref="ChatMemberFlamingoStatus"/>
        /// </summary>
        public ChatMemberFlamingoStatus NewChatMemberFlamingoStatus
        {
            get => InComing.NewChatMember.Status.ToChatMemberFlamingoStatus();
        }

        /// <summary>
        /// Converted Old <see cref="ChatMemberStatus"/> to <see cref="ChatMemberFlamingoStatus"/>
        /// </summary>
        public ChatMemberFlamingoStatus OldChatMemberFlamingoStatus
        {
            get => InComing.OldChatMember.Status.ToChatMemberFlamingoStatus();
        }

        /// <summary>
        /// If this condiment carries <c>MyChatMember</c> update
        /// </summary>
        public bool IsMine { get; set; }

        /// <inheritdoc/>
        public override Chat Chat => InComing.Chat;

        /// <summary>
        /// User who take changes in status
        /// </summary>
        public User EffectedUser => InComing.NewChatMember.User;

        /// <inheritdoc/>
        public override User Sender => InComing.From;
    }
}
