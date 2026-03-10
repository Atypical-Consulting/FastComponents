#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## SimpleHtmxComponent\<TState\> Class

Simplified base class for HTMX components with convention\-based routing

```csharp
public abstract class SimpleHtmxComponent<TState> : Microsoft.AspNetCore.Components.ComponentBase
    where TState : class, new()
```
#### Type parameters

<a name='FastComponents.SimpleHtmxComponent_TState_.TState'></a>

`TState`

The state/parameters type

Inheritance [System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object') &#129106; [Microsoft\.AspNetCore\.Components\.ComponentBase](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.ComponentBase 'Microsoft\.AspNetCore\.Components\.ComponentBase') &#129106; SimpleHtmxComponent<TState>

| Properties | |
| :--- | :--- |
| [State](FastComponents.SimpleHtmxComponent_TState_.State.md 'FastComponents\.SimpleHtmxComponent\<TState\>\.State') | The current state/parameters for this component |

| Methods | |
| :--- | :--- |
| [GetRoute\(\)](FastComponents.SimpleHtmxComponent_TState_.GetRoute().md 'FastComponents\.SimpleHtmxComponent\<TState\>\.GetRoute\(\)') | Gets the route for this component based on convention |
| [Url\(Func&lt;TState,TState&gt;\)](FastComponents.SimpleHtmxComponent_TState_.Url.md#FastComponents.SimpleHtmxComponent_TState_.Url(System.Func_TState,TState_) 'FastComponents\.SimpleHtmxComponent\<TState\>\.Url\(System\.Func\<TState,TState\>\)') | Creates a URL for this component with a state update function |
| [Url\(TState\)](FastComponents.SimpleHtmxComponent_TState_.Url.md#FastComponents.SimpleHtmxComponent_TState_.Url(TState) 'FastComponents\.SimpleHtmxComponent\<TState\>\.Url\(TState\)') | Creates a URL for this component with updated state |
