# Core Concepts

Understanding the core concepts of FastComponents will help you build more effective and maintainable applications.

## Component Architecture

### Base Classes

FastComponents provides several base classes for different scenarios:

#### HtmxComponentBase

The foundation for all HTMX-enabled components:

```csharp
@inherits HtmxComponentBase

<div @attributes="AdditionalAttributes">
    <!-- Your component content -->
</div>
```

Features:
- Access to all HTMX attributes as strongly-typed properties
- Automatic attribute rendering
- CSS class building support
- Custom attribute handling

#### SimpleHtmxComponent

A simplified base class for stateless components:

```csharp
@inherits SimpleHtmxComponent

<button hx-get="@GetRoute<CounterComponent>()">
    Load Counter
</button>
```

#### SimpleHtmxComponent<TState>

For components with state management:

```csharp
@inherits SimpleHtmxComponent<TodoState>

<div>
    <h3>@State.Title</h3>
    <button hx-post="@Url">Complete</button>
</div>

@code {
    protected override TodoState OnPost(TodoState state)
    {
        return state with { IsCompleted = true };
    }
}
```

## HTMX Attribute System

### Attribute Categories

FastComponents organizes HTMX attributes into logical groups:

#### Core Attributes (IHxCoreAttributes)
Essential HTMX functionality:
- `HxGet`, `HxPost`, `HxPut`, `HxDelete`, `HxPatch` - HTTP methods
- `HxTrigger` - Event triggers
- `HxTarget` - Target element selection
- `HxSwap` - Content swapping strategies
- `HxPushUrl` - URL management

#### Additional Attributes (IHxAdditionalAttributes)
Extended functionality:
- `HxConfirm` - Confirmation dialogs
- `HxDisable` - Element disabling
- `HxIndicator` - Loading indicators
- `HxValidate` - Form validation
- `HxSync` - Request synchronization

#### CSS Classes (IHxCssClasses)
Dynamic styling during requests:
- `HxCssRequest` - Applied during requests
- `HxCssSwapping` - Applied during content swap
- `HxCssSettling` - Applied during settling phase
- `HxCssAdded` - Applied to new content

### Using Attributes

Attributes can be set in multiple ways:

```csharp
// In code
@code {
    protected override void OnInitialized()
    {
        HxGet = "/api/data";
        HxTrigger = "click";
        HxTarget = "#result";
        HxSwap = "innerHTML";
    }
}

// Or inline
<div hx-get="/api/data" 
     hx-trigger="click" 
     hx-target="#result">
    Click me
</div>
```

## State Management

### Component Parameters

Define strongly-typed parameters using records:

```csharp
[GenerateParameterMethods]
public partial record ProductListState : HtmxComponentParameters
{
    public string SearchTerm { get; init; } = "";
    public int Page { get; init; } = 1;
    public string SortBy { get; init; } = "name";
}
```

The `[GenerateParameterMethods]` attribute generates:
- `BuildQueryString()` - Serializes state to query string
- `BindFromQuery()` - Deserializes state from query string

### State Flow

1. **Initial Request**: State is created from query parameters
2. **Component Render**: Component receives state and renders
3. **User Interaction**: HTMX sends request with current state
4. **State Update**: Handler methods update state
5. **Re-render**: Component re-renders with new state

Example flow:

```csharp
@inherits SimpleHtmxComponent<SearchState>

<!-- 1. Component renders with current state -->
<form hx-get="@Url" hx-trigger="input delay:500ms">
    <input name="query" value="@State.Query" />
</form>

<div id="results">
    @foreach (var item in GetResults())
    {
        <div>@item.Name</div>
    }
</div>

@code {
    // 2. State is automatically bound from query string
    private IEnumerable<Product> GetResults()
    {
        return ProductService.Search(State.Query);
    }
}
```

## Endpoint Routing

### Manual Mapping

Map components to specific routes:

```csharp
// GET requests
app.MapHtmxGet<ProductList, ProductListState>("/products");

// POST requests  
app.MapHtmxPost<AddToCart, CartItemState>("/cart/add");

// Multiple verbs
app.MapHtmxGet<TodoItem, TodoState>("/todo/{id}");
app.MapHtmxPost<TodoItem, TodoState>("/todo/{id}");
app.MapHtmxDelete<TodoItem, TodoState>("/todo/{id}");
```

### Convention-Based Mapping

Automatically map components by naming convention:

```csharp
// Maps all components ending with "Component" or "Example"
app.MapHtmxComponentsByConvention();

// Component: ProductListComponent
// Route: /htmx/product-list

// Component: SearchExample  
// Route: /htmx/search
```

### Route Parameters

Access route values in your components:

```csharp
@inherits SimpleHtmxComponent<ProductState>
@inject IHttpContextAccessor HttpContextAccessor

@code {
    protected override void OnInitialized()
    {
        var id = HttpContextAccessor.HttpContext?
            .GetRouteValue("id")?.ToString();
        // Use the ID...
    }
}
```

## Request/Response Headers

### Request Headers

Access HTMX request headers:

```csharp
public class MyEndpoint
{
    public IResult Handle(HttpContext context)
    {
        var htmxHeaders = context.GetHtmxRequestHeaders();
        
        if (htmxHeaders.IsHtmxRequest)
        {
            // Handle HTMX request
            var trigger = htmxHeaders.Trigger;
            var target = htmxHeaders.Target;
            
            if (htmxHeaders.IsBoosted)
            {
                // Handle boosted request
            }
        }
        
        return Results.Ok();
    }
}
```

### Response Headers

Control HTMX behavior with response headers:

```csharp
public IResult Handle(HttpContext context)
{
    var htmxResponse = context.GetHtmxResponseHeaders();
    
    // Trigger events
    htmxResponse.Trigger("dataUpdated");
    
    // Redirect
    htmxResponse.Redirect("/success");
    
    // Update URL
    htmxResponse.PushUrl("/products?page=2");
    
    // Retarget response
    htmxResponse.Retarget("#different-element");
    
    return Results.Ok();
}
```

## Component Lifecycle

### Initialization Flow

1. **Parameter Binding**: State is bound from query string
2. **OnInitialized**: Component initialization
3. **OnParametersSet**: Parameters are set
4. **BuildRenderTree**: Component renders

### HTTP Method Handlers

Components can handle different HTTP methods:

```csharp
@inherits SimpleHtmxComponent<ItemState>

@code {
    // Handle GET requests (default behavior)
    protected override void OnGet(ItemState state)
    {
        // Load data
    }
    
    // Handle POST requests
    protected override ItemState OnPost(ItemState state)
    {
        // Update and return new state
        return state with { Updated = true };
    }
    
    // Handle DELETE requests
    protected override ItemState OnDelete(ItemState state)
    {
        // Delete logic
        return state with { Deleted = true };
    }
}
```

## CSS Class Building

Use the `ClassNamesBuilder` for dynamic CSS classes:

```csharp
@inherits HtmxComponentBase

@code {
    protected override void OnBuildClassNames(ClassNamesBuilder builder)
    {
        builder
            .AddClass("card")
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass($"theme-{Theme}");
    }
}
```

## Best Practices

### 1. Component Granularity
- Keep components focused on a single responsibility
- Break complex UIs into smaller, reusable components
- Use composition over inheritance

### 2. State Management
- Use immutable records for state
- Keep state minimal and normalized
- Consider using URL state for shareable views

### 3. Performance
- Use appropriate swap strategies
- Implement proper caching headers
- Consider using SSE or WebSockets for real-time updates

### 4. Security
- Always validate input on the server
- Use anti-forgery tokens for state-changing operations
- Implement proper authorization checks

## Next Steps

- [Component Development](Component-Development.md) - Advanced component techniques
- [HTMX Attributes](HTMX-Attributes.md) - Complete attribute reference
- [State Management](State-Management.md) - Advanced state patterns
- [Performance](Performance.md) - Optimization techniques