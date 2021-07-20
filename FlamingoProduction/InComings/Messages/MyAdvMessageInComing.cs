using Flamingo.Attributes.Filters.Messages;
using Flamingo.Fishes.Advanced.InComingHandlers;
using Flamingo.Fishes.Awaitables.FillFormHelper;
using Flamingo.Fishes.Awaitables.FillFormHelper.FromDataChecks;
using Flamingo.Helpers.Types.Enums;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.InComings.Messages
{
    public class UserDataForm
    {
        [FlamingoFormProperty]
        [StringMaxLength(10, FailureMessage = "10 char at most")]
        [StringRegex(@"^[a-zA-Z]+$", FailureMessage = "only letters")]
        public string FirstName { get; set; }

        [FlamingoFormProperty]
        [StringRegex(@"^[a-zA-Z]+$", FailureMessage = "only letters")]
        public string LastName { get; set; }

        [FlamingoFormProperty]
        public int Code { get; set; }

        public string FullName => $"{FirstName} {LastName} ({Code})";
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
