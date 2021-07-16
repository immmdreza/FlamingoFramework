using Flamingo;
using Flamingo.Attributes;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Filters;
using Flamingo.Filters.Enums;
using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.Ninja;
using Flamingo.Fishes.Advanced.CarrierFishes;
using Flamingo.Fishes.Awaitables;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using Flamingo.Helpers;
using Flamingo.Helpers.Types.Enums;
using FlamingoProduction.Database;
using FlamingoProduction.MyAdvInComings;
using FlamingoProduction.MyInComings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction
{
    class Program
    {
        static async Task Main()
        {
            // Create "FlamingoCore" instance and user "InitBot" to initialize you bot!
            var flamingo = new FlamingoCore();

            // - Use classes you created as handlers by Inheriting from "InComingBase" classes!
            //      And enjoy tools and extensions we provide there!
            flamingo.AddInComing(new InComingStartMessage());

            // - Set if this handler is for edited messages by setting "isEdited: true"
            //      In "AddInComing" method!
            flamingo.AddInComing(new InComingArgumans(), isEdited: true, group: 2);

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

            // Limits message sender to 1 message per 3 seconds
            // by passing true, the thread will be blocked till limit releases
            // An the answer will be sent after ( Nothing ignored )
            flamingo.AddAutoMessageSenderLimit(TimeSpan.FromSeconds(3), true);

            // Limits callback query sender to 1 call per 5 seconds
            // This limit just ignores calls that are sent in limited duration
            flamingo.AddAutoCallbackQuerySenderLimit(TimeSpan.FromSeconds(5));


            // Work with advanced handlers
            var myAdvCarrier1 = new MessageCarrierFish<GetDataFromBaseHandler>
                (new CommandFilter("count"));

            // Use this if you didn't use [AdvancedHandlerConstructor]
            myAdvCarrier1.Carrier.Require<FlamingoContext>();

            flamingo.AddAdvancedInComing(myAdvCarrier1);

            // Filters are also passed using attributes in class definition
            var myAdvCarrier2 = new MessageCarrierFish<InsertLocalUserHandler>();

            flamingo.AddAdvancedInComing(myAdvCarrier2);


            // - You can change callback data splitter char if you want (Optional)
            //      This is used when making args
            await flamingo.InitBot("1820608649:AAHL3tH8YWDqVc8C7cc2tzEbPsGoRhNMSA8",
                callbackDataSpliter: '_');

            Console.WriteLine(flamingo.BotInfo.FirstName);

            // - Time to Fly!
            //      Call this method to start listening for updates!
            await flamingo.Fly(errorHandler: ImError);
            //                               ^
            //                               |_ Error handler method!
        }   //                                                      |
        // ________________________________________________________<|
        // v
        // Setup your error handler.
        private static Task ImError(FlamingoCore _, Exception e)
        {
            Console.WriteLine(e);
            return Task.CompletedTask;
        }

        [InComingMessage]
        [CommandFilter("start", ArgumentsMode.NoArgs)]
        public static async Task<bool> NormalStart(ICondiment<Message> cdmt)
        {
            await cdmt.ReplyText($"Just Started!");
            return false;
        }

        [InComingMessage(Group = 1)]
        [RegexFilter("^/setname")]
        [ChatTypeFilter(FlamingoChatType.Private)]
        public static async Task<bool> MyAttributedCallback(ICondiment<Message> cdmt)
        {
            await cdmt.ReplyText("OK what is your name?");

            var handle = new AwaitInComingText(cdmt.SenderId);

            var result = await cdmt.WaitForTextRespond(cdmt.SenderId);

            if(!result.Succeeded)
            {
                if(result.TimedOut)
                {
                    await cdmt.ReplyText("Request has been timed out!");
                }

                return false;
            }

            if (result.TextRespond == "/cancel")
            {
                handle.Cancell();
                await cdmt.ReplyText("Canceled");
            }

            await cdmt.ReplyText($"OK your name is {result.TextRespond}");

            return true;
        }

        // - An static callback function to use in "SimpleInComing<T>"
        // - "ICondiment<T>" is a key object when handling updates
        //      And it has a lot of useful extensions
        private static async Task<bool> MySimpleCallback(ICondiment<Message> cdmt)
        {
            // Create In-line (With callback query in easiest way possible)
            var btns = new InlineBuilder(1,
                ("Happy 10", "data_happy_10") ,
                ("Sad 10", "data_sad_10") );

            string mode = "sad";

            // If you have regex filter, you can use "cdmt.MatchCollection"
            // I prefer to use "GetRequireArgs" Extension method instead 
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
                replyMarkup: btns.Markup());
            return true;
        }

        // - Another simple callback function to handle CallbackQueries!
        private static async Task<bool> HandleCallbackQuery(ICondiment<CallbackQuery> cdmt)
        {
            // - Feel extensions ...
            // - Query data args has been made using "callbackDataSpliter" at "InitBot"
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

                // Build your buttons dynamically with "Build" Extension Method (for lists)
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
                    replyMarkup: btns.Markup());
                await cdmt.Answer();
            }

            return true;
        }
    }
}
