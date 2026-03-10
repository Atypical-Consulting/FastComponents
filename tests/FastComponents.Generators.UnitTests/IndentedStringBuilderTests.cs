using FastComponents.Generators.Helpers;
using Shouldly;

namespace FastComponents.Generators.UnitTests;

/// <summary>
/// Tests for the IndentedStringBuilder helper class.
/// </summary>
public class IndentedStringBuilderTests
{
    [Fact]
    public void Append_AddsText()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.Append("hello");

        // Assert
        builder.ToString().ShouldBe("hello");
    }

    [Fact]
    public void Append_Char_AddsCharacter()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.Append('x');

        // Assert
        builder.ToString().ShouldBe("x");
    }

    [Fact]
    public void AppendLine_AddsTextWithNewline()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.AppendLine("hello");

        // Assert
        string result = builder.ToString();
        result.ShouldStartWith("hello");
        result.ShouldContain(Environment.NewLine);
    }

    [Fact]
    public void AppendLine_EmptyString_AddsBlankLine()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.AppendLine(string.Empty);

        // Assert
        builder.ToString().ShouldBe(Environment.NewLine);
    }

    [Fact]
    public void IncrementIndent_IncreasesIndentation()
    {
        // Arrange
        IndentedStringBuilder builder = new(0, 2);

        // Act
        builder.IncrementIndent();
        builder.AppendLine("indented");

        // Assert
        builder.ToString().ShouldStartWith("  indented");
    }

    [Fact]
    public void DecrementIndent_DecreasesIndentation()
    {
        // Arrange
        IndentedStringBuilder builder = new(0, 2);

        // Act
        builder.IncrementIndent();
        builder.IncrementIndent();
        builder.DecrementIndent();
        builder.AppendLine("one level");

        // Assert
        builder.ToString().ShouldStartWith("  one level");
    }

    [Fact]
    public void DecrementIndent_AtZero_StaysAtZero()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.DecrementIndent();
        builder.AppendLine("no indent");

        // Assert
        builder.ToString().ShouldStartWith("no indent");
    }

    [Fact]
    public void Indent_CreatesScoped_Indentation()
    {
        // Arrange
        IndentedStringBuilder builder = new(0, 4);

        // Act
        builder.AppendLine("outer");
        using (builder.Indent())
        {
            builder.AppendLine("inner");
        }

        builder.AppendLine("outer again");

        // Assert
        string result = builder.ToString();
        string[] lines = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        lines[0].ShouldBe("outer");
        lines[1].ShouldStartWith("    inner");
        lines[2].ShouldBe("outer again");
    }

    [Fact]
    public void SuspendIndent_TemporarilyDisablesIndentation()
    {
        // Arrange
        IndentedStringBuilder builder = new(0, 4);
        builder.IncrementIndent();

        // Act
        builder.AppendLine("indented");
        using (builder.SuspendIndent())
        {
            builder.AppendLine("not indented");
        }

        builder.AppendLine("indented again");

        // Assert
        string result = builder.ToString();
        string[] lines = result.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        lines[0].ShouldStartWith("    indented");
        lines[1].ShouldBe("not indented");
        lines[2].ShouldStartWith("    indented again");
    }

    [Fact]
    public void Clear_ResetsBuilder()
    {
        // Arrange
        IndentedStringBuilder builder = new();
        builder.AppendLine("some content");
        builder.IncrementIndent();

        // Act
        builder.Clear();
        builder.AppendLine("fresh start");

        // Assert
        builder.ToString().ShouldStartWith("fresh start");
    }

    [Fact]
    public void Length_ReturnsCurrentLength()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.Append("hello");

        // Assert
        builder.Length.ShouldBe(5);
    }

    [Fact]
    public void AppendLines_HandlesMultilineString()
    {
        // Arrange
        IndentedStringBuilder builder = new(0, 2);
        builder.IncrementIndent();

        // Act
        builder.AppendLines("line1\nline2\nline3");

        // Assert
        string result = builder.ToString();
        result.ShouldContain("  line1");
        result.ShouldContain("  line2");
        result.ShouldContain("  line3");
    }

    [Fact]
    public void AppendLines_WithSkipFinalNewline_OmitsTrailingNewline()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.AppendLines("single line", skipFinalNewline: true);

        // Assert
        builder.ToString().ShouldBe("single line");
    }

    [Fact]
    public void Append_Enumerable_AddsAllStrings()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.Append(new[] { "hello", " ", "world" });

        // Assert
        builder.ToString().ShouldBe("hello world");
    }

    [Fact]
    public void Append_CharEnumerable_AddsAllChars()
    {
        // Arrange
        IndentedStringBuilder builder = new();

        // Act
        builder.Append(new[] { 'a', 'b', 'c' });

        // Assert
        builder.ToString().ShouldBe("abc");
    }

    [Fact]
    public void IncrementIndent_WithCount_IncreasesMultipleLevels()
    {
        // Arrange
        IndentedStringBuilder builder = new(0, 2);

        // Act
        builder.IncrementIndent(3);
        builder.AppendLine("deep");

        // Assert
        builder.ToString().ShouldStartWith("      deep"); // 3 * 2 = 6 spaces
    }

    [Fact]
    public void DecrementIndent_WithCount_DecreasesMultipleLevels()
    {
        // Arrange
        IndentedStringBuilder builder = new(0, 2);
        builder.IncrementIndent(3);

        // Act
        builder.DecrementIndent(2);
        builder.AppendLine("one level");

        // Assert
        builder.ToString().ShouldStartWith("  one level"); // 1 * 2 = 2 spaces
    }

    [Fact]
    public void Constructor_WithInitialIndent_StartsIndented()
    {
        // Arrange & Act
        IndentedStringBuilder builder = new(2, 4);
        builder.AppendLine("pre-indented");

        // Assert
        builder.ToString().ShouldStartWith("        pre-indented"); // 2 * 4 = 8 spaces
    }
}
