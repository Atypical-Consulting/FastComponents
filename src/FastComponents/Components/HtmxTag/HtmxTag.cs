using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace FastComponents;

/// <summary>
/// A component that renders a custom element with htmx attributes.
/// </summary>
public class HtmxTag : HtmxComponentBase
{
    /// <summary>
    /// The tag name to use for the root element, e.g. "div", "span", "li".
    /// Defaults to "div".
    /// </summary>
    [Parameter]
    public override string As { get; set; } = "div";

    /// <summary>
    /// The content to be rendered inside this element.
    /// </summary>
    [Parameter]
    public virtual RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // Render this fragment
        // <{As} @attributes="AdditionalAttributes" class="@ClassName">
        //   @ChildContent
        // </{As}>

        // Open tag with a custom element
        builder.OpenElement(0, As);

        // Additional attributes (comes first so that they can be overridden)
        builder.AddMultipleAttributes(1, CustomAttributes);

        // Core attributes
        if (IsTruthy(HxBoost))
            builder.AddAttribute(2, "hx-boost", HxBoost);
        if (IsTruthy(HxGet))
            builder.AddAttribute(3, "hx-get", HxGet);
        if (IsTruthy(HxPost))
            builder.AddAttribute(4, "hx-post", HxPost);
        if (IsTruthy(HxOn))
            builder.AddAttribute(5, "hx-on", HxOn);
        if (IsTruthy(HxPushUrl))
            builder.AddAttribute(6, "hx-push-url", HxPushUrl);
        if (IsTruthy(HxSelect))
            builder.AddAttribute(7, "hx-select", HxSelect);
        if (IsTruthy(HxSelectOob))
            builder.AddAttribute(8, "hx-select-oob", HxSelectOob);
        if (IsTruthy(HxSwap))
            builder.AddAttribute(9, "hx-swap", HxSwap);
        if (IsTruthy(HxSwapOob))
            builder.AddAttribute(10, "hx-swap-oob", HxSwapOob);
        if (IsTruthy(HxTarget))
            builder.AddAttribute(11, "hx-target", HxTarget);
        if (IsTruthy(HxTrigger))
            builder.AddAttribute(12, "hx-trigger", HxTrigger);
        if (IsTruthy(HxVals))
            builder.AddAttribute(13, "hx-vals", HxVals);

        // Additional attributes
        if (IsTruthy(HxConfirm))
            builder.AddAttribute(14, "hx-confirm", HxConfirm);
        if (IsTruthy(HxDelete))
            builder.AddAttribute(15, "hx-delete", HxDelete);
        if (IsTruthy(HxDisable))
            builder.AddAttribute(16, "hx-disable", HxDisable);
        if (IsTruthy(HxDisabledElt))
            builder.AddAttribute(17, "hx-disabled-elt", HxDisabledElt);
        if (IsTruthy(HxDisinherit))
            builder.AddAttribute(18, "hx-disinherit", HxDisinherit);
        if (IsTruthy(HxEncoding))
            builder.AddAttribute(19, "hx-encoding", HxEncoding);
        if (IsTruthy(HxExt))
            builder.AddAttribute(20, "hx-ext", HxExt);
        if (IsTruthy(HxHeaders))
            builder.AddAttribute(21, "hx-headers", HxHeaders);
        if (IsTruthy(HxHistory))
            builder.AddAttribute(22, "hx-history", HxHistory);
        if (IsTruthy(HxHistoryElt))
            builder.AddAttribute(23, "hx-history-elt", HxHistoryElt);
        if (IsTruthy(HxInclude))
            builder.AddAttribute(24, "hx-include", HxInclude);
        if (IsTruthy(HxIndicator))
            builder.AddAttribute(25, "hx-indicator", HxIndicator);
        if (IsTruthy(HxParams))
            builder.AddAttribute(26, "hx-params", HxParams);
        if (IsTruthy(HxPatch))
            builder.AddAttribute(27, "hx-patch", HxPatch);
        if (IsTruthy(HxPreserve))
            builder.AddAttribute(28, "hx-preserve", HxPreserve);
        if (IsTruthy(HxPrompt))
            builder.AddAttribute(29, "hx-prompt", HxPrompt);
        if (IsTruthy(HxPut))
            builder.AddAttribute(30, "hx-put", HxPut);
        if (IsTruthy(HxReplaceUrl))
            builder.AddAttribute(31, "hx-replace-url", HxReplaceUrl);
        if (IsTruthy(HxRequest))
            builder.AddAttribute(32, "hx-request", HxRequest);
        if (IsTruthy(HxSync))
            builder.AddAttribute(33, "hx-sync", HxSync);
        if (IsTruthy(HxValidate))
            builder.AddAttribute(34, "hx-validate", HxValidate);

        // Class names
        if (IsTruthy(ClassName))
            builder.AddAttribute(35, "class", ClassName);

        // Child content
        builder.AddContent(36, ChildContent);
        builder.CloseElement();
    }
    
    /// <summary>
    /// Check if the value is not null, empty, or whitespace
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is not null, empty, or whitespace; otherwise, false.</returns>
    private static bool IsTruthy(string? value)
        => !string.IsNullOrWhiteSpace(value);
}
