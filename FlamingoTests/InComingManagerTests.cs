using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flamingo;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;
using Flamingo.Fishes;

namespace Flamingo.Tests
{
    [TestClass()]
    public class InComingManagerTests
    {
        private readonly InComingManager incomings = new InComingManager();

        [TestMethod]
        public void GetInComingListTest_1()
        {
            Assert.IsInstanceOfType(
                incomings.GetInComingList<Message>(),
                typeof(Dictionary<IFish<Message>, int>));
        }

        [TestMethod]
        public void GetInComingListTest_2()
        {
            Assert.IsInstanceOfType(
                incomings.GetInComingList<ChatMemberUpdated>(),
                typeof(Dictionary<IFish<ChatMemberUpdated>, int>));
        }
    }
}