#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')

## ClassNamesBuilder\.AddClass Method

| Overloads | |
| :--- | :--- |
| [AddClass\(ClassNamesBuilder, bool\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,bool) 'FastComponents\.ClassNamesBuilder\.AddClass\(FastComponents\.ClassNamesBuilder, bool\)') | Adds classes from another builder if the condition is true\. |
| [AddClass\(ClassNamesBuilder, Func&lt;bool&gt;\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,System.Func_bool_) 'FastComponents\.ClassNamesBuilder\.AddClass\(FastComponents\.ClassNamesBuilder, System\.Func\<bool\>\)') | Adds classes from another builder if the result of the function is true\. |
| [AddClass\(string, bool\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string,bool) 'FastComponents\.ClassNamesBuilder\.AddClass\(string, bool\)') | Adds a class name to the builder if the condition is true\. |
| [AddClass\(string, Func&lt;bool&gt;\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string,System.Func_bool_) 'FastComponents\.ClassNamesBuilder\.AddClass\(string, System\.Func\<bool\>\)') | Adds a class name to the builder if the result of the function is true\. |
| [AddClass\(string\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string) 'FastComponents\.ClassNamesBuilder\.AddClass\(string\)') | Adds a class name to the builder\. |
| [AddClass\(Func&lt;string&gt;, bool\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,bool) 'FastComponents\.ClassNamesBuilder\.AddClass\(System\.Func\<string\>, bool\)') | Adds a class name to the builder if the condition is true\. |
| [AddClass\(Func&lt;string&gt;, Func&lt;bool&gt;\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,System.Func_bool_) 'FastComponents\.ClassNamesBuilder\.AddClass\(System\.Func\<string\>, System\.Func\<bool\>\)') | Adds a class name to the builder if the result of the function is true\. |

<a name='FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,bool)'></a>

## ClassNamesBuilder\.AddClass\(ClassNamesBuilder, bool\) Method

Adds classes from another builder if the condition is true\.

```csharp
public FastComponents.ClassNamesBuilder AddClass(FastComponents.ClassNamesBuilder builder, bool when);
```
#### Parameters

<a name='FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,bool).builder'></a>

`builder` [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')

The builder whose classes to add\.

<a name='FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,bool).when'></a>

`when` [System\.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System\.Boolean')

The condition to check\.

#### Returns
[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')  
The current instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')\.

<a name='FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,System.Func_bool_)'></a>

## ClassNamesBuilder\.AddClass\(ClassNamesBuilder, Func\<bool\>\) Method

Adds classes from another builder if the result of the function is true\.

```csharp
public FastComponents.ClassNamesBuilder AddClass(FastComponents.ClassNamesBuilder builder, System.Func<bool>? when);
```
#### Parameters

<a name='FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,System.Func_bool_).builder'></a>

`builder` [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')

The builder whose classes to add\.

<a name='FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,System.Func_bool_).when'></a>

`when` [System\.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')[System\.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System\.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')

A function to check the condition\.

#### Returns
[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')  
The current instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')\.

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

<a name='FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,bool)'></a>

## ClassNamesBuilder\.AddClass\(Func\<string\>, bool\) Method

Adds a class name to the builder if the condition is true\.

```csharp
public FastComponents.ClassNamesBuilder AddClass(System.Func<string> value, bool when);
```
#### Parameters

<a name='FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,bool).value'></a>

`value` [System\.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')[System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')

A function that returns the class name to add\.

<a name='FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,bool).when'></a>

`when` [System\.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System\.Boolean')

The condition to check\.

#### Returns
[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')  
The current instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')\.

<a name='FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,System.Func_bool_)'></a>

## ClassNamesBuilder\.AddClass\(Func\<string\>, Func\<bool\>\) Method

Adds a class name to the builder if the result of the function is true\.

```csharp
public FastComponents.ClassNamesBuilder AddClass(System.Func<string> value, System.Func<bool>? when);
```
#### Parameters

<a name='FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,System.Func_bool_).value'></a>

`value` [System\.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')[System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')

A function that returns the class name to add\.

<a name='FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,System.Func_bool_).when'></a>

`when` [System\.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')[System\.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System\.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-1 'System\.Func\`1')

A function to check the condition\.

#### Returns
[ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')  
The current instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')\.