#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[HtmxComponentEndpoints](FastComponents.HtmxComponentEndpoints.md 'FastComponents\.HtmxComponentEndpoints')

## HtmxComponentEndpoints\.MapHtmxPut\<TComponent,TParameters\>\(this IEndpointRouteBuilder, string\) Method

Maps a PUT endpoint for an HTMX component with parameters

```csharp
public static Microsoft.AspNetCore.Builder.RouteHandlerBuilder MapHtmxPut<TComponent,TParameters>(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder endpoints, string pattern)
    where TComponent : FastComponents.HtmxComponentBase<TParameters>
    where TParameters : FastComponents.HtmxComponentParameters, new();
```
#### Type parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPut_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TComponent'></a>

`TComponent`

The component type to render

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPut_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TParameters'></a>

`TParameters`

The parameters type
#### Parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPut_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).endpoints'></a>

`endpoints` [Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.routing.iendpointroutebuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')

The endpoint route builder

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPut_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).pattern'></a>

`pattern` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The route pattern

#### Returns
[Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.routehandlerbuilder 'Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder')  
The route handler builder for further configuration