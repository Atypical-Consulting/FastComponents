#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## HtmxComponentParameters Class

Base class for HTMX component parameters

```csharp
public abstract record HtmxComponentParameters : System.IEquatable<FastComponents.HtmxComponentParameters>
```

Inheritance [System\.Object](https://learn.microsoft.com/en-us/dotnet/api/system.object 'System\.Object') &#129106; HtmxComponentParameters

Implements [System\.IEquatable&lt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')[HtmxComponentParameters](FastComponents.HtmxComponentParameters.md 'FastComponents\.HtmxComponentParameters')[&gt;](https://learn.microsoft.com/en-us/dotnet/api/system.iequatable-1 'System\.IEquatable\`1')

| Methods | |
| :--- | :--- |
| [BindFromQuery\(IQueryCollection\)](FastComponents.HtmxComponentParameters.BindFromQuery(Microsoft.AspNetCore.Http.IQueryCollection).md 'FastComponents\.HtmxComponentParameters\.BindFromQuery\(Microsoft\.AspNetCore\.Http\.IQueryCollection\)') | Creates a new instance of parameters with values bound from the query collection |
| [BuildQueryString\(\)](FastComponents.HtmxComponentParameters.BuildQueryString().md 'FastComponents\.HtmxComponentParameters\.BuildQueryString\(\)') | Builds the query string from parameters\. Override this method to provide custom query string generation\. |
| [GetQueryInt\(IQueryCollection, string\)](FastComponents.HtmxComponentParameters.GetQueryInt(Microsoft.AspNetCore.Http.IQueryCollection,string).md 'FastComponents\.HtmxComponentParameters\.GetQueryInt\(Microsoft\.AspNetCore\.Http\.IQueryCollection, string\)') | Helper method to get an int value from query collection |
| [GetQueryValue\(IQueryCollection, string\)](FastComponents.HtmxComponentParameters.GetQueryValue(Microsoft.AspNetCore.Http.IQueryCollection,string).md 'FastComponents\.HtmxComponentParameters\.GetQueryValue\(Microsoft\.AspNetCore\.Http\.IQueryCollection, string\)') | Helper method to get a string value from query collection |
| [ToComponentUrl\(string\)](FastComponents.HtmxComponentParameters.ToComponentUrl(string).md 'FastComponents\.HtmxComponentParameters\.ToComponentUrl\(string\)') | Converts the parameters to a component URL with query string |
