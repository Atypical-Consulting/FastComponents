using Bunit;
using Microsoft.AspNetCore.Components;
using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxWsTagTests : Bunit.TestContext
{
    [Fact]
    public void HtmxWsTag_RendersDefaultElement()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>();
        
        // Assert
        cut.Find("div").ShouldNotBeNull();
    }
    
    [Fact]
    public void HtmxWsTag_RendersCustomElement()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .Add(p => p.Element, "article"));
        
        // Assert
        cut.Find("article").ShouldNotBeNull();
    }
    
    [Fact]
    public void HtmxWsTag_RendersWsConnectAttribute()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .Add(p => p.WsConnect, "ws://localhost:8080/websocket"));
        
        // Assert
        var element = cut.Find("div");
        element.GetAttribute("ws-connect").ShouldBe("ws://localhost:8080/websocket");
    }
    
    [Fact]
    public void HtmxWsTag_RendersWsSendAttribute()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .Add(p => p.WsSend, "submit"));
        
        // Assert
        var element = cut.Find("div");
        element.GetAttribute("ws-send").ShouldBe("submit");
    }
    
    [Fact]
    public void HtmxWsTag_RendersHtmxCoreAttributes()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .Add(p => p.HxPost, "/api/message")
            .Add(p => p.HxTrigger, "submit")
            .Add(p => p.HxTarget, "#messages")
            .Add(p => p.HxSwap, "beforeend")
            .Add(p => p.HxVals, "{\"user\":\"test\"}"));
        
        // Assert
        var element = cut.Find("div");
        element.GetAttribute("hx-post").ShouldBe("/api/message");
        element.GetAttribute("hx-trigger").ShouldBe("submit");
        element.GetAttribute("hx-target").ShouldBe("#messages");
        element.GetAttribute("hx-swap").ShouldBe("beforeend");
        element.GetAttribute("hx-vals").ShouldBe("{\"user\":\"test\"}");
    }
    
    [Fact]
    public void HtmxWsTag_RendersChildContent()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .Add(p => p.ChildContent, (RenderFragment)(builder => 
            {
                builder.AddMarkupContent(0, "<form><input type='text'/></form>");
            })));
        
        // Assert
        cut.Find("form").ShouldNotBeNull();
        cut.Find("input").ShouldNotBeNull();
    }
    
    [Fact]
    public void HtmxWsTag_RendersCustomAttributes()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .AddUnmatched("data-ws-room", "chat-123")
            .AddUnmatched("role", "region"));
        
        // Assert
        var element = cut.Find("div");
        element.GetAttribute("data-ws-room").ShouldBe("chat-123");
        element.GetAttribute("role").ShouldBe("region");
    }
    
    [Fact]
    public void HtmxWsTag_DoesNotRenderEmptyAttributes()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .Add(p => p.WsConnect, "")
            .Add(p => p.WsSend, null)
            .Add(p => p.HxPost, "   "));
        
        // Assert
        var element = cut.Find("div");
        element.HasAttribute("ws-connect").ShouldBeFalse();
        element.HasAttribute("ws-send").ShouldBeFalse();
        element.HasAttribute("hx-post").ShouldBeFalse();
    }
    
    [Fact]
    public void HtmxWsTag_RendersAdditionalHtmxAttributes()
    {
        // Act
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .Add(p => p.HxConfirm, "Are you sure?")
            .Add(p => p.HxIndicator, "#spinner")
            .Add(p => p.HxExt, "ws"));
        
        // Assert
        var element = cut.Find("div");
        element.GetAttribute("hx-confirm").ShouldBe("Are you sure?");
        element.GetAttribute("hx-indicator").ShouldBe("#spinner");
        element.GetAttribute("hx-ext").ShouldBe("ws");
    }
    
    [Fact]
    public void HtmxWsTag_CombinesWithSseExtension()
    {
        // Act - WebSocket tag can have hx-ext that includes multiple extensions
        var cut = RenderComponent<HtmxWsTag>(parameters => parameters
            .Add(p => p.WsConnect, "/ws")
            .Add(p => p.HxExt, "ws,sse"));
        
        // Assert
        var element = cut.Find("div");
        element.GetAttribute("ws-connect").ShouldBe("/ws");
        element.GetAttribute("hx-ext").ShouldBe("ws,sse");
    }
}