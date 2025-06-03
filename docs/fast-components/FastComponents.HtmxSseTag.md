#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## HtmxSseTag Class

HTMX component that supports Server\-Sent Events \(SSE\) attributes\.

```csharp
public class HtmxSseTag : FastComponents.HtmxComponentBase, FastComponents.IHxSseAttributes, FastComponents.IHxAttributes, FastComponents.IHxCoreAttributes, FastComponents.IHxAdditionalAttributes
```

Inheritance [System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object') &#129106; [Microsoft\.AspNetCore\.Components\.ComponentBase](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.ComponentBase 'Microsoft\.AspNetCore\.Components\.ComponentBase') &#129106; [HtmxComponentBase](FastComponents.HtmxComponentBase.md 'FastComponents\.HtmxComponentBase') &#129106; HtmxSseTag

Implements [IHxSseAttributes](FastComponents.IHxSseAttributes.md 'FastComponents\.IHxSseAttributes'), [IHxAttributes](FastComponents.IHxAttributes.md 'FastComponents\.IHxAttributes'), [IHxCoreAttributes](FastComponents.IHxCoreAttributes.md 'FastComponents\.IHxCoreAttributes'), [IHxAdditionalAttributes](FastComponents.IHxAdditionalAttributes.md 'FastComponents\.IHxAdditionalAttributes')

| Properties | |
| :--- | :--- |
| [ChildContent](FastComponents.HtmxSseTag.ChildContent.md 'FastComponents\.HtmxSseTag\.ChildContent') | Gets or sets the child content to be rendered inside the tag\. |
| [SseConnect](FastComponents.HtmxSseTag.SseConnect.md 'FastComponents\.HtmxSseTag\.SseConnect') | Establishes a Server\-Sent Events connection to the specified URL |
| [SseSwap](FastComponents.HtmxSseTag.SseSwap.md 'FastComponents\.HtmxSseTag\.SseSwap') | Swaps content based on SSE messages with the given event name |

| Methods | |
| :--- | :--- |
| [BuildRenderTree\(RenderTreeBuilder\)](FastComponents.HtmxSseTag.BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder).md 'FastComponents\.HtmxSseTag\.BuildRenderTree\(Microsoft\.AspNetCore\.Components\.Rendering\.RenderTreeBuilder\)') | Renders the component to the supplied [Microsoft\.AspNetCore\.Components\.Rendering\.RenderTreeBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder 'Microsoft\.AspNetCore\.Components\.Rendering\.RenderTreeBuilder')\. |
