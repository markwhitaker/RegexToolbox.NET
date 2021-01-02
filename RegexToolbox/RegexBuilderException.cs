using System;

namespace RegexToolbox
{
    /// <summary>
    /// Exception thrown by RegexBuilder methods
    /// </summary>
    public sealed class RegexBuilderException : Exception
    {
        /// <summary>
        /// The regex string as it currently stands
        /// </summary>
        public string Regex { get; }

        public RegexBuilderException(string message, RegexBuilder regexBuilder)
            : base(message)
        {
            Regex = regexBuilder.ToString();
        }
    }
}
