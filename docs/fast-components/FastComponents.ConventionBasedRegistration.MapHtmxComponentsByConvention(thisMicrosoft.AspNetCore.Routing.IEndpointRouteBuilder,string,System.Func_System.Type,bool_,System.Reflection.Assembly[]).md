#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ConventionBasedRegistration](FastComponents.ConventionBasedRegistration.md 'FastComponents\.ConventionBasedRegistration')

## ConventionBasedRegistration\.MapHtmxComponentsByConvention\(this IEndpointRouteBuilder, string, Func\<Type,bool\>, Assembly\[\]\) Method

Automatically maps all HTMX components in the specified assemblies using conventions

```csharp
public static Microsoft.AspNetCore.Routing.IEndpointRouteBuilder MapHtmxComponentsByConvention(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder app, string routePrefix="/htmx", System.Func<System.Type,bool>? predicate=null, params System.Reflection.Assembly[] assemblies);
```
#### Parameters

<a name='FastComponents.ConventionBasedRegistration.MapHtmxComponentsByConvention(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string,System.Func_System.Type,bool_,System.Reflection.Assembly[]).app'></a>

`app` [Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.routing.iendpointroutebuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')

The endpoint route builder

<a name='FastComponents.ConventionBasedRegistration.MapHtmxComponentsByConvention(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string,System.Func_System.Type,bool_,System.Reflection.Assembly[]).routePrefix'></a>

`routePrefix` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The route prefix for convention\-generated routes \(default: "/htmx"\)

<a name='FastComponents.ConventionBasedRegistration.MapHtmxComponentsByConvention(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string,System.Func_System.Type,bool_,System.Reflection.Assembly[]).predicate'></a>

`predicate` [System\.Func&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')[,](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')

Optional filter to select which component types to register

<a name='FastComponents.ConventionBasedRegistration.MapHtmxComponentsByConvention(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,string,System.Func_System.Type,bool_,System.Reflection.Assembly[]).assemblies'></a>

`assemblies` [System\.Reflection\.Assembly](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly 'System\.Reflection\.Assembly')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

Assemblies to scan for components \(defaults to entry assembly\)

#### Returns
[Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.routing.iendpointroutebuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')  
The endpoint route builder for chaining