using Flamingo.Filters.MessageFilters;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace DeepInsideFlamingo.Handlers
{
    public class GetDbResult : MyCustomHandlers.MessageHandler
    {
        public GetDbResult() 
            : base(new RegexFilter("^/db$"))
        { }

        protected override async Task GetEatenWapper(Message inComing)
        {
            await ReplyText(MyCdmt.Db.FetchData());
        }
    }
}
