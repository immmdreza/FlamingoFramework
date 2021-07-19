using Flamingo;
using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters.MessageFilters;
using Flamingo.Fishes.Advanced;
using Flamingo.Fishes.Advanced.CarrierFishes;
using Flamingo.Fishes.Advanced.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace FlamingoTests.Fishes.Advanced
{
    [TestClass]
    public class OverallTests
    {
        [TestMethod]
        public async Task Test_1Async()
        {
            var myAdvCarrier = new MessageCarrierFish<MyFish>
                (new CommandFilter("hello"));

            myAdvCarrier.Carrier.Require<INeedThis>();
            myAdvCarrier.Carrier.Require<INeedThat>()
                .Require<INeedThese>();

            var t = myAdvCarrier.GetType();

            var interfaces = t.GetInterfaces();

            var carrier = t.GetInterfaces().FirstOrDefault(x =>
              x.IsGenericType &&
              x.GetGenericTypeDefinition() == typeof(ICarrier<>));

            if (carrier != null)
            {
                var m = carrier.GetMethod("SetupFish");

                m.Invoke(myAdvCarrier, null);

                await myAdvCarrier.GetEaten(new MessageCondiment(
                    new Message
                    {
                        Text = ":)"
                    }, null));
            }
        }
    }
}
