using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxResponseHeadersTests
{
    [Fact]
    public void Constructor_WithHttpContext_InitializesHeaders()
    {
        // Arrange
        var context = new DefaultHttpContext();
        
        // Act
        var headers = new Http.HtmxResponseHeaders(context);
        headers.Refresh();
        
        // Assert
        context.Response.Headers["HX-Refresh"].ToString().ShouldBe("true");
    }
    
    [Fact]
    public void Constructor_WithHeaderDictionary_InitializesHeaders()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        
        // Act
        var headers = new Http.HtmxResponseHeaders(headerDict);
        headers.Refresh();
        
        // Assert
        headerDict["HX-Refresh"].ToString().ShouldBe("true");
    }
    
    [Fact]
    public void Location_WithString_SetsHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.Location("/new-page");
        
        // Assert
        headerDict["HX-Location"].ToString().ShouldBe("/new-page");
    }
    
    [Fact]
    public void Location_WithObject_SetsJsonHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        var locationData = new { path = "/new-page", target = "#content" };
        
        // Act
        headers.Location(locationData);
        
        // Assert
        var json = headerDict["HX-Location"].ToString();
        json.ShouldContain("\"path\":\"/new-page\"");
        json.ShouldContain("\"target\":\"#content\"");
    }
    
    [Fact]
    public void PushUrl_SetsHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.PushUrl("/new-url");
        
        // Assert
        headerDict["HX-Push-Url"].ToString().ShouldBe("/new-url");
    }
    
    [Fact]
    public void PreventPushUrl_SetsFalse()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.PreventPushUrl();
        
        // Assert
        headerDict["HX-Push-Url"].ToString().ShouldBe("false");
    }
    
    [Fact]
    public void Redirect_SetsHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.Redirect("/redirect-url");
        
        // Assert
        headerDict["HX-Redirect"].ToString().ShouldBe("/redirect-url");
    }
    
    [Fact]
    public void Refresh_SetsTrue()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.Refresh();
        
        // Assert
        headerDict["HX-Refresh"].ToString().ShouldBe("true");
    }
    
    [Fact]
    public void ReplaceUrl_SetsHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.ReplaceUrl("/replaced-url");
        
        // Assert
        headerDict["HX-Replace-Url"].ToString().ShouldBe("/replaced-url");
    }
    
    [Fact]
    public void PreventReplaceUrl_SetsFalse()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.PreventReplaceUrl();
        
        // Assert
        headerDict["HX-Replace-Url"].ToString().ShouldBe("false");
    }
    
    [Fact]
    public void Reswap_SetsHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.Reswap("outerHTML swap:1s");
        
        // Assert
        headerDict["HX-Reswap"].ToString().ShouldBe("outerHTML swap:1s");
    }
    
    [Fact]
    public void Retarget_SetsHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.Retarget("#new-target");
        
        // Assert
        headerDict["HX-Retarget"].ToString().ShouldBe("#new-target");
    }
    
    [Fact]
    public void Reselect_SetsHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.Reselect(".content");
        
        // Assert
        headerDict["HX-Reselect"].ToString().ShouldBe(".content");
    }
    
    [Fact]
    public void Trigger_SingleEvent_SetsHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.Trigger("myEvent");
        
        // Assert
        headerDict["HX-Trigger"].ToString().ShouldBe("myEvent");
    }
    
    [Fact]
    public void Trigger_MultipleEvents_SetsCommaSeparated()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.Trigger("event1", "event2", "event3");
        
        // Assert
        headerDict["HX-Trigger"].ToString().ShouldBe("event1,event2,event3");
    }
    
    [Fact]
    public void TriggerWithDetails_SetsJsonHeader()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        var eventDetails = new { myEvent = new { level = "info", message = "Test" } };
        
        // Act
        headers.TriggerWithDetails(eventDetails);
        
        // Assert
        var json = headerDict["HX-Trigger"].ToString();
        json.ShouldContain("\"myEvent\"");
        json.ShouldContain("\"level\":\"info\"");
    }
    
    [Fact]
    public void TriggerAfterSettle_Works()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.TriggerAfterSettle("settleEvent");
        
        // Assert
        headerDict["HX-Trigger-After-Settle"].ToString().ShouldBe("settleEvent");
    }
    
    [Fact]
    public void TriggerAfterSwap_Works()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers.TriggerAfterSwap("swapEvent");
        
        // Assert
        headerDict["HX-Trigger-After-Swap"].ToString().ShouldBe("swapEvent");
    }
    
    [Fact]
    public void MethodChaining_Works()
    {
        // Arrange
        var headerDict = new HeaderDictionary();
        var headers = new Http.HtmxResponseHeaders(headerDict);
        
        // Act
        headers
            .Location("/new-location")
            .PushUrl("/new-url")
            .Trigger("event1", "event2")
            .Retarget("#content");
        
        // Assert
        headerDict["HX-Location"].ToString().ShouldBe("/new-location");
        headerDict["HX-Push-Url"].ToString().ShouldBe("/new-url");
        headerDict["HX-Trigger"].ToString().ShouldBe("event1,event2");
        headerDict["HX-Retarget"].ToString().ShouldBe("#content");
    }
    
    [Fact]
    public void HeaderNames_Constants_HaveCorrectValues()
    {
        // Assert
        Http.HtmxResponseHeaders.Names.HxLocation.ShouldBe("HX-Location");
        Http.HtmxResponseHeaders.Names.HxPushUrl.ShouldBe("HX-Push-Url");
        Http.HtmxResponseHeaders.Names.HxRedirect.ShouldBe("HX-Redirect");
        Http.HtmxResponseHeaders.Names.HxRefresh.ShouldBe("HX-Refresh");
        Http.HtmxResponseHeaders.Names.HxReplaceUrl.ShouldBe("HX-Replace-Url");
        Http.HtmxResponseHeaders.Names.HxReswap.ShouldBe("HX-Reswap");
        Http.HtmxResponseHeaders.Names.HxRetarget.ShouldBe("HX-Retarget");
        Http.HtmxResponseHeaders.Names.HxReselect.ShouldBe("HX-Reselect");
        Http.HtmxResponseHeaders.Names.HxTrigger.ShouldBe("HX-Trigger");
        Http.HtmxResponseHeaders.Names.HxTriggerAfterSettle.ShouldBe("HX-Trigger-After-Settle");
        Http.HtmxResponseHeaders.Names.HxTriggerAfterSwap.ShouldBe("HX-Trigger-After-Swap");
    }
}