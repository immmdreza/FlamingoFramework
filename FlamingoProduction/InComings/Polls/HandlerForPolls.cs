using Flamingo.Fishes.Advanced;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoProduction.InComings.Polls
{
    public class HandlerForPolls: AdvInComingFish<Poll>
    {
        protected override Task GetEatenWrapper(Poll inComing)
        {
            Console.WriteLine("Poll received!");
            return Task.CompletedTask;
        }
    }
}
