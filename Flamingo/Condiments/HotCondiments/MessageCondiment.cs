using Telegram.Bot.Types;

namespace Flamingo.Condiments.HotCondiments
{
    /// <inheritdoc/>
    public class MessageCondiment : CondimentBase<Message>
    {
        /// <inheritdoc/>
        public MessageCondiment(
            Message inComing,
            IFlamingoCore flamingo,
            bool isChannelPost = false,
            bool isEdited = false) 
            : base(inComing, flamingo)
        {
            IsChannelPost = isChannelPost;
            IsEdited = isEdited;
        }

        /// <inheritdoc/>
        public override Chat Chat => InComing.Chat;

        /// <inheritdoc/>
        public override User Sender => InComing.From;

        /// <summary>
        /// If this update is a ChannelPost
        /// </summary>
        public bool IsChannelPost { get; }

        /// <summary>
        /// If it's an edited message
        /// </summary>
        public bool IsEdited { get; }

        /// <inheritdoc/>
        public override string StringQuery
        {
            get
            {
                return InComing switch
                {
                    { Text: { } text } => text,
                    { Caption: { } capt } => capt,
                    _ => null
                };
            }
        }
    }
}
