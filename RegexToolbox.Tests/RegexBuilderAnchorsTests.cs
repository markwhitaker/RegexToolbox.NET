namespace RegexToolbox.Tests;

[TestFixture]
public class RegexBuilderAnchorsTests
{
    [Test]
    public void TestStartOfString()
    {
        var regex = new RegexBuilder()
            .StartOfString()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"^"));
    }

    [Test]
    public void TestEndOfString()
    {
        var regex = new RegexBuilder()
            .EndOfString()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"$"));
    }

    [Test]
    public void TestWordBoundary()
    {
        var regex = new RegexBuilder()
            .WordBoundary()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\b"));
    }
}
