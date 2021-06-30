using DeepInsideFlamingo.Handlers;
using DeepInsideFlamingo.MyCustomCondiments;
using Flamingo;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace DeepInsideFlamingo
{
    class Program
    {
        private static FlamingoCore flamingo;

        static async Task Main(string[] args)
        {
            flamingo = new FlamingoCore();

            await flamingo.InitBot("API_TOKEN_HERE");

            flamingo.AddInComing(new GetDbResult());

            Console.WriteLine($"Listening to {flamingo.BotInfo.FirstName}");

            await flamingo.BotClient.ReceiveAsync(new DefaultUpdateHandler(OnUpdate, OnError));
        }

        private static Task OnError(ITelegramBotClient _, Exception arg2, CancellationToken __)
        {
            Console.WriteLine(arg2);
            return Task.CompletedTask;
        }

        private static async Task OnUpdate(ITelegramBotClient _, Update update, CancellationToken __)
        {
            switch(update)
            {
                case { Message: { } }:
                    {
                        var db = new DatabaseManager();

                        var cdmt = new MyMessageCondiment(update.Message, flamingo, db);

                        await foreach(var handler in flamingo.PassedHandlersAsync(cdmt))
                        {
                            if(!db.IsConnected)
                            {
                                db.Connect();
                            }

                            await handler.GetEaten(cdmt);
                        }

                        db.Disonnect();
                        break;
                    }
            }
        }
    }
}
