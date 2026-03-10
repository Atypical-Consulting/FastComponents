#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[SimplifiedExtensions](FastComponents.SimplifiedExtensions.md 'FastComponents\.SimplifiedExtensions')

## SimplifiedExtensions\.UseFastComponentsAuto\(this WebApplication, string, Func\<Type,bool\>, Assembly\[\]\) Method

Maps all HTMX components automatically using conventions\.
One line replaces all the manual MapHtmx\* calls\.

```csharp
public static Microsoft.AspNetCore.Builder.WebApplication UseFastComponentsAuto(this Microsoft.AspNetCore.Builder.WebApplication app, string routePrefix="/htmx", System.Func<System.Type,bool>? predicate=null, params System.Reflection.Assembly[] assemblies);
```
#### Parameters

<a name='FastComponents.SimplifiedExtensions.UseFastComponentsAuto(thisMicrosoft.AspNetCore.Builder.WebApplication,string,System.Func_System.Type,bool_,System.Reflection.Assembly[]).app'></a>

`app` [Microsoft\.AspNetCore\.Builder\.WebApplication](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication 'Microsoft\.AspNetCore\.Builder\.WebApplication')

The web application

<a name='FastComponents.SimplifiedExtensions.UseFastComponentsAuto(thisMicrosoft.AspNetCore.Builder.WebApplication,string,System.Func_System.Type,bool_,System.Reflection.Assembly[]).routePrefix'></a>

`routePrefix` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

The route prefix for convention\-generated routes \(default: "/htmx"\)

<a name='FastComponents.SimplifiedExtensions.UseFastComponentsAuto(thisMicrosoft.AspNetCore.Builder.WebApplication,string,System.Func_System.Type,bool_,System.Reflection.Assembly[]).predicate'></a>

`predicate` [System\.Func&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Type](https://learn.microsoft.com/en-us/dotnet/api/system.type 'System\.Type')[,](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')[System\.Boolean](https://learn.microsoft.com/en-us/dotnet/api/system.boolean 'System\.Boolean')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.func-2 'System\.Func\`2')

Optional filter to select which component types to register

<a name='FastComponents.SimplifiedExtensions.UseFastComponentsAuto(thisMicrosoft.AspNetCore.Builder.WebApplication,string,System.Func_System.Type,bool_,System.Reflection.Assembly[]).assemblies'></a>

`assemblies` [System\.Reflection\.Assembly](https://learn.microsoft.com/en-us/dotnet/api/system.reflection.assembly 'System\.Reflection\.Assembly')[\[\]](https://learn.microsoft.com/en-us/dotnet/api/system.array 'System\.Array')

Assemblies to scan \(defaults to entry assembly\)

#### Returns
[Microsoft\.AspNetCore\.Builder\.WebApplication](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication 'Microsoft\.AspNetCore\.Builder\.WebApplication')  
The web application for chaining