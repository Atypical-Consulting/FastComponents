using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components.Rendering;
using Shouldly;

namespace FastComponents.UnitTests;

/// <summary>
/// Tests for SimpleHtmxComponent
/// </summary>
public class SimpleHtmxComponentTests
{
    private sealed record TestState
    {
        public int Count { get; init; }
        public string Name { get; init; } = "Test";
    }

    private class TestComponent : SimpleHtmxComponent<TestState>
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, $"Count: {State.Count}, Name: {State.Name}");
            builder.CloseElement();
        }
    }

    [Fact]
    public void State_ShouldDefaultToNewInstance()
    {
        // Arrange & Act
        TestComponent component = new();

        // Assert
        component.State.ShouldNotBeNull();
        component.State.Count.ShouldBe(0);
        component.State.Name.ShouldBe("Test");
    }

    [Fact]
    public void Url_WithoutParameters_ShouldReturnBaseRoute()
    {
        // Arrange
        TestComponent component = new();

        // Act
        string url = component.Url();

        // Assert
        url.ShouldStartWith("/htmx/test?");
        url.ShouldContain("Count=0");
        url.ShouldContain("Name=Test");
    }

    [Fact]
    public void Url_WithNewState_ShouldReturnUpdatedRoute()
    {
        // Arrange
        TestComponent component = new();
        TestState newState = new() { Count = 5, Name = "Updated" };

        // Act
        string url = component.Url(newState);

        // Assert
        url.ShouldStartWith("/htmx/test?");
        url.ShouldContain("Count=5");
        url.ShouldContain("Name=Updated");
    }

    [Fact]
    [SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.")]
    public void Url_WithUpdateFunction_ShouldApplyChanges()
    {
        // Arrange
        TestComponent component = new();
        component.State = new TestState { Count = 3, Name = "Original" };

        // Act
        string url = component.Url(s => s with { Count = s.Count + 1 });

        // Assert
        url.ShouldStartWith("/htmx/test?");
        url.ShouldContain("Count=4");
        url.ShouldContain("Name=Original");
    }

    [Fact]
    public void GetRoute_ShouldReturnConventionalRoute()
    {
        // Arrange
        TestComponent component = new();

        // Act
        string route = component.GetRoute();

        // Assert
        route.ShouldBe("/htmx/test");
    }

    private class TestStatelessComponent : SimpleHtmxComponent
    {
        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, "Stateless Component");
            builder.CloseElement();
        }
    }

    [Fact]
    public void StatelessComponent_GetRoute_ShouldWork()
    {
        // Act
        string route = SimpleHtmxComponent.GetRoute<TestStatelessComponent>();

        // Assert
        route.ShouldBe("/htmx/teststateless");
    }
}
