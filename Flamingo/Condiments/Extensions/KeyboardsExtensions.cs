using Telegram.Bot.Types.ReplyMarkups;

namespace Flamingo.Condiments.Extensions
{
    public static class KeyboardsExtensions
    {
        public static InlineKeyboardButton InlineBtn<T>(this ICondiment<T> _,
            string name, string data)
        {
            return InlineKeyboardButton.WithCallbackData(name, data);
        }
    }
}
