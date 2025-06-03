#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents\.Events](FastComponents.Events.md 'FastComponents\.Events')

## HtmxEvents Class

Constants for HTMX JavaScript events

```csharp
public static class HtmxEvents
```

Inheritance [System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object') &#129106; HtmxEvents

| Fields | |
| :--- | :--- |
| [Abort](FastComponents.Events.HtmxEvents.Abort.md 'FastComponents\.Events\.HtmxEvents\.Abort') | Fired when an AJAX request is aborted |
| [AfterOnLoad](FastComponents.Events.HtmxEvents.AfterOnLoad.md 'FastComponents\.Events\.HtmxEvents\.AfterOnLoad') | Fired after an AJAX request has completed |
| [AfterProcessNode](FastComponents.Events.HtmxEvents.AfterProcessNode.md 'FastComponents\.Events\.HtmxEvents\.AfterProcessNode') | Fired after content has been processed by htmx |
| [AfterRequest](FastComponents.Events.HtmxEvents.AfterRequest.md 'FastComponents\.Events\.HtmxEvents\.AfterRequest') | Fired after an AJAX request has been made |
| [AfterSettle](FastComponents.Events.HtmxEvents.AfterSettle.md 'FastComponents\.Events\.HtmxEvents\.AfterSettle') | Fired after content has been settled |
| [AfterSwap](FastComponents.Events.HtmxEvents.AfterSwap.md 'FastComponents\.Events\.HtmxEvents\.AfterSwap') | Fired after content has been swapped |
| [BeforeCleanupElement](FastComponents.Events.HtmxEvents.BeforeCleanupElement.md 'FastComponents\.Events\.HtmxEvents\.BeforeCleanupElement') | Fired before htmx cleans up an element |
| [BeforeHistorySave](FastComponents.Events.HtmxEvents.BeforeHistorySave.md 'FastComponents\.Events\.HtmxEvents\.BeforeHistorySave') | Fired when the history cache is replaced |
| [BeforeOnLoad](FastComponents.Events.HtmxEvents.BeforeOnLoad.md 'FastComponents\.Events\.HtmxEvents\.BeforeOnLoad') | Fired before any content swapping occurs |
| [BeforeProcessNode](FastComponents.Events.HtmxEvents.BeforeProcessNode.md 'FastComponents\.Events\.HtmxEvents\.BeforeProcessNode') | Fired before htmx processes a node |
| [BeforeRequest](FastComponents.Events.HtmxEvents.BeforeRequest.md 'FastComponents\.Events\.HtmxEvents\.BeforeRequest') | Fired before an AJAX request is made |
| [BeforeSwap](FastComponents.Events.HtmxEvents.BeforeSwap.md 'FastComponents\.Events\.HtmxEvents\.BeforeSwap') | Fired before content is swapped |
| [BeforeTransition](FastComponents.Events.HtmxEvents.BeforeTransition.md 'FastComponents\.Events\.HtmxEvents\.BeforeTransition') | Fired before a new node is added to the DOM |
| [Cancel](FastComponents.Events.HtmxEvents.Cancel.md 'FastComponents\.Events\.HtmxEvents\.Cancel') | Fired when a request is cancelled |
| [ConfigRequest](FastComponents.Events.HtmxEvents.ConfigRequest.md 'FastComponents\.Events\.HtmxEvents\.ConfigRequest') | Fired when htmx is configured |
| [Confirm](FastComponents.Events.HtmxEvents.Confirm.md 'FastComponents\.Events\.HtmxEvents\.Confirm') | Fired when a user confirms an action |
| [HistoryCacheMiss](FastComponents.Events.HtmxEvents.HistoryCacheMiss.md 'FastComponents\.Events\.HtmxEvents\.HistoryCacheMiss') | Fired when the history cache is cleared |
| [HistoryCacheMissError](FastComponents.Events.HtmxEvents.HistoryCacheMissError.md 'FastComponents\.Events\.HtmxEvents\.HistoryCacheMissError') | Fired when the history cache is populated |
| [HistoryCacheMissLoad](FastComponents.Events.HtmxEvents.HistoryCacheMissLoad.md 'FastComponents\.Events\.HtmxEvents\.HistoryCacheMissLoad') | Fired when the history cache is loaded |
| [HistoryRestore](FastComponents.Events.HtmxEvents.HistoryRestore.md 'FastComponents\.Events\.HtmxEvents\.HistoryRestore') | Fired when history is restored |
| [Load](FastComponents.Events.HtmxEvents.Load.md 'FastComponents\.Events\.HtmxEvents\.Load') | Fired when htmx is loaded |
| [NoSseSourceError](FastComponents.Events.HtmxEvents.NoSseSourceError.md 'FastComponents\.Events\.HtmxEvents\.NoSseSourceError') | Fired when an out of band swap occurs |
| [OobAfterSwap](FastComponents.Events.HtmxEvents.OobAfterSwap.md 'FastComponents\.Events\.HtmxEvents\.OobAfterSwap') | Fired when htmx swaps out of band content |
| [OobBeforeSwap](FastComponents.Events.HtmxEvents.OobBeforeSwap.md 'FastComponents\.Events\.HtmxEvents\.OobBeforeSwap') | Fired before htmx swaps out of band content |
| [OobErrorNoTarget](FastComponents.Events.HtmxEvents.OobErrorNoTarget.md 'FastComponents\.Events\.HtmxEvents\.OobErrorNoTarget') | Fired when htmx handles an error swapping out of band content |
| [Prompt](FastComponents.Events.HtmxEvents.Prompt.md 'FastComponents\.Events\.HtmxEvents\.Prompt') | Fired when a prompt is shown |
| [PushedIntoHistory](FastComponents.Events.HtmxEvents.PushedIntoHistory.md 'FastComponents\.Events\.HtmxEvents\.PushedIntoHistory') | Fired after URL has been pushed to history |
| [ReplacedInHistory](FastComponents.Events.HtmxEvents.ReplacedInHistory.md 'FastComponents\.Events\.HtmxEvents\.ReplacedInHistory') | Fired after URL has been replaced in history |
| [ResponseError](FastComponents.Events.HtmxEvents.ResponseError.md 'FastComponents\.Events\.HtmxEvents\.ResponseError') | Fired when a response error occurs |
| [SendError](FastComponents.Events.HtmxEvents.SendError.md 'FastComponents\.Events\.HtmxEvents\.SendError') | Fired when an error sending an AJAX request occurs |
| [SseError](FastComponents.Events.HtmxEvents.SseError.md 'FastComponents\.Events\.HtmxEvents\.SseError') | Fired when a server sent event error occurs |
| [SseMessage](FastComponents.Events.HtmxEvents.SseMessage.md 'FastComponents\.Events\.HtmxEvents\.SseMessage') | Fired when a server sent event is received |
| [SseOpen](FastComponents.Events.HtmxEvents.SseOpen.md 'FastComponents\.Events\.HtmxEvents\.SseOpen') | Fired when a server sent event connection is opened |
| [SwapError](FastComponents.Events.HtmxEvents.SwapError.md 'FastComponents\.Events\.HtmxEvents\.SwapError') | Fired when content is swapped into the DOM |
| [TargetError](FastComponents.Events.HtmxEvents.TargetError.md 'FastComponents\.Events\.HtmxEvents\.TargetError') | Fired when a target error occurs |
| [Timeout](FastComponents.Events.HtmxEvents.Timeout.md 'FastComponents\.Events\.HtmxEvents\.Timeout') | Fired when a request timeout occurs |
| [ValidationFailed](FastComponents.Events.HtmxEvents.ValidationFailed.md 'FastComponents\.Events\.HtmxEvents\.ValidationFailed') | Fired when validation fails |
| [ValidationHalted](FastComponents.Events.HtmxEvents.ValidationHalted.md 'FastComponents\.Events\.HtmxEvents\.ValidationHalted') | Fired when validation is halted |
| [ValidationValidate](FastComponents.Events.HtmxEvents.ValidationValidate.md 'FastComponents\.Events\.HtmxEvents\.ValidationValidate') | Fired when validation fails |
| [XhrAbort](FastComponents.Events.HtmxEvents.XhrAbort.md 'FastComponents\.Events\.HtmxEvents\.XhrAbort') | Fired when an XHR request is about to be made |
| [XhrLoadEnd](FastComponents.Events.HtmxEvents.XhrLoadEnd.md 'FastComponents\.Events\.HtmxEvents\.XhrLoadEnd') | Fired when an XHR request has loaded |
| [XhrLoadStart](FastComponents.Events.HtmxEvents.XhrLoadStart.md 'FastComponents\.Events\.HtmxEvents\.XhrLoadStart') | Fired when an XHR request starts loading |
| [XhrProgress](FastComponents.Events.HtmxEvents.XhrProgress.md 'FastComponents\.Events\.HtmxEvents\.XhrProgress') | Fired during XHR request progress |
