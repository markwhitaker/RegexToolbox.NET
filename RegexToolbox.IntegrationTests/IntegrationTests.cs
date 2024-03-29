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
            .Text("Hello")
            .NewLine(RegexQuantifier.Exactly(2))
            .Text("friends")
            .BuildRegex();

        Assert.That(regex.IsMatch(TestString), Is.True);
    }
}
