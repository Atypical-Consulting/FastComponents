#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## ComponentHtmlResponseService Class

Service responsible for rendering components as HTML and returning them as HTTP responses\.

```csharp
public class ComponentHtmlResponseService
```

Inheritance [System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object') &#129106; ComponentHtmlResponseService

| Constructors | |
| :--- | :--- |
| [ComponentHtmlResponseService\(HtmlRenderer\)](FastComponents.ComponentHtmlResponseService.ComponentHtmlResponseService(Microsoft.AspNetCore.Components.Web.HtmlRenderer).md 'FastComponents\.ComponentHtmlResponseService\.ComponentHtmlResponseService\(Microsoft\.AspNetCore\.Components\.Web\.HtmlRenderer\)') | Service responsible for rendering components as HTML and returning them as HTTP responses\. |

| Methods | |
| :--- | :--- |
| [RenderAsHtmlContent&lt;TComponent&gt;\(Dictionary&lt;string,object&gt;\)](FastComponents.ComponentHtmlResponseService.RenderAsHtmlContent_TComponent_(System.Collections.Generic.Dictionary_string,object_).md 'FastComponents\.ComponentHtmlResponseService\.RenderAsHtmlContent\<TComponent\>\(System\.Collections\.Generic\.Dictionary\<string,object\>\)') | Renders a component as HTML and returns it as an HTTP content result\. |
| [RenderComponent&lt;TComponent&gt;\(ParameterView\)](FastComponents.ComponentHtmlResponseService.RenderComponent.md#FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(Microsoft.AspNetCore.Components.ParameterView) 'FastComponents\.ComponentHtmlResponseService\.RenderComponent\<TComponent\>\(Microsoft\.AspNetCore\.Components\.ParameterView\)') | Use the default dispatcher to invoke actions in the context of the  static HTML renderer and return as a string |
| [RenderComponent&lt;TComponent&gt;\(Dictionary&lt;string,object&gt;\)](FastComponents.ComponentHtmlResponseService.RenderComponent.md#FastComponents.ComponentHtmlResponseService.RenderComponent_TComponent_(System.Collections.Generic.Dictionary_string,object_) 'FastComponents\.ComponentHtmlResponseService\.RenderComponent\<TComponent\>\(System\.Collections\.Generic\.Dictionary\<string,object\>\)') | Renders a component T |
