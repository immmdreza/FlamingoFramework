using System;
using System.Threading.Tasks;
using Flamingo;

namespace QuickStart
{
    class Program
    {
        static async Task Main()
        {
            var flamingo = new FlamingoCore()
                // Add all incoming handler in 'InComings.{UpdateTypes}' namesspaces
                // Like 'InComings.Messages' for message incoming handlers
                .AutoAddInComings()
                .InitBot("BOT_TOKEN_HERE", true)
                .SetCallbackDataSpliter();

            // Start receiving updates and blocks current thread
            await flamingo.Fly(errorHandler: Err);
        }

        private static Task Err(FlamingoCore _, Exception e)
        {
            // Setup your error handler here

            Console.WriteLine(e);
            return Task.CompletedTask;
        }
    }
}
