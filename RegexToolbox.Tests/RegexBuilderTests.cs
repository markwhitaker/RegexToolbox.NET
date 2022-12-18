using NUnit.Framework;
using static RegexToolbox.RegexOptions;

namespace RegexToolbox.Tests;

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
    public void TestCompiled()
    {
        var regex = new RegexBuilder()
            .AnyCharacter()
            .BuildRegex(Compiled);

        Assert.That(regex.Options, Is.EqualTo(System.Text.RegularExpressions.RegexOptions.Compiled));
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
    public void TestAllOptions()
    {
        const System.Text.RegularExpressions.RegexOptions expectedOptions =
            System.Text.RegularExpressions.RegexOptions.Compiled |
            System.Text.RegularExpressions.RegexOptions.IgnoreCase |
            System.Text.RegularExpressions.RegexOptions.Multiline;

        var regex = new RegexBuilder()
            .AnyCharacter()
            .BuildRegex(Compiled, IgnoreCase, Multiline);

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

    [Test]
    public void TestEscapeCharacters()
    {
        var regex = new RegexBuilder()
            .Text(@"\?.+*^$()[]{}|")
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\\\?\.\+\*\^\$\(\)\[\]\{\}\|"));
    }
}
