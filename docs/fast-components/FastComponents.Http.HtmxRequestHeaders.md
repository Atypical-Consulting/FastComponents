#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents\.Http](FastComponents.Http.md 'FastComponents\.Http')

## HtmxRequestHeaders Class

Provides access to HTMX request headers from an HTTP request\.

```csharp
public class HtmxRequestHeaders
```

Inheritance [System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object') &#129106; HtmxRequestHeaders

| Constructors | |
| :--- | :--- |
| [HtmxRequestHeaders\(HttpContext\)](FastComponents.Http.HtmxRequestHeaders.#ctor.md#FastComponents.Http.HtmxRequestHeaders.HtmxRequestHeaders(Microsoft.AspNetCore.Http.HttpContext) 'FastComponents\.Http\.HtmxRequestHeaders\.HtmxRequestHeaders\(Microsoft\.AspNetCore\.Http\.HttpContext\)') | Creates a new instance of HtmxRequestHeaders from the HTTP context |
| [HtmxRequestHeaders\(IHeaderDictionary\)](FastComponents.Http.HtmxRequestHeaders.#ctor.md#FastComponents.Http.HtmxRequestHeaders.HtmxRequestHeaders(Microsoft.AspNetCore.Http.IHeaderDictionary) 'FastComponents\.Http\.HtmxRequestHeaders\.HtmxRequestHeaders\(Microsoft\.AspNetCore\.Http\.IHeaderDictionary\)') | Creates a new instance of HtmxRequestHeaders from the request headers |

| Properties | |
| :--- | :--- |
| [CurrentUrl](FastComponents.Http.HtmxRequestHeaders.CurrentUrl.md 'FastComponents\.Http\.HtmxRequestHeaders\.CurrentUrl') | The current URL of the browser |
| [IsBoosted](FastComponents.Http.HtmxRequestHeaders.IsBoosted.md 'FastComponents\.Http\.HtmxRequestHeaders\.IsBoosted') | Indicates that the request is via an element using hx\-boost |
| [IsHistoryRestoreRequest](FastComponents.Http.HtmxRequestHeaders.IsHistoryRestoreRequest.md 'FastComponents\.Http\.HtmxRequestHeaders\.IsHistoryRestoreRequest') | True if the request is for history restoration after a miss in the local history cache |
| [IsHtmxRequest](FastComponents.Http.HtmxRequestHeaders.IsHtmxRequest.md 'FastComponents\.Http\.HtmxRequestHeaders\.IsHtmxRequest') | True if this is an HTMX request |
| [Prompt](FastComponents.Http.HtmxRequestHeaders.Prompt.md 'FastComponents\.Http\.HtmxRequestHeaders\.Prompt') | The user response to an hx\-prompt |
| [Target](FastComponents.Http.HtmxRequestHeaders.Target.md 'FastComponents\.Http\.HtmxRequestHeaders\.Target') | The id of the target element if it exists |
| [Trigger](FastComponents.Http.HtmxRequestHeaders.Trigger.md 'FastComponents\.Http\.HtmxRequestHeaders\.Trigger') | The id of the triggered element if it exists |
| [TriggerName](FastComponents.Http.HtmxRequestHeaders.TriggerName.md 'FastComponents\.Http\.HtmxRequestHeaders\.TriggerName') | The name of the triggered element if it exists |
