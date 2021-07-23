using Flamingo.Attributes.Filters.Messages;
using Flamingo.Fishes.Advanced.InComingHandlers;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.InComings.Messages
{
    [CommandFilter("poll")]
    public class PollTestHandler : AdvInComingMessage
    {
        protected override async Task GetEatenWrapper(Message inComing)
        {
            var poll = await BotClient.SendPollAsync(
                Cdmt.SenderId, "Test poll", new[] { "1", "2", "3", "4" },
                false, Telegram.Bot.Types.Enums.PollType.Quiz, false, 0,
                "Just a random choice", openPeriod: 5,
                closeDate: DateTime.UtcNow + TimeSpan.FromSeconds(5));

            // Wait for poll answer right here ( for 5 seconds )!
            var pollAnswer = await Cdmt.WaitFor<PollAnswer>(
                x => x.InComing.PollId == poll.Poll.Id && x.SenderId == Cdmt.SenderId,
                timeOut: 5);

            if (pollAnswer.Succeeded)
                Console.WriteLine("Poll answer is here within the time out");
            else
                Console.WriteLine("Quiz timed out.");
        }
    }
}
