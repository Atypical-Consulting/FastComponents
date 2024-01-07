#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents.ClassNamesBuilder')

## ClassNamesBuilder.AddClass(string, Func<bool>) Method

Adds a class name to the builder if the result of the function is true.

```csharp
public FastComponents.ClassNamesBuilder AddClass(string value, System.Func<bool>? when);
```
#### Parameters

<a name='FastComponents.ClassNamesBuilder.AddClass(string,System.Func_bool_).value'></a>

`value` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

The class name to add.

<a name='FastComponents.ClassNamesBuilder.AddClass(string,System.Func_bool_).when'></a>

`when` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System.Func`1')

A function to check the condition.

#### Returns
[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents.ClassNamesBuilder')  
The current instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents.ClassNamesBuilder').