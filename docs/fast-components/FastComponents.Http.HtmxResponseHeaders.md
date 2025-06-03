#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents\.Http](FastComponents.Http.md 'FastComponents\.Http')

## HtmxResponseHeaders Class

Provides methods to set HTMX response headers on an HTTP response\.

```csharp
public class HtmxResponseHeaders
```

Inheritance [System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object') &#129106; HtmxResponseHeaders

| Constructors | |
| :--- | :--- |
| [HtmxResponseHeaders\(HttpContext\)](FastComponents.Http.HtmxResponseHeaders.#ctor.md#FastComponents.Http.HtmxResponseHeaders.HtmxResponseHeaders(Microsoft.AspNetCore.Http.HttpContext) 'FastComponents\.Http\.HtmxResponseHeaders\.HtmxResponseHeaders\(Microsoft\.AspNetCore\.Http\.HttpContext\)') | Creates a new instance of HtmxResponseHeaders from the HTTP context |
| [HtmxResponseHeaders\(IHeaderDictionary\)](FastComponents.Http.HtmxResponseHeaders.#ctor.md#FastComponents.Http.HtmxResponseHeaders.HtmxResponseHeaders(Microsoft.AspNetCore.Http.IHeaderDictionary) 'FastComponents\.Http\.HtmxResponseHeaders\.HtmxResponseHeaders\(Microsoft\.AspNetCore\.Http\.IHeaderDictionary\)') | Creates a new instance of HtmxResponseHeaders from the response headers |

| Methods | |
| :--- | :--- |
| [Location\(object\)](FastComponents.Http.HtmxResponseHeaders.Location.md#FastComponents.Http.HtmxResponseHeaders.Location(object) 'FastComponents\.Http\.HtmxResponseHeaders\.Location\(object\)') | Allows you to do a client\-side redirect with additional options |
| [Location\(string\)](FastComponents.Http.HtmxResponseHeaders.Location.md#FastComponents.Http.HtmxResponseHeaders.Location(string) 'FastComponents\.Http\.HtmxResponseHeaders\.Location\(string\)') | Allows you to do a client\-side redirect that does not do a full page reload |
| [PreventPushUrl\(\)](FastComponents.Http.HtmxResponseHeaders.PreventPushUrl().md 'FastComponents\.Http\.HtmxResponseHeaders\.PreventPushUrl\(\)') | Prevent pushing the url into the history stack |
| [PreventReplaceUrl\(\)](FastComponents.Http.HtmxResponseHeaders.PreventReplaceUrl().md 'FastComponents\.Http\.HtmxResponseHeaders\.PreventReplaceUrl\(\)') | Prevent replacing the current URL in the location bar |
| [PushUrl\(string\)](FastComponents.Http.HtmxResponseHeaders.PushUrl(string).md 'FastComponents\.Http\.HtmxResponseHeaders\.PushUrl\(string\)') | Pushes a new url into the history stack |
| [Redirect\(string\)](FastComponents.Http.HtmxResponseHeaders.Redirect(string).md 'FastComponents\.Http\.HtmxResponseHeaders\.Redirect\(string\)') | Can be used to do a client\-side redirect to a new location |
| [Refresh\(\)](FastComponents.Http.HtmxResponseHeaders.Refresh().md 'FastComponents\.Http\.HtmxResponseHeaders\.Refresh\(\)') | If set to "true" the client\-side will do a full refresh of the page |
| [ReplaceUrl\(string\)](FastComponents.Http.HtmxResponseHeaders.ReplaceUrl(string).md 'FastComponents\.Http\.HtmxResponseHeaders\.ReplaceUrl\(string\)') | Replaces the current URL in the location bar |
| [Reselect\(string\)](FastComponents.Http.HtmxResponseHeaders.Reselect(string).md 'FastComponents\.Http\.HtmxResponseHeaders\.Reselect\(string\)') | A CSS selector that allows you to choose which part of the response is used to be swapped in |
| [Reswap\(string\)](FastComponents.Http.HtmxResponseHeaders.Reswap(string).md 'FastComponents\.Http\.HtmxResponseHeaders\.Reswap\(string\)') | Allows you to specify how the response will be swapped |
| [Retarget\(string\)](FastComponents.Http.HtmxResponseHeaders.Retarget(string).md 'FastComponents\.Http\.HtmxResponseHeaders\.Retarget\(string\)') | A CSS selector that updates the target of the content update to a different element on the page |
| [Trigger\(string\)](FastComponents.Http.HtmxResponseHeaders.Trigger.md#FastComponents.Http.HtmxResponseHeaders.Trigger(string) 'FastComponents\.Http\.HtmxResponseHeaders\.Trigger\(string\)') | Allows you to trigger a client\-side event |
| [Trigger\(string\[\]\)](FastComponents.Http.HtmxResponseHeaders.Trigger.md#FastComponents.Http.HtmxResponseHeaders.Trigger(string[]) 'FastComponents\.Http\.HtmxResponseHeaders\.Trigger\(string\[\]\)') | Allows you to trigger multiple client\-side events |
| [TriggerAfterSettle\(string\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle.md#FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle(string) 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSettle\(string\)') | Allows you to trigger a client\-side event after the settle step |
| [TriggerAfterSettle\(string\[\]\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle.md#FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle(string[]) 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSettle\(string\[\]\)') | Allows you to trigger multiple client\-side events after the settle step |
| [TriggerAfterSettleWithDetails\(object\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettleWithDetails(object).md 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSettleWithDetails\(object\)') | Allows you to trigger client\-side events with details after the settle step |
| [TriggerAfterSwap\(string\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap.md#FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap(string) 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSwap\(string\)') | Allows you to trigger a client\-side event after the swap step |
| [TriggerAfterSwap\(string\[\]\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap.md#FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap(string[]) 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSwap\(string\[\]\)') | Allows you to trigger multiple client\-side events after the swap step |
| [TriggerAfterSwapWithDetails\(object\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwapWithDetails(object).md 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSwapWithDetails\(object\)') | Allows you to trigger client\-side events with details after the swap step |
| [TriggerWithDetails\(object\)](FastComponents.Http.HtmxResponseHeaders.TriggerWithDetails(object).md 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerWithDetails\(object\)') | Allows you to trigger client\-side events with details |
