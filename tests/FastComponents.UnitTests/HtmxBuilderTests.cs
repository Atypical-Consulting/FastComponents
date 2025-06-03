using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Shouldly;

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
        HtmxBuilder builder = HtmxBuilder.Create();

        // Assert
        builder.ShouldNotBeNull();
    }

    [Fact]
    public void Button_ShouldCreateButtonElement()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Button();

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("<button");
    }

    [Fact]
    public void Form_ShouldCreateFormElement()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Form();

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("<form");
    }

    [Fact]
    public void Div_ShouldCreateDivElement()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Div();

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("<div");
    }

    [Fact]
    public void Get_ShouldAddHxGetAttribute()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Get("/test-url");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-get=\"/test-url\"");
    }

    [Fact]
    public void Post_ShouldAddHxPostAttribute()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Post("/test-url");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-post=\"/test-url\"");
    }

    [Fact]
    public void Target_ShouldAddHxTargetAttribute()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Target("#my-target");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-target=\"#my-target\"");
    }

    [Fact]
    public void Swap_ShouldAddHxSwapAttribute()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Swap("innerHTML");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-swap=\"innerHTML\"");
    }

    [Fact]
    public void Trigger_ShouldAddHxTriggerAttribute()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Trigger("click");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-trigger=\"click\"");
    }

    [Fact]
    public void Class_ShouldAddClassAttribute()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Class("btn btn-primary");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("class=\"btn btn-primary\"");
    }

    [Fact]
    public void Class_MultipleClasses_ShouldConcatenate()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create()
            .Class("btn")
            .Class("btn-primary");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("class=\"btn btn-primary\"");
    }

    [Fact]
    public void Attr_ShouldAddCustomAttribute()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Attr("data-test", "value");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("data-test=\"value\"");
    }

    [Fact]
    public void Text_ShouldAddTextContent()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Text("Click me");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain(">Click me<");
    }

    [Fact]
    public void GetSelf_ShouldConfigureForSelfUpdate()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().GetSelf("/update", "my-id");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-get=\"/update\"");
        html.ShouldContain("hx-target=\"#my-id\"");
        html.ShouldContain("hx-swap=\"outerHTML\"");
    }

    [Fact]
    public void PostTo_ShouldConfigureForPostToTarget()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().PostTo("/submit", "#result");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-post=\"/submit\"");
        html.ShouldContain("hx-target=\"#result\"");
        html.ShouldContain("hx-swap=\"innerHTML\"");
    }

    [Fact]
    public void LoadOnce_ShouldConfigureForPageLoad()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().LoadOnce("/load-content");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-get=\"/load-content\"");
        html.ShouldContain("hx-trigger=\"load once\"");
    }

    [Fact]
    public void Search_ShouldConfigureForSearch()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Search("/search", "#results");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-get=\"/search\"");
        html.ShouldContain("hx-target=\"#results\"");
        html.ShouldContain("hx-trigger=\"keyup changed delay:300ms, search\"");
        html.ShouldContain("hx-indicator=\"#loading\"");
    }

    [Fact]
    public void Search_WithCustomDelay_ShouldUseCustomDelay()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Create().Search("/search", "#results", 500);

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("hx-trigger=\"keyup changed delay:500ms, search\"");
    }

    [Fact]
    public void ChainedCalls_ShouldWork()
    {
        // Act
        HtmxBuilder builder = HtmxBuilder.Button()
            .Text("Submit")
            .Post("/submit")
            .Target("#result")
            .Class("btn btn-primary");

        // Assert
        string html = RenderToString(builder);
        html.ShouldContain("<button");
        html.ShouldContain(">Submit<");
        html.ShouldContain("hx-post=\"/submit\"");
        html.ShouldContain("hx-target=\"#result\"");
        html.ShouldContain("class=\"btn btn-primary\"");
    }

    private static string RenderToString(HtmxBuilder builder)
    {
        RenderTreeBuilder renderTreeBuilder = new();
        builder.Render(renderTreeBuilder);
        ArrayRange<RenderTreeFrame> frames = renderTreeBuilder.GetFrames();

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

            if (!html.Contains('>'))
            {
                html += $"></{element.ElementName}>";
            }

            return html;
        }

        return string.Empty;
    }
}
