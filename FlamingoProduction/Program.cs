using Flamingo;
using Flamingo.Attributes;
using Flamingo.Attributes.Filters.Async.Messages;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Filters;
using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.Ninja;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using Flamingo.Helpers;
using FlamingoProduction.MyInComings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace FlamingoProduction
{
    class Program
    {
        static async Task Main()
        {
            // Create "FlamingoCore" instance and user "InitBot" to intialize you bot!
            var flamingo = await new FlamingoCore()
                // - You can change callback data spliter char if you want (Optional)
                //      This is used when making args
                .InitBot("YOUR_BOT_TOKEN_HERE", callbackDataSpliter: '_');

            // - Use classes you created as handlers by Inheritancing from "InComingBase" classes!
            //      And enjoy tools and extensions we provide there!
            flamingo.AddInComing(new InComingStartMessage());

            // - Set if this handler is for edited messages by setting "isEdited: true"
            //      In "AddInComing" method!
            flamingo.AddInComing(new InComingArgumans(), isEdited: true);

            // - use SimpleInComing<T> class to create a simple and quick handler
            //      With callback function (MySimpleCallback)
            // - Combine filters using '&' (for and), '|' (for or) and '~' (for not)
            var mySimpleInComing = new SimpleInComing<Message>(
                MySimpleCallback, 
                new RegexFilter("^/simple (?<arg>.*)?") &

                // - Use Ninja filters for a quick filtering
                ~ MessageNinja.FromUsers(123456789));

            flamingo.AddInComing(mySimpleInComing);

            // Create your custom filter using "SimpleFilter" class
            var myCustomFilter = new SimpleFilter<CallbackQuery>(
                x => x.StringQuery.StartsWith("data_"));

            // - Simple inComing to handle callbackQueries that data starts with "data_"
            var simpleInComingCallback = new SimpleInComing<CallbackQuery>(
                HandleCallbackQuery,
                // CallbackQueryNinja.Regex($"data_")
                myCustomFilter);

            flamingo.AddInComing(simpleInComingCallback);

            Console.WriteLine(flamingo.BotInfo.FirstName);

            await flamingo.ReceiveAsync(err);

            // - Time to Fly!
            //      Call this method to start listening for updates!
            await flamingo.Fly(errorHandler: ImError);
            //                               ^
            //                               |_ Error handler method!
        }   //                                                      |

        private static Task err(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        // ________________________________________________________<|
        // v
        // Setup your erorr handler.
        private static Task ImError(FlamingoCore _, Exception e)
        {
            Console.WriteLine(e);
            return Task.CompletedTask;
        }

        [InComingMessage(IsEdited = true)]
        [RegexFilter("^/attr")]
        [AdminsOnlyFilter] // This is an async filter!
        public static async Task<bool> MyAttributedCallback(ICondiment<Message> cdmt)
        {
            await cdmt.ReplyText("Yes Attr!");
            return true;
        }

        // - An static callback function to use in "SimpleInComing<T>"
        // - "ICondiment<T>" is a key object when handling updates
        //      And it has a lot of useful extensions
        private static async Task<bool> MySimpleCallback(ICondiment<Message> cdmt)
        {
            // Create Inline (With callback query in easiest way possible)
            var btns = new InlineBuilder(
                new[] { ("Happy 10", "data_happy_10") },
                new[] { ("Sad 10", "data_sad_10") });

            string mode = "sad";

            // If you have regex filter, you can use "cdmt.MatchCollection"
            // I perfer to use "GetRequireArgs" Extension method instead 
            if (cdmt.MatchCollection[0].Groups.Count > 1)
            {
                if(new[] {"sad", "happy"}.Any(
                    x=> x == cdmt.MatchCollection[0].Groups["arg"].Value))
                {
                    mode = cdmt.MatchCollection[0].Groups["arg"].Value;
                }
            }

            // - Use Extensions ...
            await cdmt.ReplyText($"Your {mode} is sad at level 0",
                replyMarkup: new InlineKeyboardMarkup(btns.Use()));
            return true;
        }

        // - Another simple callback function to handle CallbackQueries!
        private static async Task<bool> HandleCallbackQuery(ICondiment<CallbackQuery> cdmt)
        {
            // - Feel extensions ...
            // - Query data args has been maden using "callbackDataSpliter" at "InitBot"
            if (cdmt.GetRequireArgs(
                out string mode, out int level, 1))
            /*  ^                ^              ^
                |                |              |
                |_ First arg of callback data as string
                                 |_ Second arg of callback data as int
                                                |_ Start index ( set 1 to skip "data" itself)
             */
            {
                var btns = new InlineBuilder();

                // Build your btns dynamically with "Build" Extension Method (for lists)
                btns.Build(rows =>
                {
                    // "Build" is an extension method for all Lists
                    rows.Add(new List<(string, string)>().Build(row_1 =>
                    {
                        if (level == 0)
                            row_1.Add(("Happy 10", "data_happy_10"));
                        else
                            row_1.Add(("Happy 0", "data_happy_0"));
                    }));

                    rows.Add(new List<(string, string)>().Build(row_2 =>
                    {
                        if (level == 0)
                            row_2.Add(("Sad 10", "data_sad_10"));
                        else
                            row_2.Add(("Sad 0", "data_sad_0"));
                    }));
                });

                await cdmt.EditText($"Your mode is {mode} at level {level}", 
                    replyMarkup: new InlineKeyboardMarkup(btns.Use()));
                await cdmt.Answer();
            }

            return true;
        }
    }
}
