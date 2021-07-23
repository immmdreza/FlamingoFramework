using Flamingo.Fishes.Advanced;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.InComings.PollAnswers
{
    public class PollAnswersHandler : AdvInComingFish<PollAnswer>
    {
        protected override Task GetEatenWrapper(PollAnswer inComing)
        {
            Console.WriteLine("Answer received!");
            return Task.CompletedTask;
        }
    }
}
