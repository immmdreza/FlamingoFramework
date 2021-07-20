using Flamingo.Attributes.Filters.Messages;
using Flamingo.Fishes.Advanced.InComingHandlers;
using Flamingo.Fishes.Awaitables.FillFormHelper;
using Flamingo.Fishes.Awaitables.FillFormHelper.FromDataChecks;
using Flamingo.Helpers.Types.Enums;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.InComings.Messages
{
    public enum Gender
    {
        None = 0,

        Male = 1,

        Female = 2,
    }

    public class UserDataForm
    {
        [FlamingoFormProperty]
        [StringLength(10, FailureMessage = "10 char at most")]
        [StringRegex(@"^[a-zA-Z]+$", FailureMessage = "only letters")]
        public string FirstName { get; set; }

        [FlamingoFormProperty]
        [StringLength(10)]
        [StringRegex(@"^[a-zA-Z]+$", FailureMessage = "only letters")]
        public string LastName { get; set; }

        [FlamingoFormProperty]
        public int Code { get; set; }

        [FlamingoFormProperty(Required = false)]
        public Gender Gender { get; set; } = Gender.None;

        public string FullName => $"{FirstName} {LastName} ({Code}) ({Gender})";
    }

    [CommandFilter("form")]
    [ChatTypeFilter(FlamingoChatType.Private)]
    public class MyAdvMessageInComing: AdvInComingMessage
    {
        protected override async Task GetEatenWrapper(Message inComing)
        {
            // Creates a form filler instance for `UserDataForm` class
            var filler = new FillFormRequest<UserDataForm>(Flamingo);

            // Asks user for marked properties of `UserDataForm`
            // And allows user to fail for 1 time ( Type check failure or value checks )
            await filler.Ask(
                Sender.Id,
                triesOnFailure: 1,
                cancellInputPattern: "^/cancel");

            if(filler.Succeeded)
                // filler.Instance is an instance of `UserDataForm` which is filled!
                await ReplyText(filler.Instance.FullName);
            else if(filler.Canceled)
                await ReplyText("See you.");
            else
                await ReplyText("Please try again!");
        }
    }
}
