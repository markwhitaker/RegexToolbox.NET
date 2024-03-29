using System.Text.RegularExpressions;

namespace RegexToolbox;

public sealed partial class RegexBuilder
{
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

    private RegexBuilder StartNonCapturingGroup() => AddPart("(?:");

    private RegexBuilder EndGroup(RegexQuantifier quantifier = null) => AddPart(")", quantifier);
}