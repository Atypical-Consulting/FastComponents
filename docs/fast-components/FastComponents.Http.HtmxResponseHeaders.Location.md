#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents\.Http](FastComponents.Http.md 'FastComponents\.Http').[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')

## HtmxResponseHeaders\.Location Method

| Overloads | |
| :--- | :--- |
| [Location\(object\)](FastComponents.Http.HtmxResponseHeaders.Location.md#FastComponents.Http.HtmxResponseHeaders.Location(object) 'FastComponents\.Http\.HtmxResponseHeaders\.Location\(object\)') | Allows you to do a client\-side redirect with additional options |
| [Location\(string\)](FastComponents.Http.HtmxResponseHeaders.Location.md#FastComponents.Http.HtmxResponseHeaders.Location(string) 'FastComponents\.Http\.HtmxResponseHeaders\.Location\(string\)') | Allows you to do a client\-side redirect that does not do a full page reload |

<a name='FastComponents.Http.HtmxResponseHeaders.Location(object)'></a>

## HtmxResponseHeaders\.Location\(object\) Method

Allows you to do a client\-side redirect with additional options

```csharp
public FastComponents.Http.HtmxResponseHeaders Location(object locationData);
```
#### Parameters

<a name='FastComponents.Http.HtmxResponseHeaders.Location(object).locationData'></a>

`locationData` [System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object')

#### Returns
[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')

<a name='FastComponents.Http.HtmxResponseHeaders.Location(string)'></a>

## HtmxResponseHeaders\.Location\(string\) Method

Allows you to do a client\-side redirect that does not do a full page reload

```csharp
public FastComponents.Http.HtmxResponseHeaders Location(string url);
```
#### Parameters

<a name='FastComponents.Http.HtmxResponseHeaders.Location(string).url'></a>

`url` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

#### Returns
[HtmxResponseHeaders](FastComponents.Http.HtmxResponseHeaders.md 'FastComponents\.Http\.HtmxResponseHeaders')