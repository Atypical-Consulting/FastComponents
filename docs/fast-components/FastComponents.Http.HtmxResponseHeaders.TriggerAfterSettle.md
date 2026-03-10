#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents\.Http](FastComponents.Http.md 'FastComponents\.Http').[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')

## HtmxResponseHeaders\.TriggerAfterSettle Method

| Overloads | |
| :--- | :--- |
| [TriggerAfterSettle\(string\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle.md#FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle(string) 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSettle\(string\)') | Allows you to trigger a client\-side event after the settle step |
| [TriggerAfterSettle\(string\[\]\)](FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle.md#FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle(string[]) 'FastComponents\.Http\.HtmxResponseHeaders\.TriggerAfterSettle\(string\[\]\)') | Allows you to trigger multiple client\-side events after the settle step |

<a name='FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle(string)'></a>

## HtmxResponseHeaders\.TriggerAfterSettle\(string\) Method

Allows you to trigger a client\-side event after the settle step

```csharp
public FastComponents.Http.HtmxResponseHeaders TriggerAfterSettle(string eventName);
```
#### Parameters

<a name='FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle(string).eventName'></a>

`eventName` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

#### Returns
[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')

<a name='FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle(string[])'></a>

## HtmxResponseHeaders\.TriggerAfterSettle\(string\[\]\) Method

Allows you to trigger multiple client\-side events after the settle step

```csharp
public FastComponents.Http.HtmxResponseHeaders TriggerAfterSettle(params string[] eventNames);
```
#### Parameters

<a name='FastComponents.Http.HtmxResponseHeaders.TriggerAfterSettle(string[]).eventNames'></a>

`eventNames` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[\[\]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System\.Array')

#### Returns
[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')