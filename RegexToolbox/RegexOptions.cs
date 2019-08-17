namespace RegexToolbox
{
    /// <summary>
    /// Options that can be passed to <see cref="RegexBuilder"/>.BuildRegex.
    /// </summary>
    public enum RegexOptions
    {
        /// <summary>
        /// Make the regex case-insensitive
        /// </summary>
        IgnoreCase,

        /// <summary>
        /// Cause StartOfString() and EndOfString() to also match line breaks within a multi-line string
        /// </summary>
        Multiline
    }
}