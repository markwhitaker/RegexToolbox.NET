using System;
using System.Text.RegularExpressions;
using NUnit.Framework;
using RegexToolbox.Extensions;

namespace RegexToolbox.Tests.Extensions
{
    [TestFixture]
    public class RegexExtensionsTests
    {
        [Test]
        public void GivenInputStringContainingOneRegexMatch_WhenRemove_ThenMatchIsRemovedFromString()
        {
            // Given
            const string input = "Hello there friendly world";
            var regex = new RegexBuilder()
                .Whitespace()
                .Text("there")
                .BuildRegex();

            // When
            var actualResult = regex.Remove(input);
            
            // Then
            Assert.That(actualResult, Is.EqualTo("Hello friendly world"));
        }

        [Test]
        public void GivenInputStringContainingMultipleRegexMatches_WhenRemove_ThenAllMatchesAreRemovedFromString()
        {
            // Given
            const string input = "Hello there friendly world";
            var regex = new RegexBuilder()
                .Whitespace()
                .Letter(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When
            var actualResult = regex.Remove(input);
            
            // Then
            Assert.That(actualResult, Is.EqualTo("Hello"));
        }

        [Test]
        public void GivenInputStringContainingMultipleRegexMatches_WhenRemoveFirst_ThenFirstMatchIsRemovedFromString()
        {
            // Given
            const string input = "Hello there friendly world";
            var regex = new RegexBuilder()
                .Whitespace()
                .Letter(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When
            var actualResult = regex.RemoveFirst(input);
            
            // Then
            Assert.That(actualResult, Is.EqualTo("Hello friendly world"));
        }

        [Test]
        public void GivenInputStringContainingMultipleRegexMatches_WhenRemoveLast_ThenLastMatchIsRemovedFromString()
        {
            // Given
            const string input = "Hello there friendly world";
            var regex = new RegexBuilder()
                .Whitespace()
                .Letter(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When
            var actualResult = regex.RemoveLast(input);
            
            // Then
            Assert.That(actualResult, Is.EqualTo("Hello there friendly"));
        }

        [TestCase("Hello world")]
        [TestCase("")]
        public void GivenInputStringContainingNoRegexMatches_WhenRemove_ThenStringIsUnaltered(string input)
        {
            // Given
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When
            var actualResult = regex.Remove(input);
            
            // Then
            Assert.That(actualResult, Is.EqualTo(input));
        }

        [TestCase("Hello world")]
        [TestCase("")]
        public void GivenInputStringContainingNoRegexMatches_WhenRemoveFirst_ThenStringIsUnaltered(string input)
        {
            // Given
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When
            var actualResult = regex.RemoveFirst(input);
            
            // Then
            Assert.That(actualResult, Is.EqualTo(input));
        }

        [TestCase("Hello world")]
        [TestCase("")]
        public void GivenInputStringContainingNoRegexMatches_WhenRemoveLast_ThenStringIsUnaltered(string input)
        {
            // Given
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When
            var actualResult = regex.RemoveLast(input);
            
            // Then
            Assert.That(actualResult, Is.EqualTo(input));
        }

        [Test]
        public void GivenNullInputString_WhenRemove_ThenArgumentNullExceptionIsThrown()
        {
            // Given
            const string input = null;
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When/Then
            var exception = Assert.Throws<ArgumentNullException>(() => regex.Remove(input));
            
            // Then
            Assert.That(exception.ParamName, Is.EqualTo("input"));
        }

        [Test]
        public void GivenNullInputString_WhenRemoveFirst_ThenArgumentNullExceptionIsThrown()
        {
            // Given
            const string input = null;
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When/Then
            var exception = Assert.Throws<ArgumentNullException>(() => regex.RemoveFirst(input));
            
            // Then
            Assert.That(exception.ParamName, Is.EqualTo("input"));
        }

        [Test]
        public void GivenNullInputString_WhenRemoveLast_ThenArgumentNullExceptionIsThrown()
        {
            // Given
            const string input = null;
            var regex = new RegexBuilder()
                .Digit(RegexQuantifier.OneOrMore)
                .BuildRegex();

            // When/Then
            var exception = Assert.Throws<ArgumentNullException>(() => regex.RemoveLast(input));
            
            // Then
            Assert.That(exception.ParamName, Is.EqualTo("input"));
        }
 
        [Test]
        public void GivenNullRegex_WhenRemove_ThenArgumentNullExceptionIsThrown()
        {
            // Given
            const string input = "Hello world";
            Regex regex = null;
            
            // When/Then
            var exception = Assert.Throws<ArgumentNullException>(() => regex.Remove(input));
            
            // Then
            Assert.That(exception.ParamName, Is.EqualTo("regex"));
        }
 
        [Test]
        public void GivenNullRegex_WhenRemoveFirst_ThenArgumentNullExceptionIsThrown()
        {
            // Given
            const string input = "Hello world";
            Regex regex = null;
            
            // When/Then
            var exception = Assert.Throws<ArgumentNullException>(() => regex.RemoveFirst(input));
            
            // Then
            Assert.That(exception.ParamName, Is.EqualTo("regex"));
        }
 
        [Test]
        public void GivenNullRegex_WhenRemoveLast_ThenArgumentNullExceptionIsThrown()
        {
            // Given
            const string input = "Hello world";
            Regex regex = null;
            
            // When/Then
            var exception = Assert.Throws<ArgumentNullException>(() => regex.RemoveLast(input));
            
            // Then
            Assert.That(exception.ParamName, Is.EqualTo("regex"));
        }
    }
}