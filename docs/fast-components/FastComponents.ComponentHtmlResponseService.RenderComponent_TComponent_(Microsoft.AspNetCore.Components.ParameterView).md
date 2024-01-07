#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ComponentHtmlResponseService](FastComponents.ComponentHtmlResponseService.md 'FastComponents.ComponentHtmlResponseService')

## ComponentHtmlResponseService.RenderComponent<TComponent>(ParameterView) Method

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

`parameters` [Microsoft.AspNetCore.Components.ParameterView](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.ParameterView 'Microsoft.AspNetCore.Components.ParameterView')

The parameters to pass to the component

#### Returns
[System.Threading.Tasks.Task&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task-1 'System.Threading.Tasks.Task`1')  
The rendered component as a string