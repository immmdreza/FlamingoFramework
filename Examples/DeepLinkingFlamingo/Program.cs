using Flamingo;
using Flamingo.Attributes;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Filters.Enums;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DeepLinkingFlamingo
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
        [CommandFilter("start", ArgumentsMode.NoArgs)]
        public static async Task<bool> NormalStart(ICondiment<Message> cdmt)
        {
            await cdmt.ReplyText($"Just Started!");
            return true;
        }

        [InComingMessage]
        [CommandFilter("start", ArgumentsMode.Require)]
        public static async Task<bool> DeepLinks(ICondiment<Message> cdmt)
        {
            if(cdmt.QueryArgs.ElementAt(1)
                .GetRequireArgs(out string param, out long userId, splitter: '_'))
            {
                if(param == "referralCode")
                {
                    await cdmt.ReplyText($"Accepted referral from {userId}");
                }
            }

            return true;
        }
    }
}
