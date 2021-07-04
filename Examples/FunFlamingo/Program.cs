using Flamingo;
using Flamingo.Attributes;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Filters.CallbackQueryFilters;
using Flamingo.Fishes.Awaitables;
using Flamingo.Helpers;
using Flamingo.Helpers.Types.Enums;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FunFlamingo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var flamingo = new FlamingoCore();

            await flamingo.Fly();
        }

        [InComingMessage]
        [CommandFilter("fun")]
        [ChatTypeFilter(FlamingoChatType.Private)]
        public static async Task<bool> Handle(ICondiment<Message> cdmt)
        {
            var btn1 = new InlineBuilder(("Yeah", "fun1_yeah"), ("Nope", "fun1_no"));
            await cdmt.ReplyText("You need some fun yeah?",
                replyMarkup: new InlineKeyboardMarkup(btn1.Markup()));

            await cdmt.WaitFor(new SimpleAwaitableInComing<CallbackQuery>(
                new RegexFilter("^fun1_")));
        }
    }
}
