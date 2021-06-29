using Flamingo;
using Flamingo.Condiments;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters.Ninja;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace SimpleFlamingo
{
    class Program
    {
        static async Task Main()
        {
            var flamingo = new FlamingoCore();

            await flamingo.InitBot("1820608649:AAHmoEXFkw6ogl9hF2LGDL9IYjBEKhicjpA");

            var mySimpleHandler = new SimpleInComing<Message>(Callback, MessageNinja.Regex("^/test$"));

            flamingo.AddInComing(mySimpleHandler);

            Console.WriteLine($"Listening to {flamingo.BotInfo.FirstName}");

            await flamingo.Fly();
        }

        private static async Task<bool> Callback(ICondiment<Message> cdmt)
        {
            await cdmt.Flamingo.BotClient.SendTextMessageAsync(cdmt.Chat.Id, "Tested");
            return true;
        }
    }
}
