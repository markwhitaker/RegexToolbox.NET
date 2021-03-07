namespace RegexToolbox
{
    public sealed partial class RegexBuilder
    {
        /// <summary>
        /// Add a zero-width anchor element to match the start of the string
        /// </summary>
        public RegexBuilder StartOfString() => AddPart("^");

        /// <summary>
        /// Add a zero-width anchor element to match the end of the string
        /// </summary>
        public RegexBuilder EndOfString() => AddPart("$");

        /// <summary>
        /// Add a zero-width anchor element to match the boundary between an alphanumeric/underscore character
        /// and either a non-alphanumeric, non-underscore character or the start/end of the string.
        /// </summary>
        public RegexBuilder WordBoundary() => AddPart(@"\b");
    }
}
