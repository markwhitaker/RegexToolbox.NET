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
    public sealed partial class RegexBuilder
    {
        private static readonly string[] RegexUnsafeCharacters =
        {
            // These are escaped in the order declared here, so make sure \ always comes first
            @"\", "?", ".", "+", "*", "^", "$", "(", ")", "[", "]", "{", "}", "|"
        };

        private static readonly IReadOnlyDictionary<RegexOptions, System.Text.RegularExpressions.RegexOptions>
            RegexOptionsMap = new Dictionary<RegexOptions, System.Text.RegularExpressions.RegexOptions>
            {
                { RegexOptions.IgnoreCase, System.Text.RegularExpressions.RegexOptions.IgnoreCase },
                { RegexOptions.Multiline, System.Text.RegularExpressions.RegexOptions.Multiline }
            };

        private readonly StringBuilder _stringBuilder = new StringBuilder();

        /// <summary>
        /// A delegate used to build a sub-part of a regex, for example in <see cref="RegexBuilder.Group"/>.
        /// </summary>
        /// <param name="regexBuilder">An in-progress <see cref="RegexBuilder"/> to pass into the delegate function</param>
        public delegate RegexBuilder SubRegexBuilder(RegexBuilder regexBuilder);

        /// <summary>
        /// Build and return a Regex object from the current builder state.
        /// After calling this the builder is cleared and ready to re-use.
        /// </summary>
        /// <param name="options">Any number of regex options to apply to the regex</param>
        /// <returns>Regex as built</returns>
        public Regex BuildRegex(params RegexOptions[] options)
        {
            var combinedOptions = options.Aggregate(
                System.Text.RegularExpressions.RegexOptions.None,
                (current, option) => current | RegexOptionsMap[option]);

            var stringBuilt = _stringBuilder.ToString();
            var regex = new Regex(stringBuilt, combinedOptions);
            _stringBuilder.Clear();
            return regex;
        }

        public override string ToString() => _stringBuilder.ToString();

        private RegexBuilder AddPart(string part, RegexQuantifier quantifier = null)
        {
            _stringBuilder
                .Append(part)
                .Append(quantifier);
            return this;
        }

        private RegexBuilder AddPartInNonCapturingGroup(string part, RegexQuantifier quantifier = null) =>
            StartNonCapturingGroup()
                .AddPart(part)
                .EndGroup(quantifier);

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
                result = $@"\{result}";
            }

            return result;
        }

        private static string MakeSafeForRegex(string s) =>
            RegexUnsafeCharacters
                .Aggregate(s, (safe, notSafe) =>
                    safe.Replace(notSafe, $@"\{notSafe}"));
    }
}
