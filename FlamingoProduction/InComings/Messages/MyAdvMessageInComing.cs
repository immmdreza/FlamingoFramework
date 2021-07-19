using Flamingo.Attributes.Filters.Messages;
using Flamingo.Fishes.Advanced.InComingHandlers;
using Flamingo.Fishes.Awaitables.FillFormHelper;
using Flamingo.Helpers.Types.Enums;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.InComings.Messages
{
    public class UserDataForm
    {
        [FlamingoFormConstructor]
        public UserDataForm(
            [FlamingoFormData] string name,
            [FlamingoFormData] string lastName,
            [FlamingoFormData(InvalidTypeText = "Please enter a numeric value")] int code)
        {
            FullName = name + " " + lastName + $" ({code})";
        }

        public string FullName { get; }
    }

    [CommandFilter("form")]
    [ChatTypeFilter(FlamingoChatType.Private)]
    public class MyAdvMessageInComing: AdvInComingMessage
    {
        protected override async Task GetEatenWrapper(Message inComing)
        {
            var filler = new FillFormRequest<UserDataForm>(Flamingo);

            await filler.Ask(Sender.Id, 1);

            await ReplyText(filler.Instance.FullName);
        }
    }
}
