using NUnit.Framework;
using static RegexToolbox.RegexQuantifier;

namespace RegexToolbox.Tests
{
    [TestFixture]
    public class RegexQuantifierTests
    {
        [Test]
        public void TestOneOrMoreQuantifier()
        {
            var regexOneOrMore = new RegexBuilder().Text("a", OneOrMore).BuildRegex();
            var regexOneOrMoreButAsFewAsPossible = new RegexBuilder().Text("a", OneOrMore.ButAsFewAsPossible).BuildRegex();

            Assert.That(regexOneOrMore.ToString(), Is.EqualTo(@"(?:a)+"));
            Assert.That(regexOneOrMoreButAsFewAsPossible.ToString(), Is.EqualTo(@"(?:a)+?"));
        }

        [Test]
        public void TestZeroOrMoreQuantifier()
        {
            var regexZeroOrMore = new RegexBuilder().Text("a", ZeroOrMore).BuildRegex();
            var regexZeroOrMoreButAsFewAsPossible = new RegexBuilder().Text("a", ZeroOrMore.ButAsFewAsPossible).BuildRegex();

            Assert.That(regexZeroOrMore.ToString(), Is.EqualTo(@"(?:a)*"));
            Assert.That(regexZeroOrMoreButAsFewAsPossible.ToString(), Is.EqualTo(@"(?:a)*?"));
        }

        [Test]
        public void TestZeroOrOneQuantifier()
        {
            var regexZeroOrOne = new RegexBuilder().Text("a", ZeroOrOne).BuildRegex();
            var regexZeroOrOneButAsFewAsPossible = new RegexBuilder().Text("a", ZeroOrOne.ButAsFewAsPossible).BuildRegex();

            Assert.That(regexZeroOrOne.ToString(), Is.EqualTo(@"(?:a)?"));
            Assert.That(regexZeroOrOneButAsFewAsPossible.ToString(), Is.EqualTo(@"(?:a)??"));
        }

        [Test]
        public void TestAtLeastQuantifier()
        {
            var regexAtLeast = new RegexBuilder().Text("a", AtLeast(1)).BuildRegex();
            var regexAtLeastButAsFewAsPossible = new RegexBuilder().Text("a", AtLeast(1).ButAsFewAsPossible).BuildRegex();

            Assert.That(regexAtLeast.ToString(), Is.EqualTo(@"(?:a){1,}"));
            Assert.That(regexAtLeastButAsFewAsPossible.ToString(), Is.EqualTo(@"(?:a){1,}?"));
        }

        [Test]
        public void TestNoMoreThanQuantifier()
        {
            var regexNoMoreThan = new RegexBuilder().Text("a", NoMoreThan(2)).BuildRegex();
            var regexNoMoreThanButAsFewAsPossible = new RegexBuilder().Text("a", NoMoreThan(2).ButAsFewAsPossible).BuildRegex();

            Assert.That(regexNoMoreThan.ToString(), Is.EqualTo(@"(?:a){0,2}"));
            Assert.That(regexNoMoreThanButAsFewAsPossible.ToString(), Is.EqualTo(@"(?:a){0,2}?"));
        }

        [Test]
        public void TestBetweenQuantifier()
        {
            var regexBetween = new RegexBuilder().Text("a", Between(1,2)).BuildRegex();
            var regexBetweenButAsFewAsPossible = new RegexBuilder().Text("a", Between(1,2).ButAsFewAsPossible).BuildRegex();

            Assert.That(regexBetween.ToString(), Is.EqualTo(@"(?:a){1,2}"));
            Assert.That(regexBetweenButAsFewAsPossible.ToString(), Is.EqualTo(@"(?:a){1,2}?"));
        }

        [Test]
        public void TestExactlyQuantifier()
        {
            var regexExactly = new RegexBuilder().Text("a", Exactly(2)).BuildRegex();

            Assert.That(regexExactly.ToString(), Is.EqualTo(@"(?:a){2}"));
        }
    }
}
