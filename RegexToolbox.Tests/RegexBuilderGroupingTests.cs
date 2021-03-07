using NUnit.Framework;
using static RegexToolbox.RegexQuantifier;

namespace RegexToolbox.Tests
{
    [TestFixture]
    public class RegexBuilderGroupingTests
    {
        [Test]
        public void TestGroup()
        {
            var regex = new RegexBuilder()
                .Group(r => r
                    .Text("a")
                    .Digit())
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(a\d)"));
        }

        [Test]
        public void TestGroupWithQuantifier()
        {
            var regex = new RegexBuilder()
                .Group(r => r
                    .Text("a")
                    .Digit(),
                    OneOrMore)
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(a\d)+"));
        }

        [Test]
        public void TestNestedGroup()
        {
            var regex = new RegexBuilder()
                .Group(r1 => r1
                    .Text("a")
                    .Group(r2 => r2
                        .Digit())
                    )
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(a(\d))"));
        }

        [Test]
        public void TestNonCapturingGroup()
        {
            var regex = new RegexBuilder()
                .NonCapturingGroup(r => r
                    .Text("a")
                    .Digit())
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(?:a\d)"));
        }

        [Test]
        public void TestNonCapturingGroupWithQuantifier()
        {
            var regex = new RegexBuilder()
                .NonCapturingGroup(r => r
                    .Text("a")
                    .Digit(),
                    OneOrMore)
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(?:a\d)+"));
        }

        [Test]
        public void TestNestedNonCapturingGroup()
        {
            var regex = new RegexBuilder()
                .NonCapturingGroup(r1 => r1
                    .Text("a")
                    .NonCapturingGroup(r2 => r2
                        .Digit())
                    )
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(?:a(?:\d))"));
        }

        [Test]
        public void TestNamedGroup()
        {
            var regex = new RegexBuilder()
                .NamedGroup("group", r => r
                    .Text("a")
                    .Digit())
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(?<group>a\d)"));
        }

        [Test]
        public void TestNamedGroupWithQuantifier()
        {
            var regex = new RegexBuilder()
                .NamedGroup("group", r => r
                    .Text("a")
                    .Digit(),
                    OneOrMore)
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(?<group>a\d)+"));
        }

        [Test]
        public void TestNestedNamedGroup()
        {
            var regex = new RegexBuilder()
                .NamedGroup("group1", r1 => r1
                    .Text("a")
                    .NamedGroup("group2", r2 => r2
                        .Digit())
                    )
                .BuildRegex();

            Assert.That(regex.ToString(), Is.EqualTo(@"(?<group1>a(?<group2>\d))"));
        }
    }
}
