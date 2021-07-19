using System.Threading.Tasks;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments.Extensions;
using Flamingo.Fishes.Advanced;
using Flamingo.Fishes.Awaitables;
using Flamingo.Fishes.Advanced.Attributes;
using Flamingo.Helpers.Types.Enums;
using Telegram.Bot.Types;
using Flamingo.Filters.Enums;

namespace QuickStart.InComings.Messages
{
    // Add filter here
    // This will handler /start command in private chat ( with no args )
    [CommandFilter("start", ArgumentsMode.NoArgs)]
    [ChatTypeFilter(FlamingoChatType.Private)]
    public class StartMessage: AdvInComingFish<Message>
    {
        // Add your dependencies ( like db context ) here!
        [AdvancedHandlerConstructor]
        public StartMessage()
        {

        }

        // Handle your update here
        protected override async Task GetEatenWrapper(Message inComing)
        {
            await Cdmt.ReplyText("Flamingo Started!");
        }
    }
}