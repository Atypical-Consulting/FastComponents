#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')

## ClassNamesBuilder\.AddClass Method

| Overloads | |
| :--- | :--- |
| [AddClass\(string, bool\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string,bool) 'FastComponents\.ClassNamesBuilder\.AddClass\(string, bool\)') | Adds a class name to the builder if the condition is true\. |
| [AddClass\(string, Func&lt;bool&gt;\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string,System.Func_bool_) 'FastComponents\.ClassNamesBuilder\.AddClass\(string, System\.Func\<bool\>\)') | Adds a class name to the builder if the result of the function is true\. |
| [AddClass\(string\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string) 'FastComponents\.ClassNamesBuilder\.AddClass\(string\)') | Adds a class name to the builder\. |

<a name='FastComponents.ClassNamesBuilder.AddClass(string,bool)'></a>

## ClassNamesBuilder\.AddClass\(string, bool\) Method

Adds a class name to the builder if the condition is true\.

```csharp
public FastComponents.ClassNamesBuilder AddClass(string value, bool when);
```
#### Parameters

<a name='FastComponents.ClassNamesBuilder.AddClass(string,bool).value'></a>

`value` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

The class name to add\.

<a name='FastComponents.ClassNamesBuilder.AddClass(string,bool).when'></a>

`when` [System\.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System\.Boolean')

The condition to check\.

#### Returns
[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')  
The current instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')\.

<a name='FastComponents.ClassNamesBuilder.AddClass(string,System.Func_bool_)'></a>

## ClassNamesBuilder\.AddClass\(string, Func\<bool\>\) Method

Adds a class name to the builder if the result of the function is true\.

```csharp
public FastComponents.ClassNamesBuilder AddClass(string value, System.Func<bool>? when);
```
#### Parameters

<a name='FastComponents.ClassNamesBuilder.AddClass(string,System.Func_bool_).value'></a>

`value` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

The class name to add\.

<a name='FastComponents.ClassNamesBuilder.AddClass(string,System.Func_bool_).when'></a>

`when` [System\.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')[System\.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System\.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')

A function to check the condition\.

#### Returns
[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')  
The current instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')\.

<a name='FastComponents.ClassNamesBuilder.AddClass(string)'></a>

## ClassNamesBuilder\.AddClass\(string\) Method

Adds a class name to the builder\.

```csharp
public FastComponents.ClassNamesBuilder AddClass(string value);
```
#### Parameters

<a name='FastComponents.ClassNamesBuilder.AddClass(string).value'></a>

`value` [System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

The class name to add\.

#### Returns
[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')  
The current instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')\.