using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flamingo.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot.Types;

namespace Flamingo.Filters.Tests
{
    [TestClass()]
    public class FilterBaseTests
    {
        [TestMethod()]
        public void CombineTest()
        {
            var filters = new FilterBase<string>[]
            {
                new FilterBase<string>(x=> x.StartsWith("a")),
                new FilterBase<string>(x=> x.EndsWith("h")),
                new FilterBase<string>(x=> x[1] == 'r'),
                new FilterBase<string>(x=> x[^2] == 's'),
                ~ new FilterBase<string>(x=> x[^3] == 'a'),
            };

            var filter = FilterBase<string>.Combine(filters);

            Assert.IsFalse(filter.IsPassed("arash"));
        }
    }
}