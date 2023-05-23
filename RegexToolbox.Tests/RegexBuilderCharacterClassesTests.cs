using System;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using static RegexToolbox.RegexQuantifier;

namespace RegexToolbox.Tests;

[TestFixture]
public class RegexBuilderCharacterClassesTests
{
    [Test]
    public void TestText()
    {
        var regex = new RegexBuilder()
            .Text("a*b")
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"a\*b"));
    }

    [Test]
    public void TestTextWithQuantifier()
    {
        var regex = new RegexBuilder()
            .Text("a*b", OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a\*b)+"));
    }

    [Test]
    public void TestRegexText()
    {
        var regex = new RegexBuilder()
            .RegexText("a*b")
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"a*b"));
    }

    [Test]
    public void TestRegexTextWithQuantifier()
    {
        var regex = new RegexBuilder()
            .RegexText("a*b", OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a*b)+"));
    }

    [Test]
    public void TestAnyCharacter()
    {
        var regex = new RegexBuilder()
            .AnyCharacter()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"."));
    }

    [Test]
    public void TestAnyCharacterWithQuantifier()
    {
        var regex = new RegexBuilder()
            .AnyCharacter(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@".+"));
    }

    [Test]
    public void TestWhitespace()
    {
        var regex = new RegexBuilder()
            .Whitespace()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\s"));
    }

    [Test]
    public void TestWhitespaceWithQuantifier()
    {
        var regex = new RegexBuilder()
            .Whitespace(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\s+"));
    }

    [Test]
    public void TestNonWhitespace()
    {
        var regex = new RegexBuilder()
            .NonWhitespace()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\S"));
    }

    [Test]
    public void TestNonWhitespaceWithQuantifier()
    {
        var regex = new RegexBuilder()
            .NonWhitespace(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\S+"));
    }

    [Test]
    public void TestPossibleWhitespace()
    {
        var regex = new RegexBuilder()
            .PossibleWhitespace()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\s*"));
    }

    [Test]
    public void TestSpace()
    {
        var regex = new RegexBuilder()
            .Space()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@" "));
    }

    [Test]
    public void TestSpaceWithQuantifier()
    {
        var regex = new RegexBuilder()
            .Space(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@" +"));
    }

    [Test]
    public void TestTab()
    {
        var regex = new RegexBuilder()
            .Tab()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\t"));
    }

    [Test]
    public void TestTabWithQuantifier()
    {
        var regex = new RegexBuilder()
            .Tab(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\t+"));
    }

    [Test]
    public void TestLineFeed()
    {
        var regex = new RegexBuilder()
            .LineFeed()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\n"));
    }

    [Test]
    public void TestLineFeedWithQuantifier()
    {
        var regex = new RegexBuilder()
            .LineFeed(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\n+"));
    }

    [Test]
    public void TestCarriageReturn()
    {
        var regex = new RegexBuilder()
            .CarriageReturn()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\r"));
    }

    [Test]
    public void TestCarriageReturnWithQuantifier()
    {
        var regex = new RegexBuilder()
            .CarriageReturn(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\r+"));
    }

    [Test]
    public void TestDigit()
    {
        var regex = new RegexBuilder()
            .Digit()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\d"));
    }

    [Test]
    public void TestDigitWithQuantifier()
    {
        var regex = new RegexBuilder()
            .Digit(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\d+"));
    }

    [Test]
    public void TestNonDigit()
    {
        var regex = new RegexBuilder()
            .NonDigit()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\D"));
    }

    [Test]
    public void TestNonDigitWithQuantifier()
    {
        var regex = new RegexBuilder()
            .NonDigit(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\D+"));
    }

    [Test]
    public void TestLetter()
    {
        var regex = new RegexBuilder()
            .Letter()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\p{L}"));
    }

    [Test]
    public void TestLetterWithQuantifier()
    {
        var regex = new RegexBuilder()
            .Letter(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\p{L}+"));
    }

    [Test]
    public void TestNonLetter()
    {
        var regex = new RegexBuilder()
            .NonLetter()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\P{L}"));
    }

    [Test]
    public void TestNonLetterWithQuantifier()
    {
        var regex = new RegexBuilder()
            .NonLetter(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\P{L}+"));
    }

    [Test]
    public void TestUppercaseLetter()
    {
        var regex = new RegexBuilder()
            .UppercaseLetter()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\p{Lu}"));
    }

    [Test]
    public void TestUppercaseLetterWithQuantifier()
    {
        var regex = new RegexBuilder()
            .UppercaseLetter(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\p{Lu}+"));
    }

    [Test]
    public void TestLowercaseLetter()
    {
        var regex = new RegexBuilder()
            .LowercaseLetter()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\p{Ll}"));
    }

    [Test]
    public void TestLowercaseLetterWithQuantifier()
    {
        var regex = new RegexBuilder()
            .LowercaseLetter(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"\p{Ll}+"));
    }

    [Test]
    public void TestLetterOrDigit()
    {
        var regex = new RegexBuilder()
            .LetterOrDigit()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[\p{L}0-9]"));
    }

    [Test]
    public void TestLetterOrDigitWithQuantifier()
    {
        var regex = new RegexBuilder()
            .LetterOrDigit(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[\p{L}0-9]+"));
    }

    [Test]
    public void TestNonLetterOrDigit()
    {
        var regex = new RegexBuilder()
            .NonLetterOrDigit()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[^\p{L}0-9]"));
    }

    [Test]
    public void TesNonLetterOrDigitWithQuantifier()
    {
        var regex = new RegexBuilder()
            .NonLetterOrDigit(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[^\p{L}0-9]+"));
    }

    [Test]
    public void TestHexDigit()
    {
        var regex = new RegexBuilder()
            .HexDigit()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[0-9A-Fa-f]"));
    }

    [Test]
    public void TestHexDigitWithQuantifier()
    {
        var regex = new RegexBuilder()
            .HexDigit(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[0-9A-Fa-f]+"));
    }

    [Test]
    public void TestUppercaseHexDigit()
    {
        var regex = new RegexBuilder()
            .UppercaseHexDigit()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[0-9A-F]"));
    }

    [Test]
    public void TestUppercaseHexDigitWithQuantifier()
    {
        var regex = new RegexBuilder()
            .UppercaseHexDigit(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[0-9A-F]+"));
    }

    [Test]
    public void TestLowercaseHexDigit()
    {
        var regex = new RegexBuilder()
            .LowercaseHexDigit()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[0-9a-f]"));
    }

    [Test]
    public void TestLowercaseHexDigitWithQuantifier()
    {
        var regex = new RegexBuilder()
            .LowercaseHexDigit(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[0-9a-f]+"));
    }

    [Test]
    public void TestNonHexDigit()
    {
        var regex = new RegexBuilder()
            .NonHexDigit()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[^0-9A-Fa-f]"));
    }

    [Test]
    public void TestNonHexDigitWithQuantifier()
    {
        var regex = new RegexBuilder()
            .NonHexDigit(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[^0-9A-Fa-f]+"));
    }

    [Test]
    public void TestWordCharacter()
    {
        var regex = new RegexBuilder()
            .WordCharacter()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[\p{L}0-9_]"));
    }

    [Test]
    public void TestWordCharacterWithQuantifier()
    {
        var regex = new RegexBuilder()
            .WordCharacter(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[\p{L}0-9_]+"));
    }

    [Test]
    public void TestNonWordCharacter()
    {
        var regex = new RegexBuilder()
            .NonWordCharacter()
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[^\p{L}0-9_]"));
    }

    [Test]
    public void TestNonWordCharacterWithQuantifier()
    {
        var regex = new RegexBuilder()
            .NonWordCharacter(OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[^\p{L}0-9_]+"));
    }

    [Test]
    public void TestAnyCharacterFrom()
    {
        var regex = new RegexBuilder()
            .AnyCharacterFrom("^]")
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[\^\]]"));
    }

    [Test]
    public void TestAnyCharacterFromWithQuantifier()
    {
        var regex = new RegexBuilder()
            .AnyCharacterFrom("^]", OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[\^\]]+"));
    }

    [Test]
    public void TestAnyCharacterExcept()
    {
        var regex = new RegexBuilder()
            .AnyCharacterExcept("^]")
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[^\^\]]"));
    }

    [Test]
    public void TestAnyCharacterExceptWithQuantifier()
    {
        var regex = new RegexBuilder()
            .AnyCharacterExcept("^]", OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"[^\^\]]+"));
    }

    [Test]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    public void TestAnyOfWithNullStringEnumerable()
    {
        string[] strings = null;

        var exception = Assert.Throws<ArgumentNullException>(() => new RegexBuilder()
            .AnyOf(strings)
            .BuildRegex());
        Assert.That(exception, Is.Not.Null);
    }

    [Test]
    public void TestAnyOfWithEmptyStringEnumerable()
    {
        string[] strings = {};

        var exception = Assert.Throws<ArgumentException>(() => new RegexBuilder()
            .AnyOf(strings)
            .BuildRegex());
        Assert.That(exception, Is.Not.Null);
    }

    [Test]
    public void TestAnyOfWithSingleItemStringEnumerable()
    {
        string[] strings = { "a" };

        var regex = new RegexBuilder()
            .AnyOf(strings)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a)"));
    }

    [Test]
    public void TestAnyOfWithSingleItemStringEnumerableAndQuantifier()
    {
        string[] strings = { "a" };

        var regex = new RegexBuilder()
            .AnyOf(strings, OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a)+"));
    }

    [Test]
    public void TestAnyOfWithMultipleItemStringEnumerable()
    {
        string[] strings = { "a", "b" };

        var regex = new RegexBuilder()
            .AnyOf(strings)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a|b)"));
    }

    [Test]
    public void TestAnyOfWithMultipleItemStringEnumerableAndQuantifier()
    {
        string[] strings = { "a", "b" };

        var regex = new RegexBuilder()
            .AnyOf(strings, OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a|b)+"));
    }

    [Test]
    public void TestAnyOfWithSingleStringArgument()
    {
        var regex = new RegexBuilder()
            .AnyOf("a")
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a)"));
    }

    [Test]
    public void TestAnyOfWithMultipleStringArguments()
    {
        var regex = new RegexBuilder()
            .AnyOf("a", "b")
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a|b)"));
    }

    [Test]
    [SuppressMessage("ReSharper", "ExpressionIsAlwaysNull")]
    public void TestAnyOfWithNullSubRegexBuilderEnumerable()
    {
        RegexBuilder.SubRegexBuilder[] subRegexBuilders = null;

        var exception = Assert.Throws<ArgumentNullException>(() => new RegexBuilder()
            .AnyOf(subRegexBuilders)
            .BuildRegex());
        Assert.That(exception, Is.Not.Null);
    }

    [Test]
    public void TestAnyOfWithEmptySubRegexBuilderEnumerable()
    {
        RegexBuilder.SubRegexBuilder[] subRegexBuilders = {};

        var exception = Assert.Throws<ArgumentException>(() => new RegexBuilder()
            .AnyOf(subRegexBuilders)
            .BuildRegex());
        Assert.That(exception, Is.Not.Null);
    }

    [Test]
    public void TestAnyOfWithSingleItemSubRegexBuilderEnumerable()
    {
        RegexBuilder.SubRegexBuilder[] subRegexBuilders = { r => r.Text("a") };

        var regex = new RegexBuilder()
            .AnyOf(subRegexBuilders)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a)"));
    }

    [Test]
    public void TestAnyOfWithSingleItemSubRegexBuilderEnumerableAndQuantifier()
    {
        RegexBuilder.SubRegexBuilder[] subRegexBuilders = { r => r.Text("a") };

        var regex = new RegexBuilder()
            .AnyOf(subRegexBuilders, OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a)+"));
    }

    [Test]
    public void TestAnyOfWithMultipleItemSubRegexBuilderEnumerable()
    {
        RegexBuilder.SubRegexBuilder[] subRegexBuilders =
        {
            r => r.Text("a"),
            r => r.Text("b")
        };

        var regex = new RegexBuilder()
            .AnyOf(subRegexBuilders)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a|b)"));
    }

    [Test]
    public void TestAnyOfWithMultipleItemSubRegexBuilderEnumerableAndQuantifier()
    {
        RegexBuilder.SubRegexBuilder[] subRegexBuilders =
        {
            r => r.Text("a"),
            r => r.Text("b")
        };

        var regex = new RegexBuilder()
            .AnyOf(subRegexBuilders, OneOrMore)
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a|b)+"));
    }

    [Test]
    public void TestAnyOfWithSingleSubRegexBuilderArgument()
    {
        var regex = new RegexBuilder()
            .AnyOf(r => r.Text("a"))
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a)"));
    }

    [Test]
    public void TestAnyOfWithMultipleSubRegexBuilderArguments()
    {
        var regex = new RegexBuilder()
            .AnyOf(
                r => r.Text("a"),
                r => r.Text("b"))
            .BuildRegex();

        Assert.That(regex.ToString(), Is.EqualTo(@"(?:a|b)"));
    }
}
