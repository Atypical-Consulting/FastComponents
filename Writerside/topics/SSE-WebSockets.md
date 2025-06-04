# SSE & WebSockets

FastComponents provides built-in support for Server-Sent Events (SSE) and WebSockets through dedicated components.

## Server-Sent Events (SSE)

### HtmxSseTag

The `HtmxSseTag` component enables SSE connections in your components.

```C#
<HtmxSseTag SseConnect="/api/notifications" SseSwap="message">
    <div id="notifications">
        <!-- SSE messages will be swapped here -->
    </div>
</HtmxSseTag>
```

### Properties

- `SseConnect` - The URL to connect for SSE
- `SseSwap` - The event name to listen for swapping content
- `ChildContent` - The content to render inside the SSE container

### Example: Live Notifications

```C#
@page "/notifications"

<h3>Live Notifications</h3>

<HtmxSseTag SseConnect="/api/live-notifications" SseSwap="notification">
    <div id="notification-list" class="notification-container">
        <p>Waiting for notifications...</p>
    </div>
</HtmxSseTag>

@code {
    // Server endpoint sends SSE messages like:
    // event: notification
    // data: <div class="notification">New message received!</div>
}
```

### Server-Side SSE Implementation

```C#
app.MapGet("/api/live-notifications", async (HttpContext context) =>
{
    context.Response.Headers.Add("Content-Type", "text/event-stream");
    context.Response.Headers.Add("Cache-Control", "no-cache");
    
    while (!context.RequestAborted.IsCancellationRequested)
    {
        var notification = GetNextNotification();
        await context.Response.WriteAsync($"event: notification\n");
        await context.Response.WriteAsync($"data: {notification}\n\n");
        await context.Response.Body.FlushAsync();
        
        await Task.Delay(1000);
    }
});
```

## WebSockets

### HtmxWsTag

The `HtmxWsTag` component enables WebSocket connections.

```csharp
<HtmxWsTag WsConnect="/ws/chat" WsSend="true">
    <form>
        <input type="text" name="message" />
        <button type="submit">Send</button>
    </form>
    <div id="messages">
        <!-- WebSocket messages appear here -->
    </div>
</HtmxWsTag>
```

### Properties

- `WsConnect` - The WebSocket URL to connect to
- `WsSend` - Whether to send form data through WebSocket
- `ChildContent` - The content to render inside the WebSocket container

### Example: Real-Time Chat

```csharp
@page "/chat"

<h3>Real-Time Chat</h3>

<HtmxWsTag WsConnect="@($"wss://{Request.Host}/ws/chat")" WsSend="true">
    <div class="chat-container">
        <div id="chat-messages" class="messages">
            <!-- Messages will appear here -->
        </div>
        
        <form class="message-form">
            <input type="text" name="username" placeholder="Username" required />
            <input type="text" name="message" placeholder="Type a message..." required />
            <button type="submit">Send</button>
        </form>
    </div>
</HtmxWsTag>
```

### Server-Side WebSocket Implementation

```csharp
app.UseWebSockets();

app.Map("/ws/chat", async context =>
{
    if (context.WebSockets.IsWebSocketRequest)
    {
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        await HandleChatWebSocket(webSocket);
    }
    else
    {
        context.Response.StatusCode = 400;
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
            // Parse message and broadcast to other clients
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

## HTMX Integration

### SSE Events

HTMX automatically handles SSE events and swaps content:

```javascript
// Custom SSE event handling
document.body.addEventListener('htmx:sseMessage', function(evt) {
    console.log('SSE message received:', evt.detail);
});

// Handle SSE errors
document.body.addEventListener('htmx:sseError', function(evt) {
    console.error('SSE error:', evt.detail);
});
```

### WebSocket Events

```javascript
// WebSocket connected
document.body.addEventListener('htmx:wsConnecting', function(evt) {
    console.log('WebSocket connecting...');
});

// Message received
document.body.addEventListener('htmx:wsMessage', function(evt) {
    console.log('WebSocket message:', evt.detail.message);
});
```

## Best Practices

### Connection Management

1. **Automatic Reconnection**: HTMX handles reconnection automatically
2. **Error Handling**: Implement fallbacks for connection failures
3. **Resource Cleanup**: Connections are closed when elements are removed

### Security

1. **Authentication**: Validate connections server-side
2. **Message Validation**: Sanitize all incoming messages
3. **Rate Limiting**: Implement rate limiting for WebSocket messages

### Performance

1. **Message Batching**: Batch multiple updates when possible
2. **Compression**: Enable WebSocket compression for large messages
3. **Connection Pooling**: Reuse connections when appropriate

## Complete Example: Live Dashboard

```csharp
@page "/dashboard"
@inherits HtmxComponentBase

<div class="dashboard">
    <h2>Live Dashboard</h2>
    
    <!-- Real-time metrics via SSE -->
    <HtmxSseTag SseConnect="/api/metrics" SseSwap="metric-update">
        <div id="metrics" class="metrics-grid">
            <div class="metric">
                <h4>Active Users</h4>
                <span id="active-users">-</span>
            </div>
            <div class="metric">
                <h4>CPU Usage</h4>
                <span id="cpu-usage">-</span>
            </div>
        </div>
    </HtmxSseTag>
    
    <!-- Real-time chat via WebSocket -->
    <HtmxWsTag WsConnect="/ws/dashboard-chat" WsSend="true">
        <div class="chat-panel">
            <div id="chat-messages"></div>
            <form>
                <input type="text" name="message" />
                <button>Send</button>
            </form>
        </div>
    </HtmxWsTag>
</div>
```

## See Also

- [Events API](Events-API.md)
- [Advanced Features](Advanced-Features.md)
- [Component Development](Component-Development.md)