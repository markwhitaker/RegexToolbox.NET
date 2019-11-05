using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace RegexToolbox.Tests
{
    [TestFixture]
    public class RegexBuilderLoggingTests
    {
        private string _logOutput;
        private RegexBuilder _regexBuilder;

        [SetUp]
        public void SetUp()
        {
            _logOutput = string.Empty;
            _regexBuilder = new RegexBuilder().AddLogger(s => _logOutput = s);
        }
        
        [Test]
        public void TestAddLoggerInterface()
        {
            var logger = Substitute.For<RegexBuilder.ILogger>();

            new RegexBuilder()
                .AddLogger(logger)
                .Text("hello")
                .Whitespace(RegexQuantifier.OneOrMore)
                .Text("world", RegexQuantifier.ZeroOrOne)
                .BuildRegex();

            Received.InOrder(() =>
            {
                logger.Log(@"RegexBuilder: Text(""hello"") => hello");
                logger.Log(@"RegexBuilder: Whitespace(OneOrMore) => \s+");
                logger.Log(@"RegexBuilder: Text(""world"", ZeroOrOne) => (?:world)?");
                logger.Log(@"RegexBuilder: BuildRegex() => hello\s+(?:world)?");
            });
        }

        [Test]
        public void TestAddLoggerInterfaceWithTag()
        {
            var logger = Substitute.For<RegexBuilder.ILogger>();

            new RegexBuilder()
                .AddLogger(logger, "TEST")
                .Text("hello")
                .Whitespace(RegexQuantifier.OneOrMore)
                .Text("world", RegexQuantifier.ZeroOrOne)
                .BuildRegex();

            Received.InOrder(() =>
            {
                logger.Log(@"TEST: Text(""hello"") => hello");
                logger.Log(@"TEST: Whitespace(OneOrMore) => \s+");
                logger.Log(@"TEST: Text(""world"", ZeroOrOne) => (?:world)?");
                logger.Log(@"TEST: BuildRegex() => hello\s+(?:world)?");
            });
        }

        [Test]
        public void TestAddLoggerLambda()
        {
            var list = new List<string>();
            
            new RegexBuilder()
                .AddLogger(s => list.Add(s))
                .Text("hello")
                .Whitespace(RegexQuantifier.OneOrMore)
                .Text("world", RegexQuantifier.ZeroOrOne)
                .BuildRegex();

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(@"RegexBuilder: Text(""hello"") => hello", list[0]);
            Assert.AreEqual(@"RegexBuilder: Whitespace(OneOrMore) => \s+", list[1]);
            Assert.AreEqual(@"RegexBuilder: Text(""world"", ZeroOrOne) => (?:world)?", list[2]);
            Assert.AreEqual(@"RegexBuilder: BuildRegex() => hello\s+(?:world)?", list[3]);
        }

        [Test]
        public void TestAddLoggerLambdaWithTag()
        {
            var list = new List<string>();
            
            new RegexBuilder()
                .AddLogger(s => list.Add(s), "TEST")
                .Text("hello")
                .Whitespace(RegexQuantifier.OneOrMore)
                .Text("world", RegexQuantifier.ZeroOrOne)
                .BuildRegex();

            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(@"TEST: Text(""hello"") => hello", list[0]);
            Assert.AreEqual(@"TEST: Whitespace(OneOrMore) => \s+", list[1]);
            Assert.AreEqual(@"TEST: Text(""world"", ZeroOrOne) => (?:world)?", list[2]);
            Assert.AreEqual(@"TEST: BuildRegex() => hello\s+(?:world)?", list[3]);
        }

        [Test]
        public void TestText()
        {
            _regexBuilder.Text("[a-z]");
            Assert.AreEqual(@"RegexBuilder: Text(""[a-z]"") => \[a-z\]", _logOutput);
        }

        [Test]
        public void TestTextWithQuantifier()
        {
            _regexBuilder.Text("[a-z]", RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: Text(""[a-z]"", ZeroOrMore) => (?:\[a-z\])*", _logOutput);
        }

        [Test]
        public void TestRegexText()
        {
            _regexBuilder.RegexText("[a-z]");
            Assert.AreEqual(@"RegexBuilder: RegexText(""[a-z]"") => [a-z]", _logOutput);
        }

        [Test]
        public void TestRegexTextWithQuantifier()
        {
            _regexBuilder.RegexText("[a-z]", RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: RegexText(""[a-z]"", ZeroOrMore) => (?:[a-z])*", _logOutput);
        }
        
        [Test]
        public void TestAnyCharacter()
        {
            _regexBuilder.AnyCharacter();
            Assert.AreEqual(@"RegexBuilder: AnyCharacter() => .", _logOutput);
        }

        [Test]
        public void TestAnyCharacterWithQuantifier()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(ZeroOrMore) => .*", _logOutput);
        }
        
        [Test]
        public void TestWhitespace()
        {
            _regexBuilder.Whitespace();
            Assert.AreEqual(@"RegexBuilder: Whitespace() => \s", _logOutput);
        }

        [Test]
        public void TestWhitespaceWithQuantifier()
        {
            _regexBuilder.Whitespace(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: Whitespace(ZeroOrMore) => \s*", _logOutput);
        }
        
        [Test]
        public void TestNonWhitespace()
        {
            _regexBuilder.NonWhitespace();
            Assert.AreEqual(@"RegexBuilder: NonWhitespace() => \S", _logOutput);
        }

        [Test]
        public void TestNonWhitespaceWithQuantifier()
        {
            _regexBuilder.NonWhitespace(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: NonWhitespace(ZeroOrMore) => \S*", _logOutput);
        }
        
        [Test]
        public void TestPossibleWhitespace()
        {
            _regexBuilder.PossibleWhitespace();
            Assert.AreEqual(@"RegexBuilder: PossibleWhitespace() => \s*", _logOutput);
        }
        
        [Test]
        public void TestSpace()
        {
            _regexBuilder.Space();
            Assert.AreEqual(@"RegexBuilder: Space() =>  ", _logOutput);
        }

        [Test]
        public void TestSpaceWithQuantifier()
        {
            _regexBuilder.Space(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: Space(ZeroOrMore) =>  *", _logOutput);
        }
        
        [Test]
        public void TestTab()
        {
            _regexBuilder.Tab();
            Assert.AreEqual(@"RegexBuilder: Tab() => \t", _logOutput);
        }

        [Test]
        public void TestTabWithQuantifier()
        {
            _regexBuilder.Tab(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: Tab(ZeroOrMore) => \t*", _logOutput);
        }
        
        [Test]
        public void TestLineFeed()
        {
            _regexBuilder.LineFeed();
            Assert.AreEqual(@"RegexBuilder: LineFeed() => \n", _logOutput);
        }

        [Test]
        public void TestLineFeedWithQuantifier()
        {
            _regexBuilder.LineFeed(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: LineFeed(ZeroOrMore) => \n*", _logOutput);
        }
        
        [Test]
        public void TestCarriageReturn()
        {
            _regexBuilder.CarriageReturn();
            Assert.AreEqual(@"RegexBuilder: CarriageReturn() => \r", _logOutput);
        }

        [Test]
        public void TestCarriageReturnWithQuantifier()
        {
            _regexBuilder.CarriageReturn(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: CarriageReturn(ZeroOrMore) => \r*", _logOutput);
        }
        
        [Test]
        public void TestDigit()
        {
            _regexBuilder.Digit();
            Assert.AreEqual(@"RegexBuilder: Digit() => \d", _logOutput);
        }

        [Test]
        public void TestDigitWithQuantifier()
        {
            _regexBuilder.Digit(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: Digit(ZeroOrMore) => \d*", _logOutput);
        }
        
        [Test]
        public void TestNonDigit()
        {
            _regexBuilder.NonDigit();
            Assert.AreEqual(@"RegexBuilder: NonDigit() => \D", _logOutput);
        }

        [Test]
        public void TestNonDigitWithQuantifier()
        {
            _regexBuilder.NonDigit(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: NonDigit(ZeroOrMore) => \D*", _logOutput);
        }
        
        [Test]
        public void TestLetter()
        {
            _regexBuilder.Letter();
            Assert.AreEqual(@"RegexBuilder: Letter() => \p{L}", _logOutput);
        }

        [Test]
        public void TestLetterWithQuantifier()
        {
            _regexBuilder.Letter(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: Letter(ZeroOrMore) => \p{L}*", _logOutput);
        }
        
        [Test]
        public void TestNonLetter()
        {
            _regexBuilder.NonLetter();
            Assert.AreEqual(@"RegexBuilder: NonLetter() => \P{L}", _logOutput);
        }

        [Test]
        public void TestNonLetterWithQuantifier()
        {
            _regexBuilder.NonLetter(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: NonLetter(ZeroOrMore) => \P{L}*", _logOutput);
        }
        
        [Test]
        public void TestUppercaseLetter()
        {
            _regexBuilder.UppercaseLetter();
            Assert.AreEqual(@"RegexBuilder: UppercaseLetter() => \p{Lu}", _logOutput);
        }

        [Test]
        public void TestUppercaseLetterWithQuantifier()
        {
            _regexBuilder.UppercaseLetter(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: UppercaseLetter(ZeroOrMore) => \p{Lu}*", _logOutput);
        }
        
        [Test]
        public void TestLowercaseLetter()
        {
            _regexBuilder.LowercaseLetter();
            Assert.AreEqual(@"RegexBuilder: LowercaseLetter() => \p{Ll}", _logOutput);
        }

        [Test]
        public void TestLowercaseLetterWithQuantifier()
        {
            _regexBuilder.LowercaseLetter(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: LowercaseLetter(ZeroOrMore) => \p{Ll}*", _logOutput);
        }
        
        [Test]
        public void TestLetterOrDigit()
        {
            _regexBuilder.LetterOrDigit();
            Assert.AreEqual(@"RegexBuilder: LetterOrDigit() => [\p{L}0-9]", _logOutput);
        }

        [Test]
        public void TestLetterOrDigitWithQuantifier()
        {
            _regexBuilder.LetterOrDigit(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: LetterOrDigit(ZeroOrMore) => [\p{L}0-9]*", _logOutput);
        }
        
        [Test]
        public void TestNonLetterOrDigit()
        {
            _regexBuilder.NonLetterOrDigit();
            Assert.AreEqual(@"RegexBuilder: NonLetterOrDigit() => [^\p{L}0-9]", _logOutput);
        }

        [Test]
        public void TestNonLetterOrDigitWithQuantifier()
        {
            _regexBuilder.NonLetterOrDigit(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: NonLetterOrDigit(ZeroOrMore) => [^\p{L}0-9]*", _logOutput);
        }
        
        [Test]
        public void TestHexDigit()
        {
            _regexBuilder.HexDigit();
            Assert.AreEqual(@"RegexBuilder: HexDigit() => [0-9A-Fa-f]", _logOutput);
        }

        [Test]
        public void TestHexDigitWithQuantifier()
        {
            _regexBuilder.HexDigit(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: HexDigit(ZeroOrMore) => [0-9A-Fa-f]*", _logOutput);
        }
        
        [Test]
        public void TestUppercaseHexDigit()
        {
            _regexBuilder.UppercaseHexDigit();
            Assert.AreEqual(@"RegexBuilder: UppercaseHexDigit() => [0-9A-F]", _logOutput);
        }

        [Test]
        public void TestUppercaseHexDigitWithQuantifier()
        {
            _regexBuilder.UppercaseHexDigit(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: UppercaseHexDigit(ZeroOrMore) => [0-9A-F]*", _logOutput);
        }
        
        [Test]
        public void TestLowercaseHexDigit()
        {
            _regexBuilder.LowercaseHexDigit();
            Assert.AreEqual(@"RegexBuilder: LowercaseHexDigit() => [0-9a-f]", _logOutput);
        }

        [Test]
        public void TestLowercaseHexDigitWithQuantifier()
        {
            _regexBuilder.LowercaseHexDigit(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: LowercaseHexDigit(ZeroOrMore) => [0-9a-f]*", _logOutput);
        }
         
        [Test]
        public void TestNonHexDigit()
        {
            _regexBuilder.NonHexDigit();
            Assert.AreEqual(@"RegexBuilder: NonHexDigit() => [^0-9A-Fa-f]", _logOutput);
        }

        [Test]
        public void TestNonHexDigitWithQuantifier()
        {
            _regexBuilder.NonHexDigit(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: NonHexDigit(ZeroOrMore) => [^0-9A-Fa-f]*", _logOutput);
        }
        
        [Test]
        public void TestWordCharacter()
        {
            _regexBuilder.WordCharacter();
            Assert.AreEqual(@"RegexBuilder: WordCharacter() => [\p{L}0-9_]", _logOutput);
        }

        [Test]
        public void TestWordCharacterWithQuantifier()
        {
            _regexBuilder.WordCharacter(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: WordCharacter(ZeroOrMore) => [\p{L}0-9_]*", _logOutput);
        }
        
        [Test]
        public void TestNonWordCharacter()
        {
            _regexBuilder.NonWordCharacter();
            Assert.AreEqual(@"RegexBuilder: NonWordCharacter() => [^\p{L}0-9_]", _logOutput);
        }

        [Test]
        public void TestNonWordCharacterWithQuantifier()
        {
            _regexBuilder.NonWordCharacter(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: NonWordCharacter(ZeroOrMore) => [^\p{L}0-9_]*", _logOutput);
        }
        
        [Test]
        public void TestAnyCharacterFrom()
        {
            _regexBuilder.AnyCharacterFrom("abc");
            Assert.AreEqual(@"RegexBuilder: AnyCharacterFrom(""abc"") => [abc]", _logOutput);
        }

        [Test]
        public void TestAnyCharacterFromWithQuantifier()
        {
            _regexBuilder.AnyCharacterFrom("abc", RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyCharacterFrom(""abc"", ZeroOrMore) => [abc]*", _logOutput);
        }
        
        [Test]
        public void TestAnyCharacterExcept()
        {
            _regexBuilder.AnyCharacterExcept("abc");
            Assert.AreEqual(@"RegexBuilder: AnyCharacterExcept(""abc"") => [^abc]", _logOutput);
        }

        [Test]
        public void TestAnyCharacterExceptWithQuantifier()
        {
            _regexBuilder.AnyCharacterExcept("abc", RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyCharacterExcept(""abc"", ZeroOrMore) => [^abc]*", _logOutput);
        }
        
        [Test]
        public void TestAnyOfNull()
        {
            _regexBuilder.AnyOf(null);
            Assert.AreEqual(@"RegexBuilder: AnyOf() => strings collection is null, so doing nothing", _logOutput);
        }
        
        [Test]
        public void TestAnyOfNullWithQuantifier()
        {
            _regexBuilder.AnyOf(null, RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyOf() => strings collection is null, so doing nothing", _logOutput);
        }
        
        [Test]
        public void TestAnyOfEmpty()
        {
            _regexBuilder.AnyOf(new List<string>());
            Assert.AreEqual(@"RegexBuilder: AnyOf() => strings collection is empty, so doing nothing", _logOutput);
        }
        
        [Test]
        public void TestAnyOfEmptyWithQuantifier()
        {
            _regexBuilder.AnyOf(new List<string>(), RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyOf() => strings collection is empty, so doing nothing", _logOutput);
        }
        
        [Test]
        public void TestAnyOfSingle()
        {
            _regexBuilder.AnyOf(new List<string>
            {
                "abc"
            });
            Assert.AreEqual(@"RegexBuilder: AnyOf(""abc"") => abc", _logOutput);
        }
        
        [Test]
        public void TestAnyOfSingleWithQuantifier()
        {
            _regexBuilder.AnyOf(new List<string>
            {
                "abc"
            }, RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyOf(""abc"", ZeroOrMore) => (?:abc)*", _logOutput);
        }
        
        [Test]
        public void TestAnyOfParamsSingle()
        {
            _regexBuilder.AnyOf("abc");
            Assert.AreEqual(@"RegexBuilder: AnyOf(""abc"") => abc", _logOutput);
        }
        
        [Test]
        public void TestAnyOfMultiple()
        {
            _regexBuilder.AnyOf(new List<string>
            {
                "abc", "def"
            });
            Assert.AreEqual(@"RegexBuilder: AnyOf(""abc"", ""def"") => (?:abc|def)", _logOutput);
        }
        
        [Test]
        public void TestAnyOfMultipleWithQuantifier()
        {
            _regexBuilder.AnyOf(new List<string>
            {
                "abc", "def"
            }, RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyOf(""abc"", ""def"", ZeroOrMore) => (?:abc|def)*", _logOutput);
        }
        
        [Test]
        public void TestAnyOfParamsMultiple()
        {
            _regexBuilder.AnyOf("abc", "def");
            Assert.AreEqual(@"RegexBuilder: AnyOf(""abc"", ""def"") => (?:abc|def)", _logOutput);
        }
        
        [Test]
        public void TestStartOfString()
        {
            _regexBuilder.StartOfString();
            Assert.AreEqual(@"RegexBuilder: StartOfString() => ^", _logOutput);
        }
        
        [Test]
        public void TestEndOfString()
        {
            _regexBuilder.EndOfString();
            Assert.AreEqual(@"RegexBuilder: EndOfString() => $", _logOutput);
        }
        
        [Test]
        public void TestWordBoundary()
        {
            _regexBuilder.WordBoundary();
            Assert.AreEqual(@"RegexBuilder: WordBoundary() => \b", _logOutput);
        }
        
        [Test]
        public void TestStartGroup()
        {
            _regexBuilder.StartGroup();
            Assert.AreEqual(@"RegexBuilder: StartGroup() => (", _logOutput);
        }

        [Test]
        public void TestStartNonCapturingGroup()
        {
            _regexBuilder.StartNonCapturingGroup();
            Assert.AreEqual(@"RegexBuilder: StartNonCapturingGroup() => (?:", _logOutput);
        }

        [Test]
        public void TestNamedGroup()
        {
            _regexBuilder.StartNamedGroup("bert");
            Assert.AreEqual(@"RegexBuilder: StartNamedGroup(""bert"") => (?<bert>", _logOutput);
        }

        [Test]
        public void TestEndGroup()
        {
            _regexBuilder.StartGroup().EndGroup();
            Assert.AreEqual(@"RegexBuilder: EndGroup() => )", _logOutput);
        }
        
        [Test]
        public void TestQuantifierZeroOrMore()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.ZeroOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(ZeroOrMore) => .*", _logOutput);
        }
        
        [Test]
        public void TestQuantifierZeroOrMoreButAsFewAsPossible()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.ZeroOrMore.ButAsFewAsPossible);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(ZeroOrMore.ButAsFewAsPossible) => .*?", _logOutput);
        }
        
        [Test]
        public void TestQuantifierOneOrMore()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.OneOrMore);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(OneOrMore) => .+", _logOutput);
        }
        
        [Test]
        public void TestQuantifierOneOrMoreButAsFewAsPossible()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.OneOrMore.ButAsFewAsPossible);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(OneOrMore.ButAsFewAsPossible) => .+?", _logOutput);
        }
        
        [Test]
        public void TestQuantifierZeroOrOne()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.ZeroOrOne);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(ZeroOrOne) => .?", _logOutput);
        }
        
        [Test]
        public void TestQuantifierZeroOrOneButAsFewAsPossible()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.ZeroOrOne.ButAsFewAsPossible);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(ZeroOrOne.ButAsFewAsPossible) => .??", _logOutput);
        }
        
        [Test]
        public void TestQuantifierExactly()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.Exactly(10));
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(Exactly(10)) => .{10}", _logOutput);
        }
        
        [Test]
        public void TestQuantifierAtLeast()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.AtLeast(10));
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(AtLeast(10)) => .{10,}", _logOutput);
        }
        
        [Test]
        public void TestQuantifierAtLeastButAsFewAsPossible()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.AtLeast(10).ButAsFewAsPossible);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(AtLeast(10).ButAsFewAsPossible) => .{10,}?", _logOutput);
        }
        
        [Test]
        public void TestQuantifierNoMoreThan()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.NoMoreThan(10));
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(NoMoreThan(10)) => .{0,10}", _logOutput);
        }
        
        [Test]
        public void TestQuantifierNoMoreThanButAsFewAsPossible()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.NoMoreThan(10).ButAsFewAsPossible);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(NoMoreThan(10).ButAsFewAsPossible) => .{0,10}?", _logOutput);
        }
        
        [Test]
        public void TestQuantifierBetween()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.Between(10, 20));
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(Between(10, 20)) => .{10,20}", _logOutput);
        }
        
        [Test]
        public void TestQuantifierBetweenButAsFewAsPossible()
        {
            _regexBuilder.AnyCharacter(RegexQuantifier.Between(10, 20).ButAsFewAsPossible);
            Assert.AreEqual(@"RegexBuilder: AnyCharacter(Between(10, 20).ButAsFewAsPossible) => .{10,20}?", _logOutput);
        }
    }
}