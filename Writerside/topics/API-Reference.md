# API Reference

Complete API documentation for FastComponents, organized by namespace and functionality.

## Namespaces

- **FastComponents** - Core components and base classes
- **FastComponents.Http** - HTTP request/response handling
- **FastComponents.Events** - HTMX event constants
- **FastComponents.Generators** - Source generators and analyzers

## Quick Links

- [Components API](Components-API.md) - Component base classes and interfaces
- [Endpoints API](Endpoints-API.md) - Endpoint mapping and routing
- [HTTP API](HTTP-API.md) - Request and response headers
- [Events API](Events-API.md) - HTMX event handling
- [Builders API](Builders-API.md) - Fluent builders and helpers

## Core Types

### HtmxComponentBase

Base class for all HTMX-enabled components.

```csharp
public abstract class HtmxComponentBase : ComponentBase, IHxAttributes
{
    // Core HTMX attributes
    public string? HxGet { get; set; }
    public string? HxPost { get; set; }
    public string? HxPut { get; set; }
    public string? HxDelete { get; set; }
    public string? HxPatch { get; set; }
    
    // Targeting and swapping
    public string? HxTarget { get; set; }
    public string? HxSwap { get; set; }
    public string? HxTrigger { get; set; }
    
    // Additional features
    public string? HxConfirm { get; set; }
    public string? HxIndicator { get; set; }
    public string? HxPushUrl { get; set; }
    
    // CSS classes
    public string? ClassName { get; protected set; }
    
    // Lifecycle
    protected virtual void OnBuildClassNames(ClassNamesBuilder builder) { }
}
```

### SimpleHtmxComponent\<TState\>

Simplified base class for stateful components.

```csharp
public abstract class SimpleHtmxComponent<TState> : HtmxComponentBase
    where TState : HtmxComponentParameters, new()
{
    public TState State { get; protected set; }
    public string Url => State.ToComponentUrl(GetRoute());
    
    // HTTP method handlers
    protected virtual TState OnGet(TState state) => state;
    protected virtual TState OnPost(TState state) => state;
    protected virtual TState OnPut(TState state) => state;
    protected virtual TState OnDelete(TState state) => state;
    protected virtual TState OnPatch(TState state) => state;
}
```

### HtmxComponentParameters

Base class for component state/parameters.

```csharp
public abstract record HtmxComponentParameters
{
    // Query string serialization
    protected virtual string BuildQueryString();
    public virtual HtmxComponentParameters BindFromQuery(IQueryCollection query);
    
    // URL generation
    public string ToComponentUrl(string baseUrl);
    
    // Helper methods
    protected static string? GetQueryValue(IQueryCollection query, string key);
    protected static int? GetQueryInt(IQueryCollection query, string key);
}
```

## Extension Methods

### Service Registration

```csharp
// Manual registration
services.AddFastComponents();

// Auto-registration with assembly scanning
services.AddFastComponentsAuto();
```

### Endpoint Mapping

```csharp
// Manual mapping
app.MapHtmxGet<MyComponent, MyState>("/my-component");
app.MapHtmxPost<MyComponent, MyState>("/my-component");

// Convention-based mapping
app.MapHtmxComponentsByConvention();

// Auto-mapping with middleware
app.UseFastComponentsAuto();
```

### HTTP Context Extensions

```csharp
// Check if request is from HTMX
if (context.IsHtmxRequest())
{
    // Get request headers
    var headers = context.GetHtmxRequestHeaders();
    var trigger = headers.Trigger;
    
    // Set response headers
    var response = context.GetHtmxResponseHeaders();
    response.Trigger("my-event");
    response.PushUrl("/new-url");
}
```

## Attributes

### [GenerateParameterMethods]

Generates query string serialization methods.

```csharp
[GenerateParameterMethods(SkipDefaults = true)]
public partial record MyState : HtmxComponentParameters
{
    public string Name { get; init; } = "";
    public int Page { get; init; } = 1;
}
```

## Interfaces

### IHxAttributes

Complete set of HTMX attributes.

```csharp
public interface IHxAttributes : IHxCoreAttributes, IHxAdditionalAttributes
{
    // Combines all HTMX attribute interfaces
}
```

### IHxCoreAttributes

Core HTMX functionality.

```csharp
public interface IHxCoreAttributes
{
    string? HxGet { get; set; }
    string? HxPost { get; set; }
    string? HxTrigger { get; set; }
    string? HxTarget { get; set; }
    string? HxSwap { get; set; }
    // ... more attributes
}
```

## Builders

### HtmxBuilder

Fluent API for building HTMX elements.

```csharp
var builder = HtmxBuilder.Create()
    .Div()
    .Get("/api/data")
    .Trigger("click")
    .Target("#result")
    .Swap("innerHTML")
    .Class("my-component")
    .Content(childContent);
```

### ClassNamesBuilder

Fluent API for building CSS classes.

```csharp
var classes = ClassNamesBuilder.Default("base-class")
    .AddClass("active", isActive)
    .AddClass("disabled", isDisabled)
    .AddClass($"theme-{theme}")
    .Build();
```

## Services

### ComponentHtmlResponseService

Renders components as HTML responses.

```csharp
public class ComponentHtmlResponseService
{
    public async Task<IHtmlContent> RenderComponentAsync<TComponent>(
        Dictionary<string, object?>? parameters = null)
        where TComponent : IComponent;
        
    public async Task<IHtmlContent> RenderAsHtmlContentAsync<TComponent>(
        Dictionary<string, object?>? parameters = null)
        where TComponent : IComponent;
}
```

### HtmlBeautifier

Formats HTML output for readability.

```csharp
public static class HtmlBeautifier
{
    public static string BeautifyHtml(string html);
}
```

## Constants

### HtmxEvents

All HTMX JavaScript events.

```csharp
public static class HtmxEvents
{
    // Lifecycle events
    public const string BeforeRequest = "htmx:beforeRequest";
    public const string AfterRequest = "htmx:afterRequest";
    public const string BeforeSwap = "htmx:beforeSwap";
    public const string AfterSwap = "htmx:afterSwap";
    
    // Error events
    public const string ResponseError = "htmx:responseError";
    public const string SendError = "htmx:sendError";
    
    // ... many more events
}
```

### HtmxDefaults

Default values for HTMX behavior.

```csharp
public static class HtmxDefaults
{
    public const string Swap = "innerHTML";
    public const string LoadOnceTrigger = "load once";
    public const string SearchTrigger = "keyup changed delay:500ms";
}
```

## Detailed Documentation

For detailed information about specific APIs, see:

- [Components API](Components-API.md) - Detailed component class documentation
- [Endpoints API](Endpoints-API.md) - Endpoint mapping and routing details
- [HTTP API](HTTP-API.md) - Request/response header documentation
- [Events API](Events-API.md) - Event handling and constants
- [Builders API](Builders-API.md) - Builder pattern implementations