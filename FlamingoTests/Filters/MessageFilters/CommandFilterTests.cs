using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flamingo.Condiments;
using Telegram.Bot.Types;
using Flamingo.Condiments.HotCondiments;
using FlamingoTests;

namespace Flamingo.Filters.MessageFilters.Tests
{
    [TestClass()]
    public class CommandFilterTests
    {
        private readonly IFlamingoCore _flamingo = new TestFlamingoCore(
            new User { Username = "flamingo", Id = 12345678, FirstName = "Flamingo" });

        private ICondiment<Message> cdmt(string text)
        {
            return new MessageCondiment(new Message { Text = text }, _flamingo);
        }

        [DataTestMethod]
        [DataRow("/start")]
        [DataRow("/start@flamingo")]
        [DataRow("/Start@Flamingo")]
        [DataRow("/start hello")]
        [DataRow("/start@flamingo hello")]
        public void CommandFilterTest(string cmd)
        {
            Assert.IsTrue(new CommandFilter("start").IsPassed(cdmt(cmd)));
        }

        [DataTestMethod]
        [DataRow("start")]
        [DataRow("/start@something")]
        [DataRow("/Startksdjk")]
        [DataRow("hello /start")]
        public void CommandFilterTest1(string cmd)
        {
            Assert.IsFalse(new CommandFilter("start").IsPassed(cdmt(cmd)));
        }

        [DataTestMethod]
        [DataRow("!start")]
        [DataRow("!start@flamingo")]
        [DataRow("!Start@Flamingo")]
        [DataRow("!start hello")]
        [DataRow("!start@flamingo hello")]
        public void CommandFilterTest2(string cmd)
        {
            Assert.IsTrue(new CommandFilter('!', "start").IsPassed(cdmt(cmd)));
        }

        [DataTestMethod]
        [DataRow("!start hello")]
        [DataRow("!start@flamingo hello")]
        public void CommandFilterTest3(string cmd)
        {
            Assert.IsTrue(new CommandFilter('!', Enums.ArgumentsMode.Require, "start").IsPassed(cdmt(cmd)));
        }

        [DataTestMethod]
        [DataRow("!start hello")]
        [DataRow("!start@flamingo hello")]
        public void CommandFilterTest4(string cmd)
        {
            Assert.IsFalse(new CommandFilter('!', Enums.ArgumentsMode.NoArgs, "start").IsPassed(cdmt(cmd)));
        }
    }
}