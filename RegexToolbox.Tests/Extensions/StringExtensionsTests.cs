namespace RegexToolbox.Tests.Extensions;

[TestFixture]
public class StringExtensionsTests
{
    [Test]
    public void GivenInputStringContainingOneRegexMatch_WhenRemove_ThenMatchIsRemovedFromString()
    {
        // Given
        const string input = "Hello there friendly world";
        var regex = new RegexBuilder()
            .Whitespace()
            .Text("there")
            .BuildRegex();

        // When
        var actualResult = input.Remove(regex);

        // Then
        Assert.That(actualResult, Is.EqualTo("Hello friendly world"));
    }

    [Test]
    public void GivenInputStringContainingMultipleRegexMatches_WhenRemove_ThenAllMatchesAreRemovedFromString()
    {
        // Given
        const string input = "Hello there friendly world";
        var regex = new RegexBuilder()
            .Whitespace()
            .Letter(RegexQuantifier.OneOrMore)
            .BuildRegex();

        // When
        var actualResult = input.Remove(regex);

        // Then
        Assert.That(actualResult, Is.EqualTo("Hello"));
    }

    [Test]
    public void GivenInputStringContainingMultipleRegexMatches_WhenRemoveFirst_ThenFirstMatchIsRemovedFromString()
    {
        // Given
        const string input = "Hello there friendly world";
        var regex = new RegexBuilder()
            .Whitespace()
            .Letter(RegexQuantifier.OneOrMore)
            .BuildRegex();

        // When
        var actualResult = input.RemoveFirst(regex);

        // Then
        Assert.That(actualResult, Is.EqualTo("Hello friendly world"));
    }

    [Test]
    public void GivenInputStringContainingMultipleRegexMatches_WhenRemoveLast_ThenLastMatchIsRemovedFromString()
    {
        // Given
        const string input = "Hello there friendly world";
        var regex = new RegexBuilder()
            .Whitespace()
            .Letter(RegexQuantifier.OneOrMore)
            .BuildRegex();

        // When
        var actualResult = input.RemoveLast(regex);

        // Then
        Assert.That(actualResult, Is.EqualTo("Hello there friendly"));
    }

    [TestCase("Hello world")]
    [TestCase("")]
    public void GivenInputStringContainingNoRegexMatches_WhenRemove_ThenStringIsUnaltered(string input)
    {
        // Given
        var regex = new RegexBuilder()
            .Digit(RegexQuantifier.OneOrMore)
            .BuildRegex();

        // When
        var actualResult = input.Remove(regex);

        // Then
        Assert.That(actualResult, Is.SameAs(input));
    }

    [TestCase("Hello world")]
    [TestCase("")]
    public void GivenInputStringContainingNoRegexMatches_WhenRemoveFirst_ThenStringIsUnaltered(string input)
    {
        // Given
        var regex = new RegexBuilder()
            .Digit(RegexQuantifier.OneOrMore)
            .BuildRegex();

        // When
        var actualResult = input.RemoveFirst(regex);

        // Then
        Assert.That(actualResult, Is.SameAs(input));
    }

    [TestCase("Hello world")]
    [TestCase("")]
    public void GivenInputStringContainingNoRegexMatches_WhenRemoveLast_ThenStringIsUnaltered(string input)
    {
        // Given
        var regex = new RegexBuilder()
            .Digit(RegexQuantifier.OneOrMore)
            .BuildRegex();

        // When
        var actualResult = input.RemoveLast(regex);

        // Then
        Assert.That(actualResult, Is.SameAs(input));
    }

    [Test]
    public void GivenNullInputString_WhenRemove_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = null;
        var regex = new RegexBuilder()
            .Digit()
            .BuildRegex();

        // When/Then
        var exception = Assert.Throws<ArgumentNullException>(() => input.Remove(regex));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("input"));
    }

    [Test]
    public void GivenNullInputString_WhenRemoveFirst_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = null;
        var regex = new RegexBuilder()
            .Digit()
            .BuildRegex();

        // When/Then
        var exception = Assert.Throws<ArgumentNullException>(() => input.RemoveFirst(regex));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("input"));
    }

    [Test]
    public void GivenNullInputString_WhenRemoveLast_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = null;
        var regex = new RegexBuilder()
            .Digit()
            .BuildRegex();

        // When/Then
        var exception = Assert.Throws<ArgumentNullException>(() => input.RemoveLast(regex));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("input"));
    }

    [Test]
    public void GivenNullRegex_WhenRemove_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = "Hello world";
        Regex regex = null;

        // When/Then
        // ReSharper disable once ExpressionIsAlwaysNull
        var exception = Assert.Throws<ArgumentNullException>(() => input.Remove(regex));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("regex"));
    }

    [Test]
    public void GivenNullRegex_WhenRemoveFirst_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = "Hello world";
        Regex regex = null;

        // When/Then
        // ReSharper disable once ExpressionIsAlwaysNull
        var exception = Assert.Throws<ArgumentNullException>(() => input.RemoveFirst(regex));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("regex"));
    }

    [Test]
    public void GivenNullRegex_WhenRemoveLast_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = "Hello world";
        Regex regex = null;

        // When/Then
        // ReSharper disable once ExpressionIsAlwaysNull
        var exception = Assert.Throws<ArgumentNullException>(() => input.RemoveLast(regex));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("regex"));
    }

    [Test]
    public void GivenInputStringContainingMultipleRegexMatches_WhenReplace_ThenAllMatchesAreReplacedInString()
    {
        // Given
        const string input = "Hello there friendly world";
        var regex = new RegexBuilder()
            .Letter(RegexQuantifier.OneOrMore)
            .BuildRegex();

        // When
        var actualResult = input.Replace(regex, "x");

        // Then
        Assert.That(actualResult, Is.EqualTo("x x x x"));
    }

    [Test]
    public void GivenInputStringContainingNoRegexMatches_WhenReplace_ThenStringIsUnaltered()
    {
        // Given
        const string input = "Hello there friendly world";
        var regex = new RegexBuilder()
            .Digit()
            .BuildRegex();

        // When
        var actualResult = input.Replace(regex, "x");

        // Then
        Assert.That(actualResult, Is.EqualTo("Hello there friendly world"));
    }

    [Test]
    public void GivenNullInputString_WhenReplace_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = null;
        var regex = new RegexBuilder()
            .Digit()
            .BuildRegex();

        // When/Then
        var exception = Assert.Throws<ArgumentNullException>(() => input.Replace(regex, "x"));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("input"));
    }

    [Test]
    public void GivenNullRegex_WhenReplace_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = "Hello world";
        Regex regex = null;

        // When/Then
        // ReSharper disable once ExpressionIsAlwaysNull
        var exception = Assert.Throws<ArgumentNullException>(() => input.Replace(regex, "x"));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("regex"));
    }

    [Test]
    public void GivenNullReplacement_WhenReplace_ThenArgumentNullExceptionIsThrown()
    {
        // Given
        const string input = "Hello world";
        var regex = new RegexBuilder()
            .Digit()
            .BuildRegex();

        // When/Then
        // ReSharper disable once ExpressionIsAlwaysNull
        var exception = Assert.Throws<ArgumentNullException>(() => input.Replace(regex, null));

        // Then
        Assert.That(exception, Is.Not.Null);
        Assert.That(exception.ParamName, Is.EqualTo("replacement"));
    }
}
