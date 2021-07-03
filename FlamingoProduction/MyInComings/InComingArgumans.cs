using Flamingo.Filters.MessageFilters;
using Flamingo.Fishes.InComingFishes;
using Flamingo.Condiments.Extensions;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.MyInComings
{
    public class InComingArgumans : InComingMessage
    {
        public InComingArgumans() 
            : base(new RegexFilter("^/cmd"))
        { }

        protected override async Task GetEatenWrapper(Message inComing)
        {
            // Check argumans in a text Query
            if(Cdmt.GetRequireArgs(out string arg1, out int arg2, out string arg3, 1, true))
            {
                await Cdmt.ReplyText(
                    string.Format("Arg 1: {0}, arg 2: {1}, arg 3 to end: {2} ...",
                        arg1,
                        arg2,
                        arg3));
            }
        }
    }
}
