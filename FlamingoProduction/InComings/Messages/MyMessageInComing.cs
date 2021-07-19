using Flamingo.Filters.MessageFilters;
using Flamingo.Fishes.Advanced.Attributes;
using Flamingo.Fishes.InComingFishes;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.InComings.Messages
{
    [IsEditedMessage]
    [HandlingGroup(Group = 2)]
    public class MyMessageInComing : InComingMessage
    {
        public MyMessageInComing() 
            : base(new CommandFilter("test1") & new IsEditedFilter())
        { }

        protected override async Task GetEatenWrapper(Message inComing)
        {
            await ReplyText("OK tested 1");
        }
    }
}
