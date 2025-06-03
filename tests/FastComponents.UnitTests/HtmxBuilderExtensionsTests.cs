using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Shouldly;
using Xunit;

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
        var fragment = HtmxBuilderExtensions.Button("Click me", "/update", "my-button");

        // Assert
        fragment.ShouldNotBeNull();
        var html = RenderFragmentToString(fragment);
        html.ShouldContain("Click me");
        html.ShouldContain("hx-get=\"/update\"");
        html.ShouldContain("id=\"my-button\"");
        html.ShouldContain("hx-target=\"#my-button\"");
    }

    [Fact]
    public void SearchInput_ShouldCreateSearchInputRenderFragment()
    {
        // Act
        var fragment = HtmxBuilderExtensions.SearchInput("/search", "#results", "Search...");

        // Assert
        fragment.ShouldNotBeNull();
        var html = RenderFragmentToString(fragment);
        html.ShouldContain("type=\"search\"");
        html.ShouldContain("placeholder=\"Search...\"");
        html.ShouldContain("hx-get=\"/search\"");
        html.ShouldContain("hx-target=\"#results\"");
    }

    [Fact]
    public void SearchInput_WithDefaultPlaceholder_ShouldUseDefault()
    {
        // Act
        var fragment = HtmxBuilderExtensions.SearchInput("/search", "#results");

        // Assert
        fragment.ShouldNotBeNull();
        var html = RenderFragmentToString(fragment);
        html.ShouldContain("placeholder=\"Search...\"");
    }

    [Fact]
    public void LoadContainer_ShouldCreateLoadingDiv()
    {
        // Act
        var fragment = HtmxBuilderExtensions.LoadContainer("/load-content", "Loading data...");

        // Assert
        fragment.ShouldNotBeNull();
        var html = RenderFragmentToString(fragment);
        html.ShouldContain("Loading data...");
        html.ShouldContain("hx-get=\"/load-content\"");
        html.ShouldContain("hx-trigger=\"load once\"");
    }

    [Fact]
    public void LoadContainer_WithDefaultText_ShouldUseDefault()
    {
        // Act
        var fragment = HtmxBuilderExtensions.LoadContainer("/load-content");

        // Assert
        fragment.ShouldNotBeNull();
        var html = RenderFragmentToString(fragment);
        html.ShouldContain("Loading...");
    }

    private static string RenderFragmentToString(RenderFragment fragment)
    {
        var builder = new RenderTreeBuilder();
        fragment(builder);
        var frames = builder.GetFrames();
        
        // Simple HTML rendering for test purposes
        if (frames.Array.Length > 0)
        {
            var element = frames.Array[0];
            var html = $"<{element.ElementName}";
            
            // Add attributes
            for (int i = 1; i < frames.Array.Length; i++)
            {
                var frame = frames.Array[i];
                if (frame.FrameType == Microsoft.AspNetCore.Components.RenderTree.RenderTreeFrameType.Attribute)
                {
                    html += $" {frame.AttributeName}=\"{frame.AttributeValue}\"";
                }
                else if (frame.FrameType == Microsoft.AspNetCore.Components.RenderTree.RenderTreeFrameType.Text)
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
        
        return "";
    }
}