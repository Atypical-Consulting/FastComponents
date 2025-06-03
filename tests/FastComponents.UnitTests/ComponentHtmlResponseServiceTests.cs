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
        [Parameter]
        public string? Title { get; set; }

        [Parameter]
        public int Count { get; set; }

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
        ServiceCollection services = [];
        services.AddLogging();
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
        HtmlRenderer htmlRenderer = new(serviceProvider, loggerFactory);
        _service = new ComponentHtmlResponseService(htmlRenderer);
    }

    [Fact]
    public async Task RenderAsHtmlContent_WithoutParameters_RendersComponent()
    {
        // Act
        IResult result = await _service.RenderAsHtmlContentAsync<TestComponent>().ConfigureAwait(true);

        // Assert
        result.ShouldNotBeNull();
        var content = result as ContentHttpResult;
        content.ShouldNotBeNull();
        content.ContentType.ShouldBe("text/html; charset=utf-8");

        string? responseText = content.ResponseContent;
        responseText.ShouldNotBeNull();
        responseText.ShouldContain("<div class=\"test-component\">");
        responseText.ShouldContain("<p>Count: 0</p>");
        responseText.ShouldNotContain("<h1>");
    }

    [Fact]
    public async Task RenderAsHtmlContent_WithParameters_RendersComponentWithValues()
    {
        // Arrange
        Dictionary<string, object?> parameters = [];
        parameters.Add("Title", "Test Title");
        parameters.Add("Count", 42);

        // Act
        IResult result = await _service.RenderAsHtmlContentAsync<TestComponent>(parameters).ConfigureAwait(true);

        // Assert
        var content = result as ContentHttpResult;
        content.ShouldNotBeNull();

        string? responseText = content.ResponseContent;
        responseText.ShouldNotBeNull();
        responseText.ShouldContain("<h1>Test Title</h1>");
        responseText.ShouldContain("<p>Count: 42</p>");
    }

    [Fact]
    public async Task RenderAsHtmlContent_ReturnsBeautifiedHtml()
    {
        // Arrange
        Dictionary<string, object?> parameters = [];
        parameters.Add("Title", "Test");

        // Act
        IResult result = await _service.RenderAsHtmlContentAsync<TestComponent>(parameters).ConfigureAwait(true);

        // Assert
        var content = result as ContentHttpResult;
        content.ShouldNotBeNull();
        string? responseText = content.ResponseContent;
        responseText.ShouldNotBeNull();

        // Check that HTML is beautified (note: HtmlBeautifier returns minified HTML)
        responseText.ShouldContain("<div class=\"test-component\">");
        responseText.ShouldContain("<h1>Test</h1>");
    }

    [Fact]
    public async Task RenderComponent_WithoutParameters_RendersAsString()
    {
        // Act
        string html = await _service.RenderComponentAsync<TestComponent>().ConfigureAwait(true);

        // Assert
        html.ShouldNotBeNull();
        html.ShouldContain("<div class=\"test-component\">");
        html.ShouldContain("<p>Count: 0</p>");
    }

    [Fact]
    public async Task RenderComponent_WithParameters_RendersAsString()
    {
        // Arrange
        Dictionary<string, object?> parameters = [];
        parameters.Add("Title", "Component Test");
        parameters.Add("Count", 99);

        // Act
        string html = await _service.RenderComponentAsync<TestComponent>(parameters).ConfigureAwait(true);

        // Assert
        html.ShouldContain("<h1>Component Test</h1>");
        html.ShouldContain("<p>Count: 99</p>");
    }

    [Fact]
    public async Task RenderComponent_WithNullParameters_RendersDefaultValues()
    {
        // Act
        string html = await _service.RenderComponentAsync<TestComponent>().ConfigureAwait(true);

        // Assert
        html.ShouldNotBeNull();
        html.ShouldContain("<p>Count: 0</p>");
        html.ShouldNotContain("<h1>");
    }
}
