#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## IHxWsAttributes Interface

HTMX WebSocket attributes interface\.

```csharp
public interface IHxWsAttributes : FastComponents.IHxAttributes, FastComponents.IHxCoreAttributes, FastComponents.IHxAdditionalAttributes
```

Derived  
&#8627; [HtmxWsTag](FastComponents.HtmxWsTag.md 'FastComponents\.HtmxWsTag')

Implements [IHxAttributes](FastComponents.IHxAttributes.md 'FastComponents\.IHxAttributes'), [IHxCoreAttributes](FastComponents.IHxCoreAttributes.md 'FastComponents\.IHxCoreAttributes'), [IHxAdditionalAttributes](FastComponents.IHxAdditionalAttributes.md 'FastComponents\.IHxAdditionalAttributes')

| Properties | |
| :--- | :--- |
| [WsConnect](FastComponents.IHxWsAttributes.WsConnect.md 'FastComponents\.IHxWsAttributes\.WsConnect') | Establishes a WebSocket connection to the specified URL |
| [WsSend](FastComponents.IHxWsAttributes.WsSend.md 'FastComponents\.IHxWsAttributes\.WsSend') | Sends WebSocket messages based on events |
