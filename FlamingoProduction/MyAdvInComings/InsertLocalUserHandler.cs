using Flamingo.Fishes.Advanced.InComingHandlers;
using FlamingoProduction.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.MyAdvInComings
{
    public class InsertLocalUserHandler: AdvInComingMessage
    {
        private FlamingoContext _flamingoContext;

        public InsertLocalUserHandler(FlamingoContext flamingoContext)
        {
            _flamingoContext = flamingoContext;
        }

        protected override async Task GetEatenWrapper(Message inComing)
        {
            // if(Cdmt.GetRequireArgs(out string name))

            if(await _flamingoContext.LocalUsers.AnyAsync(x=> x.TelegramId == Cdmt.SenderId))
            {
                await ReplyText("Already know you!");
            }
            else
            {
                _flamingoContext.LocalUsers.Add(new Models.LocalUser
                {
                    TelegramId = Cdmt.SenderId
                });

                await _flamingoContext.SaveChangesAsync();
                await ReplyText("You're mine now :)");
            }
        }
    }
}
