using Flamingo.Attributes.Filters.Messages;
using Flamingo.Fishes.Advanced.InComingHandlers;
using Flamingo.Fishes.Awaitables.FillFormHelper;
using Flamingo.Fishes.Awaitables.FillFormHelper.FromDataChecks;
using Flamingo.Helpers.Types.Enums;
using System;
using System.Text.RegularExpressions;
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
    public class MyAdvMessageInComing : AdvInComingMessage
    {
        protected override async Task GetEatenWrapper(Message inComing)
        {
            // Creates a form filler instance for `UserDataForm` class
            // 'StatusChanged' is a callback function that is called when 
            // status changed in asking process (eg new answer, validation failure, ...)
            // this functions provides enough information about filler and can cancel
            // processing there is you suppose to using 'filler.Terminate()'
            var filler = Flamingo.CreateFormFiller<UserDataForm>(StatusChanged);

            // Asks user for marked properties of `UserDataForm`
            // And allows user to fail for 1 time ( Type check failure or value checks )
            await filler.Ask(
                Cdmt.SenderId,
                triesOnFailure: 1,
                timeOut: 10,
                cancellInputPattern: new Regex("^/cancel"));

            if (filler.Succeeded)
                // filler.Instance is an instance of `UserDataForm` which is filled!
                await ReplyText(filler.Instance.FullName);
            else if (filler.Canceled)
                await ReplyText("See you.");
            else if (filler.TimedOut)
                await ReplyText("Answer faster next time.");
            else if (filler.Terminated)
                await ReplyText("Operation terminated.");
            else
                await ReplyText("Something wrong try again.");
        }

        // if this function returns false then no message will send to user.
        private bool StatusChanged(
            FillFormRequest<UserDataForm> form,
            FlamingoFormStatus status,
            IFlamingoFormData data)
        {
            if (status == FlamingoFormStatus.TimedOut)
            {
                // This means: don't send any further message for this question
                return false;
            }
            else if (status == FlamingoFormStatus.RecoverableTimedOut)
            {
                form.Flamingo.BotClient.SendTextMessageAsync(
                    form.UserId, $"You missed {data.Name}. but no worries it's optional!");

                return false;
            }
            else if (status == FlamingoFormStatus.ValidatingFailed)
            {
                form.Flamingo.BotClient.SendTextMessageAsync(
                    form.UserId, $"No mistakes allowed!");
                form.Terminate();

                return false;
            }

            return true;
        }
    }
}
