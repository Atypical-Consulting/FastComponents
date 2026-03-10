#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ConventionBasedRegistration](FastComponents.ConventionBasedRegistration.md 'FastComponents\.ConventionBasedRegistration')

## ConventionBasedRegistration\.MapHtmxComponentsByConvention\(this IEndpointRouteBuilder, Assembly\[\]\) Method

Automatically maps all HTMX components in the specified assemblies using conventions

```csharp
public static Microsoft.AspNetCore.Routing.IEndpointRouteBuilder MapHtmxComponentsByConvention(this Microsoft.AspNetCore.Routing.IEndpointRouteBuilder app, params System.Reflection.Assembly[] assemblies);
```
#### Parameters

<a name='FastComponents.ConventionBasedRegistration.MapHtmxComponentsByConvention(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,System.Reflection.Assembly[]).app'></a>

`app` [Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Routing.IEndpointRouteBuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')

The endpoint route builder

<a name='FastComponents.ConventionBasedRegistration.MapHtmxComponentsByConvention(thisMicrosoft.AspNetCore.Routing.IEndpointRouteBuilder,System.Reflection.Assembly[]).assemblies'></a>

`assemblies` [System\.Reflection\.Assembly](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.Assembly 'System\.Reflection\.Assembly')[\[\]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System\.Array')

Assemblies to scan for components \(defaults to calling assembly\)

#### Returns
[Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Routing.IEndpointRouteBuilder 'Microsoft\.AspNetCore\.Routing\.IEndpointRouteBuilder')  
The endpoint route builder for chaining