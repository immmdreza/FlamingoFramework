using Flamingo;
using Flamingo.Attributes;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Filters.CallbackQueryFilters;
using Flamingo.Fishes.Awaitables;
using Flamingo.Helpers;
using Flamingo.Helpers.Types.Enums;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FunFlamingo
{
    class Program
    {
        static async Task Main()
        {
            var flamingo = await new FlamingoCore()
                .InitBot("1820608649:AAG981uKed7_ZE-VrN4MzIYnvPuI1KCz7N8");

            await flamingo.Fly();
        }

        [InComingMessage]
        [CommandFilter("fun")]
        [ChatTypeFilter(FlamingoChatType.Private)]
        public static async Task<bool> Handle(ICondiment<Message> cdmt)
        {
            var btn1 = new InlineBuilder(("Yeah", "fun1_yeah"), ("Nope", "fun1_no"));
            await cdmt.ReplyText("You need some fun yeah?",
                replyMarkup: btn1.Markup());

            var call = await cdmt.WaitFor(Awaiters.AwaitCallbackQuery(new RegexFilter("^fun1_")));

            if(call.Succeeded)
            {
                if(call.Cdmt.GetRequireArgs(out string mode, 1))
                {
                    if(mode == "yeah")
                    {
                        await call.Cdmt.EditText("Someone needs fun!");
                        await cdmt.RespondText("OK, what's your name fun hunter?");

                        var respond = await cdmt.WaitFor(Awaiters.AwaitTextRespond(cdmt.SenderId));

                        if(respond.Succeeded)
                        {
                            var name = respond.Cdmt.StringQuery;

                            await respond.Cdmt.ReplyText($"Dear {name}, if you need some fun then use Flamingo 😁 ");
                        }
                    }
                    else
                    {
                        await call.Cdmt.EditText("How bad :(");
                        await call.Cdmt.Answer();
                    }
                }
            }

            return true;
        }
    }
}
