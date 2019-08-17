namespace RegexToolbox.Tests
{
    public static class Strings
    {
        #region Character classes
        public const string BothCaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public const string UpperCaseAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public const string LowerCaseAlphabet = "abcdefghijklmnopqrstuvwxyz";
        public const string DecimalDigits = "0123456789";
        public const string BothCaseHexDigits = "0123456789ABCDEFabcdef";
        public const string UpperCaseHexDigits = "0123456789ABCDEF";
        public const string LowerCaseHexDigits = "0123456789abcdef";
        public const string Symbols = "!\"\\|£$%^&*()-=_+[]{};'#:@~,./<>?";
        public const string WhiteSpace = " \t\n\r\v\f";
        public const string ControlCharacters = "\a\b";
        public const string Empty = "";
        #endregion

        #region Example strings
        public const string SimpleName = "Jo Smith";
        public const string SimpleEmailAddress = "regex.toolbox@mainwave.co.uk";
        public const string SimpleHttpUrl = "http://www.website.com/";
        public const string SimpleHttpsUrl = "https://www.website.com/";
        public const string Ipv4Address = "172.15.254.1";
        public const string Ipv6Address = "2001:0db8:85a3:0000:0000:8a2e:0370:7334";
        public const string MacAddress = "00:3e:e1:c4:5d:df";
        #endregion
    }
}