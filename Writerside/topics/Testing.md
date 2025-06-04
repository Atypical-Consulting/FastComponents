# Testing

FastComponents supports comprehensive testing strategies for both unit and integration testing of HTMX-enabled components.

<tip>
FastComponents components are standard Blazor components, so you can use all existing Blazor testing patterns and tools.
</tip>

<chapter title="Unit Testing Components" id="unit-testing">

### Component Testing with xUnit

```C#
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

public class CounterComponentTests
{
    [Fact]
    public void Counter_RendersCorrectly()
    {
        // Arrange
        using var serviceProvider = new ServiceCollection()
            .AddScoped<HtmlRenderer>()
            .AddScoped<ComponentHtmlResponseService>()
            .BuildServiceProvider();
            
        var htmlRenderer = serviceProvider.GetRequiredService<HtmlRenderer>();
        var component = new CounterComponent();
        
        // Act
        var result = htmlRenderer.RenderComponentAsync<CounterComponent>(
            ParameterView.FromDictionary(new Dictionary<string, object?>
            {
                ["State"] = new CounterState { Count = 5 }
            })).Result;
            
        // Assert
        Assert.Contains("Count: 5", result.ToHtmlString());
    }
}
```

### Testing Component State

```C#
public class ComponentStateTests
{
    [Fact]
    public void CounterState_BindsFromQuery()
    {
        // Arrange
        var query = new QueryCollection(new Dictionary<string, StringValues>
        {
            ["count"] = "42",
            ["message"] = "test"
        });
        
        var state = new CounterState();
        
        // Act
        state.BindFromQuery(query);
        
        // Assert
        Assert.Equal(42, state.Count);
        Assert.Equal("test", state.Message);
    }
    
    [Fact]
    public void CounterState_BuildsQueryString()
    {
        // Arrange
        var state = new CounterState 
        { 
            Count = 10, 
            Message = "hello" 
        };
        
        // Act
        var queryString = state.BuildQueryString();
        
        // Assert
        Assert.Equal("?count=10&message=hello", queryString);
    }
}
```

## Integration Testing

### HTMX Endpoint Testing

```C#
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using Xunit;

public class HtmxEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public HtmxEndpointTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task CounterEndpoint_ReturnsHtml()
    {
        // Arrange
        _client.DefaultRequestHeaders.Add("HX-Request", "true");
        
        // Act
        var response = await _client.GetAsync("/htmx/counter?count=5");
        
        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Count: 5", content);
        Assert.Equal("text/html", response.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task CounterEndpoint_HandlesPost()
    {
        // Arrange
        _client.DefaultRequestHeaders.Add("HX-Request", "true");
        var formData = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("count", "10")
        });
        
        // Act
        var response = await _client.PostAsync("/htmx/counter", formData);
        
        // Assert
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Count: 11", content); // Assuming increment logic
    }
}
```

### Testing HTMX Headers

```C#
[Fact]
public async Task Component_SetsCorrectHtmxHeaders()
{
    // Arrange
    _client.DefaultRequestHeaders.Add("HX-Request", "true");
    _client.DefaultRequestHeaders.Add("HX-Target", "#content");
    
    // Act
    var response = await _client.GetAsync("/htmx/redirect-component");
    
    // Assert
    Assert.True(response.Headers.Contains("HX-Redirect"));
    Assert.Equal("/new-page", response.Headers.GetValues("HX-Redirect").First());
}
```

## Testing with Playwright

### End-to-End HTMX Testing

```C#
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class HtmxBehaviorTests : PageTest
{
    [TestMethod]
    public async Task Counter_IncrementsOnClick()
    {
        // Arrange
        await Page.GotoAsync("https://localhost:5001/counter");
        
        // Act
        await Page.ClickAsync("#increment-button");
        
        // Wait for HTMX to complete
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        
        // Assert
        var countText = await Page.TextContentAsync("#count-display");
        Assert.AreEqual("Count: 1", countText);
    }

    [TestMethod]
    public async Task Search_UpdatesResults()
    {
        // Arrange
        await Page.GotoAsync("https://localhost:5001/search");
        
        // Act
        await Page.FillAsync("#search-input", "test query");
        
        // Wait for debounced request
        await Page.WaitForTimeoutAsync(500);
        await Page.WaitForSelectorAsync("#search-results [data-testid='result-item']");
        
        // Assert
        var results = await Page.QuerySelectorAllAsync("#search-results [data-testid='result-item']");
        Assert.IsTrue(results.Count > 0);
    }
}
```

### Testing WebSocket/SSE

```C#
[TestMethod]
public async Task SSE_ReceivesUpdates()
{
    // Arrange
    await Page.GotoAsync("https://localhost:5001/live-updates");
    
    // Wait for SSE connection
    await Page.WaitForSelectorAsync("[data-sse-connected='true']");
    
    // Trigger server-side event
    await Page.EvaluateAsync(@"
        fetch('/api/trigger-event', { method: 'POST' })
    ");
    
    // Wait for SSE update
    await Page.WaitForSelectorAsync("#notification:has-text('New notification')");
    
    // Assert
    var notification = await Page.TextContentAsync("#notification");
    Assert.IsTrue(notification.Contains("New notification"));
}
```

## Testing Utilities

### HTMX Test Client

```C#
public class HtmxTestClient
{
    private readonly HttpClient _client;
    
    public HtmxTestClient(HttpClient client)
    {
        _client = client;
        _client.DefaultRequestHeaders.Add("HX-Request", "true");
    }
    
    public async Task<HtmxResponse> GetAsync(string url, string? target = null)
    {
        if (target != null)
        {
            _client.DefaultRequestHeaders.Add("HX-Target", target);
        }
        
        var response = await _client.GetAsync(url);
        return new HtmxResponse(response);
    }
    
    public async Task<HtmxResponse> PostAsync(string url, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        
        var response = await _client.PostAsync(url, content);
        return new HtmxResponse(response);
    }
}

public class HtmxResponse
{
    private readonly HttpResponseMessage _response;
    
    public HtmxResponse(HttpResponseMessage response)
    {
        _response = response;
    }
    
    public async Task<string> GetContentAsync() 
        => await _response.Content.ReadAsStringAsync();
    
    public string? GetHeader(string name) 
        => _response.Headers.GetValues(name)?.FirstOrDefault();
    
    public bool HasTrigger(string eventName)
        => GetHeader("HX-Trigger")?.Contains(eventName) ?? false;
        
    public string? RedirectUrl => GetHeader("HX-Redirect");
    public string? RetargetSelector => GetHeader("HX-Retarget");
}
```

### Component Test Base

```C#
public abstract class ComponentTestBase<TComponent, TState> 
    where TComponent : HtmxComponentBase<TState>, new()
    where TState : HtmxComponentParameters, new()
{
    protected IServiceProvider ServiceProvider { get; private set; }
    protected ComponentHtmlResponseService RenderService { get; private set; }
    
    protected ComponentTestBase()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        
        ServiceProvider = services.BuildServiceProvider();
        RenderService = ServiceProvider.GetRequiredService<ComponentHtmlResponseService>();
    }
    
    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<HtmlRenderer>();
        services.AddScoped<ComponentHtmlResponseService>();
    }
    
    protected async Task<string> RenderComponentAsync(TState state)
    {
        var parameters = new Dictionary<string, object?> { ["State"] = state };
        var result = await RenderService.RenderComponentAsync<TComponent>(parameters);
        return result.ToHtmlString();
    }
    
    protected async Task<string> RenderComponentAsync(
        Dictionary<string, object?> parameters)
    {
        var result = await RenderService.RenderComponentAsync<TComponent>(parameters);
        return result.ToHtmlString();
    }
}
```

## Mocking Dependencies

### Mocking Services

```C#
[Fact]
public async Task SearchComponent_UsesSearchService()
{
    // Arrange
    var mockSearchService = new Mock<ISearchService>();
    mockSearchService
        .Setup(s => s.SearchAsync("test"))
        .ReturnsAsync(new[] { new SearchResult { Title = "Test Result" } });
    
    var services = new ServiceCollection()
        .AddScoped<HtmlRenderer>()
        .AddScoped<ComponentHtmlResponseService>()
        .AddScoped(_ => mockSearchService.Object)
        .BuildServiceProvider();
    
    var renderService = services.GetRequiredService<ComponentHtmlResponseService>();
    
    // Act
    var result = await renderService.RenderComponentAsync<SearchComponent>(
        new Dictionary<string, object?> 
        {
            ["State"] = new SearchState { Query = "test" }
        });
    
    // Assert
    var html = result.ToHtmlString();
    Assert.Contains("Test Result", html);
    mockSearchService.Verify(s => s.SearchAsync("test"), Times.Once);
}
```

## Test Coverage

### Measuring Coverage

Use the coverage script provided in the project:

```bash
# Run tests with coverage
./coverage.sh

# View HTML report
open CoverageReport/index.html
```

### Coverage Configuration

```xml
<!-- In test project -->
<PropertyGroup>
    <CollectCoverage>true</CollectCoverage>
    <CoverletOutputFormat>cobertura</CoverletOutputFormat>
    <CoverletOutput>./TestResults/</CoverletOutput>
    <Include>[FastComponents]*</Include>
    <Exclude>[FastComponents.Tests]*</Exclude>
</PropertyGroup>
```

## Best Practices

### Test Organization

1. **Arrange-Act-Assert**: Follow AAA pattern consistently
2. **One Concept Per Test**: Test one thing at a time
3. **Descriptive Names**: Use clear, descriptive test names
4. **Test Data Builders**: Use builders for complex test data

### HTMX-Specific Testing

1. **Test HTMX Headers**: Verify correct headers are set
2. **Test Partial Updates**: Ensure only necessary content is returned
3. **Test Error Scenarios**: Verify error handling works correctly
4. **Test State Transitions**: Verify component state changes properly

### Performance Testing

```C#
[Fact]
public async Task Component_RendersWithinTimeLimit()
{
    // Arrange
    var stopwatch = Stopwatch.StartNew();
    var state = new ComplexState { /* large data */ };
    
    // Act
    await RenderComponentAsync(state);
    stopwatch.Stop();
    
    // Assert
    Assert.True(stopwatch.ElapsedMilliseconds < 100, 
        $"Component took {stopwatch.ElapsedMilliseconds}ms to render");
}
```

## CI/CD Integration

### GitHub Actions

```yaml
name: Test
on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest
    steps:
    - uses: actions/checkout@v3
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: '9.0.x'
    
    - name: Run tests
      run: dotnet test --configuration Release --logger trx --collect:"XPlat Code Coverage"
    
    - name: Upload coverage
      uses: codecov/codecov-action@v3
```

## See Also

- [Performance](Performance.md)
- [Deployment](Deployment.md)
- [Examples](Examples.md)