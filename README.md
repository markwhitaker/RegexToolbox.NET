![icon](Artwork/RegexToolbox-icon-100.png)

# RegexToolbox.NET [![GitHub Actions build](https://github.com/markwhitaker/RegexToolbox.NET/workflows/Build%20and%20test/badge.svg)](https://github.com/markwhitaker/RegexToolbox.NET/actions) [![NuGet Version and Downloads count](https://buildstats.info/nuget/RegexToolbox)](https://www.nuget.org/packages/RegexToolbox/)

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

## New in 1.3: Logging

Use the new `AddLogger()` method to connect a logger of your choice and see how your regex is built, step by step. For example:

```c#
var regex = new RegexBuilder()
    .AddLogger(Console.WriteLine)
    .WordBoundary()
    .Text("Regex")
    .AnyOf("Builder", "Toolbox")
    .WordBoundary()
    .BuildRegex();
```

will output this to your console:

```text
RegexBuilder: WordBoundary(): \b
RegexBuilder: Text("Regex"): Regex
RegexBuilder: AnyOf("Builder", "Toolbox"): (?:Builder|Toolbox)
RegexBuilder: WordBoundary(): \b
RegexBuilder: BuildRegex(): \bRegex(?:Builder|Toolbox)\b
```

---
![icon](https://raw.githubusercontent.com/markwhitaker/RegexToolbox.Java/master/artwork/RegexToolbox-icon-32.png) **Java developer?** Check out the Java version of this library, [RegexToolbox.Java](https://github.com/markwhitaker/RegexToolbox.Java).

![icon](https://raw.githubusercontent.com/markwhitaker/RegexToolbox.kt/master/artwork/RegexToolbox-icon-32.png) **Kotlin developer?** Check out the Kotlin version of this library, [RegexToolbox.kt](https://github.com/markwhitaker/RegexToolbox.kt).

![icon](https://raw.githubusercontent.com/markwhitaker/RegexToolbox.JS/master/artwork/RegexToolbox-icon-32.png) **Web developer?** Check out the web version of this library, [RegexToolbox.JS](https://github.com/markwhitaker/RegexToolbox.JS).

---
###### **RegexToolbox:** Now you can be a [hero](https://xkcd.com/208/) without knowing regular expressions.
