using Microsoft.AspNetCore.Components.Rendering;
using Shouldly;
using Xunit;

namespace FastComponents.UnitTests;

/// <summary>
/// Tests for HtmxBuilder
/// </summary>
public class HtmxBuilderTests
{
    [Fact]
    public void Create_ShouldReturnNewBuilder()
    {
        // Act
        var builder = HtmxBuilder.Create();

        // Assert
        builder.ShouldNotBeNull();
    }

    [Fact]
    public void Button_ShouldCreateButtonElement()
    {
        // Act
        var builder = HtmxBuilder.Button();

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("<button");
    }

    [Fact]
    public void Form_ShouldCreateFormElement()
    {
        // Act
        var builder = HtmxBuilder.Form();

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("<form");
    }

    [Fact]
    public void Div_ShouldCreateDivElement()
    {
        // Act
        var builder = HtmxBuilder.Div();

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("<div");
    }

    [Fact]
    public void Get_ShouldAddHxGetAttribute()
    {
        // Act
        var builder = HtmxBuilder.Create().Get("/test-url");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-get=\"/test-url\"");
    }

    [Fact]
    public void Post_ShouldAddHxPostAttribute()
    {
        // Act
        var builder = HtmxBuilder.Create().Post("/test-url");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-post=\"/test-url\"");
    }

    [Fact]
    public void Target_ShouldAddHxTargetAttribute()
    {
        // Act
        var builder = HtmxBuilder.Create().Target("#my-target");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-target=\"#my-target\"");
    }

    [Fact]
    public void Swap_ShouldAddHxSwapAttribute()
    {
        // Act
        var builder = HtmxBuilder.Create().Swap("innerHTML");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-swap=\"innerHTML\"");
    }

    [Fact]
    public void Trigger_ShouldAddHxTriggerAttribute()
    {
        // Act
        var builder = HtmxBuilder.Create().Trigger("click");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-trigger=\"click\"");
    }

    [Fact]
    public void Class_ShouldAddClassAttribute()
    {
        // Act
        var builder = HtmxBuilder.Create().Class("btn btn-primary");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("class=\"btn btn-primary\"");
    }

    [Fact]
    public void Class_MultipleClasses_ShouldConcatenate()
    {
        // Act
        var builder = HtmxBuilder.Create()
            .Class("btn")
            .Class("btn-primary");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("class=\"btn btn-primary\"");
    }

    [Fact]
    public void Attr_ShouldAddCustomAttribute()
    {
        // Act
        var builder = HtmxBuilder.Create().Attr("data-test", "value");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("data-test=\"value\"");
    }

    [Fact]
    public void Text_ShouldAddTextContent()
    {
        // Act
        var builder = HtmxBuilder.Create().Text("Click me");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain(">Click me<");
    }

    [Fact]
    public void GetSelf_ShouldConfigureForSelfUpdate()
    {
        // Act
        var builder = HtmxBuilder.Create().GetSelf("/update", "my-id");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-get=\"/update\"");
        html.ShouldContain("hx-target=\"#my-id\"");
        html.ShouldContain("hx-swap=\"outerHTML\"");
    }

    [Fact]
    public void PostTo_ShouldConfigureForPostToTarget()
    {
        // Act
        var builder = HtmxBuilder.Create().PostTo("/submit", "#result");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-post=\"/submit\"");
        html.ShouldContain("hx-target=\"#result\"");
        html.ShouldContain("hx-swap=\"innerHTML\"");
    }

    [Fact]
    public void LoadOnce_ShouldConfigureForPageLoad()
    {
        // Act
        var builder = HtmxBuilder.Create().LoadOnce("/load-content");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-get=\"/load-content\"");
        html.ShouldContain("hx-trigger=\"load once\"");
    }

    [Fact]
    public void Search_ShouldConfigureForSearch()
    {
        // Act
        var builder = HtmxBuilder.Create().Search("/search", "#results");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-get=\"/search\"");
        html.ShouldContain("hx-target=\"#results\"");
        html.ShouldContain("hx-trigger=\"keyup changed delay:300ms, search\"");
        html.ShouldContain("hx-indicator=\"#loading\"");
    }

    [Fact]
    public void Search_WithCustomDelay_ShouldUseCustomDelay()
    {
        // Act
        var builder = HtmxBuilder.Create().Search("/search", "#results", 500);

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("hx-trigger=\"keyup changed delay:500ms, search\"");
    }

    [Fact]
    public void ChainedCalls_ShouldWork()
    {
        // Act
        var builder = HtmxBuilder.Button()
            .Text("Submit")
            .Post("/submit")
            .Target("#result")
            .Class("btn btn-primary");

        // Assert
        var html = RenderToString(builder);
        html.ShouldContain("<button");
        html.ShouldContain(">Submit<");
        html.ShouldContain("hx-post=\"/submit\"");
        html.ShouldContain("hx-target=\"#result\"");
        html.ShouldContain("class=\"btn btn-primary\"");
    }

    private static string RenderToString(HtmxBuilder builder)
    {
        var renderTreeBuilder = new RenderTreeBuilder();
        builder.Render(renderTreeBuilder);
        var frames = renderTreeBuilder.GetFrames();
        
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