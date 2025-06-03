using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Shouldly;
using Xunit;

namespace FastComponents.UnitTests;

/// <summary>
/// Tests for SimpleHtmxComponent
/// </summary>
public class SimpleHtmxComponentTests
{
    private record TestState
    {
        public int Count { get; init; } = 0;
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
        var component = new TestComponent();

        // Assert
        component.State.ShouldNotBeNull();
        component.State.Count.ShouldBe(0);
        component.State.Name.ShouldBe("Test");
    }

    [Fact]
    public void Url_WithoutParameters_ShouldReturnBaseRoute()
    {
        // Arrange
        var component = new TestComponent();

        // Act
        var url = component.Url();

        // Assert
        url.ShouldStartWith("/htmx/test?");
        url.ShouldContain("Count=0");
        url.ShouldContain("Name=Test");
    }

    [Fact]
    public void Url_WithNewState_ShouldReturnUpdatedRoute()
    {
        // Arrange
        var component = new TestComponent();
        var newState = new TestState { Count = 5, Name = "Updated" };

        // Act
        var url = component.Url(newState);

        // Assert
        url.ShouldStartWith("/htmx/test?");
        url.ShouldContain("Count=5");
        url.ShouldContain("Name=Updated");
    }

    [Fact]
    public void Url_WithUpdateFunction_ShouldApplyChanges()
    {
        // Arrange
        var component = new TestComponent();
        component.State = new TestState { Count = 3, Name = "Original" };

        // Act
        var url = component.Url(s => s with { Count = s.Count + 1 });

        // Assert
        url.ShouldStartWith("/htmx/test?");
        url.ShouldContain("Count=4");
        url.ShouldContain("Name=Original");
    }

    [Fact]
    public void GetRoute_ShouldReturnConventionalRoute()
    {
        // Arrange
        var component = new TestComponent();

        // Act
        var route = component.GetRoute();

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
        var route = SimpleHtmxComponent.GetRoute<TestStatelessComponent>();

        // Assert
        route.ShouldBe("/htmx/teststateless");
    }
}