namespace RegexToolbox
{
    /// <summary>
    /// Quantifiers that can be applied to regex elements or groups
    /// </summary>
    public class RegexQuantifier
    {
        private string RegexString { get; set; }

        private RegexQuantifier(string regexString)
        {
            RegexString = regexString;
        }

        /// <summary>
        /// Quantifier to match the preceding element zero or more times
        /// </summary>
        public static RegexGreedyQuantifier ZeroOrMore => new RegexGreedyQuantifier("*");

        /// <summary>
        /// Quantifier to match the preceding element one or more times
        /// </summary>
        public static RegexGreedyQuantifier OneOrMore => new RegexGreedyQuantifier("+");

        /// <summary>
        /// Quantifier to match the preceding element once or not at all
        /// </summary>
        public static RegexGreedyQuantifier ZeroOrOne => new RegexGreedyQuantifier("?");

        /// <summary>
        /// Quantifier to match an exact number of occurrences of the preceding element
        /// </summary>
        /// <param name="times">The exact number of occurrences to match</param>
        public static RegexQuantifier Exactly(int times) => new RegexQuantifier("{" + times + "}");

        /// <summary>
        /// Quantifier to match at least a minimum number of occurrences of the preceding element
        /// </summary>
        /// <param name="minimum">The minimum number of occurrences to match</param>
        public static RegexGreedyQuantifier AtLeast(int minimum) =>
            new RegexGreedyQuantifier("{" + minimum + ",}");

        /// <summary>
        /// Quantifier to match no more than a maximum number of occurrences of the preceding element
        /// </summary>
        /// <param name="maximum">The maximum number of occurrences to match</param>
        public static RegexGreedyQuantifier NoMoreThan(int maximum) =>
            new RegexGreedyQuantifier("{0," + maximum + "}");

        /// <summary>
        /// Quantifier to match at least a minimum, and no more than a maximum, occurrences of the preceding element
        /// </summary>
        /// <param name="minimum">The minimum number of occurrences to match</param>
        /// <param name="maximum">The maximum number of occurrences to match</param>
        public static RegexGreedyQuantifier Between(int minimum, int maximum) =>
            new RegexGreedyQuantifier("{" + minimum + "," + maximum + "}");

        public override string ToString() => RegexString;

        /// <summary>
        /// A quantifier which defaults to greedy matching: in other words, if used
        /// to match a variable number of elements it will match as many as possible.
        /// </summary>
        public sealed class RegexGreedyQuantifier : RegexQuantifier
        {
            public RegexGreedyQuantifier(string regexString) : base(regexString)
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
                    RegexString += "?";
                    return this;
                }
            }
        }
    }
}
