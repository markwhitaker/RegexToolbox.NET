using System;
using System.Text;

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

        public RegexBuilderException(string message, StringBuilder stringBuilder)
            : base(message)
        {
            Regex = stringBuilder.ToString();
        }
    }
}
