![icon](Artwork/RegexToolbox-icon-100.png)

# RegexToolbox.NET [![GitHub Workflow Status](https://img.shields.io/github/workflow/status/markwhitaker/RegexToolbox.NET/build-and-test)](https://github.com/markwhitaker/RegexToolbox.NET/actions) [![NuGet Version and Downloads count](https://buildstats.info/nuget/RegexToolbox)](https://www.nuget.org/packages/RegexToolbox/)

Regular expression tools for .NET developers.


## RegexBuilder

`RegexBuilder` is a class for building regular expressions in a more human-readable way using a fluent API. It offers a number of benefits over using raw regex syntax in strings:

 - No knowledge of regular expression syntax is required: just use simple, intuitively-named classes and methods.
 - Code is easier to read, understand and maintain.
 - Code is safer and far less prone to regular expression syntax errors and programmer errors.

Here's an example:

```c#
var regex = new RegexBuilder()
    .Text("Hello")
    .Whitespace(RegexQuantifier.OneOrMore)
    .Text("world!")
    .BuildRegex();
```

But that's just a taste of what `RegexBuilder` does: for full API documentation, head over to the [project wiki](https://github.com/markwhitaker/RegexToolbox.NET/wiki).

## Breaking changes in 2.0

All `RegexBuilder` features that were deprecated in version 1.6 have been removed in 2.0.

|Removed|Replaced with|
|---|---|
|`StartGroup()`...`EndGroup()`|`Group()`|
|`StartNamedGroup()`...`EndGroup()`|`NamedGroup()`|
|`StartNonCapturingGroup()`...`EndGroup()`|`NonCapturingGroup()`|
|`AddLogger()`|No replacement: logging has been removed.|

## New in 1.6

### Improved grouping methods

Version 1.6 introduces a simplified syntax for cleaner, less error-prone creating of groups. Go from this:

```c#
var regex = new RegexBuilder()
    .StartGroup()
    .Letter()
    .Digit()
    .BuildRegex(); // ERROR: forgot to call EndGroup()
```

to this:

```c#
var regex = new RegexBuilder()
    .Group(r => r
        .Letter()
        .Digit()
    ) // Yay! Can't forget to end the group
    .BuildRegex();
```

There are also new `NamedGroup()` and `NonCapturingGroup()` methods.

**The old methods `StartGroup()`, `StartNamedGroup()`, `StartNonCapturingGroup()` and `EndGroup()` have been deprecated and will be removed in version 2.0.**

### More powerful `AnyOf()` overloads

The `AnyOf()` method is used to match any of a set of alternatives.
Previously it only let you specify strings as the alternatives, for example:

```c#
var animalLoverRegex = new RegexBuilder()
    .Text("I love ")
    .AnyOf("cats", "dogs", "tortoises")
    .BuildRegex();
```

Now you can specify arbitrarily complex sub-regexes as the alternatives, such as:

```c#
// Match 3 letters followed by a full stop OR 4 digits followed by a comma
// (for some weird reason)
var regex = new RegexBuilder()
    .AnyOf(
        r => r
            .Letter(Exactly(3))
            .Text("."),
        r => r
            .Digit(Exactly(4))
            .Text(",")
    )
    .BuildRegex();
```

### New `string` extension method

`string.Replace(Regex regex, string replacement)`

### Logging deprecated (removed in version 2.0)

The logging feature introduced in version 1.3 wasn't proving as useful as I'd imagined and had introduced various maintenance difficulties.
In this release the APIs are deprecated and do nothing. **Logging will be removed altogether in version 2.0.**

## Also for .NET developers

![icon](https://raw.githubusercontent.com/markwhitaker/MimeTypes.NET/main/Artwork/MimeTypes-icon-32.png) [MimeTypes.NET](https://github.com/markwhitaker/MimeTypes.NET): MIME type constants for your .NET projects

## RegexToolbox for other languages

![icon](https://raw.githubusercontent.com/markwhitaker/RegexToolbox.Java/master/artwork/RegexToolbox-icon-32.png) [RegexToolbox for Java](https://github.com/markwhitaker/RegexToolbox.Java)

![icon](https://raw.githubusercontent.com/markwhitaker/RegexToolbox.kt/master/artwork/RegexToolbox-icon-32.png) [RegexToolbox for Kotlin](https://github.com/markwhitaker/RegexToolbox.kt)

![icon](https://raw.githubusercontent.com/markwhitaker/RegexToolbox.JS/master/artwork/RegexToolbox-icon-32.png) [RegexToolbox for JavaScript](https://github.com/markwhitaker/RegexToolbox.JS)

---
###### **RegexToolbox:** Now you can be a [hero](https://xkcd.com/208/) without knowing regular expressions.
