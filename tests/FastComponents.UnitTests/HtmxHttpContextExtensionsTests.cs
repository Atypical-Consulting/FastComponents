using FastComponents.Http;
using Microsoft.AspNetCore.Http;
using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxHttpContextExtensionsTests
{
    [Fact]
    public void GetHtmxRequestHeaders_ReturnsInstance()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["HX-Request"] = "true";
        
        // Act
        var headers = context.GetHtmxRequestHeaders();
        
        // Assert
        headers.ShouldNotBeNull();
        headers.IsHtmxRequest.ShouldBeTrue();
    }
    
    [Fact]
    public void GetHtmxRequestHeaders_CachesInstance()
    {
        // Arrange
        var context = new DefaultHttpContext();
        
        // Act
        var headers1 = context.GetHtmxRequestHeaders();
        var headers2 = context.GetHtmxRequestHeaders();
        
        // Assert
        headers1.ShouldBeSameAs(headers2);
    }
    
    [Fact]
    public void GetHtmxResponseHeaders_ReturnsInstance()
    {
        // Arrange
        var context = new DefaultHttpContext();
        
        // Act
        var headers = context.GetHtmxResponseHeaders();
        
        // Assert
        headers.ShouldNotBeNull();
    }
    
    [Fact]
    public void GetHtmxResponseHeaders_CachesInstance()
    {
        // Arrange
        var context = new DefaultHttpContext();
        
        // Act
        var headers1 = context.GetHtmxResponseHeaders();
        var headers2 = context.GetHtmxResponseHeaders();
        
        // Assert
        headers1.ShouldBeSameAs(headers2);
    }
    
    [Fact]
    public void IsHtmxRequest_WhenTrue_ReturnsTrue()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["HX-Request"] = "true";
        
        // Act
        var result = context.IsHtmxRequest();
        
        // Assert
        result.ShouldBeTrue();
    }
    
    [Fact]
    public void IsHtmxRequest_WhenFalse_ReturnsFalse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        
        // Act
        var result = context.IsHtmxRequest();
        
        // Assert
        result.ShouldBeFalse();
    }
    
    [Fact]
    public void IsHtmxBoostedRequest_WhenTrue_ReturnsTrue()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["HX-Boosted"] = "true";
        
        // Act
        var result = context.IsHtmxBoostedRequest();
        
        // Assert
        result.ShouldBeTrue();
    }
    
    [Fact]
    public void IsHtmxBoostedRequest_WhenFalse_ReturnsFalse()
    {
        // Arrange
        var context = new DefaultHttpContext();
        
        // Act
        var result = context.IsHtmxBoostedRequest();
        
        // Assert
        result.ShouldBeFalse();
    }
    
    [Fact]
    public void ResponseHeaders_ModifyActualHeaders()
    {
        // Arrange
        var context = new DefaultHttpContext();
        
        // Act
        var headers = context.GetHtmxResponseHeaders();
        headers.Trigger("test-event");
        
        // Assert
        context.Response.Headers["HX-Trigger"].ToString().ShouldBe("test-event");
    }
}