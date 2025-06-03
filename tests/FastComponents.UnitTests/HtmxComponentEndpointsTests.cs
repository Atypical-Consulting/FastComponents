using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxComponentEndpointsTests
{
    public class SimpleTestComponent : HtmxComponentBase
    {
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, "Simple Component");
            builder.CloseElement();
        }
    }
    
    public class TestComponentWithParams : HtmxComponentBase<TestParameters>
    {
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder builder)
        {
            builder.OpenElement(0, "div");
            builder.AddContent(1, $"Name: {Parameters?.Name ?? "Unknown"}, Count: {Parameters?.Count ?? 0}");
            builder.CloseElement();
        }
    }
    
    public record TestParameters : HtmxComponentParameters
    {
        public string? Name { get; init; }
        public int? Count { get; init; }
        
        protected override string BuildQueryString()
        {
            var parts = new List<string>();
            if (!string.IsNullOrEmpty(Name)) parts.Add($"name={Uri.EscapeDataString(Name)}");
            if (Count.HasValue) parts.Add($"count={Count}");
            return string.Join("&", parts);
        }
        
        public override HtmxComponentParameters BindFromQuery(IQueryCollection query)
        {
            return new TestParameters
            {
                Name = GetQueryValue(query, "name"),
                Count = GetQueryInt(query, "count")
            };
        }
    }
    
    private WebApplication CreateTestApp()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddFastComponents();
        var app = builder.Build();
        app.UseFastComponents();
        return app;
    }
    
    [Fact]
    public void MapHtmxGet_WithoutParameters_RegistersEndpoint()
    {
        // Arrange
        var app = CreateTestApp();
        
        // Act
        var routeBuilder = app.MapHtmxGet<SimpleTestComponent>("/test");
        
        // Assert
        routeBuilder.ShouldNotBeNull();
    }
    
    [Fact]
    public void MapHtmxGet_WithParameters_RegistersEndpoint()
    {
        // Arrange
        var app = CreateTestApp();
        
        // Act
        var routeBuilder = app.MapHtmxGet<TestComponentWithParams, TestParameters>("/test-params");
        
        // Assert
        routeBuilder.ShouldNotBeNull();
    }
    
    [Fact]
    public void MapHtmxPost_WithoutParameters_RegistersEndpoint()
    {
        // Arrange
        var app = CreateTestApp();
        
        // Act
        var routeBuilder = app.MapHtmxPost<SimpleTestComponent>("/test-post");
        
        // Assert
        routeBuilder.ShouldNotBeNull();
    }
    
    [Fact]
    public void MapHtmxPost_WithParameters_RegistersEndpoint()
    {
        // Arrange
        var app = CreateTestApp();
        
        // Act
        var routeBuilder = app.MapHtmxPost<TestComponentWithParams, TestParameters>("/test-post-params");
        
        // Assert
        routeBuilder.ShouldNotBeNull();
    }
    
    [Fact]
    public void MapHtmxPut_WithParameters_RegistersEndpoint()
    {
        // Arrange
        var app = CreateTestApp();
        
        // Act
        var routeBuilder = app.MapHtmxPut<TestComponentWithParams, TestParameters>("/test-put");
        
        // Assert
        routeBuilder.ShouldNotBeNull();
    }
    
    [Fact]
    public void MapHtmxDelete_WithParameters_RegistersEndpoint()
    {
        // Arrange
        var app = CreateTestApp();
        
        // Act
        var routeBuilder = app.MapHtmxDelete<TestComponentWithParams, TestParameters>("/test-delete");
        
        // Assert
        routeBuilder.ShouldNotBeNull();
    }
    
    [Fact]
    public void MapHtmxPatch_WithParameters_RegistersEndpoint()
    {
        // Arrange
        var app = CreateTestApp();
        
        // Act
        var routeBuilder = app.MapHtmxPatch<TestComponentWithParams, TestParameters>("/test-patch");
        
        // Assert
        routeBuilder.ShouldNotBeNull();
    }
    
    [Fact]
    public void AllHttpVerbs_CanBeRegisteredForSameRoute()
    {
        // Arrange
        var app = CreateTestApp();
        
        // Act - Register all HTTP verbs for the same route
        var getBuilder = app.MapHtmxGet<SimpleTestComponent>("/api/resource");
        var postBuilder = app.MapHtmxPost<SimpleTestComponent>("/api/resource");
        var getParamsBuilder = app.MapHtmxGet<TestComponentWithParams, TestParameters>("/api/resource-params");
        var postParamsBuilder = app.MapHtmxPost<TestComponentWithParams, TestParameters>("/api/resource-params");
        var putBuilder = app.MapHtmxPut<TestComponentWithParams, TestParameters>("/api/resource-params");
        var deleteBuilder = app.MapHtmxDelete<TestComponentWithParams, TestParameters>("/api/resource-params");
        var patchBuilder = app.MapHtmxPatch<TestComponentWithParams, TestParameters>("/api/resource-params");
        
        // Assert - All route builders should be returned
        getBuilder.ShouldNotBeNull();
        postBuilder.ShouldNotBeNull();
        getParamsBuilder.ShouldNotBeNull();
        postParamsBuilder.ShouldNotBeNull();
        putBuilder.ShouldNotBeNull();
        deleteBuilder.ShouldNotBeNull();
        patchBuilder.ShouldNotBeNull();
    }
}