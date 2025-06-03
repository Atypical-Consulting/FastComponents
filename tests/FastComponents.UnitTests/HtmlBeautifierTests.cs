/*
 * Copyright 2025 Atypical Consulting SRL
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Shouldly;

namespace FastComponents.UnitTests;

public class HtmlBeautifierTests
{
    [Fact]
    public void BeautifyHtml_WithValidHtml_ShouldReturnSameHtml()
    {
        // Arrange
        const string html = "<div class=\"test\">Content</div>";

        // Act
        var result = HtmlBeautifier.BeautifyHtml(html);

        // Assert
        result.ShouldBe(html);
    }

    [Fact]
    public void BeautifyHtml_WithEmptyString_ShouldReturnEmptyString()
    {
        // Arrange
        const string html = "";

        // Act
        var result = HtmlBeautifier.BeautifyHtml(html);

        // Assert
        result.ShouldBe("");
    }

    [Fact]
    public void BeautifyHtml_WithComplexHtml_ShouldReturnSameHtml()
    {
        // Arrange
        const string html = """
            <div class="container">
                <h1>Title</h1>
                <p>Paragraph with <strong>bold</strong> text.</p>
                <ul>
                    <li>Item 1</li>
                    <li>Item 2</li>
                </ul>
            </div>
            """;

        // Act
        var result = HtmlBeautifier.BeautifyHtml(html);

        // Assert
        result.ShouldBe(html);
    }

    [Theory]
    [InlineData("<div>Test</div>")]
    [InlineData("<p class=\"test\">Content</p>")]
    [InlineData("<input type=\"text\" value=\"test\" />")]
    [InlineData("<!-- Comment -->")]
    public void BeautifyHtml_WithVariousHtmlElements_ShouldReturnSameHtml(string html)
    {
        // Act
        var result = HtmlBeautifier.BeautifyHtml(html);

        // Assert
        result.ShouldBe(html);
    }

    [Fact]
    public void BeautifyHtml_WithNullInput_ShouldReturnNull()
    {
        // Act
        var result = HtmlBeautifier.BeautifyHtml(null!);

        // Assert
        result.ShouldBeNull();
    }
}