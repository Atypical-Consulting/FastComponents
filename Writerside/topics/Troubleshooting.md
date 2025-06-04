# Troubleshooting

This guide helps you diagnose and fix common issues when working with FastComponents.

## Common Issues

### Components Not Rendering

**Symptom**: Component endpoints return 404 or empty responses.

**Solutions**:

1. **Check service registration**:
```C#
// Ensure services are registered
builder.Services.AddFastComponents();

// And middleware is configured
app.UseFastComponents();
```

2. **Verify endpoint mapping**:
```C#
// Check that endpoints are mapped
app.MapHtmxGet<MyComponent, MyState>("/my-component");

// Or using auto-mapping
app.UseFastComponentsAuto();
```

3. **Confirm component inheritance**:
```C#
// Component must inherit from base class
@inherits SimpleHtmxComponent<MyState>
// or
@inherits HtmxComponentBase
```

### HTMX Attributes Not Working

**Symptom**: HTMX attributes don't trigger requests or updates.

**Solutions**:

1. **Include HTMX JavaScript**:
```html
<script src="https://unpkg.com/htmx.org@2.0.0"></script>
<!-- Or local copy -->
<script src="/lib/htmx.min.js"></script>
```

2. **Check attribute syntax**:
```Razor
<!-- Correct -->
<button hx-get="/api/data" hx-target="#result">Click</button>

<!-- Incorrect (missing hx- prefix) -->
<button get="/api/data" target="#result">Click</button>
```

3. **Verify target elements exist**:
```html
<!-- Target must exist in DOM -->
<div id="result"></div>
<button hx-get="/api/data" hx-target="#result">Load</button>
```

### State Not Persisting

**Symptom**: Component state resets on each request.

**Solutions**:

1. **Use query string state**:
```C#
[GenerateParameterMethods]
public partial record MyState : HtmxComponentParameters
{
    public int Counter { get; init; }
}

// State persists in URL: /component?counter=5
```

2. **Check state binding**:
```C#
// Ensure state is properly bound from query
protected override MyState OnPost(MyState state)
{
    // State parameter contains current values
    return state with { Counter = state.Counter + 1 };
}
```

3. **Use session or database for complex state**:
```C#
protected override async Task<MyState> OnGetAsync(MyState state)
{
    // Load from session
    var sessionState = HttpContext.Session.GetString("state");
    if (sessionState != null)
    {
        state = JsonSerializer.Deserialize<MyState>(sessionState)!;
    }
    return state;
}
```

### Validation Errors

**Symptom**: Forms don't validate or show errors properly.

**Solutions**:

1. **Enable HTMX validation**:
```html
<form hx-post="/submit" hx-validate="true">
    <input type="email" required />
    <button type="submit">Submit</button>
</form>
```

2. **Handle validation in component**:
```C#
protected override FormState OnPost(FormState state)
{
    var errors = new List<string>();
    
    if (string.IsNullOrEmpty(state.Email))
        errors.Add("Email is required");
    
    if (errors.Any())
        return state with { Errors = errors };
    
    // Process valid form...
}
```

3. **Display validation errors**:
```Razor
@if (State.Errors.Any())
{
    <div class="errors">
        @foreach (var error in State.Errors)
        {
            <p>@error</p>
        }
    </div>
}
```

### Performance Issues

**Symptom**: Slow component rendering or response times.

**Solutions**:

1. **Enable output caching**:
```C#
app.MapHtmxGet<SlowComponent, ComponentState>("/slow")
   .CacheOutput(policy => policy.Expire(TimeSpan.FromMinutes(5)));
```

2. **Optimize state serialization**:
```C#
[GenerateParameterMethods(SkipDefaults = true)]
public partial record OptimizedState : HtmxComponentParameters
{
    // Only non-default values in query string
}
```

3. **Use appropriate swap strategies**:
```html
<!-- More efficient for large updates -->
<div hx-get="/data" hx-swap="outerHTML">

<!-- Less efficient for small updates -->
<div hx-get="/data" hx-swap="innerHTML">
```

## Debugging Techniques

### Enable HTMX Debugging

Add HTMX debug extension:

```html
<script>
    htmx.logAll(); // Log all HTMX events
</script>

<!-- Or use custom middleware -->
app.UseMiddleware<HtmxDebuggingMiddleware>();
```

### Browser Developer Tools

1. **Network tab**: Monitor HTMX requests
2. **Console**: Check for JavaScript errors
3. **Elements**: Inspect DOM changes
4. **Headers**: Verify HTMX headers

### Server-Side Logging

```C#
public class MyComponent : SimpleHtmxComponent<MyState>
{
    private readonly ILogger<MyComponent> _logger;
    
    protected override MyState OnPost(MyState state)
    {
        _logger.LogInformation("Processing request: {@State}", state);
        
        try
        {
            // Component logic
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing component");
            throw;
        }
    }
}
```

### Request/Response Inspection

```C#
// Log HTMX headers
app.Use(async (context, next) =>
{
    if (context.IsHtmxRequest())
    {
        var headers = context.GetHtmxRequestHeaders();
        logger.LogInformation("HTMX Request: {Trigger} -> {Target}", 
            headers.Trigger, headers.Target);
    }
    
    await next();
});
```

## Error Messages

### "Record with [GenerateParameterMethods] must be partial"

**Cause**: Source generator requires partial modifier.

**Fix**:
```C#
// ❌ Wrong
[GenerateParameterMethods]
public record MyState : HtmxComponentParameters { }

// ✅ Correct
[GenerateParameterMethods]
public partial record MyState : HtmxComponentParameters { }
```

### "Component must inherit from HtmxComponentParameters"

**Cause**: State record missing base class.

**Fix**:
```C#
// ❌ Wrong
public partial record MyState { }

// ✅ Correct
public partial record MyState : HtmxComponentParameters { }
```

### "Cannot resolve service for type 'HtmlRenderer'"

**Cause**: FastComponents services not registered.

**Fix**:
```C#
// Add in Program.cs
builder.Services.AddFastComponents();
```

### "The response has already started"

**Cause**: Trying to modify response after writing content.

**Fix**:
```C#
// Set headers before writing response
protected override async Task OnGetAsync(MyState state)
{
    // ✅ Headers first
    HttpContext.GetHtmxResponseHeaders().Trigger("my-event");
    
    // Then render content
    await base.OnGetAsync(state);
}
```

## Platform-Specific Issues

### Linux/macOS Case Sensitivity

File paths are case-sensitive on Linux/macOS:

```Razor
// Windows (works)
@using components.shared

// Linux/macOS (correct)
@using Components.Shared
```

### AOT Compilation Warnings

For AOT deployment:

```C#
// Suppress warnings where necessary
[UnconditionalSuppressMessage("Trimming", "IL2026")]
[UnconditionalSuppressMessage("AOT", "IL3050")]
public static void MapComponents(IEndpointRouteBuilder app)
{
    app.MapHtmxComponentsByConvention();
}
```

### Container Deployment

Ensure static files are included:

```docker
# Copy wwwroot
COPY wwwroot ./wwwroot

# Ensure HTMX files are present
RUN test -f wwwroot/lib/htmx.min.js
```

## Getting Help

### Check Documentation

1. Review the [Getting Started](Getting-Started.md) guide
2. Check [Examples](Examples.md) for similar use cases
3. Read the [API Reference](API-Reference.md)

### Enable Verbose Logging

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "FastComponents": "Debug",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
```

### Community Resources

- **GitHub Issues**: Report bugs and request features
- **Discussions**: Ask questions and share solutions
- **Stack Overflow**: Tag questions with `fastcomponents`

### Minimal Reproduction

When reporting issues, provide:

1. **Component code**:
```razor
@inherits SimpleHtmxComponent<MyState>
<!-- Minimal component showing issue -->
```

2. **State definition**:
```C#
[GenerateParameterMethods]
public partial record MyState : HtmxComponentParameters
{
    // Properties
}
```

3. **Registration code**:
```C#
// Program.cs setup
builder.Services.AddFastComponents();
app.MapHtmxGet<MyComponent, MyState>("/test");
```

4. **Error messages** and stack traces

## Diagnostic Checklist

- [ ] HTMX JavaScript is loaded
- [ ] Services are registered (`AddFastComponents()`)
- [ ] Middleware is configured (`UseFastComponents()`)
- [ ] Endpoints are mapped
- [ ] Component inherits from correct base class
- [ ] State record is partial with `[GenerateParameterMethods]`
- [ ] Target elements exist in DOM
- [ ] No JavaScript console errors
- [ ] Network requests show correct headers
- [ ] Response content-type is `text/html`

## Next Steps

- [FAQ](FAQ.md) - Frequently asked questions
- [Migration Guide](Migration-Guide.md) - Upgrading FastComponents
- [API Reference](API-Reference.md) - Detailed API documentation
- [Examples](Examples.md) - Working examples