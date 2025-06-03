#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ComponentHtmlResponseService](FastComponents.ComponentHtmlResponseService.md 'FastComponents\.ComponentHtmlResponseService')

## ComponentHtmlResponseService\.RenderComponentAsync Method

| Overloads | |
| :--- | :--- |
| [RenderComponentAsync&lt;TComponent&gt;\(ParameterView\)](FastComponents.ComponentHtmlResponseService.RenderComponentAsync.md#FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(Microsoft.AspNetCore.Components.ParameterView) 'FastComponents\.ComponentHtmlResponseService\.RenderComponentAsync\<TComponent\>\(Microsoft\.AspNetCore\.Components\.ParameterView\)') | Use the default dispatcher to invoke actions in the context of the  static HTML renderer and return as a string |
| [RenderComponentAsync&lt;TComponent&gt;\(Dictionary&lt;string,object&gt;\)](FastComponents.ComponentHtmlResponseService.RenderComponentAsync.md#FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(System.Collections.Generic.Dictionary_string,object_) 'FastComponents\.ComponentHtmlResponseService\.RenderComponentAsync\<TComponent\>\(System\.Collections\.Generic\.Dictionary\<string,object\>\)') | Renders a component T |

<a name='FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(Microsoft.AspNetCore.Components.ParameterView)'></a>

## ComponentHtmlResponseService\.RenderComponentAsync\<TComponent\>\(ParameterView\) Method

Use the default dispatcher to invoke actions in the context of the 
static HTML renderer and return as a string

```csharp
private System.Threading.Tasks.Task<string> RenderComponentAsync<TComponent>(Microsoft.AspNetCore.Components.ParameterView parameters)
    where TComponent : FastComponents.HtmxComponentBase;
```
#### Type parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(Microsoft.AspNetCore.Components.ParameterView).TComponent'></a>

`TComponent`

The component to render
#### Parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(Microsoft.AspNetCore.Components.ParameterView).parameters'></a>

`parameters` [Microsoft\.AspNetCore\.Components\.ParameterView](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.ParameterView 'Microsoft\.AspNetCore\.Components\.ParameterView')

The parameters to pass to the component

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System\.Threading\.Tasks\.Task\`1')[System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System\.Threading\.Tasks\.Task\`1')  
The rendered component as a string

<a name='FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(System.Collections.Generic.Dictionary_string,object_)'></a>

## ComponentHtmlResponseService\.RenderComponentAsync\<TComponent\>\(Dictionary\<string,object\>\) Method

Renders a component T

```csharp
public System.Threading.Tasks.Task<string> RenderComponentAsync<TComponent>(System.Collections.Generic.Dictionary<string,object?>? dictionary=null)
    where TComponent : FastComponents.HtmxComponentBase;
```
#### Type parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(System.Collections.Generic.Dictionary_string,object_).TComponent'></a>

`TComponent`

The component to render
#### Parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(System.Collections.Generic.Dictionary_string,object_).dictionary'></a>

`dictionary` [System\.Collections\.Generic\.Dictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')

The dictionary of parameters to pass to the component

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System\.Threading\.Tasks\.Task\`1')[System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System\.Threading\.Tasks\.Task\`1')  
The rendered component as a string