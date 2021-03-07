using NUnit.Framework;
using static RegexToolbox.RegexOptions;

namespace RegexToolbox.Tests
{
    [TestFixture]
    public class RegexBuilderTests
    {
        [Test]
        public void TestNoOptions()
        {
            var regex = new RegexBuilder()
                .AnyCharacter()
                .BuildRegex();

            Assert.That(regex.Options, Is.EqualTo(System.Text.RegularExpressions.RegexOptions.None));
        }

        [Test]
        public void TestIgnoreCase()
        {
            var regex = new RegexBuilder()
                .AnyCharacter()
                .BuildRegex(IgnoreCase);

            Assert.That(regex.Options, Is.EqualTo(System.Text.RegularExpressions.RegexOptions.IgnoreCase));
        }

        [Test]
        public void TestMultiline()
        {
            var regex = new RegexBuilder()
                .AnyCharacter()
                .BuildRegex(Multiline);

            Assert.That(regex.Options, Is.EqualTo(System.Text.RegularExpressions.RegexOptions.Multiline));
        }

        [Test]
        public void TestIgnoreCaseAndMultiline()
        {
            const System.Text.RegularExpressions.RegexOptions expectedOptions =
                System.Text.RegularExpressions.RegexOptions.IgnoreCase |
                System.Text.RegularExpressions.RegexOptions.Multiline;

            var regex = new RegexBuilder()
                .AnyCharacter()
                .BuildRegex(IgnoreCase, Multiline);

            Assert.That(regex.Options, Is.EqualTo(expectedOptions));
        }

        [Test]
        public void TestToString()
        {
            var s = new RegexBuilder()
                .Text("test*")
                .ToString();

            Assert.That(s, Is.EqualTo(@"test\*"));
        }
    }
}
