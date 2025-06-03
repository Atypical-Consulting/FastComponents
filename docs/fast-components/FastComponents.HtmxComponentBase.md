#### [FastComponents](FastComponents.md 'FastComponents')
### [FastComponents](FastComponents.md 'FastComponents')

## HtmxComponentBase Class

Base class for all components that are rendered on the server\.

```csharp
public abstract class HtmxComponentBase : Microsoft.AspNetCore.Components.ComponentBase, FastComponents.IHxAttributes, FastComponents.IHxCoreAttributes, FastComponents.IHxAdditionalAttributes
```

Inheritance [System\.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System\.Object') &#129106; [Microsoft\.AspNetCore\.Components\.ComponentBase](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.AspNetCore.Components.ComponentBase 'Microsoft\.AspNetCore\.Components\.ComponentBase') &#129106; HtmxComponentBase

Derived  
&#8627; [HtmxComponentBase&lt;TParameters&gt;](FastComponents.HtmxComponentBase_TParameters_.md 'FastComponents\.HtmxComponentBase\<TParameters\>')  
&#8627; [HtmxTag](FastComponents.HtmxTag.md 'FastComponents\.HtmxTag')

Implements [IHxAttributes](FastComponents.IHxAttributes.md 'FastComponents\.IHxAttributes'), [IHxCoreAttributes](FastComponents.IHxCoreAttributes.md 'FastComponents\.IHxCoreAttributes'), [IHxAdditionalAttributes](FastComponents.IHxAdditionalAttributes.md 'FastComponents\.IHxAdditionalAttributes')

| Properties | |
| :--- | :--- |
| [As](FastComponents.HtmxComponentBase.As.md 'FastComponents\.HtmxComponentBase\.As') | Gets or sets a custom tag name for the component\. |
| [HxBoost](FastComponents.HtmxComponentBase.HxBoost.md 'FastComponents\.HtmxComponentBase\.HxBoost') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Add or remove progressive enhancement for links and forms |
| [HxConfirm](FastComponents.HtmxComponentBase.HxConfirm.md 'FastComponents\.HtmxComponentBase\.HxConfirm') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Shows a confirm\(\) dialog before issuing a request\. |
| [HxDelete](FastComponents.HtmxComponentBase.HxDelete.md 'FastComponents\.HtmxComponentBase\.HxDelete') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Issues a DELETE to the specified URL\. |
| [HxDisable](FastComponents.HtmxComponentBase.HxDisable.md 'FastComponents\.HtmxComponentBase\.HxDisable') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Disables htmx processing for the given node and any children nodes\. |
| [HxDisabledElt](FastComponents.HtmxComponentBase.HxDisabledElt.md 'FastComponents\.HtmxComponentBase\.HxDisabledElt') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Adds the disabled attribute to the specified elements while a request is in flight\. |
| [HxDisinherit](FastComponents.HtmxComponentBase.HxDisinherit.md 'FastComponents\.HtmxComponentBase\.HxDisinherit') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Control and disable automatic attribute inheritance for child nodes\. |
| [HxEncoding](FastComponents.HtmxComponentBase.HxEncoding.md 'FastComponents\.HtmxComponentBase\.HxEncoding') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Changes the request encoding type\. |
| [HxExt](FastComponents.HtmxComponentBase.HxExt.md 'FastComponents\.HtmxComponentBase\.HxExt') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Extensions to use for this element\. |
| [HxGet](FastComponents.HtmxComponentBase.HxGet.md 'FastComponents\.HtmxComponentBase\.HxGet') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Issues a GET to the specified URL |
| [HxHeaders](FastComponents.HtmxComponentBase.HxHeaders.md 'FastComponents\.HtmxComponentBase\.HxHeaders') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Adds to the headers that will be submitted with the request\. |
| [HxHistory](FastComponents.HtmxComponentBase.HxHistory.md 'FastComponents\.HtmxComponentBase\.HxHistory') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Prevent sensitive data from being saved to the history cache\. |
| [HxHistoryElt](FastComponents.HtmxComponentBase.HxHistoryElt.md 'FastComponents\.HtmxComponentBase\.HxHistoryElt') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> The element to snapshot and restore during history navigation\. |
| [HxInclude](FastComponents.HtmxComponentBase.HxInclude.md 'FastComponents\.HtmxComponentBase\.HxInclude') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Include additional data in requests\. |
| [HxIndicator](FastComponents.HtmxComponentBase.HxIndicator.md 'FastComponents\.HtmxComponentBase\.HxIndicator') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> The element to put the htmx\-request class on during the request\. |
| [HxOn](FastComponents.HtmxComponentBase.HxOn.md 'FastComponents\.HtmxComponentBase\.HxOn') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Handle events with a inline scripts on elements |
| [HxParams](FastComponents.HtmxComponentBase.HxParams.md 'FastComponents\.HtmxComponentBase\.HxParams') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Filters the parameters that will be submitted with a request\. |
| [HxPatch](FastComponents.HtmxComponentBase.HxPatch.md 'FastComponents\.HtmxComponentBase\.HxPatch') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Issues a PATCH to the specified URL\. |
| [HxPost](FastComponents.HtmxComponentBase.HxPost.md 'FastComponents\.HtmxComponentBase\.HxPost') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Issues a POST to the specified URL |
| [HxPreserve](FastComponents.HtmxComponentBase.HxPreserve.md 'FastComponents\.HtmxComponentBase\.HxPreserve') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Specifies elements to keep unchanged between requests\. |
| [HxPrompt](FastComponents.HtmxComponentBase.HxPrompt.md 'FastComponents\.HtmxComponentBase\.HxPrompt') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Shows a prompt\(\) dialog before submitting a request\. |
| [HxPushUrl](FastComponents.HtmxComponentBase.HxPushUrl.md 'FastComponents\.HtmxComponentBase\.HxPushUrl') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Pushes the URL into the browser location bar, creating a new history entry |
| [HxPut](FastComponents.HtmxComponentBase.HxPut.md 'FastComponents\.HtmxComponentBase\.HxPut') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Issues a PUT to the specified URL\. |
| [HxReplaceUrl](FastComponents.HtmxComponentBase.HxReplaceUrl.md 'FastComponents\.HtmxComponentBase\.HxReplaceUrl') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Replace the URL in the browser location bar\. |
| [HxRequest](FastComponents.HtmxComponentBase.HxRequest.md 'FastComponents\.HtmxComponentBase\.HxRequest') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Configures various aspects of the request\. |
| [HxSelect](FastComponents.HtmxComponentBase.HxSelect.md 'FastComponents\.HtmxComponentBase\.HxSelect') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Select content to swap in from a response |
| [HxSelectOob](FastComponents.HtmxComponentBase.HxSelectOob.md 'FastComponents\.HtmxComponentBase\.HxSelectOob') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Select content to swap in from a response, out of band \(somewhere other than the target\) |
| [HxSwap](FastComponents.HtmxComponentBase.HxSwap.md 'FastComponents\.HtmxComponentBase\.HxSwap') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Controls how content is swapped in \(outerHTML, beforeend, afterend, …\) |
| [HxSwapOob](FastComponents.HtmxComponentBase.HxSwapOob.md 'FastComponents\.HtmxComponentBase\.HxSwapOob') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Marks content in a response to be out of band \(should swap in somewhere other than the target\) |
| [HxSync](FastComponents.HtmxComponentBase.HxSync.md 'FastComponents\.HtmxComponentBase\.HxSync') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Control how requests made by different elements are synchronized\. |
| [HxTarget](FastComponents.HtmxComponentBase.HxTarget.md 'FastComponents\.HtmxComponentBase\.HxTarget') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Specifies the target element to be swapped |
| [HxTrigger](FastComponents.HtmxComponentBase.HxTrigger.md 'FastComponents\.HtmxComponentBase\.HxTrigger') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Specifies the event that triggers the request |
| [HxValidate](FastComponents.HtmxComponentBase.HxValidate.md 'FastComponents\.HtmxComponentBase\.HxValidate') | ADDITIONAL ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Force elements to validate themselves before a request\. |
| [HxVals](FastComponents.HtmxComponentBase.HxVals.md 'FastComponents\.HtmxComponentBase\.HxVals') | CORE ATTRIBUTE<br/> \-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-\-<br/> Adds values to the parameters to submit with the request \(JSON\-formatted\) |

| Methods | |
| :--- | :--- |
| [OnParametersSet\(\)](FastComponents.HtmxComponentBase.OnParametersSet().md 'FastComponents\.HtmxComponentBase\.OnParametersSet\(\)') | Method invoked when the component has received parameters from its parent in the render tree, and the incoming values have been assigned to properties\. |
