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
        DefaultHttpContext context = new();
        context.Request.Headers["HX-Request"] = "true";

        // Act
        HtmxRequestHeaders headers = context.GetHtmxRequestHeaders();

        // Assert
        headers.ShouldNotBeNull();
        headers.IsHtmxRequest.ShouldBeTrue();
    }

    [Fact]
    public void GetHtmxRequestHeaders_CachesInstance()
    {
        // Arrange
        DefaultHttpContext context = new();

        // Act
        HtmxRequestHeaders headers1 = context.GetHtmxRequestHeaders();
        HtmxRequestHeaders headers2 = context.GetHtmxRequestHeaders();

        // Assert
        headers1.ShouldBeSameAs(headers2);
    }

    [Fact]
    public void GetHtmxResponseHeaders_ReturnsInstance()
    {
        // Arrange
        DefaultHttpContext context = new();

        // Act
        HtmxResponseHeaders headers = context.GetHtmxResponseHeaders();

        // Assert
        headers.ShouldNotBeNull();
    }

    [Fact]
    public void GetHtmxResponseHeaders_CachesInstance()
    {
        // Arrange
        DefaultHttpContext context = new();

        // Act
        HtmxResponseHeaders headers1 = context.GetHtmxResponseHeaders();
        HtmxResponseHeaders headers2 = context.GetHtmxResponseHeaders();

        // Assert
        headers1.ShouldBeSameAs(headers2);
    }

    [Fact]
    public void IsHtmxRequest_WhenTrue_ReturnsTrue()
    {
        // Arrange
        DefaultHttpContext context = new();
        context.Request.Headers["HX-Request"] = "true";

        // Act
        bool result = context.IsHtmxRequest();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsHtmxRequest_WhenFalse_ReturnsFalse()
    {
        // Arrange
        DefaultHttpContext context = new();

        // Act
        bool result = context.IsHtmxRequest();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void IsHtmxBoostedRequest_WhenTrue_ReturnsTrue()
    {
        // Arrange
        DefaultHttpContext context = new();
        context.Request.Headers["HX-Boosted"] = "true";

        // Act
        bool result = context.IsHtmxBoostedRequest();

        // Assert
        result.ShouldBeTrue();
    }

    [Fact]
    public void IsHtmxBoostedRequest_WhenFalse_ReturnsFalse()
    {
        // Arrange
        DefaultHttpContext context = new();

        // Act
        bool result = context.IsHtmxBoostedRequest();

        // Assert
        result.ShouldBeFalse();
    }

    [Fact]
    public void ResponseHeaders_ModifyActualHeaders()
    {
        // Arrange
        DefaultHttpContext context = new();

        // Act
        HtmxResponseHeaders headers = context.GetHtmxResponseHeaders();
        headers.Trigger("test-event");

        // Assert
        context.Response.Headers["HX-Trigger"].ToString().ShouldBe("test-event");
    }
}
