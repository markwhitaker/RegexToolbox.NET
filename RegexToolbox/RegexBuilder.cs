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
    }
}
