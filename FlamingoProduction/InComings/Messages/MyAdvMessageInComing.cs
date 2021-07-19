using Flamingo.Attributes.Filters.Messages;
using Flamingo.Fishes.Advanced.Attributes;
using Flamingo.Fishes.Advanced.InComingHandlers;
using FlamingoProduction.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.InComings.Messages
{
    [HandlingGroup(Group = 1)]
    [CommandFilter("test2")]
    public class MyAdvMessageInComing: AdvInComingMessage
    {
        private readonly FlamingoContext _flamingoContext;

        [AdvancedHandlerConstructor]
        public MyAdvMessageInComing(FlamingoContext flamingoContext)
        {
            _flamingoContext = flamingoContext;
        }

        protected override async Task GetEatenWrapper(Message inComing)
        {
            var users = await _flamingoContext.LocalUsers.ToListAsync();

            await ReplyText(string.Join("\n", users.Select(x=> x.TelegramId.ToString())));
        }
    }
}
