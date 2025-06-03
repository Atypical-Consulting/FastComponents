using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace FastComponents.UnitTests;

/// <summary>
/// Tests for SimplifiedExtensions
/// </summary>
public class SimplifiedExtensionsTests
{
    [Fact]
    public void AddFastComponentsAuto_ShouldAddCoreServices()
    {
        // Arrange
        ServiceCollection services = [];
        services.AddLogging(); // Add logging services for HtmlRenderer dependency

        // Act
        IServiceCollection result = services.AddFastComponentsAuto();

        // Assert
        result.ShouldBeSameAs(services);

        // Verify that core FastComponents services are registered
        ServiceProvider serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<ComponentHtmlResponseService>().ShouldNotBeNull();
    }

    [Fact]
    public void UseFastComponentsAuto_ShouldReturnApp()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        WebApplication app = builder.Build();

        // Act
        WebApplication result = app.UseFastComponentsAuto();

        // Assert
        result.ShouldBeSameAs(app);
    }

    [Fact]
    public void HtmxDefaults_ShouldHaveCorrectValues()
    {
        // Assert
        HtmxDefaults.Swap.ShouldBe("outerHTML");
        HtmxDefaults.SearchTrigger.ShouldBe("keyup changed delay:300ms, search");
        HtmxDefaults.LoadOnceTrigger.ShouldBe("load once");
    }

    [Fact]
    public void HtmxPatterns_SelfUpdatingButton_ShouldReturnCorrectAttributes()
    {
        // Act
        Dictionary<string, object> attributes = HtmxPatterns.SelfUpdatingButton("/update", "my-button");

        // Assert
        attributes.ShouldContainKeyAndValue("hx-get", "/update");
        attributes.ShouldContainKeyAndValue("hx-target", "#my-button");
        attributes.ShouldContainKeyAndValue("hx-swap", "outerHTML");
        attributes.ShouldContainKeyAndValue("id", "my-button");
    }

    [Fact]
    public void HtmxPatterns_SearchInput_ShouldReturnCorrectAttributes()
    {
        // Act
        Dictionary<string, object> attributes = HtmxPatterns.SearchInput("/search", "#results");

        // Assert
        attributes.ShouldContainKeyAndValue("hx-get", "/search");
        attributes.ShouldContainKeyAndValue("hx-target", "#results");
        attributes.ShouldContainKeyAndValue("hx-trigger", "keyup changed delay:300ms, search");
        attributes.ShouldContainKeyAndValue("hx-indicator", "#loading");
    }

    [Fact]
    public void HtmxPatterns_LoadOnce_ShouldReturnCorrectAttributes()
    {
        // Act
        Dictionary<string, object> attributes = HtmxPatterns.LoadOnce("/load-content");

        // Assert
        attributes.ShouldContainKeyAndValue("hx-get", "/load-content");
        attributes.ShouldContainKeyAndValue("hx-trigger", "load once");
    }
}
