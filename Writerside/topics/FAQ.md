# Frequently Asked Questions

Common questions and answers about FastComponents.

## General Questions

### What is FastComponents?

FastComponents is a .NET library that combines the component model of Blazor with the simplicity of HTMX. It allows you to build dynamic web applications using server-side rendering and C# without writing JavaScript.

### How is it different from Blazor?

While Blazor can run on the client (WebAssembly) or server (Server-Side Blazor with SignalR), FastComponents always renders on the server and uses HTMX for dynamic updates. This means:

- No WebAssembly download
- No SignalR connection
- Works without JavaScript (progressive enhancement)
- SEO-friendly by default
- Simpler deployment model

### Do I need to know HTMX to use FastComponents?

Basic HTMX knowledge is helpful but not required. FastComponents provides strongly-typed C# properties for all HTMX attributes, so you get IntelliSense and compile-time checking.

### Can I use it with existing ASP.NET Core applications?

Yes! FastComponents integrates seamlessly with existing ASP.NET Core applications. You can:
- Add it to existing MVC or Razor Pages apps
- Use it alongside Web API endpoints
- Mix it with other frontend frameworks

## Technical Questions

### How does state management work?

FastComponents uses immutable records for state that can be serialized to query strings:

```C#
[GenerateParameterMethods]
public partial record MyState : HtmxComponentParameters
{
    public int Page { get; init; } = 1;
}
```

State flows through URL parameters, making it shareable and bookmarkable.

### Can I use dependency injection?

Yes, components support full dependency injection:

```C#
public class MyComponent : SimpleHtmxComponent<MyState>
{
    [Inject] private IMyService MyService { get; set; } = null!;
    
    protected override async Task OnGetAsync(MyState state)
    {
        var data = await MyService.GetDataAsync();
        // Use the service
    }
}
```

### How do I handle forms?

Forms work like regular HTML forms with HTMX enhancement:

```Razor
<form hx-post="@Url" hx-target="this" hx-swap="outerHTML">
    <input name="email" type="email" required />
    <button type="submit">Submit</button>
</form>

@code {
    protected override MyState OnPost(MyState state)
    {
        var email = Request.Form["email"];
        // Process form
    }
}
```

### Can I use it with a CSS framework?

Yes! FastComponents renders plain HTML that works with any CSS framework:
- Bootstrap
- Tailwind CSS
- Bulma
- Material Design
- Custom CSS

### How do I handle authentication?

Use standard ASP.NET Core authentication:

```C#
app.MapHtmxGet<SecureComponent, SecureState>("/secure")
   .RequireAuthorization();

// In component
[Authorize]
public class SecureComponent : SimpleHtmxComponent<SecureState>
{
    // Only authenticated users can access
}
```

## Performance Questions

### Is it faster than Blazor Server?

For many scenarios, yes:
- No SignalR overhead
- No WebSocket connection to maintain
- Stateless HTTP requests
- Better caching opportunities
- Works with CDNs

### How do I optimize performance?

1. **Use output caching**:
```C#
app.MapHtmxGet<MyComponent, MyState>("/cached")
   .CacheOutput();
```

2. **Minimize state**:
```C#
[GenerateParameterMethods(SkipDefaults = true)]
```

3. **Use appropriate swap strategies**
4. **Enable response compression**

### Can it scale horizontally?

Yes! Since FastComponents is stateless (no SignalR), you can:
- Load balance across multiple servers
- Use container orchestration (Kubernetes)
- Deploy to serverless platforms
- Scale based on HTTP request load

## Development Questions

### What IDE should I use?

FastComponents works with:
- Visual Studio 2022
- Visual Studio Code
- JetBrains Rider
- Any editor with C# support

### How do I debug components?

Standard debugging techniques work:
```C#
protected override MyState OnPost(MyState state)
{
    // Set breakpoint here
    Debugger.Break();
    
    // Or use logging
    _logger.LogInformation("State: {@State}", state);
}
```

### Can I unit test components?

Yes, components are easily testable:
```C#
[Fact]
public void Component_Updates_State()
{
    var component = new MyComponent();
    var oldState = new MyState { Count = 1 };
    
    var newState = component.OnPost(oldState);
    
    Assert.Equal(2, newState.Count);
}
```

### How do I handle errors?

Implement error handling in your components:
```C#
protected override async Task<MyState> OnGetAsync(MyState state)
{
    try
    {
        return await LoadDataAsync(state);
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "Failed to load data");
        return state with { Error = "An error occurred" };
    }
}
```

## Deployment Questions

### Where can I deploy FastComponents apps?

Anywhere that supports ASP.NET Core:
- Azure App Service
- AWS Elastic Beanstalk
- Google Cloud Run
- Docker containers
- Traditional IIS
- Linux VPS

### Does it support AOT compilation?

Yes, with some limitations:
```xml
<PublishAot>true</PublishAot>
```

Convention-based features require reflection, so use explicit registration for full AOT.

### What are the hosting requirements?

- .NET 9.0 or later runtime
- Any OS that supports .NET (Windows, Linux, macOS)
- No special server requirements
- Works behind reverse proxies

## Migration Questions

### Can I migrate from Blazor?

Yes, components are similar:
1. Change base class to `SimpleHtmxComponent<TState>`
2. Replace `@onclick` with HTMX attributes
3. Update state management pattern
4. Remove JavaScript interop

### Can I use both Blazor and FastComponents?

Yes, you can use both in the same application:
- Use Blazor for complex interactivity
- Use FastComponents for simpler, SEO-friendly pages
- Share services and models

### How do I migrate from MVC/Razor Pages?

1. Extract view logic into components
2. Convert controllers/pages to component endpoints
3. Add HTMX attributes for dynamic behavior
4. Keep existing routing if desired

## Best Practices

### When should I use FastComponents?

FastComponents is ideal for:
- Content-focused websites
- Forms and CRUD operations
- SEO-critical applications
- Progressive enhancement scenarios
- Teams familiar with server-side rendering

### When should I use something else?

Consider alternatives for:
- Heavy client-side interactivity (use Blazor WebAssembly)
- Real-time collaboration (use Blazor Server)
- Offline-first applications (use PWA)
- Complex state management (use React/Angular)

### How do I structure large applications?

```
/Components
  /Shared      # Reusable components
  /Features    # Feature-specific components
  /Layouts     # Layout components
/Models        # Shared models
/Services      # Business logic
/wwwroot       # Static files
```

## Troubleshooting

### Why isn't HTMX working?

Check:
1. HTMX JavaScript is included
2. Correct attribute syntax (`hx-get` not `get`)
3. Target elements exist
4. No JavaScript errors

### Why is my state not persisting?

Ensure:
1. Using `[GenerateParameterMethods]`
2. Record is `partial`
3. Inherits from `HtmxComponentParameters`
4. Properties are `init`-only

### Why am I getting 404 errors?

Verify:
1. Endpoints are mapped
2. Routes are correct
3. Services are registered
4. Middleware is configured

## Getting Help

### Where can I get support?

- GitHub Issues for bugs
- GitHub Discussions for questions
- Stack Overflow with tag `fastcomponents`
- Official documentation

### How can I contribute?

- Report bugs
- Suggest features
- Submit pull requests
- Improve documentation
- Share your experiences

### Is there commercial support?

Contact Atypical Consulting SRL for:
- Training
- Consulting
- Custom development
- Priority support