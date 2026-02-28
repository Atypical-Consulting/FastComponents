#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ComponentHtmlResponseService](FastComponents.ComponentHtmlResponseService.md 'FastComponents\.ComponentHtmlResponseService')

## ComponentHtmlResponseService\.RenderAsHtmlContent\<TComponent\>\(Dictionary\<string,object\>\) Method

Renders a component as HTML and returns it as an HTTP content result\.

```csharp
public System.Threading.Tasks.Task<Microsoft.AspNetCore.Http.IResult> RenderAsHtmlContent<TComponent>(System.Collections.Generic.Dictionary<string,object?>? parameters=null)
    where TComponent : FastComponents.HtmxComponentBase;
```
#### Type parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderAsHtmlContent_TComponent_(System.Collections.Generic.Dictionary_string,object_).TComponent'></a>

`TComponent`

The type of the Blazor component to render\.
#### Parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderAsHtmlContent_TComponent_(System.Collections.Generic.Dictionary_string,object_).parameters'></a>

`parameters` [System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')

Optional parameters to pass to the component during rendering\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[Microsoft\.AspNetCore\.Http\.IResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iresult 'Microsoft\.AspNetCore\.Http\.IResult')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
An [Microsoft\.AspNetCore\.Http\.IResult](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.iresult 'Microsoft\.AspNetCore\.Http\.IResult') representing the HTTP content result of the rendered HTML\.