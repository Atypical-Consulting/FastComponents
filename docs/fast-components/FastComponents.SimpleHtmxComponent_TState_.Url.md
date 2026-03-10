#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents').[SimpleHtmxComponent&lt;TState&gt;](FastComponents.SimpleHtmxComponent_TState_.md 'FastComponents\.SimpleHtmxComponent\<TState\>')

## SimpleHtmxComponent\<TState\>\.Url Method

| Overloads | |
| :--- | :--- |
| [Url\(Func&lt;TState,TState&gt;\)](FastComponents.SimpleHtmxComponent_TState_.Url.md#FastComponents.SimpleHtmxComponent_TState_.Url(System.Func_TState,TState_) 'FastComponents\.SimpleHtmxComponent\<TState\>\.Url\(System\.Func\<TState,TState\>\)') | Creates a URL for this component with a state update function |
| [Url\(TState\)](FastComponents.SimpleHtmxComponent_TState_.Url.md#FastComponents.SimpleHtmxComponent_TState_.Url(TState) 'FastComponents\.SimpleHtmxComponent\<TState\>\.Url\(TState\)') | Creates a URL for this component with updated state |

<a name='FastComponents.SimpleHtmxComponent_TState_.Url(System.Func_TState,TState_)'></a>

## SimpleHtmxComponent\<TState\>\.Url\(Func\<TState,TState\>\) Method

Creates a URL for this component with a state update function

```csharp
public string Url(System.Func<TState,TState> updateState);
```
#### Parameters

<a name='FastComponents.SimpleHtmxComponent_TState_.Url(System.Func_TState,TState_).updateState'></a>

`updateState` [System\.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System\.Func\`2')[TState](FastComponents.SimpleHtmxComponent_TState_.md#FastComponents.SimpleHtmxComponent_TState_.TState 'FastComponents\.SimpleHtmxComponent\<TState\>\.TState')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System\.Func\`2')[TState](FastComponents.SimpleHtmxComponent_TState_.md#FastComponents.SimpleHtmxComponent_TState_.TState 'FastComponents\.SimpleHtmxComponent\<TState\>\.TState')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System\.Func\`2')

#### Returns
[System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')

<a name='FastComponents.SimpleHtmxComponent_TState_.Url(TState)'></a>

## SimpleHtmxComponent\<TState\>\.Url\(TState\) Method

Creates a URL for this component with updated state

```csharp
public string Url(TState? newState=null);
```
#### Parameters

<a name='FastComponents.SimpleHtmxComponent_TState_.Url(TState).newState'></a>

`newState` [TState](FastComponents.SimpleHtmxComponent_TState_.md#FastComponents.SimpleHtmxComponent_TState_.TState 'FastComponents\.SimpleHtmxComponent\<TState\>\.TState')

#### Returns
[System\.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System\.String')