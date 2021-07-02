using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Telegram.Bot.Types;
using Flamingo.Fishes.InComingFishes.SimpleInComings;
using System.Threading.Tasks;

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
                typeof(SortedSet<GroupedInComing<Message>>));
        }

        [TestMethod]
        public void GetInComingListTest_2()
        {
            Assert.IsInstanceOfType(
                incomings.GetInComingList<ChatMemberUpdated>(),
                typeof(SortedSet<GroupedInComing<ChatMemberUpdated>>));
        }

        [TestMethod()]
        public async Task GetInComingListTestAsync()
        {

        }

        [TestMethod()]
        public async Task SafeAddTestAsync()
        {
            var myHanlers = new GroupedInComing<Message>[]
            {
                new GroupedInComing<Message>(new SimpleInComing<Message>(null), 0),
                new GroupedInComing<Message>(new SimpleInComing<Message>(null), 1),
                new GroupedInComing<Message>(new SimpleInComing<Message>(null), 2),
                new GroupedInComing<Message>(new SimpleInComing<Message>(null), 3),
                new GroupedInComing<Message>(new SimpleInComing<Message>(null), 4),
            };

            var tasks = new List<Task>();

            var bools = new List<bool>();

            foreach (var item in myHanlers)
            {
                tasks.Add(
                    Task.Run(
                    () =>  incomings.SafeAdd(item)
                    // () => incomings.InComingMessages.Add(item)
               ));
            }

            
            await Task.WhenAll(tasks);
            Assert.AreEqual(incomings.InComingMessages.Count, 5);

            foreach (var item in myHanlers)
            {
                tasks.Add(Task.Run(
                    () => incomings.SafeRemove(
                        incomings.InComingMessages, item)
                    // () => incomings.InComingMessages.Add(item)
               ));
            }

            await Task.WhenAll(tasks);

            Assert.AreEqual(incomings.InComingMessages.Count, 0);
        }

        [TestMethod()]
        public void SafeRemoveTest()
        {
            Assert.Fail();
        }
    }
}