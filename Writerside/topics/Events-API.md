# Events API

FastComponents provides constants for all HTMX events to ensure type safety and consistency.

## HtmxEvents

Static class containing all HTMX event names as constants.

### Request Lifecycle Events

#### BeforeRequest
Triggered before an AJAX request is sent.

```C#
HxOn = $"{HtmxEvents.BeforeRequest}: showLoader()"
```

#### AfterRequest
Triggered after an AJAX request completes.

```C#
HxOn = $"{HtmxEvents.AfterRequest}: hideLoader()"
```

#### BeforeSwap
Triggered before content is swapped into the DOM.

```C#
HxOn = $"{HtmxEvents.BeforeSwap}: prepareContent(event)"
```

#### AfterSwap
Triggered after content is swapped into the DOM.

```C#
HxOn = $"{HtmxEvents.AfterSwap}: initializeComponents()"
```

#### AfterSettle
Triggered after content has settled (animations complete).

```C#
HxOn = $"{HtmxEvents.AfterSettle}: focusFirstInput()"
```

### Error Events

#### ResponseError
Triggered when a response error occurs.

```C#
HxOn = $"{HtmxEvents.ResponseError}: handleError(event)"
```

#### SendError
Triggered when a network error occurs.

```C#
HxOn = $"{HtmxEvents.SendError}: showNetworkError()"
```

#### TargetError
Triggered when the target element is not found.

```C#
HxOn = $"{HtmxEvents.TargetError}: console.error('Target not found')"
```

#### Timeout
Triggered when a request times out.

```C#
HxOn = $"{HtmxEvents.Timeout}: showTimeoutMessage()"
```

### History Events

#### HistoryRestore
Triggered when history is restored.

```C#
HxOn = $"{HtmxEvents.HistoryRestore}: restorePageState()"
```

#### BeforeHistorySave
Triggered before history is saved.

```C#
HxOn = $"{HtmxEvents.BeforeHistorySave}: savePageState()"
```

### Validation Events

#### ValidationValidate
Triggered during form validation.

```C#
HxOn = $"{HtmxEvents.ValidationValidate}: customValidation(event)"
```

#### ValidationFailed
Triggered when validation fails.

```C#
HxOn = $"{HtmxEvents.ValidationFailed}: showValidationErrors(event)"
```

### Other Events

#### Confirm
Triggered for confirmation dialogs.

```C#
HxOn = $"{HtmxEvents.Confirm}: customConfirm(event)"
```

#### Prompt
Triggered for prompt dialogs.

```csharp
HxOn = $"{HtmxEvents.Prompt}: customPrompt(event)"
```

#### Load
Triggered when content is loaded.

```csharp
HxOn = $"{HtmxEvents.Load}: onContentLoaded()"
```

#### ConfigRequest
Triggered to configure requests.

```csharp
HxOn = $"{HtmxEvents.ConfigRequest}: addAuthHeaders(event)"
```

## Usage Examples

### Multiple Event Handlers

```csharp
@inherits HtmxComponentBase

<div hx-get="/api/data"
     hx-on="@($"{HtmxEvents.BeforeRequest}: showSpinner(); {HtmxEvents.AfterRequest}: hideSpinner()")">
    Load Data
</div>
```

### Component Example

```csharp
@inherits HtmxComponentBase

@code {
    protected override void OnInitialized()
    {
        HxOn = $@"
            {HtmxEvents.BeforeRequest}: this.classList.add('loading');
            {HtmxEvents.AfterRequest}: this.classList.remove('loading');
            {HtmxEvents.ResponseError}: alert('Error loading data');
        ";
    }
}
```

### JavaScript Integration

```javascript
document.body.addEventListener('htmx:beforeRequest', function(evt) {
    // Show loading indicator
});

document.body.addEventListener('htmx:afterSwap', function(evt) {
    // Initialize new components
});
```

## Complete Event List

- `Abort`
- `AfterOnLoad`
- `AfterProcessNode`
- `AfterRequest`
- `AfterSettle`
- `AfterSwap`
- `BeforeCleanupElement`
- `BeforeHistorySave`
- `BeforeOnLoad`
- `BeforeProcessNode`
- `BeforeRequest`
- `BeforeSwap`
- `BeforeTransition`
- `Cancel`
- `ConfigRequest`
- `Confirm`
- `HistoryCacheMiss`
- `HistoryCacheMissError`
- `HistoryCacheMissLoad`
- `HistoryRestore`
- `Load`
- `NoSseSourceError`
- `OobAfterSwap`
- `OobBeforeSwap`
- `OobErrorNoTarget`
- `Prompt`
- `PushedIntoHistory`
- `ReplacedInHistory`
- `ResponseError`
- `SendError`
- `SseError`
- `SseMessage`
- `SseOpen`
- `SwapError`
- `TargetError`
- `Timeout`
- `ValidationFailed`
- `ValidationHalted`
- `ValidationValidate`
- `XhrAbort`
- `XhrLoadEnd`
- `XhrLoadStart`
- `XhrProgress`

## See Also

- [HTMX Attributes](HTMX-Attributes.md)
- [HTTP API](HTTP-API.md)
- [Component Development](Component-Development.md)