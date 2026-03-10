#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ComponentHtmlResponseService](FastComponents.ComponentHtmlResponseService.md 'FastComponents\.ComponentHtmlResponseService')

## ComponentHtmlResponseService\.RenderAsHtmlContentAsync\<TComponent\>\(Dictionary\<string,object\>\) Method

Renders a component as HTML and returns it as an HTTP content result\.

```csharp
public System.Threading.Tasks.Task<Microsoft.AspNetCore.Http.IResult> RenderAsHtmlContentAsync<TComponent>(System.Collections.Generic.Dictionary<string,object?>? parameters=null)
    where TComponent : FastComponents.HtmxComponentBase;
```
#### Type parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderAsHtmlContentAsync_TComponent_(System.Collections.Generic.Dictionary_string,object_).TComponent'></a>

`TComponent`

The type of the Blazor component to render\.
#### Parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderAsHtmlContentAsync_TComponent_(System.Collections.Generic.Dictionary_string,object_).parameters'></a>

`parameters` [System\.Collections\.Generic\.Dictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')

Optional parameters to pass to the component during rendering\.

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System\.Threading\.Tasks\.Task\`1')[Microsoft\.AspNetCore\.Http\.IResult](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Http.IResult 'Microsoft\.AspNetCore\.Http\.IResult')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System\.Threading\.Tasks\.Task\`1')  
An [Microsoft\.AspNetCore\.Http\.IResult](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Http.IResult 'Microsoft\.AspNetCore\.Http\.IResult') representing the HTTP content result of the rendered HTML\.