using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Shouldly;

namespace FastComponents.UnitTests;

/// <summary>
/// Tests for HtmxBuilderExtensions
/// </summary>
public class HtmxBuilderExtensionsTests
{
    [Fact]
    public void Button_ShouldCreateButtonRenderFragment()
    {
        // Act
        RenderFragment fragment = HtmxBuilderExtensions.Button("Click me", "/update", "my-button");

        // Assert
        fragment.ShouldNotBeNull();
        string html = RenderFragmentToString(fragment);
        html.ShouldContain("Click me");
        html.ShouldContain("hx-get=\"/update\"");
        html.ShouldContain("id=\"my-button\"");
        html.ShouldContain("hx-target=\"#my-button\"");
    }

    [Fact]
    public void SearchInput_ShouldCreateSearchInputRenderFragment()
    {
        // Act
        RenderFragment fragment = HtmxBuilderExtensions.SearchInput("/search", "#results");

        // Assert
        fragment.ShouldNotBeNull();
        string html = RenderFragmentToString(fragment);
        html.ShouldContain("type=\"search\"");
        html.ShouldContain("placeholder=\"Search...\"");
        html.ShouldContain("hx-get=\"/search\"");
        html.ShouldContain("hx-target=\"#results\"");
    }

    [Fact]
    public void SearchInput_WithDefaultPlaceholder_ShouldUseDefault()
    {
        // Act
        RenderFragment fragment = HtmxBuilderExtensions.SearchInput("/search", "#results");

        // Assert
        fragment.ShouldNotBeNull();
        string html = RenderFragmentToString(fragment);
        html.ShouldContain("placeholder=\"Search...\"");
    }

    [Fact]
    public void LoadContainer_ShouldCreateLoadingDiv()
    {
        // Act
        RenderFragment fragment = HtmxBuilderExtensions.LoadContainer("/load-content", "Loading data...");

        // Assert
        fragment.ShouldNotBeNull();
        string html = RenderFragmentToString(fragment);
        html.ShouldContain("Loading data...");
        html.ShouldContain("hx-get=\"/load-content\"");
        html.ShouldContain("hx-trigger=\"load once\"");
    }

    [Fact]
    public void LoadContainer_WithDefaultText_ShouldUseDefault()
    {
        // Act
        RenderFragment fragment = HtmxBuilderExtensions.LoadContainer("/load-content");

        // Assert
        fragment.ShouldNotBeNull();
        string html = RenderFragmentToString(fragment);
        html.ShouldContain("Loading...");
    }

    private static string RenderFragmentToString(RenderFragment fragment)
    {
        RenderTreeBuilder builder = new();
        fragment(builder);
        ArrayRange<RenderTreeFrame> frames = builder.GetFrames();

        // Simple HTML rendering for test purposes
        if (frames.Array.Length > 0)
        {
            RenderTreeFrame element = frames.Array[0];
            string html = $"<{element.ElementName}";

            // Add attributes
            for (int i = 1; i < frames.Array.Length; i++)
            {
                RenderTreeFrame frame = frames.Array[i];
                if (frame.FrameType == RenderTreeFrameType.Attribute)
                {
                    html += $" {frame.AttributeName}=\"{frame.AttributeValue}\"";
                }
                else if (frame.FrameType == RenderTreeFrameType.Text)
                {
                    html += $">{frame.TextContent}</{element.ElementName}";
                    break;
                }
            }

            if (!html.Contains(">"))
            {
                html += $"></{element.ElementName}>";
            }

            return html;
        }

        return string.Empty;
    }
}
