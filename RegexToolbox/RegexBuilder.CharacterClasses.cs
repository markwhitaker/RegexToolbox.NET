using System;
using System.Collections.Generic;
using System.Linq;

namespace RegexToolbox;

public sealed partial class RegexBuilder
{
    /// <summary>
    /// Add text to the regex. Any regex special characters will be escaped as necessary
    /// so there's no need to do that yourself.
    /// </summary>
    /// <example>
    /// "Hello (world)" will be converted to "Hello \(world\)" so the brackets are treated
    /// as normal, human-readable brackets, not regex grouping brackets.
    /// It WILL match the string literal "Hello (world)".
    /// It WILL NOT match the string literal "Hello world".
    /// </example>
    /// <param name="text">Text to add</param>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder Text(string text, RegexQuantifier quantifier = null)
    {
        var safeText = MakeSafeForRegex(text);

        // If we have a quantifier, apply it to the whole string by putting it in a non-capturing group
        return quantifier == null
            ? AddPart(safeText)
            : AddPartInNonCapturingGroup(safeText, quantifier);
    }

    /// <summary>
    /// Add literal regex text to the regex. Regex special characters will NOT be escaped.
    /// Only call this if you're comfortable with regex syntax.
    /// </summary>
    /// <example>
    /// "Hello (world)" will be left as "Hello (world)", meaning that when the regex is built
    /// the brackets will be treated as regex grouping brackets rather than normal, human-readable
    /// brackets.
    /// It WILL match the string literal "Hello world" (and capture the word "world" as a group).
    /// It WILL NOT match the string literal "Hello (world)".
    /// </example>
    /// <param name="text">regex text to add</param>
    /// <param name="quantifier">Quantifier to apply to the whole string</param>
    public RegexBuilder RegexText(string text, RegexQuantifier quantifier = null) =>
        // If we have a quantifier, apply it to the whole string by putting it in a non-capturing group
        quantifier == null
            ? AddPart(text)
            : AddPartInNonCapturingGroup(text, quantifier);

    /// <summary>
    /// Add an element to match any character.
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder AnyCharacter(RegexQuantifier quantifier = null) => AddPart(".", quantifier);

    /// <summary>
    /// Add an element to match any single whitespace character.
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder Whitespace(RegexQuantifier quantifier = null) => AddPart(@"\s", quantifier);

    /// <summary>
    /// Add an element to match any single non-whitespace character.
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder NonWhitespace(RegexQuantifier quantifier = null) => AddPart(@"\S", quantifier);

    /// <summary>
    /// Add an element to represent any amount of white space, including none. This is just a convenient alias for
    /// <code>Whitespace(RegexQuantifier.ZeroOrMore)</code>.
    /// </summary>
    /// <returns></returns>
    public RegexBuilder PossibleWhitespace() => AddPart(@"\s", RegexQuantifier.ZeroOrMore);

    /// <summary>
    /// Add an element to match a single space character. If you want to match any kind of white space, use
    /// <see cref="Whitespace"/>.
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder Space(RegexQuantifier quantifier = null) => AddPart(" ", quantifier);

    /// <summary>
    /// Add an element to match a single tab character.
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder Tab(RegexQuantifier quantifier = null) => AddPart(@"\t", quantifier);

    /// <summary>
    /// Add an element to match a single line feed character.
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder LineFeed(RegexQuantifier quantifier = null) => AddPart(@"\n", quantifier);

    /// <summary>
    /// Add an element to match a single carriage return character.
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder CarriageReturn(RegexQuantifier quantifier = null) => AddPart(@"\r", quantifier);

    public RegexBuilder NewLine(RegexQuantifier quantifier = null) => AddPart(@"\r?\n", quantifier);

    /// <summary>
    /// Add an element to match any single decimal digit (0-9).
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder Digit(RegexQuantifier quantifier = null) => AddPart(@"\d", quantifier);

    /// <summary>
    /// Add an element to match any character that is not a decimal digit (0-9).
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder NonDigit(RegexQuantifier quantifier = null) => AddPart(@"\D", quantifier);

    /// <summary>
    /// Add an element to match any Unicode letter
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder Letter(RegexQuantifier quantifier = null) => AddPart(@"\p{L}", quantifier);

    /// <summary>
    /// Add an element to match any character that is not a Unicode letter
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder NonLetter(RegexQuantifier quantifier = null) => AddPart(@"\P{L}", quantifier);

    /// <summary>
    /// Add an element to match any upper-case Unicode letter
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder UppercaseLetter(RegexQuantifier quantifier = null) => AddPart(@"\p{Lu}", quantifier);

    /// <summary>
    /// Add an element to match any lowercase Unicode letter
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder LowercaseLetter(RegexQuantifier quantifier = null) => AddPart(@"\p{Ll}", quantifier);

    /// <summary>
    /// Add an element to match any Unicode letter or decimal digit
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder LetterOrDigit(RegexQuantifier quantifier = null) => AddPart(@"[\p{L}0-9]", quantifier);

    /// <summary>
    /// Add an element to match any character that is not a Unicode letter or a decimal digit
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder NonLetterOrDigit(RegexQuantifier quantifier = null) => AddPart(@"[^\p{L}0-9]", quantifier);

    /// <summary>
    /// Add an element to match any hexadecimal digit (a-f, A-F, 0-9)
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder HexDigit(RegexQuantifier quantifier = null) => AddPart("[0-9A-Fa-f]", quantifier);

    /// <summary>
    /// Add an element to match any uppercase hexadecimal digit (A-F, 0-9)
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder UppercaseHexDigit(RegexQuantifier quantifier = null) => AddPart("[0-9A-F]", quantifier);

    /// <summary>
    /// Add an element to match any lowercase hexadecimal digit (a-f, 0-9)
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder LowercaseHexDigit(RegexQuantifier quantifier = null) => AddPart("[0-9a-f]", quantifier);

    /// <summary>
    /// Add an element to match any character that is not a hexadecimal digit (a-f, A-F, 0-9)
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder NonHexDigit(RegexQuantifier quantifier = null) => AddPart("[^0-9A-Fa-f]", quantifier);

    /// <summary>
    /// Add an element to match any Unicode letter, decimal digit, or underscore
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder WordCharacter(RegexQuantifier quantifier = null) => AddPart(@"[\p{L}0-9_]", quantifier);

    /// <summary>
    /// Add an element to match any character that is not a Unicode letter, decimal digit, or underscore
    /// </summary>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder NonWordCharacter(RegexQuantifier quantifier = null) => AddPart(@"[^\p{L}0-9_]", quantifier);

    /// <summary>
    /// Add an element (a character class) to match any of the characters provided.
    /// </summary>
    /// <param name="characters">String containing all characters to include in the character class</param>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder AnyCharacterFrom(string characters, RegexQuantifier quantifier = null) =>
        AddPart("[" + MakeSafeForCharacterClass(characters) + "]", quantifier);

    /// <summary>
    /// Add an element (a character class) to match any character except those provided.
    /// </summary>
    /// <param name="characters">String containing all characters to exclude from the character class</param>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    public RegexBuilder AnyCharacterExcept(string characters, RegexQuantifier quantifier = null) =>
        AddPart("[^" + MakeSafeForCharacterClass(characters) + "]", quantifier);

    /// <summary>
    /// Add a group of alternatives, to match any of the strings provided
    /// </summary>
    /// <param name="strings">A number of strings, any one of which will be matched</param>
    /// <param name="quantifier">Quantifier to apply to this element</param>
    /// <exception cref="System.ArgumentNullException"><paramref name="strings"/> is null</exception>
    /// <exception cref="System.ArgumentException"><paramref name="strings"/> is empty</exception>
    public RegexBuilder AnyOf(IEnumerable<string> strings, RegexQuantifier quantifier = null)
    {
        if (strings is null)
        {
            throw new ArgumentNullException(nameof(strings));
        }

        var stringsList = strings.ToList();
        if (!stringsList.Any())
        {
            throw new ArgumentException("Argument list is empty", nameof(strings));
        }

        return AddPartInNonCapturingGroup(
            string.Join("|", stringsList.Select(MakeSafeForRegex)),
            quantifier);
    }

    /// <summary>
    /// Add a group of alternatives, to match any of the strings provided. If you need to add a quantifier,
    /// use the overloaded method.
    /// </summary>
    /// <param name="strings">A number of strings, any one of which will be matched</param>
    public RegexBuilder AnyOf(params string[] strings) => AnyOf(strings, null);

    /// <summary>
    /// Add a group of alternatives, to match any of the sub-regexes provided. Each sub-regex can be an arbitrarily
    /// complex regex in its own right.
    /// </summary>
    /// <example>
    /// <code>
    /// Regex regex = new RegexBuilder()
    ///     .Text("(")
    ///     .AnyOf(
    ///         r => r.Digit(Exactly(3)),
    ///         r => r.Letter(Exactly(4))
    ///     )
    ///     .Text(")")
    ///     .BuildRegex();
    /// </code>
    /// </example>
    /// <param name="subRegexBuilders">RegexBuilder chains that represent alternative sub-regexes to match</param>
    /// <exception cref="System.ArgumentNullException"><paramref name="subRegexBuilders"/> is null</exception>
    /// <exception cref="System.ArgumentException"><paramref name="subRegexBuilders"/> is empty</exception>
    public RegexBuilder AnyOf(params SubRegexBuilder[] subRegexBuilders) =>
        AnyOf(subRegexBuilders, null);

    /// <summary>
    /// Add a group of alternatives, to match any of the sub-regexes provided. Each sub-regex can be an arbitrarily
    /// complex regex in its own right.
    /// </summary>
    /// <example>
    /// <code>
    /// Regex regex = new RegexBuilder()
    ///     .Text("(")
    ///     .AnyOf(new SubRegexBuilder[]
    ///     {
    ///         r => r.Digit(Exactly(3)),
    ///         r => r.Letter(Exactly(4))
    ///     }, RegexQuantifier.OneOrMore)
    ///     .Text(")")
    ///     .BuildRegex();
    /// </code>
    /// </example>
    /// <param name="subRegexBuilders">RegexBuilder chains that represent alternative sub-regexes to match</param>
    /// <param name="quantifier">Quantifier to apply to this group of alternatives</param>
    /// <exception cref="System.ArgumentNullException"><paramref name="subRegexBuilders"/> is null</exception>
    /// <exception cref="System.ArgumentException"><paramref name="subRegexBuilders"/> is empty</exception>
    public RegexBuilder AnyOf(
        IEnumerable<SubRegexBuilder> subRegexBuilders,
        RegexQuantifier quantifier = null)
    {
        if (subRegexBuilders is null)
        {
            throw new ArgumentNullException(nameof(subRegexBuilders));
        }

        var subRegexesArray = subRegexBuilders.ToArray();
        if (!subRegexesArray.Any())
        {
            throw new ArgumentException("Argument list is empty", nameof(subRegexBuilders));
        }

        StartNonCapturingGroup();
        for (var i = 0; i < subRegexesArray.Length; i++)
        {
            subRegexesArray[i](this);
            if (i < subRegexesArray.Length - 1)
            {
                _stringBuilder.Append("|");
            }
        }
        return EndGroup(quantifier);
    }
}
