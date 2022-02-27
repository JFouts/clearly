// Copyright (c) Justin Fouts All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Xunit;

namespace DomainModeling.Crud.Test.Unit;

public class FormatTokenizedStringTests
{
    [Fact]
    public void ItParses_SingleToken()
    {
        // Arrange
        var replacements = new Dictionary<string, string?> { { "Key", "Example" } };
        var pattern = "[Key]";

        // Act
        var result = pattern.FormatTokenizedString(replacements);

        // Assert
        Assert.Equal("Example", result);
    }

    [Fact]
    public void ItParses_BackToBackTokens()
    {
        // Arrange
        var replacements = new Dictionary<string, string?> { { "1", "back to " }, { "2", "back" } };
        var pattern = "These are [1][2].";

        // Act
        var result = pattern.FormatTokenizedString(replacements);

        // Assert
        Assert.Equal("These are back to back.", result);
    }

    [Fact]
    public void ItParses_EscapesInTokens()
    {
        // Arrange
        var replacements = new Dictionary<string, string?> { { "An[Odd]Key", "is" } };
        var pattern = "This [An[[Odd]]Key] escaped.";

        // Act
        var result = pattern.FormatTokenizedString(replacements);

        // Assert
        Assert.Equal("This is escaped.", result);
    }

    [Fact]
    public void ItParses_EscapesInTokens_ImmediatelyAfterAToken()
    {
        // Arrange
        var replacements = new Dictionary<string, string?> { { "First", "a " }, { "Second]Key", "test" } };
        var pattern = "This is [First][Second]]Key] of sorts.";

        // Act
        var result = pattern.FormatTokenizedString(replacements);

        // Assert
        Assert.Equal("This is a test of sorts.", result);
    }

    [Fact]
    public void ItParses_EscapesInTokens_AtEnds()
    {
        // Arrange
        var replacements = new Dictionary<string, string?> { { "Odd", "is" } };
        var pattern = "This [[[Odd]]] escaped.";

        // Act
        var result = pattern.FormatTokenizedString(replacements);

        // Assert
        Assert.Equal("This [is] escaped.", result);
    }

    [Fact]
    public void ItParses_RouteLikeTokens()
    {
        // Arrange
        var replacements = new Dictionary<string, string?> { { "Part1", "api" }, { "Part2", "controller" }, { "Part3", "action" } };
        var pattern = "[Part1]/[Part2]/[Part3]";

        // Act
        var result = pattern.FormatTokenizedString(replacements);

        // Assert
        Assert.Equal("api/controller/action", result);
    }

    [Fact]
    public void ItThrowsOn_UnclosedBrackets()
    {
        // Arrange
        var replacements = new Dictionary<string, string?>();
        var pattern = "This has an [ unclosed bracket!";

        // Act
        void Act() => pattern.FormatTokenizedString(replacements!);

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void ItThrowsOn_UnclosedBrackets_AtEnd()
    {
        // Arrange
        var replacements = new Dictionary<string, string?>();
        var pattern = "This has an unclosed bracket![";

        // Act
        void Act() => pattern.FormatTokenizedString(replacements!);

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void ItThrowsOn_UnMatchedClosingBracket()
    {
        // Arrange
        var replacements = new Dictionary<string, string?>();
        var pattern = "This has an unclosed bracket!]";

        // Act
        void Act() => pattern.FormatTokenizedString(replacements!);

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void ItThrowsOn_OpeningBracketInToken()
    {
        // Arrange
        var replacements = new Dictionary<string, string?>();
        var pattern = "This has [Brackets [in] a token]!";

        // Act
        void Act() => pattern.FormatTokenizedString(replacements!);

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }

    [Fact]
    public void ItThrowsOn_KeyNotFound()
    {
        // Arrange
        var replacements = new Dictionary<string, string?> { { "OtherKey", "Example" } };
        var pattern = "[Key]";

        // Act
        void Act() => pattern.FormatTokenizedString(replacements!);

        // Assert
        Assert.Throws<InvalidOperationException>(Act);
    }
}
