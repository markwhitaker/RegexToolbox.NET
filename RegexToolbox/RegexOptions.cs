namespace RegexToolbox;

/// <summary>
/// Options that can be passed to <see cref="RegexBuilder"/>.<see cref="RegexBuilder.BuildRegex"/>.
/// </summary>
public enum RegexOptions
{
    /// <summary>
    /// Specifies that the regular expression is compiled to MSIL code, instead of being interpreted. Compiled
    /// regular expressions maximize run-time performance at the expense of initialization time.
    /// </summary>
    Compiled,

    /// <summary>
    /// Make the regex case-insensitive
    /// </summary>
    IgnoreCase,

    /// <summary>
    /// Cause StartOfString() and EndOfString() to also match line breaks within a multi-line string
    /// </summary>
    Multiline
}