using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RegexToolbox.Extensions;

public static class RegexExtensions
{
    /// <summary>
    /// Remove all matches of this Regex from the supplied string, returning the modified string. If no match for
    /// the Regex is found in the string, the input string is returned.
    /// </summary>
    /// <param name="regex">Regex to match</param>
    /// <param name="input">String to process</param>
    /// <returns>A copy of the input string with all matches of the regex removed</returns>
    /// <exception cref="ArgumentNullException">input is null</exception>
    public static string Remove(this Regex regex, string input)
    {
        if (regex == null)
        {
            throw new ArgumentNullException(nameof(regex));
        }
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }
        return regex.Replace(input, string.Empty);
    }

    /// <summary>
    /// Remove the first match of this Regex from the supplied string, returning the modified string. If no match
    /// for the Regex is found in the string, the input string is returned.
    /// </summary>
    /// <param name="regex">Regex to match</param>
    /// <param name="input">String to process</param>
    /// <returns>A copy of the input string with the first match of the regex removed</returns>
    /// <exception cref="ArgumentNullException">input is null</exception>
    public static string RemoveFirst(this Regex regex, string input)
    {
        if (regex == null)
        {
            throw new ArgumentNullException(nameof(regex));
        }
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }
        return regex.Replace(input, string.Empty, 1);
    }

    /// <summary>
    /// Remove the last match of this Regex from the supplied string, returning the modified string. If no match
    /// for the Regex is found in the string, the input string is returned.
    /// </summary>
    /// <param name="regex">Regex to match</param>
    /// <param name="input">String to process</param>
    /// <returns>A copy of the input string with the last match of the regex removed</returns>
    /// <exception cref="ArgumentNullException">input is null</exception>
    public static string RemoveLast(this Regex regex, string input)
    {
        if (regex == null)
        {
            throw new ArgumentNullException(nameof(regex));
        }
        if (input == null)
        {
            throw new ArgumentNullException(nameof(input));
        }
        var matches = regex.Matches(input);
        if (matches.Count == 0)
        {
            return input;
        }
        return regex.Replace(
            input,
            string.Empty,
            1,
            matches.Cast<Match>().Last().Index);
    }
}