#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents\.Http](FastComponents.Http.md 'FastComponents\.Http').[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')

## HtmxResponseHeaders\.TriggerAfterSwap Method

| Overloads | |
| :--- | :--- |
| [TriggerAfterSwap\(string\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap.md#FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap(string) 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSwap\(string\)') | Allows you to trigger a client\-side event after the swap step |
| [TriggerAfterSwap\(string\[\]\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap.md#FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap(string[]) 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSwap\(string\[\]\)') | Allows you to trigger multiple client\-side events after the swap step |

<a name='FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap(string)'></a>

## HtmxResponseHeaders\.TriggerAfterSwap\(string\) Method

Allows you to trigger a client\-side event after the swap step

```csharp
public FastComponents.Http.HtmxResponseHeaders TriggerAfterSwap(string eventName);
```
#### Parameters

<a name='FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap(string).eventName'></a>

`eventName` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

#### Returns
[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')

<a name='FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap(string[])'></a>

## HtmxResponseHeaders\.TriggerAfterSwap\(string\[\]\) Method

Allows you to trigger multiple client\-side events after the swap step

```csharp
public FastComponents.Http.HtmxResponseHeaders TriggerAfterSwap(params string[] eventNames);
```
#### Parameters

<a name='FastComponents.Http.HtmxResponseHeaders.TriggerAfterSwap(string[]).eventNames'></a>

`eventNames` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[\[\]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System\.Array')

#### Returns
[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')