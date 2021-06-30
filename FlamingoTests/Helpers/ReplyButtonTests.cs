using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flamingo.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flamingo.Helpers.Tests
{
    [TestClass()]
    public class ReplyButtonTests
    {
        [TestMethod()]
        public void ReplyButtonTest()
        {
            var btns = new ReplyButton("Test");
        }

        [TestMethod()]
        public void ReplyButtonTest1()
        {
            var btns = new ReplyButton("Test1", "Test2", "Test3");
        }

        [TestMethod()]
        public void ReplyButtonTest2()
        {
            var btns = new ReplyButton(3, "Test1", "Test2", "Test3", "Test4");
        }

        [TestMethod()]
        public void ReplyButtonTest3()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MarkupTest()
        {
            Assert.Fail();
        }
    }
}