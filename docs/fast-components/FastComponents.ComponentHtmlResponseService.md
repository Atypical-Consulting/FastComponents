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
| [RenderAsHtmlContentAsync&lt;TComponent&gt;\(Dictionary&lt;string,object&gt;\)](FastComponents.ComponentHtmlResponseService.RenderAsHtmlContentAsync_TComponent_(System.Collections.Generic.Dictionary_string,object_).md 'FastComponents\.ComponentHtmlResponseService\.RenderAsHtmlContentAsync\<TComponent\>\(System\.Collections\.Generic\.Dictionary\<string,object\>\)') | Renders a component as HTML and returns it as an HTTP content result\. |
| [RenderComponentAsync&lt;TComponent&gt;\(ParameterView\)](FastComponents.ComponentHtmlResponseService.RenderComponentAsync.md#FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(Microsoft.AspNetCore.Components.ParameterView) 'FastComponents\.ComponentHtmlResponseService\.RenderComponentAsync\<TComponent\>\(Microsoft\.AspNetCore\.Components\.ParameterView\)') | Use the default dispatcher to invoke actions in the context of the  static HTML renderer and return as a string |
| [RenderComponentAsync&lt;TComponent&gt;\(Dictionary&lt;string,object&gt;\)](FastComponents.ComponentHtmlResponseService.RenderComponentAsync.md#FastComponents.ComponentHtmlResponseService.RenderComponentAsync_TComponent_(System.Collections.Generic.Dictionary_string,object_) 'FastComponents\.ComponentHtmlResponseService\.RenderComponentAsync\<TComponent\>\(System\.Collections\.Generic\.Dictionary\<string,object\>\)') | Renders a component T |
