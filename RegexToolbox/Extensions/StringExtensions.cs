using System;
using System.Text.RegularExpressions;

namespace RegexToolbox.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove all matches of the supplied Regex from this string, returning the modified string. If no match for
        /// the Regex is found in the string, the input string is returned.
        /// </summary>
        /// <param name="input">String to process</param>
        /// <param name="regex">Regex to match</param>
        /// <returns>A copy of this string with all matches of the regex removed</returns>
        public static string Remove(this string input, Regex regex)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (regex == null)
            {
                throw new ArgumentNullException(nameof(regex));
            }
            return regex.Remove(input);
        }
        
        /// <summary>
        /// Remove the first match of this Regex from the supplied string, returning the modified string. If no match
        /// for the Regex is found in the string, the input string is returned.
        /// </summary>
        /// <param name="input">String to process</param>
        /// <param name="regex">Regex to match</param>
        /// <returns>A copy of this string with all matches of the regex removed</returns>
        public static string RemoveFirst(this string input, Regex regex)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (regex == null)
            {
                throw new ArgumentNullException(nameof(regex));
            }
            return regex.RemoveFirst(input);
        }

        /// <summary>
        /// Remove the last match of this Regex from the supplied string, returning the modified string. If no match
        /// for the Regex is found in the string, the input string is returned.
        /// </summary>
        /// <param name="input">String to process</param>
        /// <param name="regex">Regex to match</param>
        /// <returns>A copy of this string with all matches of the regex removed</returns>
        public static string RemoveLast(this string input, Regex regex)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            if (regex == null)
            {
                throw new ArgumentNullException(nameof(regex));
            }
            return regex.RemoveLast(input);
        }
    }
}