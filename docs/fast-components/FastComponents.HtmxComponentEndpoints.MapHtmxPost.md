#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[HtmxComponentEndpoints](FastComponents.HtmxComponentEndpoints.md 'FastComponents\.HtmxComponentEndpoints')

## HtmxComponentEndpoints\.MapHtmxPost Method

| Overloads | |
| :--- | :--- |
| [MapHtmxPost&lt;TComponent,TParameters&gt;\(this IEndpointRouteBuilder, string\)](FastComponents.HtmxComponentEndpoints.MapHtmxPost.md#FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string) 'FastComponents\.HtmxComponentEndpoints\.MapHtmxPost\<TComponent,TParameters\>\(this Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder, string\)') | Maps a POST endpoint for an HTMX component with parameters |
| [MapHtmxPost&lt;TComponent&gt;\(this IEndpointRouteBuilder, string\)](FastComponents.HtmxComponentEndpoints.MapHtmxPost.md#FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string) 'FastComponents\.HtmxComponentEndpoints\.MapHtmxPost\<TComponent\>\(this Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder, string\)') | Maps a POST endpoint for an HTMX component without parameters |

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string)'></a>

## HtmxComponentEndpoints\.MapHtmxPost\<TComponent,TParameters\>\(this IEndpointRouteBuilder, string\) Method

Maps a POST endpoint for an HTMX component with parameters

```csharp
public static Microsoft.AspNetCore.Builder.RouteHandlerBuilder MapHtmxPost<TComponent,TParameters>(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder endpoints, string pattern)
    where TComponent : FastComponents.HtmxComponentBase<TParameters>
    where TParameters : FastComponents.HtmxComponentParameters, new();
```
#### Type parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TComponent'></a>

`TComponent`

The component type to render

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TParameters'></a>

`TParameters`

The parameters type
#### Parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).endpoints'></a>

`endpoints` [Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Routing.IEndpointRouteBuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')

The endpoint route builder

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent,TParameters_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).pattern'></a>

`pattern` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

The route pattern

#### Returns
[Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Builder.RouteHandlerBuilder 'Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder')  
The route handler builder for further configuration

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string)'></a>

## HtmxComponentEndpoints\.MapHtmxPost\<TComponent\>\(this IEndpointRouteBuilder, string\) Method

Maps a POST endpoint for an HTMX component without parameters

```csharp
public static Microsoft.AspNetCore.Builder.RouteHandlerBuilder MapHtmxPost<TComponent>(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder endpoints, string pattern)
    where TComponent : FastComponents.HtmxComponentBase;
```
#### Type parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).TComponent'></a>

`TComponent`

The component type to render
#### Parameters

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).endpoints'></a>

`endpoints` [Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Routing.IEndpointRouteBuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')

The endpoint route builder

<a name='FastComponents.HtmxComponentEndpoints.MapHtmxPost_TComponent_(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string).pattern'></a>

`pattern` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

The route pattern

#### Returns
[Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Builder.RouteHandlerBuilder 'Microsoft\.AspNetCore\.Builder\.RouteHandlerBuilder')  
The route handler builder for further configuration