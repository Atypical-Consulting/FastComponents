#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ComponentHtmlResponseService](FastComponents.ComponentHtmlResponseService.md 'FastComponents.ComponentHtmlResponseService')

## ComponentHtmlResponseService.RenderComponent<TComponent>(Dictionary<string,object>) Method

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

`dictionary` [System.Collections.Generic.Dictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')

The dictionary of parameters to pass to the component

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The rendered component as a string