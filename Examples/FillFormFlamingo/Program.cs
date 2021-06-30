using Flamingo;
using Flamingo.Attributes;
using Flamingo.Attributes.Filters.Messages;
using Flamingo.Condiments;
using Flamingo.Condiments.Extensions;
using Flamingo.Fishes;
using Flamingo.Fishes.Awaitables;
using Flamingo.Helpers;
using Flamingo.Helpers.Types.Enums;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using static FillFormFlamingo.PasswordCheck;

namespace FillFormFlamingo
{
    class Program
    {
        static async Task Main()
        {
            var flamingo = await new FlamingoCore()
                .InitBot("API_TOKEN_HERE");

            await flamingo.Fly();
        }

        [InComingMessage]
        [CommandFilter("signup")]
        [ChatTypeFilter(FlamingoChatType.Private)]
        public static async Task<bool> FillForm(ICondiment<Message> cdmt)
        {
            var info = new SignUpInfo(cdmt.SenderId);
            await cdmt.ReplyText("👤 Please enter your user-name");

            var name = await cdmt.WaitFor(new AwaitInComingText(cdmt.SenderId));
            if(name.Status == AwaitableStatus.Succeeded)
            {
                await cdmt.ReplyText($"OK user-name is {name.Cdmt.StringQuery}");
                info.Username = name.Cdmt.StringQuery;

                await cdmt.RespondText($"🔐 Now enter your password");

                while (true)
                {
                    var pass = await cdmt.WaitFor(new AwaitInComingText(cdmt.SenderId));

                    if (pass.Status == AwaitableStatus.Succeeded)
                    {
                        if(pass.Cdmt.StringQuery == "cancel")
                        {
                            await cdmt.ReplyText("canceled!",
                                replyMarkup: new ReplyKeyboardRemove());
                            break;
                        }

                        var strength = GetPasswordStrength(pass.Cdmt.StringQuery);

                        if (strength < PasswordStrength.Medium)
                        {
                            await cdmt.ReplyText($"Please enter an stronger password!",
                                replyMarkup: new ReplyButton("cancel").Markup());
                            continue;
                        }

                        await cdmt.ReplyText($"Password confirmed!\nStrength: {strength}");
                        await cdmt.RespondText("Your sign in steps completed!");
                        break;
                    }
                    else
                    {
                        await cdmt.RespondText("Timed out!");
                        break;
                    }
                }
            }
            else
            {
                await cdmt.RespondText("Timed out!");
            }

            return true;
        }

        public class SignUpInfo
        {
            public SignUpInfo(long userid)
            {
                Userid = userid;
            }

            public long Userid { get; set; }

            public string Username { get; set; }

            public string Password { get; set; }

            public PasswordStrength PasswordStrength { get; set; }
        }
    }
}
