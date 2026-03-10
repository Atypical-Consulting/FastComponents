# HTTP API

FastComponents provides extensions and helpers for working with HTMX HTTP headers.

## HtmxHttpContextExtensions

Extension methods for HttpContext to work with HTMX requests and responses.

### Request Methods

#### IsHtmxRequest

Checks if the current request is from HTMX.

```C#
if (HttpContext.IsHtmxRequest())
{
    // Handle HTMX request
}
```

#### IsHtmxBoostedRequest

Checks if the request is a boosted request (progressive enhancement).

```C#
if (HttpContext.IsHtmxBoostedRequest())
{
    // Return partial content for boosted requests
}
```

#### GetHtmxRequestHeaders

Gets the HTMX request headers.

```C#
var htmxHeaders = HttpContext.GetHtmxRequestHeaders();
var trigger = htmxHeaders.Trigger;
var target = htmxHeaders.Target;
```

#### GetHtmxResponseHeaders

Gets the HTMX response headers helper.

```C#
var htmxResponse = HttpContext.GetHtmxResponseHeaders();
htmxResponse.Redirect("/new-page");
```

## HtmxRequestHeaders

Represents HTMX request headers sent by the client.

### Properties

- `IsHtmxRequest` - True if this is an HTMX request
- `IsBoosted` - True if this is a boosted request
- `IsHistoryRestoreRequest` - True if restoring from history
- `CurrentUrl` - The current URL of the browser
- `Target` - The target element ID
- `Trigger` - The ID of the triggered element
- `TriggerName` - The name of the triggered element
- `Prompt` - User response to hx-prompt

### Example

```C#
public IResult HandleRequest()
{
    var headers = HttpContext.GetHtmxRequestHeaders();
    
    if (headers.Trigger == "search-button")
    {
        // Handle search
    }
    
    return Results.Ok();
}
```

## HtmxResponseHeaders

Helper for setting HTMX response headers.

### Methods

#### Redirect

Triggers a client-side redirect.

```C#
response.Redirect("/new-location");
```

#### Refresh

Triggers a full page refresh.

```csharp
response.Refresh();
```

#### Location

Sets the HX-Location header for client-side navigation.

```csharp
response.Location = new
{
    path = "/new-page",
    target = "#content",
    swap = "innerHTML"
};
```

#### PushUrl / ReplaceUrl

Updates the browser URL without navigation.

```csharp
response.PushUrl("/new-url");
response.ReplaceUrl("/replacement-url");
```

#### Retarget / Reswap / Reselect

Overrides client-side targeting and swapping.

```csharp
response.Retarget("#different-target");
response.Reswap("outerHTML");
response.Reselect(".new-selector");
```

#### Trigger Events

Triggers client-side events.

```csharp
// Simple trigger
response.Trigger = "dataUpdated";

// Trigger with details
response.TriggerWithDetails(new { 
    eventName = "itemAdded",
    itemId = 123 
});

// Trigger after swap/settle
response.TriggerAfterSwap = "afterSwapEvent";
response.TriggerAfterSettle = "afterSettleEvent";
```

### Complete Example

```csharp
app.MapPost("/api/update", (HttpContext context) =>
{
    var htmxResponse = context.GetHtmxResponseHeaders();
    
    // Update data...
    
    // Set response headers
    htmxResponse.Retarget("#notifications");
    htmxResponse.TriggerWithDetails(new
    {
        eventName = "itemUpdated",
        itemId = 42,
        timestamp = DateTime.UtcNow
    });
    
    return Results.Ok("<div>Update successful!</div>");
});
```

## See Also

- [HTMX Attributes](HTMX-Attributes.md)
- [Events API](Events-API.md)
- [Core Concepts](Core-Concepts.md)