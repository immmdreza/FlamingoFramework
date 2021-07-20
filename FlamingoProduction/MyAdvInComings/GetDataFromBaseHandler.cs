using Flamingo.Fishes.Advanced.InComingHandlers;
using FlamingoProduction.Database;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.MyAdvInComings
{
    public class GetDataFromBaseHandler: AdvInComingMessage
    {
        private readonly FlamingoContext _flamingoContext;

        public GetDataFromBaseHandler(FlamingoContext flamingoContext)
        {
            _flamingoContext = flamingoContext;
        }

        protected override async Task GetEatenWrapper(Message inComing)
        {
            await ReplyText(_flamingoContext.MagicalItem.MagicalWords);
        }
    }
}
