using Flamingo;
using Flamingo.Attributes;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace KeyboardsFlamingo
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("Hello World!");
            var flamingo = await new FlamingoCore()
                .InitBot("1820608649:AAG981uKed7_ZE-VrN4MzIYnvPuI1KCz7N8");

            await flamingo.Fly();
        }

        private static IEnumerable<(string, string)> GenerateInline(int count)
        {
            return Enumerable.Range(1, count).Select(x => ($"{x}.{x}", $"{x}.{x}"));
        }

        /// <summary>
        /// Command /inline [rowCount] [count],  
        /// </summary>
        /// <remarks>/inline 3 10</remarks>
        [InComingMessage]
        [CommandFilter("inline")]
        public static async Task<bool> VAsync(ICondiment<Message> cdmt)
        {
            InlineBuilder btns;
            if (cdmt.GetRequireArgs(out int columnCount, 1))
            {
                if (cdmt.GetRequireArgs(out int count, 2))
                {
                    btns = new InlineBuilder(columnCount, GenerateInline(count).ToArray());
                }
                else
                {
                    btns = new InlineBuilder(GenerateInline(count).ToArray());
                }
            }
            else
            {
                btns = new InlineBuilder(
                    ("1.1", "1.1"), ("1.2", "1.2"),
                    ("2.1", "2.1"), ("2.2", "2.2"));
            }

            await cdmt.ReplyText("Here are your buttons", replyMarkup: btns.Markup());
            return true;
        }

        private static IEnumerable<string> GenerateReply(int count)
        {
            return Enumerable.Range(1, count).Select(x => $"{x}.{x}");
        }

        /// <summary>
        /// Command /reply [rowCount] [count],  
        /// </summary>
        /// <remarks>/reply 3 10</remarks>
        [InComingMessage]
        [CommandFilter("reply")]
        public static async Task<bool> VAsync2(ICondiment<Message> cdmt)
        {
            ReplyButton btns;
            if (cdmt.GetRequireArgs(out int columnCount, 1))
            {
                if (cdmt.GetRequireArgs(out int count, 2))
                {
                    btns = new ReplyButton(columnCount, GenerateReply(count).ToArray());
                }
                else
                {
                    btns = new ReplyButton(GenerateReply(count).ToArray());
                }
            }
            else
            {
                btns = new ReplyButton("1.1");
            }

            await cdmt.ReplyText("Here are your buttons", replyMarkup: btns.Markup());
            return true;
        }
    }
}
