#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ComponentHtmlResponseService](FastComponents.ComponentHtmlResponseService.md 'FastComponents\.ComponentHtmlResponseService')

## ComponentHtmlResponseService\.RenderComponent Method

| Overloads | |
| :--- | :--- |
| [RenderComponent&lt;TComponent&gt;\(ParameterView\)](FastComponents.ComponentHtmlResponseService.RenderComponent.md#FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(Microsoft.AspNetCore.Components.ParameterView) 'FastComponents\.ComponentHtmlResponseService\.RenderComponent\<TComponent\>\(Microsoft\.AspNetCore\.Components\.ParameterView\)') | Use the default dispatcher to invoke actions in the context of the  static HTML renderer and return as a string |
| [RenderComponent&lt;TComponent&gt;\(Dictionary&lt;string,object&gt;\)](FastComponents.ComponentHtmlResponseService.RenderComponent.md#FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(System.Collections.Generic.Dictionary_string,object_) 'FastComponents\.ComponentHtmlResponseService\.RenderComponent\<TComponent\>\(System\.Collections\.Generic\.Dictionary\<string,object\>\)') | Renders a component T |

<a name='FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(Microsoft.AspNetCore.Components.ParameterView)'></a>

## ComponentHtmlResponseService\.RenderComponent\<TComponent\>\(ParameterView\) Method

Use the default dispatcher to invoke actions in the context of the 
static HTML renderer and return as a string

```csharp
private System.Threading.Tasks.Task<string> RenderComponent<TComponent>(Microsoft.AspNetCore.Components.ParameterView parameters)
    where TComponent : FastComponents.HtmxComponentBase;
```
#### Type parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(Microsoft.AspNetCore.Components.ParameterView).TComponent'></a>

`TComponent`

The component to render
#### Parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(Microsoft.AspNetCore.Components.ParameterView).parameters'></a>

`parameters` [Microsoft\.AspNetCore\.Components\.ParameterView](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.components.parameterview 'Microsoft\.AspNetCore\.Components\.ParameterView')

The parameters to pass to the component

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
The rendered component as a string

<a name='FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(System.Collections.Generic.Dictionary_string,object_)'></a>

## ComponentHtmlResponseService\.RenderComponent\<TComponent\>\(Dictionary\<string,object\>\) Method

Renders a component T

```csharp
public System.Threading.Tasks.Task<string> RenderComponent<TComponent>(System.Collections.Generic.Dictionary<string,object?>? dictionary=null)
    where TComponent : FastComponents.HtmxComponentBase;
```
#### Type parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(System.Collections.Generic.Dictionary_string,object_).TComponent'></a>

`TComponent`

The component to render
#### Parameters

<a name='FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(System.Collections.Generic.Dictionary_string,object_).dictionary'></a>

`dictionary` [System\.Collections\.Generic\.Dictionary&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[,](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')[System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2 'System\.Collections\.Generic\.Dictionary\`2')

The dictionary of parameters to pass to the component

#### Returns
[System\.Threading\.Tasks\.Task&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')[System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1 'System\.Threading\.Tasks\.Task\`1')  
The rendered component as a string