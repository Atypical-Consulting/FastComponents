# Advanced Features

Explore the advanced capabilities of FastComponents for building sophisticated web applications.

## Server-Sent Events (SSE)

### Overview

Server-Sent Events enable real-time, one-way communication from server to client. FastComponents provides the `HtmxSseTag` component for easy SSE integration.

### Basic SSE Setup

```razor
@* Real-time notifications *@
<HtmxSseTag SseConnect="/notifications/stream" 
            SseSwap="message">
    <div id="notifications">
        <!-- Messages will appear here -->
    </div>
</HtmxSseTag>

@* Server endpoint *@
app.MapGet("/notifications/stream", async (HttpContext context) =>
{
    context.Response.Headers.Append("Content-Type", "text/event-stream");
    context.Response.Headers.Append("Cache-Control", "no-cache");
    context.Response.Headers.Append("Connection", "keep-alive");
    
    await foreach (var notification in GetNotificationsAsync(context.RequestAborted))
    {
        var html = $"<div class='notification'>{notification.Message}</div>";
        await context.Response.WriteAsync($"event: message\n");
        await context.Response.WriteAsync($"data: {html}\n\n");
        await context.Response.Body.FlushAsync();
    }
});
```

### Multiple Event Types

```razor
<HtmxSseTag SseConnect="/dashboard/stream">
    <div sse-swap="users" id="user-count">0</div>
    <div sse-swap="sales" id="sales-total">$0</div>
    <div sse-swap="alerts" id="alerts"></div>
</HtmxSseTag>

@* Server sends different event types *@
// User count update
await response.WriteAsync("event: users\n");
await response.WriteAsync($"data: <span>{userCount}</span>\n\n");

// Sales update
await response.WriteAsync("event: sales\n");
await response.WriteAsync($"data: <span>${salesTotal:N2}</span>\n\n");

// Alert
await response.WriteAsync("event: alerts\n");
await response.WriteAsync($"data: <div class='alert'>{alertMessage}</div>\n\n");
```

## WebSocket Support

### Overview

WebSockets provide full-duplex communication for real-time applications. Use `HtmxWsTag` for WebSocket integration.

### Chat Application Example

```razor
@* Chat component *@
<HtmxWsTag WsConnect="/ws/chat" 
           WsSend="true">
    <div id="chat-container">
        <div id="messages" hx-swap-oob="beforeend">
            <!-- Messages appear here -->
        </div>
        
        <form ws-send>
            <input name="message" placeholder="Type a message..." />
            <button type="submit">Send</button>
        </form>
    </div>
</HtmxWsTag>

@* WebSocket handler *@
app.UseWebSockets();
app.Map("/ws/chat", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await HandleChatWebSocket(webSocket);
    }
});

async Task HandleChatWebSocket(WebSocket webSocket)
{
    var buffer = new byte[1024 * 4];
    
    while (webSocket.State == WebSocketState.Open)
    {
        var result = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), 
            CancellationToken.None);
            
        if (result.MessageType == WebSocketMessageType.Text)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var response = ProcessChatMessage(message);
            
            await webSocket.SendAsync(
                Encoding.UTF8.GetBytes(response),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);
        }
    }
}
```

## Source Generators

### GenerateParameterMethods Attribute

The source generator creates efficient query string handling:

```csharp
[GenerateParameterMethods(SkipDefaults = true)]
public partial record SearchParameters : HtmxComponentParameters
{
    public string Query { get; init; } = "";
    public int Page { get; init; } = 1;
    public string Category { get; init; } = "all";
    public List<string> Tags { get; init; } = [];
}

// Generated code includes:
// - BuildQueryString() with optimal serialization
// - BindFromQuery() with efficient parsing
// - GetQueryBool() helper for boolean parsing
```

### Custom Analyzers

FastComponents includes analyzers to catch common mistakes:

- **FC0001**: Missing `[GenerateParameterMethods]` attribute
- **FC0002**: Record must be partial for code generation
- **FC0003**: Must inherit from `HtmxComponentParameters`
- **FC0004**: Properties should be init-only
- **FC0005**: Manual implementation conflicts with generated code

## AOT (Ahead-of-Time) Compilation

### Enabling AOT

FastComponents supports AOT compilation with some considerations:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <PublishAot>true</PublishAot>
    <IsAotCompatible>true</IsAotCompatible>
  </PropertyGroup>
</Project>
```

### AOT-Safe Component Registration

```csharp
// Use explicit registration for AOT
app.MapHtmxGet<CounterComponent, CounterState>("/counter")
   .RequireUnreferencedCode("Component rendering");

// Suppress warnings where necessary
[UnconditionalSuppressMessage("Trimming", "IL2026")]
public static void RegisterComponents(IEndpointRouteBuilder app)
{
    app.MapHtmxComponentsByConvention();
}
```

### Trimming Considerations

Some features require runtime code generation:
- Convention-based mapping uses reflection
- Component rendering uses dynamic code
- Parameter binding may use reflection

For full AOT support, use explicit registration and avoid convention-based features.

## Performance Optimization

### Component Caching

Cache rendered components for better performance:

```csharp
// Output caching
app.MapHtmxGet<ExpensiveComponent, ComponentState>("/expensive")
   .CacheOutput(policy => policy
       .Expire(TimeSpan.FromMinutes(5))
       .VaryByQuery("id", "category"));

// Response caching
[ResponseCache(Duration = 300, VaryByQueryKeys = ["page"])]
public class ProductListComponent : SimpleHtmxComponent<ProductListState>
{
    // Component implementation
}
```

### Efficient State Serialization

Optimize query string generation:

```csharp
[GenerateParameterMethods(SkipDefaults = true)]
public partial record OptimizedState : HtmxComponentParameters
{
    // Only non-default values appear in URL
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string SortBy { get; init; } = "name";
}
```

### Streaming Responses

Stream large responses for better perceived performance:

```csharp
public class StreamingComponent : SimpleHtmxComponent<StreamingState>
{
    protected override async Task OnGetAsync(StreamingState state)
    {
        Response.Headers.Append("X-Content-Type-Options", "nosniff");
        
        await Response.WriteAsync("<div class='results'>");
        
        await foreach (var item in GetItemsAsync())
        {
            var html = RenderItem(item);
            await Response.WriteAsync(html);
            await Response.Body.FlushAsync();
        }
        
        await Response.WriteAsync("</div>");
    }
}
```

## Security Features

### CSRF Protection

Integrate with ASP.NET Core's anti-forgery tokens:

```razor
@inject IAntiforgery Antiforgery

<form hx-post="@Url" hx-target="this">
    @{
        var token = Antiforgery.GetAndStoreTokens(HttpContext);
    }
    <input type="hidden" 
           name="@token.FormFieldName" 
           value="@token.RequestToken" />
    
    <!-- Form fields -->
    <button type="submit">Submit</button>
</form>
```

### Content Security Policy

Configure CSP headers for HTMX:

```csharp
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Content-Security-Policy",
        "default-src 'self'; " +
        "script-src 'self' 'unsafe-inline' unpkg.com; " +
        "style-src 'self' 'unsafe-inline'; " +
        "connect-src 'self' ws: wss:;");
    
    await next();
});
```

### Input Validation

Validate all input on the server:

```csharp
public record SecureState : HtmxComponentParameters
{
    private string _email = "";
    
    public string Email 
    { 
        get => _email;
        init => _email = SanitizeEmail(value);
    }
    
    private static string SanitizeEmail(string email)
    {
        // Validation and sanitization logic
        return email.Trim().ToLowerInvariant();
    }
}
```

## Custom Extensions

### Creating HTMX Extensions

Create custom HTMX extensions for specialized behavior:

```javascript
// wwwroot/js/custom-extension.js
htmx.defineExtension('custom-loader', {
    onEvent: function(name, evt) {
        if (name === "htmx:beforeRequest") {
            // Show custom loader
            showCustomLoader(evt.detail.elt);
        } else if (name === "htmx:afterRequest") {
            // Hide custom loader
            hideCustomLoader(evt.detail.elt);
        }
    }
});
```

Usage in components:

```razor
<div hx-ext="custom-loader" 
     hx-get="/api/data" 
     hx-trigger="click">
    Load with custom loader
</div>
```

### Component Middleware

Create middleware for cross-cutting concerns:

```csharp
public class HtmxComponentMiddleware
{
    private readonly RequestDelegate _next;
    
    public async Task InvokeAsync(HttpContext context)
    {
        // Before component processing
        if (context.IsHtmxRequest())
        {
            context.Items["RequestTime"] = DateTime.UtcNow;
        }
        
        await _next(context);
        
        // After component processing
        if (context.IsHtmxRequest())
        {
            var elapsed = DateTime.UtcNow - (DateTime)context.Items["RequestTime"]!;
            context.Response.Headers.Append("X-Processing-Time", 
                elapsed.TotalMilliseconds.ToString());
        }
    }
}

// Register middleware
app.UseMiddleware<HtmxComponentMiddleware>();
```

## Integration Patterns

### Hybrid Architectures

Combine FastComponents with other technologies:

```csharp
// API endpoints for data
app.MapGet("/api/products", () => GetProducts());

// FastComponents for UI
app.MapHtmxGet<ProductList, ProductListState>("/products");

// Static pages with enhancement
app.MapFallbackToFile("index.html");
```

### Progressive Enhancement

Start with working HTML, enhance with HTMX:

```razor
@* Works without JavaScript *@
<form method="post" action="/search" 
      hx-boost="true" 
      hx-target="#results">
    <input name="q" />
    <button type="submit">Search</button>
</form>

<div id="results">
    @* Results render here with or without HTMX *@
</div>
```

## Advanced Patterns

### Component Composition

Build complex UIs from simple components:

```csharp
public abstract class ComposableComponent<TState> : SimpleHtmxComponent<TState>
    where TState : HtmxComponentParameters, new()
{
    protected RenderFragment RenderChild<TChild, TChildState>(TChildState state)
        where TChild : SimpleHtmxComponent<TChildState>, new()
        where TChildState : HtmxComponentParameters, new()
    {
        return builder =>
        {
            builder.OpenComponent<TChild>(0);
            builder.AddAttribute(1, "State", state);
            builder.CloseComponent();
        };
    }
}
```

### Event-Driven Architecture

Use HTMX events for loose coupling:

```razor
@* Publisher component *@
<button hx-post="/action" 
        hx-trigger="click"
        hx-on:htmx:after-request="htmx.trigger('body', 'data-updated')">
    Update Data
</button>

@* Subscriber components *@
<div hx-get="/widget1" 
     hx-trigger="data-updated from:body">
    <!-- Refreshes when data-updated fires -->
</div>

<div hx-get="/widget2" 
     hx-trigger="data-updated from:body">
    <!-- Also refreshes -->
</div>
```

## Next Steps

- [SSE & WebSockets](SSE-WebSockets.md) - Deep dive into real-time features
- [Source Generators](Source-Generators.md) - Custom code generation
- [AOT Support](AOT-Support.md) - Native compilation details
- [Performance](Performance.md) - Optimization guide
- [Security](Security.md) - Security best practices