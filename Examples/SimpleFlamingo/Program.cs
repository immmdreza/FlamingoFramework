using Flamingo;
using Flamingo.Attributes;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.SharedFilters;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using Flamingo.Helpers.Types.Enums;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SimpleFlamingo
{
    class Program
    {
        static async Task Main()
        {
            var flamingo = new FlamingoCore();

            await flamingo.InitBot("API_TOKEN_HERE");

            var chatFilter = new ChatTypeFilter<Message>(FlamingoChatType.Private);

            var commandFilter = new CommandFilter("start");

            var startHandler = new SimpleInComing<Message>(CallbackFunc,
                chatFilter & commandFilter);

            flamingo.AddInComing(startHandler);

            await flamingo.Fly();
        }

        [InComingMessage]
        [CommandFilter("about")]
        [ChatTypeFilter(FlamingoChatType.Private)]
        public static async Task<bool> AboutCallbackFunc(ICondiment<Message> cdmt)
        {
            await cdmt.ReplyText(
                "I'm Flamingo bot!\n" + 
                "written using [FlamingoFramework!](https://github.com/immmdreza/FlamingoFramework)",
                Telegram.Bot.Types.Enums.ParseMode.Markdown);
            return true;
        }

        private static async Task<bool> CallbackFunc(ICondiment<Message> cdmt)
        {
            await cdmt.ReplyText("Just started!");
            return true;
        }
    }
}
