# HTMX Attributes Reference

This comprehensive guide covers all HTMX attributes available in FastComponents, organized by category.

## Core Attributes

These are the fundamental HTMX attributes for making requests and updating content.

### HTTP Methods

#### HxGet
Performs a GET request to the specified URL.

```razor
<button hx-get="/api/users">Load Users</button>

@code {
    HxGet = "/api/users";
}
```

#### HxPost
Performs a POST request to the specified URL.

```razor
<form hx-post="/api/users">
    <input name="name" />
    <button type="submit">Create User</button>
</form>

@code {
    HxPost = "/api/users";
}
```

#### HxPut
Performs a PUT request to the specified URL.

```razor
<form hx-put="/api/users/123">
    <input name="name" />
    <button type="submit">Update User</button>
</form>
```

#### HxDelete
Performs a DELETE request to the specified URL.

```razor
<button hx-delete="/api/users/123" 
        hx-confirm="Are you sure?">
    Delete User
</button>
```

#### HxPatch
Performs a PATCH request to the specified URL.

```razor
<button hx-patch="/api/users/123/status">
    Toggle Status
</button>
```

### Request Configuration

#### HxTrigger
Specifies what triggers the request.

```razor
<!-- Basic triggers -->
<div hx-get="/data" hx-trigger="click">Click me</div>
<input hx-get="/search" hx-trigger="keyup" />
<div hx-get="/poll" hx-trigger="every 2s" />

<!-- Modifiers -->
<input hx-get="/search" hx-trigger="keyup changed delay:500ms" />
<button hx-post="/save" hx-trigger="click once" />
<div hx-get="/data" hx-trigger="revealed" />

<!-- Multiple triggers -->
<div hx-get="/update" hx-trigger="click, keyup[ctrlKey]" />

@code {
    HxTrigger = "click delay:500ms";
}
```

Common triggers:
- `click` - Mouse click
- `change` - Input value change
- `submit` - Form submission
- `keyup`, `keydown` - Keyboard events
- `mouseenter`, `mouseleave` - Mouse hover
- `load` - Element loads
- `revealed` - Element scrolls into view
- `every Xs` - Polling interval

#### HxTarget
Specifies where to place the response content.

```razor
<!-- CSS selectors -->
<button hx-get="/content" hx-target="#result">
    Load into #result
</button>

<!-- Relative targets -->
<button hx-get="/content" hx-target="this">
    Replace this button
</button>

<button hx-get="/content" hx-target="closest div">
    Replace parent div
</button>

<button hx-get="/content" hx-target="next .content">
    Next sibling with class
</button>

@code {
    HxTarget = "#result-container";
}
```

Special targets:
- `this` - The element itself
- `closest <selector>` - Closest ancestor matching selector
- `next <selector>` - Next sibling matching selector
- `previous <selector>` - Previous sibling matching selector

#### HxSwap
Specifies how to swap the content.

```razor
<!-- Swap strategies -->
<div hx-get="/content" hx-swap="innerHTML">
    Replace inner content (default)
</div>

<div hx-get="/content" hx-swap="outerHTML">
    Replace entire element
</div>

<div hx-get="/content" hx-swap="beforebegin">
    Insert before element
</div>

<div hx-get="/content" hx-swap="afterend">
    Insert after element
</div>

<!-- Modifiers -->
<div hx-get="/content" hx-swap="innerHTML settle:500ms">
    Settle after 500ms
</div>

<div hx-get="/content" hx-swap="innerHTML show:top">
    Scroll to top after swap
</div>

@code {
    HxSwap = "outerHTML";
}
```

Swap options:
- `innerHTML` - Replace inner HTML
- `outerHTML` - Replace entire element
- `beforebegin` - Insert before element
- `afterbegin` - Insert at start of element
- `beforeend` - Insert at end of element
- `afterend` - Insert after element
- `delete` - Delete target element
- `none` - No swap

### URL Management

#### HxPushUrl
Updates the browser URL without reload.

```razor
<a hx-get="/page2" hx-push-url="true">
    Go to Page 2 (updates URL)
</a>

<a hx-get="/page2" hx-push-url="/custom-url">
    Load Page 2 (sets custom URL)
</a>

@code {
    HxPushUrl = "true";
}
```

#### HxReplaceUrl
Replaces the current URL in history.

```razor
<button hx-post="/update" hx-replace-url="true">
    Update (replaces current history entry)
</button>
```

#### HxBoost
Converts normal links and forms to use HTMX.

```razor
<div hx-boost="true">
    <!-- All links and forms inside use HTMX -->
    <a href="/page">This uses HTMX</a>
    <form action="/submit" method="post">
        This form uses HTMX
    </form>
</div>
```

## Additional Attributes

These attributes provide extended functionality beyond basic requests.

### User Interaction

#### HxConfirm
Shows a confirmation dialog before request.

```razor
<button hx-delete="/api/users/123" 
        hx-confirm="Are you sure you want to delete this user?">
    Delete User
</button>

@code {
    HxConfirm = "This action cannot be undone. Continue?";
}
```

#### HxPrompt
Shows a prompt dialog and includes the value.

```razor
<button hx-post="/rename" 
        hx-prompt="Enter new name:">
    Rename
</button>
```

### Request Control

#### HxDisable
Disables elements during the request.

```razor
<form hx-post="/submit" hx-disable="button">
    <input name="data" />
    <button type="submit">Submit</button>
</form>
```

#### HxDisabledElt
Specifies which elements to disable.

```razor
<button hx-post="/action" 
        hx-disabled-elt="this">
    Click me (disables during request)
</button>

<button hx-post="/action" 
        hx-disabled-elt=".submit-buttons">
    Submit (disables all .submit-buttons)
</button>
```

#### HxIndicator
Shows/hides elements during requests.

```razor
<button hx-get="/slow-request" hx-indicator="#spinner">
    Load Data
</button>

<div id="spinner" class="htmx-indicator">
    Loading...
</div>

<style>
.htmx-indicator {
    display: none;
}
.htmx-request .htmx-indicator {
    display: inline;
}
</style>
```

### Data Handling

#### HxVals
Includes additional values with the request.

```razor
<button hx-post="/action" 
        hx-vals='{"action": "approve", "id": 123}'>
    Approve
</button>

<button hx-post="/action" 
        hx-vals='js:{timestamp: Date.now()}'>
    Submit with timestamp
</button>

@code {
    HxVals = JsonSerializer.Serialize(new { key = "value" });
}
```

#### HxVars
Sets variables that can be used in other attributes.

```razor
<div hx-vars="baseUrl:'/api/v1'">
    <button hx-get="${baseUrl}/users">Users</button>
    <button hx-get="${baseUrl}/posts">Posts</button>
</div>
```

#### HxParams
Controls which parameters are submitted.

```razor
<!-- Include all -->
<form hx-post="/submit" hx-params="*">
    <input name="field1" />
    <input name="field2" />
</form>

<!-- Include none -->
<form hx-post="/submit" hx-params="none">
    <!-- No form values submitted -->
</form>

<!-- Include specific -->
<form hx-post="/submit" hx-params="field1,field3">
    <input name="field1" /> <!-- included -->
    <input name="field2" /> <!-- excluded -->
    <input name="field3" /> <!-- included -->
</form>
```

#### HxHeaders
Adds custom headers to the request.

```razor
<button hx-get="/api/data" 
        hx-headers='{"X-Custom-Header": "value"}'>
    Load with custom header
</button>

@code {
    HxHeaders = JsonSerializer.Serialize(new 
    { 
        Authorization = "Bearer token123" 
    });
}
```

### Selection and Filtering

#### HxSelect
Selects a subset of the response.

```razor
<div hx-get="/page" hx-select="#content">
    Load only #content from response
</div>
```

#### HxSelectOob
Selects out-of-band content to swap.

```razor
<div hx-get="/update" hx-select-oob="#notifications">
    Also updates #notifications from response
</div>
```

### Advanced Features

#### HxSync
Synchronizes requests with other elements.

```razor
<!-- Abort previous request -->
<input hx-get="/search" 
       hx-trigger="keyup" 
       hx-sync="this:abort">

<!-- Queue requests -->
<button hx-post="/action" 
        hx-sync="form:queue">
    Queued Submit
</button>
```

#### HxValidate
Validates an element before including in request.

```razor
<form hx-post="/submit">
    <input name="email" 
           type="email" 
           hx-validate="true" 
           required />
    <button type="submit">Submit</button>
</form>
```

#### HxOn
Handles HTMX and DOM events inline.

```razor
<div hx-on:htmx:after-request="console.log('Request completed')">
    <!-- Content -->
</div>

<button hx-on:click="alert('Clicked!')">
    Click me
</button>
```

#### HxExt
Enables HTMX extensions.

```razor
<div hx-ext="json-enc">
    <!-- JSON encoding extension -->
    <form hx-post="/api/data">
        <input name="field" />
    </form>
</div>
```

## CSS Classes

HTMX adds these classes during various stages of requests.

```razor
@inherits HtmxComponentBase

@code {
    protected override void OnInitialized()
    {
        // Classes added during request
        HxCssRequest = "loading";
        
        // Classes added while swapping
        HxCssSwapping = "swapping";
        
        // Classes added during settle phase
        HxCssSettling = "settling";
        
        // Classes added to new content
        HxCssAdded = "new-content";
        
        // Classes for indicators
        HxCssIndicator = "spinner";
    }
}
```

### CSS Class Lifecycle

```css
/* Element making request */
.htmx-request { opacity: 0.5; }
.loading { cursor: wait; }

/* Target being swapped */
.htmx-swapping { opacity: 0; }
.swapping { transform: scale(0.95); }

/* During settle phase */
.htmx-settling { }
.settling { transition: all 0.3s; }

/* New content */
.htmx-added { }
.new-content { 
    animation: fadeIn 0.3s; 
}

/* Loading indicators */
.htmx-indicator { display: none; }
.htmx-request .htmx-indicator { display: block; }
```

## Special Attributes

### SSE (Server-Sent Events)

```razor
@inherits HtmxSseTag

<HtmxSseTag SseConnect="/sse/notifications" 
            SseSwap="message">
    <div id="notifications">
        <!-- SSE messages appear here -->
    </div>
</HtmxSseTag>
```

### WebSocket

```razor
@inherits HtmxWsTag

<HtmxWsTag WsConnect="/ws/chat" 
           WsSend="true">
    <div id="chat">
        <div id="messages"></div>
        <form>
            <input name="message" />
            <button type="submit">Send</button>
        </form>
    </div>
</HtmxWsTag>
```

## Attribute Inheritance

Some attributes can be inherited from parent elements:

```razor
<div hx-target="#result" hx-swap="innerHTML">
    <!-- These buttons inherit target and swap -->
    <button hx-get="/data1">Load 1</button>
    <button hx-get="/data2">Load 2</button>
    
    <!-- Override inherited values -->
    <button hx-get="/data3" hx-target="#other">
        Load 3 (different target)
    </button>
</div>
```

Use `hx-disinherit` to stop inheritance:

```razor
<div hx-target="#result">
    <div hx-disinherit="hx-target">
        <!-- Buttons here don't inherit target -->
        <button hx-get="/data">No inherited target</button>
    </div>
</div>
```

## Best Practices

1. **Use Appropriate Triggers**: Choose triggers that match user expectations
2. **Set Clear Targets**: Be specific about where content should be placed
3. **Provide Feedback**: Use indicators and CSS classes for loading states
4. **Confirm Destructive Actions**: Always use `hx-confirm` for deletions
5. **Optimize Requests**: Use `hx-trigger` modifiers to debounce/throttle
6. **Handle Errors**: Implement proper error handling and display
7. **Test Accessibility**: Ensure HTMX enhancements don't break accessibility

## Next Steps

- [Component Development](Component-Development.md) - Building components
- [Examples](Examples.md) - Real-world usage examples
- [Events](Events.md) - Handling HTMX events
- [Performance](Performance.md) - Optimization techniques