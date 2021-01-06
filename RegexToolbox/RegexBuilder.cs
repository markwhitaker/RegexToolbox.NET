using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexToolbox
{
    /// <summary>
    /// Class to build regular expressions in a more human-readable way using a fluent API.
    ///
    /// To use, chain method calls representing the elements you want to match, and finish with
    /// <see cref="BuildRegex"/> to build the Regex.
    /// </summary>
    /// <example>
    /// <code>
    /// Regex regex = new RegexBuilder()
    ///     .Text("cat")
    ///     .EndOfString()
    ///     .BuildRegex();
    /// </code>
    /// </example>
    public sealed class RegexBuilder
    {
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        /// <summary>
        /// A delegate used to build a sub-part of a regex, for example in <see cref="RegexBuilder.Group"/>.
        /// </summary>
        /// <param name="regexBuilder">An in-progress <see cref="RegexBuilder"/> to pass into the delegate function</param>
        public delegate RegexBuilder SubRegexBuilder(RegexBuilder regexBuilder);

        #region Build method

        /// <summary>
        /// Build and return a Regex object from the current builder state.
        /// After calling this the builder is cleared and ready to re-use.
        /// </summary>
        /// <param name="options">Any number of regex options to apply to the regex</param>
        /// <returns>Regex as built</returns>
        public Regex BuildRegex(params RegexOptions[] options)
        {
            var combinedOptions = System.Text.RegularExpressions.RegexOptions.None;
            foreach (var option in options)
            {
                switch (option)
                {
                    case RegexOptions.IgnoreCase:
                        combinedOptions |= System.Text.RegularExpressions.RegexOptions.IgnoreCase;
                        break;
                    case RegexOptions.Multiline:
                        combinedOptions |= System.Text.RegularExpressions.RegexOptions.Multiline;
                        break;
                }
            }

            var stringBuilt = _stringBuilder.ToString();
            var regex = new Regex(stringBuilt, combinedOptions);
            _stringBuilder.Clear();
            return regex;
        }

        #endregion

        #region Character matches

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
        public RegexBuilder RegexText(string text, RegexQuantifier quantifier = null)
        {
            // If we have a quantifier, apply it to the whole string by putting it in a non-capturing group
            return quantifier == null
                ? AddPart(text)
                : AddPartInNonCapturingGroup(text, quantifier);
        }

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
        public RegexBuilder AnyOf(IEnumerable<string> strings, RegexQuantifier quantifier = null)
        {
            var stringsList = strings?.ToList();
            if (stringsList == null || !stringsList.Any())
            {
                throw new RegexBuilderException("No parameters passed to AnyOf", this);
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
        /// <exception cref="RegexBuilderException">subRegexBuilders is null or empty</exception>
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
        /// <param name="subRegexes">RegexBuilder chains that represent alternative sub-regexes to match</param>
        /// <param name="quantifier">Quantifier to apply to this group of alternatives</param>
        /// <exception cref="RegexBuilderException">subRegexBuilders is null or empty</exception>
        public RegexBuilder AnyOf(
            IEnumerable<SubRegexBuilder> subRegexes,
            RegexQuantifier quantifier = null)
        {
            var subRegexesArray = subRegexes?.ToArray();
            if (subRegexesArray == null || !subRegexesArray.Any())
            {
                throw new RegexBuilderException("No parameters passed to AnyOf", this);
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

        #endregion

        #region Anchors (zero-width assertions)

        /// <summary>
        /// Add a zero-width anchor element to match the start of the string
        /// </summary>
        public RegexBuilder StartOfString() => AddPart("^");

        /// <summary>
        /// Add a zero-width anchor element to match the end of the string
        /// </summary>
        public RegexBuilder EndOfString() => AddPart("$");

        /// <summary>
        /// Add a zero-width anchor element to match the boundary between an alphanumeric/underscore character
        /// and either a non-alphanumeric, non-underscore character or the start/end of the string.
        /// </summary>
        public RegexBuilder WordBoundary() => AddPart(@"\b");

        #endregion

        #region Grouping

        /// <summary>
        /// Add a capture group to the regex. Capture groups have two purposes: they group part of the expression so it
        /// can have quantifiers applied to it, and they capture the results of each group match and allow you to access
        /// them afterwards using <see cref="Match.Groups"/>.
        ///
        /// If you don't want to capture the group match, use <see cref="NonCapturingGroup"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Regex regex = new RegexBuilder()
        ///     .Group(r => r
        ///         .Letter()
        ///         .Digit())
        ///     .BuildRegex();
        /// </code>
        /// </example>
        /// <param name="groupElements">
        /// A lambda containing the <see cref="RegexBuilder"/> elements required within the group
        /// </param>
        /// <param name="quantifier">Quantifier to apply to this group</param>
        public RegexBuilder Group(
            SubRegexBuilder groupElements,
            RegexQuantifier quantifier = null)
        {
            AddPart("(");
            groupElements(this);
            return EndGroup(quantifier);
        }

        /// <summary>
        /// Add a non-capturing group to the regex. Non-capturing groups group part of the expression so it can have
        /// quantifiers applied to it, but do not capture the results of each group match, meaning you can't access them
        /// afterwards using <see cref="Match.Groups"/>.
        ///
        /// If you want to capture group results, use <see cref="Group"/> or <see cref="NamedGroup"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Regex regex = new RegexBuilder()
        ///     .NonCapturingGroup(r => r
        ///         .Letter()
        ///         .Digit())
        ///     .BuildRegex();
        /// </code>
        /// </example>
        /// <param name="groupElements">
        /// A lambda containing the <see cref="RegexBuilder"/> elements required within the group
        /// </param>
        /// <param name="quantifier">Quantifier to apply to this group</param>
        public RegexBuilder NonCapturingGroup(
            SubRegexBuilder groupElements,
            RegexQuantifier quantifier = null)
        {
            StartNonCapturingGroup();
            groupElements(this);
            return EndGroup(quantifier);
        }

        /// <summary>
        /// Add a named capture group to the regex. Capture groups have two purposes: they group part of the expression
        /// so it can have quantifiers applied to it, and they capture the results of each group match and allow you to
        /// access them afterwards using <see cref="Match.Groups"/>. Named capture groups can be accessed by indexing
        /// into <see cref="Match.Groups"/> using either the assigned name or a numerical index.
        ///
        /// If you don't want to name the group match, use <see cref="Group"/>.
        ///
        /// If you don't want to capture the group match, use <see cref="NonCapturingGroup"/>.
        /// </summary>
        /// <example>
        /// <code>
        /// Regex regex = new RegexBuilder()
        ///     .NamedGroup("name", r => r
        ///         .Letter()
        ///         .Digit())
        ///     .BuildRegex();
        /// </code>
        /// </example>
        /// <param name="name">Name that can be used to access the group from <see cref="Match.Groups"/></param>
        /// <param name="groupElements">
        /// A lambda containing the <see cref="RegexBuilder"/> elements required within the group
        /// </param>
        /// <param name="quantifier">Quantifier to apply to this group</param>
        public RegexBuilder NamedGroup(
            string name,
            SubRegexBuilder groupElements,
            RegexQuantifier quantifier = null)
        {
            AddPart($"(?<{name}>");
            groupElements(this);
            return EndGroup(quantifier);
        }

        #endregion

        #region Object methods

        public override string ToString() => _stringBuilder.ToString();

        #endregion

        #region Private methods

        private RegexBuilder AddPart(string part, RegexQuantifier quantifier = null)
        {
            _stringBuilder
                .Append(part)
                .Append(quantifier);
            return this;
        }

        private RegexBuilder AddPartInNonCapturingGroup(string part, RegexQuantifier quantifier = null) =>
            AddPart($"(?:{part})", quantifier);

        private static string MakeSafeForCharacterClass(string s)
        {
            var result = s
                // Replace ] with \]
                .Replace("]", @"\]")
                // Replace - with \-
                .Replace("-", @"\-");

            // replace ^ with \^ if it occurs at the start of the string
            if (result.StartsWith("^"))
            {
                result = @"\" + result;
            }

            return result;
        }

        private static string MakeSafeForRegex(string s)
        {
            var result = s
                // Make sure this always comes first!
                .Replace(@"\", @"\\")
                .Replace("?", @"\?")
                .Replace(".", @"\.")
                .Replace("+", @"\+")
                .Replace("*", @"\*")
                .Replace("^", @"\^")
                .Replace("$", @"\$")
                .Replace("(", @"\(")
                .Replace(")", @"\)")
                .Replace("[", @"\[")
                .Replace("]", @"\]")
                .Replace("{", @"\{")
                .Replace("}", @"\}")
                .Replace("|", @"\|");

            return result;
        }

        private void StartNonCapturingGroup() => AddPart("(?:");

        private RegexBuilder EndGroup(RegexQuantifier quantifier = null) => AddPart(")", quantifier);

        #endregion
    }
}
