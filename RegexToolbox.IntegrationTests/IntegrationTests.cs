namespace RegexToolbox.IntegrationTests;

public class IntegrationTests
{
    private const string TestString =
"""
Hello

friends
""";

    [Test]
    public void Test1()
    {
        var regex = new RegexBuilder()
            .StartOfString()
            .Text("Hello")
            .NewLine(RegexQuantifier.Exactly(2))
            .Text("friends")
            .EndOfString()
            .BuildRegex();

        Assert.That(TestString, Does.Match(regex));
    }
}
