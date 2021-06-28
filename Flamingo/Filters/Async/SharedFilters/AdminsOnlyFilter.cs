using Flamingo.Condiments;
using System.Linq;

namespace Flamingo.Filters.Async.SharedFilters
{
    class AdminsOnlyFilter<T> : FilterBaseAsync<ICondiment<T>>
    {
        public AdminsOnlyFilter() 
            : base(async x =>
            {
                if (x.Chat == null) return false;

                if (x.Chat.Type == Telegram.Bot.Types.Enums.ChatType.Private) return false;

                if (x.Sender == null) return false;

                var admins = await x.Flamingo.BotClient.GetChatAdministratorsAsync(x.Chat.Id);

                return admins.Any(a => a.User.Id == x.Sender.Id);
            })
        {
        }
    }
}
