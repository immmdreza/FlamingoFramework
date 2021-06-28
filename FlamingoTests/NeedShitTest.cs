using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace FlamingoTests
{
    [TestClass]
    public class NeedShitTest
    {
        public class Shit
        {
            public Shit()
            {
                MuNum = 10;
            }

            public int MuNum { get; }
        }

        public class MyMthyc
        {
            private readonly Shit _shit;

            public MyMthyc(Shit shit)
            {
                _shit = shit;
            }
        }

        public interface ShitInjector<T>
        {
            T Construct(Func<T> builder);
        }

        public class InJectedMyMthyc : ShitInjector<MyMthyc>
        {
            public MyMthyc Construct(Func<MyMthyc> builder)
            {
                return new MyMthyc(new Shit());
            }
        }

        [TestMethod]
        public void Test_1()
        {
            Type type = typeof(MyMthyc);
            var ctors = type.GetConstructors();

            // object instance = ctor.Invoke(new object[] { 10 });
        }
    }
}
