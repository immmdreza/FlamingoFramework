using Flamingo.Filters.MessageFilters;
using Flamingo.Filters.SharedFilters;
using Flamingo.Fishes.InComingFishes;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.MyInComings
{
    class InComingStartMessage : InComingMessage
    {
        public InComingStartMessage() 
            : base(new PrivateFilter<Message>() & new RegexFilter("^/start$"))
        { }

        protected override async Task GetEatenWapper(Message inComing)
        {
            await ReplyText("Just Started!");
        }
    }
}
