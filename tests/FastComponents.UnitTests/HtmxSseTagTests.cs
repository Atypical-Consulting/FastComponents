using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxSseTagTests : Bunit.TestContext
{
    [Fact]
    public void HtmxSseTag_RendersDefaultElement()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>();

        // Assert
        cut.Find("div").ShouldNotBeNull();
    }

    [Fact]
    public void HtmxSseTag_RendersCustomElement()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>(parameters => parameters
            .Add(p => p.Element, "section"));

        // Assert
        cut.Find("section").ShouldNotBeNull();
    }

    [Fact]
    public void HtmxSseTag_RendersSseConnectAttribute()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>(parameters => parameters
            .Add(p => p.SseConnect, "/sse-endpoint"));

        // Assert
        IElement element = cut.Find("div");
        element.GetAttribute("sse-connect").ShouldBe("/sse-endpoint");
    }

    [Fact]
    public void HtmxSseTag_RendersSseSwapAttribute()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>(parameters => parameters
            .Add(p => p.SseSwap, "message"));

        // Assert
        IElement element = cut.Find("div");
        element.GetAttribute("sse-swap").ShouldBe("message");
    }

    [Fact]
    public void HtmxSseTag_RendersHtmxCoreAttributes()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>(parameters => parameters
            .Add(p => p.HxGet, "/api/data")
            .Add(p => p.HxTrigger, "click")
            .Add(p => p.HxTarget, "#result")
            .Add(p => p.HxSwap, "innerHTML")
            .Add(p => p.HxVals, "{\"myVar\":\"computeValue()\"}"));

        // Assert
        IElement element = cut.Find("div");
        element.GetAttribute("hx-get").ShouldBe("/api/data");
        element.GetAttribute("hx-trigger").ShouldBe("click");
        element.GetAttribute("hx-target").ShouldBe("#result");
        element.GetAttribute("hx-swap").ShouldBe("innerHTML");
        element.GetAttribute("hx-vals").ShouldBe("{\"myVar\":\"computeValue()\"}");
    }

    [Fact]
    public void HtmxSseTag_RendersChildContent()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => builder.AddMarkupContent(0, "<span>SSE Content</span>"))));

        // Assert
        cut.Find("span").TextContent.ShouldBe("SSE Content");
    }

    [Fact]
    public void HtmxSseTag_RendersCustomAttributes()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>(parameters => parameters
            .AddUnmatched("data-test", "value")
            .AddUnmatched("id", "sse-component"));

        // Assert
        IElement element = cut.Find("div");
        element.GetAttribute("data-test").ShouldBe("value");
        element.GetAttribute("id").ShouldBe("sse-component");
    }

    [Fact]
    public void HtmxSseTag_DoesNotRenderEmptyAttributes()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>(parameters => parameters
            .Add(p => p.SseConnect, string.Empty)
            .Add(p => p.SseSwap, null)
            .Add(p => p.HxGet, "   "));

        // Assert
        IElement element = cut.Find("div");
        element.HasAttribute("sse-connect").ShouldBeFalse();
        element.HasAttribute("sse-swap").ShouldBeFalse();
        element.HasAttribute("hx-get").ShouldBeFalse();
    }

    [Fact]
    public void HtmxSseTag_RendersClassAttribute()
    {
        // Act
        IRenderedComponent<HtmxSseTag> cut = RenderComponent<HtmxSseTag>(parameters => parameters
            .AddUnmatched("class", "sse-container"));

        // Assert
        IElement element = cut.Find("div");
        element.GetAttribute("class").ShouldBe("sse-container");
    }
}
