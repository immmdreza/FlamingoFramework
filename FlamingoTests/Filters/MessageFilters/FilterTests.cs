using Flamingo.Condiments.HotCondiments;
using Flamingo.Filters.SharedFilters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telegram.Bot.Types;

namespace Flamingo.Filters.MessageFilters.Tests
{
    [TestClass]
    public class FilterTests
    {
        [TestMethod]
        public void PrivateFilterTest()
        {
            var message = new Message()
            {
                Chat = new Chat 
                { 
                    Type = Telegram.Bot.Types.Enums.ChatType.Private 
                },
                Text = "Hello"
            };

            var cdmt = new MessageCondiment(message, null);

            Assert.IsTrue(new PrivateFilter<Message>().IsPassed(cdmt));
        }

        [TestMethod]
        public void CombinedFilters_Test_1()
        {
            var message = new Message()
            {
                Chat = new Chat
                {
                    Type = Telegram.Bot.Types.Enums.ChatType.Private
                },
                Text = "Hello"
            };

            var cdmt = new MessageCondiment(message, null);

            var filter = new PrivateFilter<Message>() & new TextFilter();

            Assert.IsTrue(filter.IsPassed(cdmt));
        }

        [TestMethod]
        public void CombinedFilters_Test_2()
        {
            var message = new Message()
            {
                Chat = new Chat
                {
                    Type = Telegram.Bot.Types.Enums.ChatType.Private
                },
            };

            var cdmt = new MessageCondiment(message, null);

            var filter = new PrivateFilter<Message>() & new TextFilter();

            Assert.IsFalse(filter.IsPassed(cdmt));
        }

        [TestMethod]
        public void CombinedFilters_Test_3()
        {
            var message = new Message()
            {
                Chat = new Chat
                {
                    Type = Telegram.Bot.Types.Enums.ChatType.Private
                },
            };

            var cdmt = new MessageCondiment(message, null);

            var filter = new PrivateFilter<Message>() | new TextFilter();

            Assert.IsTrue(filter.IsPassed(cdmt));
        }
    }
}