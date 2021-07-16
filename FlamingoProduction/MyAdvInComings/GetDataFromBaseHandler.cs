using Flamingo.Fishes.Advanced.InComingHandlers;
using FlamingoProduction.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.MyAdvInComings
{
    public class GetDataFromBaseHandler: AdvInComingMessage
    {
        private FlamingoContext _flamingoContext;

        public GetDataFromBaseHandler(FlamingoContext flamingoContext)
        {
            _flamingoContext = flamingoContext;
        }

        protected override async Task GetEatenWrapper(Message inComing)
        {
            var users = await _flamingoContext.LocalUsers.ToListAsync();
            await ReplyText(users.Count.ToString());
        }
    }
}
