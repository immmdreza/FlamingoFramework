using Flamingo.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.Payments;

namespace Flamingo
{
    /// <summary>
    /// A Set of useful extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Finds out <see cref="UpdateType"/>
        /// </summary>
        /// <typeparam name="T">Update type</typeparam>
        public static UpdateType AsUpdateType<T>(
            bool isEdited = false,
            bool isChannelPost = false,
            bool isMine = false)
        {
            var type = typeof(T);

            if (type == typeof(Message))
            {
                if (isChannelPost)
                {
                    if (isEdited) return UpdateType.EditedChannelPost;
                    else return UpdateType.ChannelPost;
                }
                else
                {
                    if (isEdited) return UpdateType.EditedMessage;
                    else return UpdateType.Message;
                }
            }
            else if (type == typeof(CallbackQuery)) return UpdateType.CallbackQuery;
            else if (type == typeof(InlineQuery)) return UpdateType.InlineQuery;
            else if (type == typeof(ChosenInlineResult)) return UpdateType.ChosenInlineResult;
            else if (type == typeof(ChatMemberUpdated))
            {
                if (isMine) return UpdateType.MyChatMember;
                else return UpdateType.ChatMember;
            }
            else if (type == typeof(Poll)) return UpdateType.Poll;
            else if (type == typeof(PollAnswer)) return UpdateType.PollAnswer;
            else if (type == typeof(ShippingQuery)) return UpdateType.ShippingQuery;
            else if (type == typeof(PreCheckoutQuery)) return UpdateType.PreCheckoutQuery;
            else throw new NotAnUpdateTypeException<T>();
        }
    }
}
