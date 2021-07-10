using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flamingo.BotExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using FlamingoTests;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace Flamingo.BotExtensions.Tests
{
    [TestClass()]
    public class MessengerTests
    {
        private readonly IFlamingoCore _flamingo = new TestFlamingoCore(
            new User { Username = "flamingo", Id = 12345678, FirstName = "Flamingo" });

        [TestMethod()]
        public async Task SendTestAsync()
        {
            var messenger = new Messenger(_flamingo)
                .AppendText("Hello");

            await messenger.Send(123456);
        }

        [TestMethod()]
        public void MessengerTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void AppendTextTest()
        {
            Assert.Fail();
        }
    }
}