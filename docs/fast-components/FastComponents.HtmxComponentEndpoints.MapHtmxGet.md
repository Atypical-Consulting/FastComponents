#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[HtmxComponentEndpoints](FastComponents.HtmxComponentEndpoints.md 'FastComponents\.HtmxComponentEndpoints')

## HtmxComponentEndpoints\.MapHtmxGet Method

| Overloads | |
| :--- | :--- |
| [MapHtmxGet&lt;TComponent,TParameters&gt;\(this IEndpointRouteBuilder, string\)](FastComponents.HtmxComponentEndpoints.MapHtmxGet.md#FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string) 'FastComponents\.HtmxComponentEndpoints\.MapHtmxGet\<TComponent,TParameters\>\(this Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder, string\)') | Maps a GET endpoint for an HTMX component with parameters |
| [MapHtmxGet&lt;TComponent&gt;\(this IEndpointRouteBuilder, string\)](FastComponents.HtmxComponentEndpoints.MapHtmxGet.md#FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string) 'FastComponents\.HtmxComponentEndpoints\.MapHtmxGet\<TComponent\>\(this Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder, string\)') | Maps a GET endpoint for an HTMX component without parameters |

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string)'></a>

## HtmxComponentEndpoints\.MapHtmxGet\<TComponent,TParameters\>\(this IEndpointRouteBuilder, string\) Method

Maps a GET endpoint for an HTMX component with parameters

```csharp
public static Microsoft.AspNetCore.Builder.RouteHandlerBuilder MapHtmxGet<TComponent,TParameters>(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder endpoints, string pattern)
    where TComponent : FastComponents.HtmxComponentBase<TParameters>
    where TParameters : FastComponents.HtmxComponentParameters, new();
```
#### Type parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TComponent'></a>

`TComponent`

The component type to render

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TParameters'></a>

`TParameters`

The parameters type
#### Parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).endpoints'></a>

`endpoints` [Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Routing.IEndpointRouteBuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')

The endpoint route builder

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).pattern'></a>

`pattern` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

The route pattern

#### Returns
[Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Builder.RouteHandlerBuilder 'Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder')  
The route handler builder for further configuration

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string)'></a>

## HtmxComponentEndpoints\.MapHtmxGet\<TComponent\>\(this IEndpointRouteBuilder, string\) Method

Maps a GET endpoint for an HTMX component without parameters

```csharp
public static Microsoft.AspNetCore.Builder.RouteHandlerBuilder MapHtmxGet<TComponent>(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder endpoints, string pattern)
    where TComponent : FastComponents.HtmxComponentBase;
```
#### Type parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TComponent'></a>

`TComponent`

The component type to render
#### Parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).endpoints'></a>

`endpoints` [Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Routing.IEndpointRouteBuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')

The endpoint route builder

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxGet_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).pattern'></a>

`pattern` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

The route pattern

#### Returns
[Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Builder.RouteHandlerBuilder 'Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder')  
The route handler builder for further configuration