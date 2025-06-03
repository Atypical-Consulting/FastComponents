#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[HtmxComponentEndpoints](FastComponents.HtmxComponentEndpoints.md 'FastComponents\.HtmxComponentEndpoints')

## HtmxComponentEndpoints\.MapHtmxDelete\<TComponent,TParameters\>\(this IEndpointRouteBuilder, string\) Method

Maps a DELETE endpoint for an HTMX component with parameters

```csharp
public static Microsoft.AspNetCore.Builder.RouteHandlerBuilder MapHtmxDelete<TComponent,TParameters>(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder endpoints, string pattern)
    where TComponent : FastComponents.HtmxComponentBase<TParameters>
    where TParameters : FastComponents.HtmxComponentParameters, new();
```
#### Type parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxDelete_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TComponent'></a>

`TComponent`

The component type to render

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxDelete_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TParameters'></a>

`TParameters`

The parameters type
#### Parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxDelete_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).endpoints'></a>

`endpoints` [Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Routing.IEndpointRouteBuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')

The endpoint route builder

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxDelete_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).pattern'></a>

`pattern` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

The route pattern

#### Returns
[Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Builder.RouteHandlerBuilder 'Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder')  
The route handler builder for further configuration