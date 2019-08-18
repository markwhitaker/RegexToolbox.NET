using System.Text.RegularExpressions;
using NUnit.Framework;

namespace RegexToolbox.Tests
{
    [TestFixture]
    public class RegexBuilderTests
    {
        [Test]
        public void TestSimpleText()
        {
            var regex = new RegexBuilder()
                .Text("cat")
                .BuildRegex();

            Assert.AreEqual("cat", regex.ToString());
            Assert.IsTrue(regex.IsMatch("cat"));
            Assert.IsTrue(regex.IsMatch("scatter"));
            Assert.IsFalse(regex.IsMatch("Cat"));
            Assert.IsFalse(regex.IsMatch("dog"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestSimpleTextWithQuantifier()
        {
            var regex = new RegexBuilder()
                .Text("cat", RegexQuantifier.Exactly(2))
                .BuildRegex();

            Assert.AreEqual("(?:cat){2}", regex.ToString());
            Assert.IsFalse(regex.IsMatch("cat"));
            Assert.IsTrue(regex.IsMatch("catcat"));
            Assert.IsTrue(regex.IsMatch("catcatcat"));
            Assert.IsFalse(regex.IsMatch("scatter"));
            Assert.IsFalse(regex.IsMatch("Cat"));
            Assert.IsFalse(regex.IsMatch("dog"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestSimpleTextCaseInsensitive()
        {
            var regex = new RegexBuilder()
                .Text("cat")
                .BuildRegex(RegexOptions.IgnoreCase);

            Assert.AreEqual("cat", regex.ToString());
            Assert.IsTrue(regex.IsMatch("cat"));
            Assert.IsTrue(regex.IsMatch("scatter"));
            Assert.IsTrue(regex.IsMatch("Cat"));
            Assert.IsFalse(regex.IsMatch("dog"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestSimpleTextWithRegexCharacters()
        {
            var regex = new RegexBuilder()
                .Text(@"\.+*?[]{}()|^$")
                .BuildRegex();

            Assert.AreEqual(@"\\\.\+\*\?\[\]\{\}\(\)\|\^\$", regex.ToString());
            Assert.IsTrue(regex.IsMatch(@"\.+*?[]{}()|^$"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestRegexText()
        {
            var regex = new RegexBuilder()
                .RegexText(@"^\scat\b")
                .BuildRegex();

            Assert.AreEqual(@"^\scat\b", regex.ToString());
            Assert.IsTrue(regex.IsMatch(" cat"));
            Assert.IsTrue(regex.IsMatch(" cat."));
            Assert.IsTrue(regex.IsMatch("\tcat "));
            Assert.IsTrue(regex.IsMatch(" cat-"));
            Assert.IsTrue(regex.IsMatch(" cat "));
            Assert.IsFalse(regex.IsMatch("cat"));
            Assert.IsFalse(regex.IsMatch(" catheter"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAnyCharacter()
        {
            var regex = new RegexBuilder()
                .AnyCharacter()
                .BuildRegex();

            Assert.AreEqual(".", regex.ToString());
            Assert.IsTrue(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("a"));
            Assert.IsTrue(regex.IsMatch("1"));
            Assert.IsTrue(regex.IsMatch(@"\"));
            Assert.IsFalse(regex.IsMatch(string.Empty));
            Assert.IsFalse(regex.IsMatch("\n"));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestWhitespace()
        {
            var regex = new RegexBuilder()
                .Whitespace()
                .BuildRegex();

            Assert.AreEqual(@"\s", regex.ToString());
            Assert.IsTrue(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("\t"));
            Assert.IsTrue(regex.IsMatch("\r"));
            Assert.IsTrue(regex.IsMatch("\n"));
            Assert.IsTrue(regex.IsMatch("\r\n"));
            Assert.IsTrue(regex.IsMatch("\t \t"));
            Assert.IsTrue(regex.IsMatch("                hi!"));
            Assert.IsFalse(regex.IsMatch("cat"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestSpace()
        {
            var regex = new RegexBuilder()
                .Space()
                .BuildRegex();

            Assert.AreEqual(" ", regex.ToString());
            Assert.IsTrue(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("\t"));
            Assert.IsFalse(regex.IsMatch("\r"));
            Assert.IsFalse(regex.IsMatch("\n"));
            Assert.IsFalse(regex.IsMatch("\r\n"));
            Assert.IsTrue(regex.IsMatch("\t \t"));
            Assert.IsTrue(regex.IsMatch("                hi!"));
            Assert.IsFalse(regex.IsMatch("cat"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestTab()
        {
            var regex = new RegexBuilder()
                .Tab()
                .BuildRegex();

            Assert.AreEqual(@"\t", regex.ToString());
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("\t"));
            Assert.IsFalse(regex.IsMatch("\r"));
            Assert.IsFalse(regex.IsMatch("\n"));
            Assert.IsFalse(regex.IsMatch("\r\n"));
            Assert.IsTrue(regex.IsMatch("\t \t"));
            Assert.IsFalse(regex.IsMatch("                hi!"));
            Assert.IsFalse(regex.IsMatch("cat"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestLineFeed()
        {
            var regex = new RegexBuilder()
                .LineFeed()
                .BuildRegex();

            Assert.AreEqual(@"\n", regex.ToString());
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("\t"));
            Assert.IsFalse(regex.IsMatch("\r"));
            Assert.IsTrue(regex.IsMatch("\n"));
            Assert.IsTrue(regex.IsMatch("\r\n"));
            Assert.IsFalse(regex.IsMatch("\t \t"));
            Assert.IsFalse(regex.IsMatch("                hi!"));
            Assert.IsFalse(regex.IsMatch("cat"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestCarriageReturn()
        {
            var regex = new RegexBuilder()
                .CarriageReturn()
                .BuildRegex();

            Assert.AreEqual(@"\r", regex.ToString());
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("\t"));
            Assert.IsTrue(regex.IsMatch("\r"));
            Assert.IsFalse(regex.IsMatch("\n"));
            Assert.IsTrue(regex.IsMatch("\r\n"));
            Assert.IsFalse(regex.IsMatch("\t \t"));
            Assert.IsFalse(regex.IsMatch("                hi!"));
            Assert.IsFalse(regex.IsMatch("cat"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNonWhitespace()
        {
            var regex = new RegexBuilder()
                .NonWhitespace()
                .BuildRegex();

            Assert.AreEqual(@"\S", regex.ToString());
            Assert.IsTrue(regex.IsMatch("a"));
            Assert.IsTrue(regex.IsMatch("1"));
            Assert.IsTrue(regex.IsMatch("-"));
            Assert.IsTrue(regex.IsMatch("*"));
            Assert.IsTrue(regex.IsMatch("abc"));
            Assert.IsTrue(regex.IsMatch("                hi!"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("\t"));
            Assert.IsFalse(regex.IsMatch("\r"));
            Assert.IsFalse(regex.IsMatch("\n"));
            Assert.IsFalse(regex.IsMatch("\t\t\r\n   "));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestPossibleWhitespace()
        {
            var regex = new RegexBuilder()
                .NonWhitespace()
                .PossibleWhitespace()
                .NonWhitespace()
                .BuildRegex();

            Assert.AreEqual(@"\S\s*\S", regex.ToString());
            Assert.IsFalse(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("0"));
            Assert.IsTrue(regex.IsMatch("999"));
            Assert.IsTrue(regex.IsMatch("there's a digit in here s0mewhere"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("abc"));
            Assert.IsTrue(regex.IsMatch("xFFF"));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }
        
        [Test]
        public void TestDigit()
        {
            var regex = new RegexBuilder()
                .Digit()
                .BuildRegex();

            Assert.AreEqual(@"\d", regex.ToString());
            Assert.IsTrue(regex.IsMatch("1"));
            Assert.IsTrue(regex.IsMatch("0"));
            Assert.IsTrue(regex.IsMatch("999"));
            Assert.IsTrue(regex.IsMatch("there's a digit in here s0mewhere"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("abc"));
            Assert.IsFalse(regex.IsMatch("xFFF"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNonDigit()
        {
            var regex = new RegexBuilder()
                .NonDigit()
                .BuildRegex();

            Assert.AreEqual(@"\D", regex.ToString());
            Assert.IsTrue(regex.IsMatch(" 1"));
            Assert.IsTrue(regex.IsMatch("a0"));
            Assert.IsTrue(regex.IsMatch("999_"));
            Assert.IsTrue(regex.IsMatch("1,000"));
            Assert.IsTrue(regex.IsMatch("there's a digit in here s0mewhere"));
            Assert.IsFalse(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("0"));
            Assert.IsFalse(regex.IsMatch("999"));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestLetter()
        {
            var regex = new RegexBuilder()
                .Letter()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("a"));
            Assert.IsTrue(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        z"));
            Assert.IsTrue(regex.IsMatch("text with spaces"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("%"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNonLetter()
        {
            var regex = new RegexBuilder()
                .NonLetter()
                .BuildRegex();

            Assert.AreEqual(@"[^a-zA-Z]", regex.ToString());
            Assert.IsTrue(regex.IsMatch(" 1"));
            Assert.IsTrue(regex.IsMatch("0"));
            Assert.IsTrue(regex.IsMatch("999_"));
            Assert.IsTrue(regex.IsMatch("1,000"));
            Assert.IsTrue(regex.IsMatch("text with spaces"));
            Assert.IsFalse(regex.IsMatch("a"));
            Assert.IsFalse(regex.IsMatch("ZZZ"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestUppercaseLetter()
        {
            var regex = new RegexBuilder()
                .UppercaseLetter()
                .BuildRegex();

            Assert.AreEqual(@"[A-Z]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        Z"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("%"));
            Assert.IsFalse(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestLowercaseLetter()
        {
            var regex = new RegexBuilder()
                .LowercaseLetter()
                .BuildRegex();

            Assert.AreEqual(@"[a-z]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("a"));
            Assert.IsTrue(regex.IsMatch("        z"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("%"));
            Assert.IsFalse(regex.IsMatch("S"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestLetterOrDigit()
        {
            var regex = new RegexBuilder()
                .LetterOrDigit()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z0-9]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        Z"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("%"));
            Assert.IsFalse(regex.IsMatch("_"));
            Assert.IsTrue(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNonLetterOrDigit()
        {
            var regex = new RegexBuilder()
                .NonLetterOrDigit()
                .BuildRegex();

            Assert.AreEqual(@"[^a-zA-Z0-9]", regex.ToString());
            Assert.IsFalse(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        Z"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsTrue(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("1"));
            Assert.IsTrue(regex.IsMatch("%"));
            Assert.IsTrue(regex.IsMatch("_"));
            Assert.IsFalse(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestHexDigit()
        {
            var regex = new RegexBuilder()
                .HexDigit()
                .BuildRegex();

            Assert.AreEqual(@"[0-9A-Fa-f]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        f"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("%"));
            Assert.IsFalse(regex.IsMatch("_"));
            Assert.IsFalse(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestUppercaseHexDigit()
        {
            var regex = new RegexBuilder()
                .UppercaseHexDigit()
                .BuildRegex();

            Assert.AreEqual(@"[0-9A-F]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("A"));
            Assert.IsFalse(regex.IsMatch("        f"));
            Assert.IsFalse(regex.IsMatch("text with Spaces"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("%"));
            Assert.IsFalse(regex.IsMatch("_"));
            Assert.IsFalse(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestLowercaseHexDigit()
        {
            var regex = new RegexBuilder()
                .LowercaseHexDigit()
                .BuildRegex();

            Assert.AreEqual(@"[0-9a-f]", regex.ToString());
            Assert.IsFalse(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        f"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("%"));
            Assert.IsFalse(regex.IsMatch("_"));
            Assert.IsFalse(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNonHexDigit()
        {
            var regex = new RegexBuilder()
                .NonHexDigit()
                .BuildRegex();

            Assert.AreEqual(@"[^0-9A-Fa-f]", regex.ToString());
            Assert.IsFalse(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        f"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsTrue(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("1"));
            Assert.IsTrue(regex.IsMatch("%"));
            Assert.IsTrue(regex.IsMatch("_"));
            Assert.IsTrue(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestWordCharacter()
        {
            var regex = new RegexBuilder()
                .WordCharacter()
                .BuildRegex();

            Assert.AreEqual(@"\w", regex.ToString());
            Assert.IsTrue(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        Z"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsTrue(regex.IsMatch("1"));
            Assert.IsFalse(regex.IsMatch("%"));
            Assert.IsTrue(regex.IsMatch("_"));
            Assert.IsTrue(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNonWordCharacter()
        {
            var regex = new RegexBuilder()
                .NonWordCharacter()
                .BuildRegex();

            Assert.AreEqual(@"\W", regex.ToString());
            Assert.IsFalse(regex.IsMatch("A"));
            Assert.IsTrue(regex.IsMatch("        Z"));
            Assert.IsTrue(regex.IsMatch("text with Spaces"));
            Assert.IsTrue(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch("1"));
            Assert.IsTrue(regex.IsMatch("%"));
            Assert.IsFalse(regex.IsMatch("_"));
            Assert.IsFalse(regex.IsMatch("s"));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAnyCharacterFrom()
        {
            var regex = new RegexBuilder()
                .AnyCharacterFrom("cat")
                .BuildRegex();

            Assert.AreEqual("[cat]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("cat"));
            Assert.IsTrue(regex.IsMatch("parrot"));
            Assert.IsTrue(regex.IsMatch("tiger"));
            Assert.IsTrue(regex.IsMatch("cow"));
            Assert.IsFalse(regex.IsMatch("CAT"));
            Assert.IsFalse(regex.IsMatch("dog"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAnyCharacterFromWithCaretAtStart()
        {
            var regex = new RegexBuilder()
                .AnyCharacterFrom("^abc")
                .BuildRegex();

            Assert.AreEqual(@"[\^abc]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("jazz"));
            Assert.IsTrue(regex.IsMatch("_^_"));
            Assert.IsTrue(regex.IsMatch("oboe"));
            Assert.IsTrue(regex.IsMatch("cue"));
            Assert.IsFalse(regex.IsMatch("CAT"));
            Assert.IsFalse(regex.IsMatch("dog"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAnyCharacterFromWithCaretNotAtStart()
        {
            var regex = new RegexBuilder()
                .AnyCharacterFrom("a^bc")
                .BuildRegex();

            Assert.AreEqual("[a^bc]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("jazz"));
            Assert.IsTrue(regex.IsMatch("_^_"));
            Assert.IsTrue(regex.IsMatch("oboe"));
            Assert.IsTrue(regex.IsMatch("cue"));
            Assert.IsFalse(regex.IsMatch("CAT"));
            Assert.IsFalse(regex.IsMatch("dog"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAnyCharacterExcept()
        {
            var regex = new RegexBuilder()
                .AnyCharacterExcept("cat")
                .BuildRegex();

            Assert.AreEqual("[^cat]", regex.ToString());
            Assert.IsFalse(regex.IsMatch("cat"));
            Assert.IsFalse(regex.IsMatch("tata"));
            Assert.IsTrue(regex.IsMatch("parrot"));
            Assert.IsTrue(regex.IsMatch("tiger"));
            Assert.IsTrue(regex.IsMatch("cow"));
            Assert.IsTrue(regex.IsMatch("CAT"));
            Assert.IsTrue(regex.IsMatch("dog"));
            Assert.IsTrue(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAnyOf()
        {
            var regex = new RegexBuilder()
                .AnyOf(new[] { "cat", "dog", "|" })
                .BuildRegex();

            Assert.AreEqual(@"(?:cat|dog|\|)", regex.ToString());
            Assert.IsFalse(regex.IsMatch("ca do"));
            Assert.IsTrue(regex.IsMatch("cat"));
            Assert.IsTrue(regex.IsMatch("dog"));
            Assert.IsTrue(regex.IsMatch("|"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAnyOfNullEmptyOrSingle()
        {
            var anyOfNullRegex = new RegexBuilder()
                .AnyOf(null)
                .BuildRegex();

            var anyOfEmptyRegex = new RegexBuilder()
                .AnyOf(new string[] { })
                .BuildRegex();

            var anyOfSingleRegex = new RegexBuilder()
                .AnyOf(new []{"cat"})
                .BuildRegex();

            Assert.AreEqual(string.Empty, anyOfNullRegex.ToString());
            Assert.AreEqual(string.Empty, anyOfEmptyRegex.ToString());
            Assert.AreEqual("cat", anyOfSingleRegex.ToString());
        }

        [Test]
        public void TestStartOfString()
        {
            var regex = new RegexBuilder()
                .StartOfString()
                .Text("a")
                .BuildRegex();

            Assert.AreEqual("^a", regex.ToString());
            Assert.IsTrue(regex.IsMatch("a"));
            Assert.IsTrue(regex.IsMatch("aA"));
            Assert.IsTrue(regex.IsMatch("a_"));
            Assert.IsTrue(regex.IsMatch("a        big gap"));
            Assert.IsFalse(regex.IsMatch(" a space before"));
            Assert.IsFalse(regex.IsMatch("A capital letter"));
            Assert.IsFalse(regex.IsMatch("Aa"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestEndOfString()
        {
            var regex = new RegexBuilder()
                .Text("z")
                .EndOfString()
                .BuildRegex();

            Assert.AreEqual("z$", regex.ToString());
            Assert.IsTrue(regex.IsMatch("z"));
            Assert.IsTrue(regex.IsMatch("zzz"));
            Assert.IsTrue(regex.IsMatch("fizz buzz"));
            Assert.IsFalse(regex.IsMatch("buzz!"));
            Assert.IsFalse(regex.IsMatch("zzz "));
            Assert.IsFalse(regex.IsMatch("zZ"));
            Assert.IsFalse(regex.IsMatch("z "));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestWordBoundary()
        {
            var regex = new RegexBuilder()
                .Text("a")
                .WordBoundary()
                .BuildRegex();

            Assert.AreEqual(@"a\b", regex.ToString());
            Assert.IsTrue(regex.IsMatch("a"));
            Assert.IsTrue(regex.IsMatch("spa"));
            Assert.IsTrue(regex.IsMatch("papa don't preach"));
            Assert.IsTrue(regex.IsMatch("a dog"));
            Assert.IsTrue(regex.IsMatch("a-dog"));
            Assert.IsFalse(regex.IsMatch("an apple"));
            Assert.IsFalse(regex.IsMatch("asp"));
            Assert.IsFalse(regex.IsMatch("a1b"));
            Assert.IsFalse(regex.IsMatch("a_b"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestSingleGroup()
        {
            var regex = new RegexBuilder()
                .AnyCharacter(RegexQuantifier.ZeroOrMore)
                .StartGroup()
                    .Letter()
                    .Digit()
                .EndGroup()
                .BuildRegex();

            Assert.AreEqual(@".*([a-zA-Z]\d)", regex.ToString());

            Match match = regex.Match("Class A1");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("Class A1", match.Groups[0].Value);
            Assert.AreEqual("A1", match.Groups[1].Value);

            match = regex.Match("he likes F1 racing");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("he likes F1", match.Groups[0].Value);
            Assert.AreEqual("F1", match.Groups[1].Value);

            match = regex.Match("A4 paper");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("A4", match.Groups[0].Value);
            Assert.AreEqual("A4", match.Groups[1].Value);

            match = regex.Match("A 4-legged dog");
            Assert.IsFalse(match.Success);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestRepeatGroup()
        {
            var regex = new RegexBuilder()
                .StartGroup()
                    .Letter()
                    .Digit()
                .EndGroup()
                .BuildRegex();

            Assert.AreEqual(@"([a-zA-Z]\d)", regex.ToString());

            MatchCollection matches = regex.Matches("Class A1 f2 ZZ88");
            Assert.AreEqual(3, matches.Count);
            Assert.AreEqual("A1", matches[0].Value);
            Assert.AreEqual("f2", matches[1].Value);
            Assert.AreEqual("Z8", matches[2].Value);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNamedGroup()
        {
            var regex = new RegexBuilder()
                .LowercaseLetter(RegexQuantifier.OneOrMore)
                .StartNamedGroup("test123")
                    .Digit(RegexQuantifier.OneOrMore)
                .EndGroup()
                .LowercaseLetter(RegexQuantifier.OneOrMore)
                .BuildRegex();

            Assert.AreEqual(@"[a-z]+(?<test123>\d+)[a-z]+", regex.ToString());

            Match match = regex.Match("a99z");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("a99z", match.Groups[0].Value);
            Assert.AreEqual("99", match.Groups[1].Value);
            Assert.AreEqual("99", match.Groups["test123"].Value);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNonCapturingGroup()
        {
            var regex = new RegexBuilder()
                .LowercaseLetter(RegexQuantifier.OneOrMore)
                .StartNonCapturingGroup()
                    .Digit(RegexQuantifier.OneOrMore)
                .EndGroup()
                .LowercaseLetter(RegexQuantifier.OneOrMore)
                .BuildRegex();

            Assert.AreEqual(@"[a-z]+(?:\d+)[a-z]+", regex.ToString());

            Match match = regex.Match("a99z");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("a99z", match.Groups[0].Value);
            Assert.AreEqual(string.Empty, match.Groups[1].Value);
            Assert.AreEqual(1, match.Groups.Count);
            Assert.AreEqual("a99z", match.Captures[0].Value);
            Assert.AreEqual(1, match.Captures.Count);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestMultipleGroups()
        {
            var regex = new RegexBuilder()
                .StartGroup()
                    .AnyCharacter(RegexQuantifier.ZeroOrMore)
                .EndGroup()
                .StartGroup()
                    .Letter()
                    .Digit()
                .EndGroup()
                .BuildRegex();

            Assert.AreEqual(@"(.*)([a-zA-Z]\d)", regex.ToString());

            Match match = regex.Match("Class A1");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("Class A1", match.Groups[0].Value);
            Assert.AreEqual("Class ", match.Groups[1].Value);
            Assert.AreEqual("A1", match.Groups[2].Value);

            match = regex.Match("he likes F1 racing");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("he likes F1", match.Groups[0].Value);
            Assert.AreEqual("he likes ", match.Groups[1].Value);
            Assert.AreEqual("F1", match.Groups[2].Value);

            match = regex.Match("A4 paper");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("A4", match.Groups[0].Value);
            Assert.AreEqual(string.Empty, match.Groups[1].Value);
            Assert.AreEqual("A4", match.Groups[2].Value);

            match = regex.Match("A 4-legged dog");
            Assert.IsFalse(match.Success);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNestedGroups()
        {
            var regex = new RegexBuilder()
                .AnyCharacter() // Omit first character from groups
                .StartGroup()
                    .AnyCharacter(RegexQuantifier.ZeroOrMore)
                    .StartGroup()
                        .Letter()
                        .Digit()
                    .EndGroup()
                .EndGroup()
                .BuildRegex();

            Assert.AreEqual(@".(.*([a-zA-Z]\d))", regex.ToString());

            Match match = regex.Match("Class A1");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("Class A1", match.Groups[0].Value);
            Assert.AreEqual("lass A1", match.Groups[1].Value);
            Assert.AreEqual("A1", match.Groups[2].Value);

            match = regex.Match("he likes F1 racing");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("he likes F1", match.Groups[0].Value);
            Assert.AreEqual("e likes F1", match.Groups[1].Value);
            Assert.AreEqual("F1", match.Groups[2].Value);

            match = regex.Match(" A4 paper");
            Assert.IsTrue(match.Success);
            Assert.AreEqual(" A4", match.Groups[0].Value);
            Assert.AreEqual("A4", match.Groups[1].Value);
            Assert.AreEqual("A4", match.Groups[2].Value);

            match = regex.Match("A 4-legged dog");
            Assert.IsFalse(match.Success);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestZeroOrMore()
        {
            var regex = new RegexBuilder()
                .Letter()
                .Digit(RegexQuantifier.ZeroOrMore)
                .Letter()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z]\d*[a-zA-Z]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("ab"));
            Assert.IsTrue(regex.IsMatch("a1b"));
            Assert.IsTrue(regex.IsMatch("a123b"));
            Assert.IsFalse(regex.IsMatch("a 1 b"));
            Assert.IsFalse(regex.IsMatch("a b"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestOneOrMore()
        {
            var regex = new RegexBuilder()
                .Letter()
                .Digit(RegexQuantifier.OneOrMore)
                .Letter()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z]\d+[a-zA-Z]", regex.ToString());
            Assert.IsFalse(regex.IsMatch("ab"));
            Assert.IsTrue(regex.IsMatch("a1b"));
            Assert.IsTrue(regex.IsMatch("a123b"));
            Assert.IsFalse(regex.IsMatch("a 1 b"));
            Assert.IsFalse(regex.IsMatch("a b"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestOneOrNone()
        {
            var regex = new RegexBuilder()
                .Letter()
                .Digit(RegexQuantifier.NoneOrOne)
                .Letter()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z]\d?[a-zA-Z]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("ab"));
            Assert.IsTrue(regex.IsMatch("a1b"));
            Assert.IsFalse(regex.IsMatch("a123b"));
            Assert.IsFalse(regex.IsMatch("a 1 b"));
            Assert.IsFalse(regex.IsMatch("a b"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestExactlyNTimes()
        {
            var regex = new RegexBuilder()
                .Letter()
                .Digit(RegexQuantifier.Exactly(3))
                .Letter()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z]\d{3}[a-zA-Z]", regex.ToString());
            Assert.IsFalse(regex.IsMatch("ab"));
            Assert.IsFalse(regex.IsMatch("a1b"));
            Assert.IsFalse(regex.IsMatch("a12b"));
            Assert.IsTrue(regex.IsMatch("a123b"));
            Assert.IsFalse(regex.IsMatch("a1234b"));
            Assert.IsFalse(regex.IsMatch("a12345b"));
            Assert.IsFalse(regex.IsMatch("a 1 b"));
            Assert.IsFalse(regex.IsMatch("a b"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAtLeastQuantifier()
        {
            var regex = new RegexBuilder()
                .Letter()
                .Digit(RegexQuantifier.AtLeast(3))
                .Letter()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z]\d{3,}[a-zA-Z]", regex.ToString());
            Assert.IsFalse(regex.IsMatch("ab"));
            Assert.IsFalse(regex.IsMatch("a1b"));
            Assert.IsFalse(regex.IsMatch("a12b"));
            Assert.IsTrue(regex.IsMatch("a123b"));
            Assert.IsTrue(regex.IsMatch("a1234b"));
            Assert.IsTrue(regex.IsMatch("a12345b"));
            Assert.IsFalse(regex.IsMatch("a 1 b"));
            Assert.IsFalse(regex.IsMatch("a b"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNoMoreThanQuantifier()
        {
            var regex = new RegexBuilder()
                .Letter()
                .Digit(RegexQuantifier.NoMoreThan(3))
                .Letter()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z]\d{0,3}[a-zA-Z]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("ab"));
            Assert.IsTrue(regex.IsMatch("a1b"));
            Assert.IsTrue(regex.IsMatch("a12b"));
            Assert.IsTrue(regex.IsMatch("a123b"));
            Assert.IsFalse(regex.IsMatch("a1234b"));
            Assert.IsFalse(regex.IsMatch("a12345b"));
            Assert.IsFalse(regex.IsMatch("a 1 b"));
            Assert.IsFalse(regex.IsMatch("a b"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestBetweenMinMaxTimes()
        {
            var regex = new RegexBuilder()
                .Letter()
                .Digit(RegexQuantifier.Between(2, 4))
                .Letter()
                .BuildRegex();

            Assert.AreEqual(@"[a-zA-Z]\d{2,4}[a-zA-Z]", regex.ToString());
            Assert.IsFalse(regex.IsMatch("ab"));
            Assert.IsFalse(regex.IsMatch("a1b"));
            Assert.IsTrue(regex.IsMatch("a12b"));
            Assert.IsTrue(regex.IsMatch("a123b"));
            Assert.IsTrue(regex.IsMatch("a1234b"));
            Assert.IsFalse(regex.IsMatch("a12345b"));
            Assert.IsFalse(regex.IsMatch("a 1 b"));
            Assert.IsFalse(regex.IsMatch("a b"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestOptionMultiLine()
        {
            var regex = new RegexBuilder()
                .StartOfString()
                .Text("find me!")
                .EndOfString()
                .BuildRegex(RegexOptions.Multiline);

            Assert.AreEqual(@"^find me!$", regex.ToString());
            Assert.IsTrue(regex.Options.HasFlag(System.Text.RegularExpressions.RegexOptions.Multiline));
            Assert.IsTrue(regex.IsMatch("find me!"));
            Assert.IsTrue(regex.IsMatch("find me!\nline 2"));
            Assert.IsTrue(regex.IsMatch("line 1\nfind me!"));
            Assert.IsTrue(regex.IsMatch("line 1\nfind me!\nline 3"));
            Assert.IsFalse(regex.IsMatch(" find me!"));
            Assert.IsFalse(regex.IsMatch("find me! "));
            Assert.IsFalse(regex.IsMatch(" find me! "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestOptionIgnoreCase()
        {
            var regex = new RegexBuilder()
                .AnyCharacterFrom("cat")
                .BuildRegex(RegexOptions.IgnoreCase);

            Assert.AreEqual("[cat]", regex.ToString());
            Assert.IsTrue(regex.Options.HasFlag(System.Text.RegularExpressions.RegexOptions.IgnoreCase));
            Assert.IsTrue(regex.IsMatch("cat"));
            Assert.IsTrue(regex.IsMatch("tiger"));
            Assert.IsTrue(regex.IsMatch("Ant"));
            Assert.IsTrue(regex.IsMatch("CAT"));
            Assert.IsTrue(regex.IsMatch("                A"));
            Assert.IsFalse(regex.IsMatch("dog"));
            Assert.IsFalse(regex.IsMatch(" "));
            Assert.IsFalse(regex.IsMatch(string.Empty));

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestEmailAddress()
        {
            // Very basic e-mail address checker!
            var regex = new RegexBuilder()
                .StartOfString()
                .NonWhitespace(RegexQuantifier.AtLeast(2))
                .Text("@")
                .NonWhitespace(RegexQuantifier.AtLeast(2))
                .Text(".")
                .NonWhitespace(RegexQuantifier.AtLeast(2))
                .EndOfString()
                .BuildRegex();

            Assert.AreEqual(@"^\S{2,}@\S{2,}\.\S{2,}$", regex.ToString());
            Assert.IsTrue(regex.IsMatch("test.user@mainwave.co.uk"));
            Assert.IsTrue(regex.IsMatch("aa@bb.cc"));
            Assert.IsTrue(regex.IsMatch("__@__.__"));
            Assert.IsTrue(regex.IsMatch("..@....."));
            Assert.IsFalse(regex.IsMatch("aa@bb.c"));
            Assert.IsFalse(regex.IsMatch("aa@b.cc"));
            Assert.IsFalse(regex.IsMatch("a@bb.cc"));
            Assert.IsFalse(regex.IsMatch("a@b.c"));
            Assert.IsFalse(regex.IsMatch("  @  .  "));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestUrl()
        {
            // Very basic URL checker!
            var regex = new RegexBuilder()
                .Text("http")
                .Text("s", RegexQuantifier.NoneOrOne)
                .Text("://")
                .NonWhitespace(RegexQuantifier.OneOrMore)
                .AnyCharacterFrom("a-zA-Z0-9_/") // Valid last characters
                .BuildRegex();

            Assert.AreEqual(@"http(?:s)?://\S+[a-zA-Z0-9_/]", regex.ToString());
            Assert.IsTrue(regex.IsMatch("http://www.mainwave.co.uk"));
            Assert.IsTrue(regex.IsMatch("https://www.mainwave.co.uk"));
            Assert.IsFalse(regex.IsMatch("www.mainwave.co.uk"));
            Assert.IsFalse(regex.IsMatch("ftp://www.mainwave.co.uk"));

            Match match = regex.Match("Go to http://www.mainwave.co.uk. Then click the link.");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("http://www.mainwave.co.uk", match.Value);

            match = regex.Match("Go to https://www.mainwave.co.uk/test/, then click the link.");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("https://www.mainwave.co.uk/test/", match.Value);

            match = regex.Match("Go to 'http://www.mainwave.co.uk' then click the link.");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("http://www.mainwave.co.uk", match.Value);

            match = regex.Match("Go to \"http://www.mainwave.co.uk\" then click the link.");
            Assert.IsTrue(match.Success);
            Assert.AreEqual("http://www.mainwave.co.uk", match.Value);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestIp4Address()
        {
            // Very basic IPv4 address checker!
            // (doesn't check values are in range, for example)
            var regex = new RegexBuilder()
                .StartOfString()
                .StartGroup()
                    .Digit(RegexQuantifier.Between(1, 3))
                    .Text(".")
                .EndGroup(RegexQuantifier.Exactly(3))
                .Digit(RegexQuantifier.Between(1, 3))
                .EndOfString()
                .BuildRegex();

            Assert.AreEqual(@"^(\d{1,3}\.){3}\d{1,3}$", regex.ToString());
            Assert.IsTrue(regex.IsMatch("10.1.1.100"));
            Assert.IsTrue(regex.IsMatch("1.1.1.1"));
            Assert.IsTrue(regex.IsMatch("0.0.0.0"));
            Assert.IsTrue(regex.IsMatch("255.255.255.255"));
            Assert.IsTrue(regex.IsMatch("999.999.999.999"));
            Assert.IsFalse(regex.IsMatch("1.1.1."));
            Assert.IsFalse(regex.IsMatch("1.1.1."));
            Assert.IsFalse(regex.IsMatch("1.1.1.1."));
            Assert.IsFalse(regex.IsMatch("1.1.1.1.1"));
            Assert.IsFalse(regex.IsMatch("1.1.1.1000"));

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsFalse(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsFalse(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsFalse(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestExceptionGroupMismatch1()
        {
            RegexBuilderException exception = null;

            try
            {
                new RegexBuilder()
                    .EndGroup()
                    .BuildRegex();
            }
            catch (RegexBuilderException e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception);
            Assert.AreEqual(string.Empty, exception.Regex);
        }

        [Test]
        public void TestExceptionGroupMismatch2()
        {
            RegexBuilderException exception = null;

            try
            {
                new RegexBuilder()
                    .StartGroup()
                    .BuildRegex();
            }
            catch (RegexBuilderException e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception);
            Assert.AreEqual("(", exception.Regex);
        }

        [Test]
        public void TestExceptionGroupMismatch3()
        {
            RegexBuilderException exception = null;

            try
            {
                new RegexBuilder()
                    .StartGroup()
                    .StartGroup()
                    .EndGroup()
                    .BuildRegex();
            }
            catch (RegexBuilderException e)
            {
                exception = e;
            }
            Assert.IsNotNull(exception);
            Assert.AreEqual("(()", exception.Regex);
        }

        [Test]
        public void TestZeroOrMoreButAsFewAsPossible()
        {
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.ZeroOrMore.ButAsFewAsPossible)
                .BuildRegex();

            Assert.AreEqual(@"\d*?", regex.ToString());
            var nonGreedyMatch = regex.Match("999");
            Assert.IsTrue(nonGreedyMatch.Success);
            Assert.AreEqual(string.Empty, nonGreedyMatch.Value);

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsTrue(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestOneOrMoreButAsFewAsPossible()
        {
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.OneOrMore.ButAsFewAsPossible)
                .BuildRegex();

            Assert.AreEqual(@"\d+?", regex.ToString());
            var nonGreedyMatch = regex.Match("999");
            Assert.IsTrue(nonGreedyMatch.Success);
            Assert.AreEqual("9", nonGreedyMatch.Value);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestAtLeastButAsFewAsPossible()
        {
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.AtLeast(1).ButAsFewAsPossible)
                .BuildRegex();

            Assert.AreEqual(@"\d{1,}?", regex.ToString());
            var nonGreedyMatch = regex.Match("999");
            Assert.IsTrue(nonGreedyMatch.Success);
            Assert.AreEqual("9", nonGreedyMatch.Value);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestBetweenButAsFewAsPossible()
        {
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.Between(2, 100).ButAsFewAsPossible)
                .BuildRegex();

            Assert.AreEqual(@"\d{2,100}?", regex.ToString());
            var nonGreedyMatch = regex.Match("999");
            Assert.IsTrue(nonGreedyMatch.Success);
            Assert.AreEqual("99", nonGreedyMatch.Value);

            Assert.IsFalse(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsFalse(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsFalse(regex.IsMatch(Strings.Symbols));
            Assert.IsFalse(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsFalse(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsFalse(regex.IsMatch(Strings.Empty));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleName));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsFalse(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNoMoreThanButAsFewAsPossible()
        {
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.NoMoreThan(2).ButAsFewAsPossible)
                .BuildRegex();

            Assert.AreEqual(@"\d{0,2}?", regex.ToString());
            var nonGreedyMatch = regex.Match("999");
            Assert.IsTrue(nonGreedyMatch.Success);
            Assert.AreEqual(string.Empty, nonGreedyMatch.Value);

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsTrue(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }

        [Test]
        public void TestNoneOrOneButAsFewAsPossible()
        {
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.NoneOrOne.ButAsFewAsPossible)
                .BuildRegex();

            Assert.AreEqual(@"\d??", regex.ToString());
            var nonGreedyMatch = regex.Match("999");
            Assert.IsTrue(nonGreedyMatch.Success);
            Assert.AreEqual(string.Empty, nonGreedyMatch.Value);

            Assert.IsTrue(regex.IsMatch(Strings.BothCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseAlphabet));
            Assert.IsTrue(regex.IsMatch(Strings.DecimalDigits));
            Assert.IsTrue(regex.IsMatch(Strings.BothCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.UpperCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.LowerCaseHexDigits));
            Assert.IsTrue(regex.IsMatch(Strings.Symbols));
            Assert.IsTrue(regex.IsMatch(Strings.WhiteSpace));
            Assert.IsTrue(regex.IsMatch(Strings.ControlCharacters));
            Assert.IsTrue(regex.IsMatch(Strings.Empty));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleName));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleEmailAddress));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpUrl));
            Assert.IsTrue(regex.IsMatch(Strings.SimpleHttpsUrl));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv4Address));
            Assert.IsTrue(regex.IsMatch(Strings.Ipv6Address));
            Assert.IsTrue(regex.IsMatch(Strings.MacAddress));
        }
    }
}
