using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace MaskedTextBoxBehavior.TESTS
{
    [TestClass]
    public class ProviderTests
    {
        [TestMethod]
        public void TestPatternGen()
        {
            var masker = new MaskedTextProvider();
            masker.MatchPattern = "###-##-####";

            Assert.AreEqual(@"^(\d{0,1})(\d{0,1})(\d{0,1})(\-{0,1})(\d{0,1})(\d{0,1})(\-{0,1})(\d{0,1})(\d{0,1})(\d{0,1})(\d{0,1})$", masker.RegExMatch);
        }

        [TestMethod]
        public void TestSSNStyle()
        {
            var masker = new MaskedTextProvider();
            masker.MatchPattern = "###-##-####";

            Assert.AreEqual("1", masker.ReplaceString("1"));
            Assert.AreEqual("12", masker.ReplaceString("12"));
            Assert.AreEqual("123", masker.ReplaceString("123"));
            Assert.AreEqual("123-", masker.ReplaceString("123-"));
            Assert.AreEqual("123-45", masker.ReplaceString("123-45"));
            Assert.AreEqual("123-45-6789", masker.ReplaceString("123-45-6789"));
            Assert.AreEqual("123-45-6789", masker.ReplaceString("123456789"));
        }

        [TestMethod]
        public void TestPhoneStyle()
        {
            var masker = new MaskedTextProvider();
            masker.MatchPattern = "(###)###-####";
            Assert.AreEqual("(1", masker.ReplaceString("1"));

        }
    }
}
