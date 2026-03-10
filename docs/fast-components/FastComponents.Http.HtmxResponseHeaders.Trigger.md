#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents\.Http](FastComponents.Http.md 'FastComponents\.Http').[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')

## HtmxResponseHeaders\.Trigger Method

| Overloads | |
| :--- | :--- |
| [Trigger\(string\)](FastComponents.Http.HtmxResponseHeaders.Trigger.md#FastComponents.Http.HtmxResponseHeaders.Trigger(string) 'FastComponents\.Http\.HtmxResponseHeaders\.Trigger\(string\)') | Allows you to trigger a client\-side event |
| [Trigger\(string\[\]\)](FastComponents.Http.HtmxResponseHeaders.Trigger.md#FastComponents.Http.HtmxResponseHeaders.Trigger(string[]) 'FastComponents\.Http\.HtmxResponseHeaders\.Trigger\(string\[\]\)') | Allows you to trigger multiple client\-side events |

<a name='FastComponents.Http.HtmxResponseHeaders.Trigger(string)'></a>

## HtmxResponseHeaders\.Trigger\(string\) Method

Allows you to trigger a client\-side event

```csharp
public FastComponents.Http.HtmxResponseHeaders Trigger(string eventName);
```
#### Parameters

<a name='FastComponents.Http.HtmxResponseHeaders.Trigger(string).eventName'></a>

`eventName` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

#### Returns
[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')

<a name='FastComponents.Http.HtmxResponseHeaders.Trigger(string[])'></a>

## HtmxResponseHeaders\.Trigger\(string\[\]\) Method

Allows you to trigger multiple client\-side events

```csharp
public FastComponents.Http.HtmxResponseHeaders Trigger(params string[] eventNames);
```
#### Parameters

<a name='FastComponents.Http.HtmxResponseHeaders.Trigger(string[]).eventNames'></a>

`eventNames` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[\[\]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System\.Array')

#### Returns
[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')