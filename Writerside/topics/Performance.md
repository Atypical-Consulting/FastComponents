# Performance

FastComponents is designed for high-performance web applications. This guide covers optimization techniques and best practices.

<note>
Performance optimization should be based on actual measurements. Always profile your application before making changes.
</note>

<chapter title="Performance Characteristics" id="performance-characteristics">

### Server-Side Rendering

- **No JavaScript Framework Overhead**: Pure HTML responses
- **Minimal Client Processing**: HTMX handles DOM updates efficiently
- **Reduced Bundle Size**: ~14KB (minified + gzipped) for HTMX vs 100KB+ for typical SPAs

### Network Efficiency

- **Partial Updates**: Only send changed content
- **HTTP Caching**: Full support for browser caching
- **Compression**: Works with standard HTTP compression

## Optimization Techniques

### Component Design

#### 1. Minimize Component State

```C#
// ❌ Large state object
public class ProductListState : HtmxComponentParameters
{
    public List<Product> Products { get; set; } = new();
    public Dictionary<int, bool> ExpandedStates { get; set; } = new();
    public UserPreferences Preferences { get; set; } = new();
}

// ✅ Focused state
public class ProductListState : HtmxComponentParameters
{
    public int Page { get; set; } = 1;
    public string? Filter { get; set; }
    public string? Sort { get; set; }
}
```

#### 2. Use Streaming for Large Lists

```csharp
@inherits HtmxComponentBase

@foreach (var item in GetItemsStreaming())
{
    <div class="item">@item.Name</div>
}

@code {
    private async IAsyncEnumerable<Item> GetItemsStreaming()
    {
        await foreach (var item in repository.GetItemsAsync())
        {
            yield return item;
        }
    }
}
```

### Response Optimization

#### 1. Enable Response Compression

```csharp
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.EnableForHttps = true;
});

app.UseResponseCompression();
```

#### 2. Use HTTP Caching

```csharp
app.MapHtmxGet<StaticComponent, EmptyState>("/htmx/static")
   .CacheOutput(policy => policy.Expire(TimeSpan.FromHours(1)));
```

#### 3. Implement ETags

```csharp
public class CachedComponent : HtmxComponentBase<CachedState>
{
    protected override void OnParametersSet()
    {
        var etag = ComputeETag(Parameters);
        if (Context.Request.Headers.IfNoneMatch == etag)
        {
            Context.Response.StatusCode = 304; // Not Modified
            return;
        }
        
        Context.Response.Headers.ETag = etag;
    }
}
```

### Database Optimization

#### 1. Use Projection

```csharp
// ❌ Loading entire entities
var products = await context.Products
    .Include(p => p.Category)
    .Include(p => p.Reviews)
    .ToListAsync();

// ✅ Project only needed data
var products = await context.Products
    .Select(p => new ProductViewModel
    {
        Id = p.Id,
        Name = p.Name,
        Price = p.Price,
        CategoryName = p.Category.Name
    })
    .ToListAsync();
```

#### 2. Implement Pagination

```csharp
public class PaginatedList : HtmxComponentBase<PaginatedState>
{
    private const int PageSize = 20;
    
    protected override async Task OnParametersSetAsync()
    {
        var query = repository.GetQuery();
        
        TotalCount = await query.CountAsync();
        Items = await query
            .Skip((Parameters.Page - 1) * PageSize)
            .Take(PageSize)
            .ToListAsync();
    }
}
```

### HTMX-Specific Optimizations

#### 1. Use hx-boost Selectively

```csharp
// Boost only navigation links
<nav hx-boost="true">
    <a href="/page1">Page 1</a>
    <a href="/page2">Page 2</a>
</nav>

// Regular forms without boost
<form action="/submit" method="post">
    <!-- Form content -->
</form>
```

#### 2. Optimize Polling Intervals

```csharp
// ❌ Too frequent polling
HxTrigger = "every 100ms"

// ✅ Reasonable intervals
HxTrigger = "every 2s"

// ✅ Even better: Use SSE for real-time updates
<HtmxSseTag SseConnect="/api/live-data" SseSwap="update">
    <div id="data-container"></div>
</HtmxSseTag>
```

#### 3. Debounce User Input

```csharp
// Search with debouncing
HxTrigger = "keyup changed delay:300ms, search"
```

## Memory Management

### Component Lifecycle

```csharp
public class ResourceIntensiveComponent : HtmxComponentBase<MyState>, IDisposable
{
    private readonly MemoryCache _cache;
    
    public ResourceIntensiveComponent(IMemoryCache cache)
    {
        _cache = cache;
    }
    
    public void Dispose()
    {
        // Clean up resources
        _cache.Dispose();
    }
}
```

### String Optimization

```csharp
// Use StringBuilder for large HTML generation
protected override void BuildRenderTree(RenderTreeBuilder builder)
{
    var sb = new StringBuilder();
    foreach (var item in LargeCollection)
    {
        sb.AppendFormat("<div>{0}</div>", item.Name);
    }
    
    builder.AddMarkupContent(0, sb.ToString());
}
```

## Monitoring and Profiling

### Request Timing

```csharp
public class PerformanceMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sw = Stopwatch.StartNew();
        
        await next(context);
        
        if (context.IsHtmxRequest())
        {
            context.Response.Headers.Add("X-Response-Time", 
                $"{sw.ElapsedMilliseconds}ms");
        }
    }
}
```

### Component Metrics

```csharp
public abstract class InstrumentedComponent<T> : HtmxComponentBase<T>
    where T : HtmxComponentParameters, new()
{
    private readonly ILogger<InstrumentedComponent<T>> _logger;
    
    protected override async Task OnParametersSetAsync()
    {
        using var activity = Activity.StartActivity("Component.Render");
        activity?.SetTag("component.type", GetType().Name);
        
        var sw = Stopwatch.StartNew();
        await base.OnParametersSetAsync();
        
        _logger.LogDebug("Component {Name} rendered in {Time}ms", 
            GetType().Name, sw.ElapsedMilliseconds);
    }
}
```

## Load Testing

### Example with k6

```javascript
import http from 'k6/http';
import { check } from 'k6';

export let options = {
    stages: [
        { duration: '30s', target: 100 },
        { duration: '1m', target: 100 },
        { duration: '30s', target: 0 },
    ],
};

export default function() {
    let response = http.get('https://yourapp.com/htmx/component', {
        headers: {
            'HX-Request': 'true',
            'HX-Target': '#content'
        }
    });
    
    check(response, {
        'status is 200': (r) => r.status === 200,
        'response time < 200ms': (r) => r.timings.duration < 200,
    });
}
```

## Best Practices Summary

### Do's

1. ✅ Use partial updates instead of full page reloads
2. ✅ Implement proper caching strategies
3. ✅ Optimize database queries with projection
4. ✅ Use SSE/WebSockets for real-time data
5. ✅ Profile and monitor performance regularly

### Don'ts

1. ❌ Don't over-poll with aggressive intervals
2. ❌ Don't load unnecessary data in components
3. ❌ Don't ignore browser caching capabilities
4. ❌ Don't render large lists without pagination
5. ❌ Don't neglect compression settings

## Performance Checklist

- [ ] Response compression enabled
- [ ] HTTP caching configured
- [ ] Database queries optimized
- [ ] Component state minimized
- [ ] Polling intervals reasonable
- [ ] Large lists paginated
- [ ] Static assets cached
- [ ] CDN configured for static files
- [ ] Monitoring in place
- [ ] Load testing performed

## See Also

- [AOT Support](AOT-Support.md)
- [Deployment](Deployment.md)
- [Advanced Features](Advanced-Features.md)