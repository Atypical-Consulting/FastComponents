#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## HtmxComponentBase<TParameters> Class

Base class for all components that are rendered on the server.

```csharp
public abstract class HtmxComponentBase<TParameters> : FastComponents.HtmxComponentBase
    where TParameters : class, new()
```
#### Type parameters

<a name='FastComponents.HtmxComponentBase_TParameters_.TParameters'></a>

`TParameters`

The type of the parameters.

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; [Microsoft.AspNetCore.Components.ComponentBase](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.ComponentBase 'Microsoft.AspNetCore.Components.ComponentBase') &#129106; [HtmxComponentBase](FastComponents.HtmxComponentBase.md 'FastComponents.HtmxComponentBase') &#129106; HtmxComponentBase<TParameters>

| Properties | |
| :--- | :--- |
| [Parameters](FastComponents.HtmxComponentBase_TParameters_.Parameters.md 'FastComponents.HtmxComponentBase<TParameters>.Parameters') | Gets or sets the parameters. |

| Methods | |
| :--- | :--- |
| [CreateDefaultParameters()](FastComponents.HtmxComponentBase_TParameters_.CreateDefaultParameters().md 'FastComponents.HtmxComponentBase<TParameters>.CreateDefaultParameters()') | Creates the default parameters. |
