using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Flamingo.Helpers.Tests
{
    [TestClass()]
    public class TextParserTests
    {
        [TestMethod()]
        public void AppendCodeTest()
        {
            var textParser = new TextParser();
            textParser.AppendCode("This is code!");

            Assert.AreEqual("<code>This is code!</code>", textParser);
        }

        [TestMethod()]
        public void TagCombineTest()
        {
            var textParser = new TextParser();
            textParser.AppendUnderLine(
                TextParser.HtmlBold("Bold in Underline") + " Hello others", false);
            // false here means: Don't encode <b> and </b> tags inside <u>
            

            Assert.AreEqual("<u><b>Bold in Underline</b> Hello others</u>", textParser,
                message: "Inner html tags should not be encoded here!");
        }
    }
}