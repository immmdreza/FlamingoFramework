using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.SharedFilters;
using Flamingo.Fishes.InComingFishes;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.MyInComings
{
    public class InComingStartMessage : InComingMessage
    {
        public InComingStartMessage() 
            : base(new PrivateFilter<Message>() & new CommandFilter("start"))
        { }

        protected override async Task GetEatenWrapper(Message inComing)
        {
            await ReplyText("Just Started!");
        }
    }
}
