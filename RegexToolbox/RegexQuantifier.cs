using System;

namespace RegexToolbox
{
    /// <summary>
    /// Quantifiers that can be applied to regex elements or groups
    /// </summary>
    public class RegexQuantifier
    {
        #region Private property and constructor

        private string RegexString { get; set; }
        internal string Name { get; private set; }

        private RegexQuantifier(string name, string regexString)
        {
            Name = name;
            RegexString = regexString;
        }

        #endregion

        #region Public static properties

        /// <summary>
        /// Quantifier to match the preceding element zero or more times
        /// </summary>
        public static RegexGreedyQuantifier ZeroOrMore => new RegexGreedyQuantifier("ZeroOrMore", "*");

        /// <summary>
        /// Quantifier to match the preceding element one or more times
        /// </summary>
        public static RegexGreedyQuantifier OneOrMore => new RegexGreedyQuantifier("OneOrMore", "+");

        /// <summary>
        /// Quantifier to match the preceding element once or not at all
        /// </summary>
        public static RegexGreedyQuantifier ZeroOrOne => new RegexGreedyQuantifier("ZeroOrOne", "?");

        #endregion

        #region Methods

        /// <summary>
        /// Quantifier to match an exact number of occurrences of the preceding element
        /// </summary>
        /// <param name="times">The exact number of occurrences to match</param>
        public static RegexQuantifier Exactly(int times) =>
            new RegexQuantifier($"Exactly({times})", "{" + times + "}");

        /// <summary>
        /// Quantifier to match at least a minimum number of occurrences of the preceding element
        /// </summary>
        /// <param name="minimum">The minimum number of occurrences to match</param>
        public static RegexGreedyQuantifier AtLeast(int minimum) =>
            new RegexGreedyQuantifier($"AtLeast({minimum})", "{" + minimum + ",}");

        /// <summary>
        /// Quantifier to match no more than a maximum number of occurrences of the preceding element
        /// </summary>
        /// <param name="maximum">The maximum number of occurrences to match</param>
        public static RegexGreedyQuantifier NoMoreThan(int maximum) =>
            new RegexGreedyQuantifier($"NoMoreThan({maximum})", "{0," + maximum + "}");

        /// <summary>
        /// Quantifier to match at least a minimum, and no more than a maximum, occurrences of the preceding element
        /// </summary>
        /// <param name="minimum">The minimum number of occurrences to match</param>
        /// <param name="maximum">The maximum number of occurrences to match</param>
        public static RegexGreedyQuantifier Between(int minimum, int maximum) =>
            new RegexGreedyQuantifier($"Between({minimum}, {maximum})", "{" + minimum + "," + maximum + "}");

        public override string ToString()
        {
            return RegexString;
        }

        #endregion

        #region Member classes

        /// <summary>
        /// A quantifier which defaults to greedy matching: in other words, if used
        /// to match a variable number of elements it will match as many as possible.
        /// </summary>
        public sealed class RegexGreedyQuantifier : RegexQuantifier
        {
            public RegexGreedyQuantifier(string name, string regexString) : base(name, regexString)
            {
            }

            /// <summary>
            /// Get a non-greedy version of this quantifier: in other words, if used
            /// to match a variable number of elements it will match as few as possible.
            /// </summary>
            public RegexQuantifier ButAsFewAsPossible
            {
                get
                {
                    Name += ".ButAsFewAsPossible";
                    RegexString += "?";
                    return this;
                }
            }
        }

        #endregion
    }
}