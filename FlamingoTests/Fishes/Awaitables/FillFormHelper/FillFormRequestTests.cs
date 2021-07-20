using Microsoft.VisualStudio.TestTools.UnitTesting;
using Flamingo.Fishes.Awaitables.FillFormHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Flamingo.Fishes.Awaitables.FillFormHelper.Tests
{
    [TestClass()]
    public class FillFormRequestTests
    {
        public class UserDataForm
        {
            [FlamingoFormProperty]
            public string Name { get; set; }

            [FlamingoFormProperty]
            public string LastName { get; set; }

            public string FullName => Name + LastName;

            public int SecretI { get; set; }

            private bool Love { get; set; }

            public UserDataForm(bool love)
            {
                Love = love;
            }
        }

        [TestMethod()]
        public void FillFormRequestTest()
        {
            // var fff = new FillFormRequest<UserDataForm>();
        }
    }
}