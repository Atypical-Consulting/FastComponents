using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Shouldly;

namespace FastComponents.UnitTests;

public class ComponentHtmlResponseServiceTests
{
    public class TestComponent : HtmxComponentBase
    {
        [Parameter] public string? Title { get; set; }
        [Parameter] public int Count { get; set; }
        
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddAttribute(1, "class", "test-component");
            
            if (!string.IsNullOrEmpty(Title))
            {
                builder.OpenElement(2, "h1");
                builder.AddContent(3, Title);
                builder.CloseElement();
            }
            
            builder.OpenElement(4, "p");
            builder.AddContent(5, $"Count: {Count}");
            builder.CloseElement();
            
            builder.CloseElement();
        }
    }
    
    private readonly ComponentHtmlResponseService _service;
    
    public ComponentHtmlResponseServiceTests()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        var serviceProvider = services.BuildServiceProvider();
        var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);
        _service = new ComponentHtmlResponseService(htmlRenderer);
    }
    
    [Fact]
    public async Task RenderAsHtmlContent_WithoutParameters_RendersComponent()
    {
        // Act
        var result = await _service.RenderAsHtmlContent<TestComponent>();
        
        // Assert
        result.ShouldNotBeNull();
        var content = result as ContentHttpResult;
        content.ShouldNotBeNull();
        content.ContentType.ShouldBe("text/html; charset=utf-8");
        
        var responseText = content.ResponseContent;
        responseText.ShouldContain("<div class=\"test-component\">");
        responseText.ShouldContain("<p>Count: 0</p>");
        responseText.ShouldNotContain("<h1>");
    }
    
    [Fact]
    public async Task RenderAsHtmlContent_WithParameters_RendersComponentWithValues()
    {
        // Arrange
        var parameters = new Dictionary<string, object?>
        {
            { "Title", "Test Title" },
            { "Count", 42 }
        };
        
        // Act
        var result = await _service.RenderAsHtmlContent<TestComponent>(parameters);
        
        // Assert
        var content = result as ContentHttpResult;
        content.ShouldNotBeNull();
        
        var responseText = content.ResponseContent;
        responseText.ShouldContain("<h1>Test Title</h1>");
        responseText.ShouldContain("<p>Count: 42</p>");
    }
    
    [Fact]
    public async Task RenderAsHtmlContent_ReturnsBeautifiedHtml()
    {
        // Arrange
        var parameters = new Dictionary<string, object?>
        {
            { "Title", "Test" }
        };
        
        // Act
        var result = await _service.RenderAsHtmlContent<TestComponent>(parameters);
        
        // Assert
        var content = result as ContentHttpResult;
        var responseText = content.ResponseContent;
        
        // Check that HTML is beautified (note: HtmlBeautifier returns minified HTML)
        responseText.ShouldContain("<div class=\"test-component\">");
        responseText.ShouldContain("<h1>Test</h1>");
    }
    
    [Fact]
    public async Task RenderComponent_WithoutParameters_RendersAsString()
    {
        // Act
        var html = await _service.RenderComponent<TestComponent>();
        
        // Assert
        html.ShouldNotBeNull();
        html.ShouldContain("<div class=\"test-component\">");
        html.ShouldContain("<p>Count: 0</p>");
    }
    
    [Fact]
    public async Task RenderComponent_WithParameters_RendersAsString()
    {
        // Arrange
        var parameters = new Dictionary<string, object?>
        {
            { "Title", "Component Test" },
            { "Count", 99 }
        };
        
        // Act
        var html = await _service.RenderComponent<TestComponent>(parameters);
        
        // Assert
        html.ShouldContain("<h1>Component Test</h1>");
        html.ShouldContain("<p>Count: 99</p>");
    }
    
    [Fact]
    public async Task RenderComponent_WithNullParameters_RendersDefaultValues()
    {
        // Act
        var html = await _service.RenderComponent<TestComponent>(null);
        
        // Assert
        html.ShouldNotBeNull();
        html.ShouldContain("<p>Count: 0</p>");
        html.ShouldNotContain("<h1>");
    }
}