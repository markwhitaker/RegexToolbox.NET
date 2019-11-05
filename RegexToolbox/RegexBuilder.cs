using System;
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
    /// <see cref="BuildRegex"/> to build the Regex. Example:
    ///
    /// <code>
    /// Regex regex = new RegexBuilder()
    ///     .Text("cat")
    ///     .EndOfString()
    ///     .BuildRegex();
    /// </code>
    /// 
    /// </summary>
    public sealed class RegexBuilder
    {
        /// <summary>
        /// Interface to a logger attached by the client code which will receive log messages as the regex is built.
        /// </summary>
        public interface ILogger
        {
            /// <summary>
            /// Log a message to a real logger (Trace, Console, a third-party logging framework, or whatever you want).
            /// </summary>
            /// <param name="message">Message to log</param>
            void Log(string message);
        }
        
        private const string DefaultLogTag = "RegexBuilder";
        private readonly StringBuilder _stringBuilder = new StringBuilder();
        private Action<string> _logFunction;
        private string _prefix;
        
        private int _openGroupCount;

        #region Build method

        /// <summary>
        /// Build and return a Regex object from the current builder state.
        /// After calling this the builder is cleared and ready to re-use.
        /// </summary>
        /// <param name="options">Any number of regex options to apply to the regex</param>
        /// <returns>Regex as built</returns>
        public Regex BuildRegex(params RegexOptions[] options)
        {
            if (_openGroupCount == 1)
            {
                throw new RegexBuilderException("A group has been started but not ended", _stringBuilder);
            }

            if (_openGroupCount > 1)
            {
                throw new RegexBuilderException(_openGroupCount + " groups have been started but not ended", _stringBuilder);
            }

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
            var optionsString = string.Join(", ", options.Select(o => Enum.GetName(typeof(RegexOptions), o)));
            Log($"BuildRegex({optionsString})", stringBuilt);
            var regex = new Regex(stringBuilt, combinedOptions);
            _stringBuilder.Clear();
            return regex;
        }

        #endregion

        #region Logging

        /// <summary>
        /// Attach a logger to this builder using this <see cref="ILogger"/> interface. The builder will emit logging
        /// messages to it as the regex is built.
        /// </summary>
        /// <param name="logger">Logger to receive log messages from the builder</param>
        /// <param name="prefix">A prefix to add at the start of each log message. Defaults to "RegexBuilder".</param>
        public RegexBuilder AddLogger(ILogger logger, string prefix = DefaultLogTag)
        {
            return AddLogger(logger.Log, prefix);
        }

        /// <summary>
        /// Attach a logger to this builder using a lambda expression. The builder will emit logging
        /// messages to it as the regex is built.
        /// </summary>
        /// <param name="logFunction">Lambda to invoke with log messages from the builder</param>
        /// <param name="prefix">A prefix to add at the start of each log message. Defaults to "RegexBuilder".</param>
        public RegexBuilder AddLogger(Action<string> logFunction, string prefix = DefaultLogTag)
        {
            _logFunction = logFunction;
            _prefix = prefix;
            return this;
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
            var method = quantifier == null ? $"Text(\"{text}\")" : $"Text(\"{text}\", {quantifier.Name})";
            var safeText = MakeSafeForRegex(text);
            // If we have a quantifier, apply it to the whole string by putting it in a non-capturing group
            return (quantifier == null)
                ? AddPart(method, safeText)
                : AddPartInNonCapturingGroup(method, safeText, quantifier);
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
            var method = quantifier == null ? $"RegexText(\"{text}\")" : $"RegexText(\"{text}\", {quantifier.Name})";
            // If we have a quantifier, apply it to the whole string by putting it in a non-capturing group
            return (quantifier == null)
                ? AddPart(method, text)
                : AddPartInNonCapturingGroup(method, text, quantifier);
        }

        /// <summary>
        /// Add an element to match any character.
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder AnyCharacter(RegexQuantifier quantifier = null) =>
            AddPart($"AnyCharacter({quantifier?.Name})", ".", quantifier);

        /// <summary>
        /// Add an element to match any single whitespace character.
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder Whitespace(RegexQuantifier quantifier = null) =>
            AddPart($"Whitespace({quantifier?.Name})", @"\s", quantifier);

        /// <summary>
        /// Add an element to match any single non-whitespace character.
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder NonWhitespace(RegexQuantifier quantifier = null) =>
            AddPart($"NonWhitespace({quantifier?.Name})", @"\S", quantifier);

        /// <summary>
        /// Add an element to represent any amount of white space, including none. This is just a convenient alias for
        /// <code>Whitespace(RegexQuantifier.ZeroOrMore)</code>.
        /// </summary>
        /// <returns></returns>
        public RegexBuilder PossibleWhitespace() =>
            AddPart("PossibleWhitespace()", @"\s", RegexQuantifier.ZeroOrMore);

        /// <summary>
        /// Add an element to match a single space character. If you want to match any kind of white space, use
        /// <see cref="Whitespace"/>.
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder Space(RegexQuantifier quantifier = null) =>
            AddPart($"Space({quantifier?.Name})", " ", quantifier);

        /// <summary>
        /// Add an element to match a single tab character.
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder Tab(RegexQuantifier quantifier = null) =>
            AddPart($"Tab({quantifier?.Name})", @"\t", quantifier);

        /// <summary>
        /// Add an element to match a single line feed character.
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder LineFeed(RegexQuantifier quantifier = null) =>
            AddPart($"LineFeed({quantifier?.Name})", @"\n", quantifier);

        /// <summary>
        /// Add an element to match a single carriage return character.
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder CarriageReturn(RegexQuantifier quantifier = null) =>
            AddPart($"CarriageReturn({quantifier?.Name})", @"\r", quantifier);

        /// <summary>
        /// Add an element to match any single decimal digit (0-9).
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder Digit(RegexQuantifier quantifier = null) =>
            AddPart($"Digit({quantifier?.Name})", @"\d", quantifier);

        /// <summary>
        /// Add an element to match any character that is not a decimal digit (0-9).
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder NonDigit(RegexQuantifier quantifier = null) =>
            AddPart($"NonDigit({quantifier?.Name})", @"\D", quantifier);

        /// <summary>
        /// Add an element to match any Unicode letter
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder Letter(RegexQuantifier quantifier = null) =>
            AddPart($"Letter({quantifier?.Name})", @"\p{L}", quantifier);

        /// <summary>
        /// Add an element to match any character that is not a Unicode letter
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder NonLetter(RegexQuantifier quantifier = null) =>
            AddPart($"NonLetter({quantifier?.Name})", @"\P{L}", quantifier);

        /// <summary>
        /// Add an element to match any upper-case Unicode letter
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder UppercaseLetter(RegexQuantifier quantifier = null) =>
            AddPart($"UppercaseLetter({quantifier?.Name})", @"\p{Lu}", quantifier);

        /// <summary>
        /// Add an element to match any lowercase Unicode letter
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder LowercaseLetter(RegexQuantifier quantifier = null) =>
            AddPart($"LowercaseLetter({quantifier?.Name})", @"\p{Ll}", quantifier);

        /// <summary>
        /// Add an element to match any Unicode letter or decimal digit
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder LetterOrDigit(RegexQuantifier quantifier = null) =>
            AddPart($"LetterOrDigit({quantifier?.Name})", @"[\p{L}0-9]", quantifier);

        /// <summary>
        /// Add an element to match any character that is not a Unicode letter or a decimal digit
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder NonLetterOrDigit(RegexQuantifier quantifier = null) =>
            AddPart($"NonLetterOrDigit({quantifier?.Name})", @"[^\p{L}0-9]", quantifier);

        /// <summary>
        /// Add an element to match any hexadecimal digit (a-f, A-F, 0-9)
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder HexDigit(RegexQuantifier quantifier = null) =>
            AddPart($"HexDigit({quantifier?.Name})", "[0-9A-Fa-f]", quantifier);

        /// <summary>
        /// Add an element to match any uppercase hexadecimal digit (A-F, 0-9)
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder UppercaseHexDigit(RegexQuantifier quantifier = null) =>
            AddPart($"UppercaseHexDigit({quantifier?.Name})", "[0-9A-F]", quantifier);

        /// <summary>
        /// Add an element to match any lowercase hexadecimal digit (a-f, 0-9)
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder LowercaseHexDigit(RegexQuantifier quantifier = null) =>
            AddPart($"LowercaseHexDigit({quantifier?.Name})", "[0-9a-f]", quantifier);

        /// <summary>
        /// Add an element to match any character that is not a hexadecimal digit (a-f, A-F, 0-9)
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder NonHexDigit(RegexQuantifier quantifier = null) =>
            AddPart($"NonHexDigit({quantifier?.Name})", "[^0-9A-Fa-f]", quantifier);

        /// <summary>
        /// Add an element to match any Unicode letter, decimal digit, or underscore
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder WordCharacter(RegexQuantifier quantifier = null) =>
            AddPart($"WordCharacter({quantifier?.Name})", @"[\p{L}0-9_]", quantifier);

        /// <summary>
        /// Add an element to match any character that is not a Unicode letter, decimal digit, or underscore
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder NonWordCharacter(RegexQuantifier quantifier = null) =>
            AddPart($"NonWordCharacter({quantifier?.Name})", @"[^\p{L}0-9_]", quantifier);

        /// <summary>
        /// Add an element (a character class) to match any of the characters provided.
        /// </summary>
        /// <param name="characters">String containing all characters to include in the character class</param>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder AnyCharacterFrom(string characters, RegexQuantifier quantifier = null)
        {
            var method = quantifier == null
                ? $"AnyCharacterFrom(\"{characters}\")"
                : $"AnyCharacterFrom(\"{characters}\", {quantifier.Name})";

            // Build a character class, remembering to escape any ] character if passed in
            return AddPart(method, "[" + MakeSafeForCharacterClass(characters) + "]", quantifier);
        }

        /// <summary>
        /// Add an element (a character class) to match any character except those provided.
        /// </summary>
        /// <param name="characters">String containing all characters to exclude from the character class</param>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder AnyCharacterExcept(string characters, RegexQuantifier quantifier = null)
        {
            var method = quantifier == null
                ? $"AnyCharacterExcept(\"{characters}\")"
                : $"AnyCharacterExcept(\"{characters}\", {quantifier.Name})";

            // Build a character class, remembering to escape any ] character if passed in
            return AddPart(method, "[^" + MakeSafeForCharacterClass(characters) + "]", quantifier);
        }

        /// <summary>
        /// Add a group of alternatives, to match any of the strings provided
        /// </summary>
        /// <param name="strings">A number of strings, any one of which will be matched</param>
        /// <param name="quantifier">Quantifier to apply to this element</param>
        public RegexBuilder AnyOf(IEnumerable<string> strings, RegexQuantifier quantifier = null)
        {
            if (strings == null)
            {
                Log("AnyOf()", "strings collection is null, so doing nothing");
                return this;
            }

            var stringsList = strings.ToList();
            if (!stringsList.Any())
            {
                Log("AnyOf()", "strings collection is empty, so doing nothing");
                return this;
            }

            var joinedStrings = string.Join(", ", stringsList.Select(s => $"\"{s}\""));
            var method = quantifier == null ? $"AnyOf({joinedStrings})" : $"AnyOf([{joinedStrings}], {quantifier.Name})";
            return (stringsList.Count == 1)
                ? AddPart(method, MakeSafeForRegex(stringsList[0]), quantifier)
                : AddPartInNonCapturingGroup(method, string.Join("|", stringsList.Select(MakeSafeForRegex)), quantifier);
        }

        /// <summary>
        /// Add a group of alternatives, to match any of the strings provided. If you need to add a quantifier,
        /// use the overloaded method.
        /// </summary>
        /// <param name="strings">A number of strings, any one of which will be matched</param>
        public RegexBuilder AnyOf(params string[] strings)
        {
            return AnyOf(strings, null);
        }

        #endregion

        #region Anchors (zero-width assertions)

        /// <summary>
        /// Add a zero-width anchor element to match the start of the string
        /// </summary>
        public RegexBuilder StartOfString() => AddPart("StartOfString()", "^");

        /// <summary>
        /// Add a zero-width anchor element to match the end of the string
        /// </summary>
        public RegexBuilder EndOfString() => AddPart("EndOfString()", "$");

        /// <summary>
        /// Add a zero-width anchor element to match the boundary between an alphanumeric/underscore character
        /// and either a non-alphanumeric, non-underscore character or the start/end of the string.
        /// </summary>
        public RegexBuilder WordBoundary() => AddPart("WordBoundary()", @"\b");

        #endregion

        #region Grouping

        /// <summary>
        /// Start a capture group. Capture groups have two purposes: they group part of the expression so
        /// it can have quantifiers applied to it, and they capture the results of each group match and
        /// allow you to access them afterwards using Match.Groups.
        /// 
        /// If you don't want to capture the group match, use <see cref="StartNonCapturingGroup"/>.
        /// 
        /// Note: all groups must be ended with <see cref="EndGroup"/> before calling <see cref="BuildRegex"/>.
        /// </summary>
        public RegexBuilder StartGroup()
        {
            _openGroupCount++;
            return AddPart("StartGroup()", "(");
        }

        /// <summary>
        /// Start a non-capturing group. Non-capturing groups group part of the expression so
        /// it can have quantifiers applied to it, but do not capture the results of each group match, meaning
        /// you can't access them afterwards using Match.Groups.
        /// 
        /// If you want to capture group results, use <see cref="StartGroup"/> or <see cref="StartNamedGroup"/>.
        /// 
        /// Note: all groups must be ended with <see cref="EndGroup"/> before calling <see cref="BuildRegex"/>.
        /// </summary>
        public RegexBuilder StartNonCapturingGroup()
        {
            _openGroupCount++;
            return AddPart("StartNonCapturingGroup()", "(?:");
        }

        /// <summary>
        /// Start a named capture group. Capture groups have two purposes: they group part of the expression so
        /// it can have quantifiers applied to it, and they capture the results of each group match and
        /// allow you to access them afterwards using Match.Groups. Named capture groups can be accessed by
        /// indexing into Match.Groups with the assigned name as well as a numerical index.
        /// 
        /// If you don't want to capture the group match, use <see cref="StartNonCapturingGroup"/>.
        /// 
        /// Note: all groups must be ended with <see cref="EndGroup"/> before calling <see cref="BuildRegex"/>.
        /// </summary>
        public RegexBuilder StartNamedGroup(string name)
        {
            _openGroupCount++;
            return AddPart($"StartNamedGroup(\"{name}\")", $"(?<{name}>");
        }

        /// <summary>
        /// End the innermost group previously started with <see cref="StartGroup"/>, <see cref="StartNonCapturingGroup"/> or
        /// <see cref="StartNamedGroup"/>.
        /// </summary>
        /// <param name="quantifier">Quantifier to apply to this group</param>
        public RegexBuilder EndGroup(RegexQuantifier quantifier = null)
        {
            if (_openGroupCount == 0)
            {
                throw new RegexBuilderException("Cannot call endGroup() until a group has been started with startGroup()", _stringBuilder);
            }

            _openGroupCount--;
            return AddPart($"EndGroup({quantifier?.Name})", ")", quantifier);
        }

        #endregion

        #region Private methods

        private RegexBuilder AddPart(string method, string part, RegexQuantifier quantifier = null)
        {
            Log(method, $"{part}{quantifier}");
            _stringBuilder
                .Append(part)
                .Append(quantifier);
            return this;
        }

        private void Log(string method, string message)
        {
            _logFunction?.Invoke($"{_prefix}: {method} => {message}");
        }
        
        private RegexBuilder AddPartInNonCapturingGroup(string methodName, string part, RegexQuantifier quantifier = null) =>
            AddPart(methodName, $"(?:{part})", quantifier);
        
        private string MakeSafeForCharacterClass(string s)
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

        #endregion
    }
}