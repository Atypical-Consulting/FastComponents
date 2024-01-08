#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## IHxCssClasses Interface

Htmx CSS classes

```csharp
public interface IHxCssClasses
```

| Properties | |
| :--- | :--- |
| [HxCssAdded](FastComponents.IHxCssClasses.HxCssAdded.md 'FastComponents.IHxCssClasses.HxCssAdded') | CSS CLASS<br/><br/>------------------------------<br/><br/>Applied to a new piece of content before it is swapped, removed after it is settled. |
| [HxCssIndicator](FastComponents.IHxCssClasses.HxCssIndicator.md 'FastComponents.IHxCssClasses.HxCssIndicator') | CSS CLASS<br/><br/>------------------------------<br/><br/>A dynamically generated class that will toggle visible (opacity:1) when a htmx-request class is present |
| [HxCssRequest](FastComponents.IHxCssClasses.HxCssRequest.md 'FastComponents.IHxCssClasses.HxCssRequest') | CSS CLASS<br/><br/>------------------------------<br/><br/>Applied to either the element or the element specified with hx-indicator while a request is ongoing |
| [HxCssSettling](FastComponents.IHxCssClasses.HxCssSettling.md 'FastComponents.IHxCssClasses.HxCssSettling') | CSS CLASS<br/><br/>------------------------------<br/><br/>Applied to a target after content is swapped, removed after it is settled. The duration can be modified via hx-swap. |
| [HxCssSwapping](FastComponents.IHxCssClasses.HxCssSwapping.md 'FastComponents.IHxCssClasses.HxCssSwapping') | CSS CLASS<br/><br/>------------------------------<br/><br/>Applied to a target before any content is swapped, removed after it is swapped. The duration can be modified via hx-swap. |
