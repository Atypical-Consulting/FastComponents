#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## IHxSseAttributes Interface

HTMX SSE \(Server\-Sent Events\) attributes interface\.

```csharp
public interface IHxSseAttributes : FastComponents.IHxAttributes, FastComponents.IHxCoreAttributes, FastComponents.IHxAdditionalAttributes
```

Derived  
&#8627; [HtmxSseTag](FastComponents.HtmxSseTag.md 'FastComponents\.HtmxSseTag')

Implements [IHxAttributes](FastComponents.IHxAttributes.md 'FastComponents\.IHxAttributes'), [IHxCoreAttributes](FastComponents.IHxCoreAttributes.md 'FastComponents\.IHxCoreAttributes'), [IHxAdditionalAttributes](FastComponents.IHxAdditionalAttributes.md 'FastComponents\.IHxAdditionalAttributes')

| Properties | |
| :--- | :--- |
| [SseConnect](FastComponents.IHxSseAttributes.SseConnect.md 'FastComponents\.IHxSseAttributes\.SseConnect') | Establishes a Server\-Sent Events connection to the specified URL |
| [SseSwap](FastComponents.IHxSseAttributes.SseSwap.md 'FastComponents\.IHxSseAttributes\.SseSwap') | Swaps content based on SSE messages with the given event name |
