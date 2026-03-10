#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## ClassNamesBuilder Struct

A helper class for building a string of CSS class names\.

```csharp
public readonly struct ClassNamesBuilder
```

| Constructors | |
| :--- | :--- |
| [ClassNamesBuilder\(string, string, string\)](FastComponents.ClassNamesBuilder.ClassNamesBuilder(string,string,string).md 'FastComponents\.ClassNamesBuilder\.ClassNamesBuilder\(string, string, string\)') | A helper class for building a string of CSS class names\. |

| Methods | |
| :--- | :--- |
| [AddClass\(ClassNamesBuilder, bool\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,bool) 'FastComponents\.ClassNamesBuilder\.AddClass\(FastComponents\.ClassNamesBuilder, bool\)') | Adds classes from another builder if the condition is true\. |
| [AddClass\(ClassNamesBuilder, Func&lt;bool&gt;\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(FastComponents.ClassNamesBuilder,System.Func_bool_) 'FastComponents\.ClassNamesBuilder\.AddClass\(FastComponents\.ClassNamesBuilder, System\.Func\<bool\>\)') | Adds classes from another builder if the result of the function is true\. |
| [AddClass\(string, bool\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string,bool) 'FastComponents\.ClassNamesBuilder\.AddClass\(string, bool\)') | Adds a class name to the builder if the condition is true\. |
| [AddClass\(string, Func&lt;bool&gt;\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string,System.Func_bool_) 'FastComponents\.ClassNamesBuilder\.AddClass\(string, System\.Func\<bool\>\)') | Adds a class name to the builder if the result of the function is true\. |
| [AddClass\(string\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(string) 'FastComponents\.ClassNamesBuilder\.AddClass\(string\)') | Adds a class name to the builder\. |
| [AddClass\(Func&lt;string&gt;, bool\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,bool) 'FastComponents\.ClassNamesBuilder\.AddClass\(System\.Func\<string\>, bool\)') | Adds a class name to the builder if the condition is true\. |
| [AddClass\(Func&lt;string&gt;, Func&lt;bool&gt;\)](FastComponents.ClassNamesBuilder.AddClass.md#FastComponents.ClassNamesBuilder.AddClass(System.Func_string_,System.Func_bool_) 'FastComponents\.ClassNamesBuilder\.AddClass\(System\.Func\<string\>, System\.Func\<bool\>\)') | Adds a class name to the builder if the result of the function is true\. |
| [AddClassFromAttributes\(IReadOnlyDictionary&lt;string,object&gt;\)](FastComponents.ClassNamesBuilder.AddClassFromAttributes(System.Collections.Generic.IReadOnlyDictionary_string,object_).md 'FastComponents\.ClassNamesBuilder\.AddClassFromAttributes\(System\.Collections\.Generic\.IReadOnlyDictionary\<string,object\>\)') | Adds class names from the "class" attribute in additional attributes\. |
| [AddRawValue\(string\)](FastComponents.ClassNamesBuilder.AddRawValue(string).md 'FastComponents\.ClassNamesBuilder\.AddRawValue\(string\)') | Adds a raw value to the builder\. |
| [Build\(\)](FastComponents.ClassNamesBuilder.Build().md 'FastComponents\.ClassNamesBuilder\.Build\(\)') | Builds the string of class names\. |
| [Default\(string\)](FastComponents.ClassNamesBuilder.Default(string).md 'FastComponents\.ClassNamesBuilder\.Default\(string\)') | Creates a new instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder')\. |
| [Empty\(\)](FastComponents.ClassNamesBuilder.Empty().md 'FastComponents\.ClassNamesBuilder\.Empty\(\)') | Creates a new instance of [ClassNamesBuilder](FastComponents.ClassNamesBuilder.md 'FastComponents\.ClassNamesBuilder') with a prefix\. |
| [ToString\(\)](FastComponents.ClassNamesBuilder.ToString().md 'FastComponents\.ClassNamesBuilder\.ToString\(\)') | Builds the string of class names\. |
