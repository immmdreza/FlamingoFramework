using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telegram.Bot.Types;
using Flamingo.Fishes.Advanced.InComingHandlers;
using System.Threading.Tasks;
using System;

namespace Flamingo.Fishes.Advanced.Tests
{
    public class INeedThis
    {
        public INeedThis()
        {
            Number = 1000;
        }

        public int Number { get; }
    }

    public class INeedThat
    {
        public INeedThat(INeedThese needThese)
        {
            Text = "My lovely test!";
            Text += " " + needThese.Text;
        }

        public string Text { get; }
    }

    public class INeedThese
    {
        public INeedThese()
        {
            Text = "These are added too!";
        }

        public string Text { get; }
    }

    public class MyFish : AdvInComingMessage
    {
        private INeedThis _needThis;
        private INeedThat _needThat;

        public MyFish(INeedThis needThis, INeedThat needThat) 
            : base()
        {
            _needThis = needThis;
            _needThat = needThat;
        }

        protected override Task GetEatenWrapper(Message inComing)
        {
            Console.WriteLine(NeedThat.Text);
            return Task.CompletedTask;
        }

        public INeedThis NeedThis => _needThis;

        public INeedThat NeedThat => _needThat;
    }

    [TestClass()]
    public class CarrierTests
    {
        [TestMethod()]
        public void CarrierTest()
        {

        }
    }
}