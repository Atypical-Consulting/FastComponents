/*
 * Copyright 2025 Atypical Consulting SRL
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
    public override string Element { get; set; } = "div";

    /// <summary>
    /// The content to be rendered inside this element.
    /// </summary>
    [Parameter]
    public virtual RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        // Render this fragment
        // <{Element} @attributes="AdditionalAttributes" class="@ClassName">
        //   @ChildContent
        // </{Element}>

        // Open tag with a custom element
        builder.OpenElement(0, Element);

        // Additional attributes (comes first so that they can be overridden)
        builder.AddMultipleAttributes(1, CustomAttributes);

        // Core attributes
        if (!string.IsNullOrWhiteSpace(HxBoost))
        {
            builder.AddAttribute(2, "hx-boost", HxBoost);
        }

        if (!string.IsNullOrWhiteSpace(HxGet))
        {
            builder.AddAttribute(3, "hx-get", HxGet);
        }

        if (!string.IsNullOrWhiteSpace(HxPost))
        {
            builder.AddAttribute(4, "hx-post", HxPost);
        }

        if (!string.IsNullOrWhiteSpace(HxOn))
        {
            builder.AddAttribute(5, "hx-on", HxOn);
        }

        if (!string.IsNullOrWhiteSpace(HxPushUrl))
        {
            builder.AddAttribute(6, "hx-push-url", HxPushUrl);
        }

        if (!string.IsNullOrWhiteSpace(HxSelect))
        {
            builder.AddAttribute(7, "hx-select", HxSelect);
        }

        if (!string.IsNullOrWhiteSpace(HxSelectOob))
        {
            builder.AddAttribute(8, "hx-select-oob", HxSelectOob);
        }

        if (!string.IsNullOrWhiteSpace(HxSwap))
        {
            builder.AddAttribute(9, "hx-swap", HxSwap);
        }

        if (!string.IsNullOrWhiteSpace(HxSwapOob))
        {
            builder.AddAttribute(10, "hx-swap-oob", HxSwapOob);
        }

        if (!string.IsNullOrWhiteSpace(HxTarget))
        {
            builder.AddAttribute(11, "hx-target", HxTarget);
        }

        if (!string.IsNullOrWhiteSpace(HxTrigger))
        {
            builder.AddAttribute(12, "hx-trigger", HxTrigger);
        }

        if (!string.IsNullOrWhiteSpace(HxVals))
        {
            builder.AddAttribute(13, "hx-vals", HxVals);
        }

        // Additional attributes
        if (!string.IsNullOrWhiteSpace(HxConfirm))
        {
            builder.AddAttribute(14, "hx-confirm", HxConfirm);
        }

        if (!string.IsNullOrWhiteSpace(HxDelete))
        {
            builder.AddAttribute(15, "hx-delete", HxDelete);
        }

        if (!string.IsNullOrWhiteSpace(HxDisable))
        {
            builder.AddAttribute(16, "hx-disable", HxDisable);
        }

        if (!string.IsNullOrWhiteSpace(HxDisabledElt))
        {
            builder.AddAttribute(17, "hx-disabled-elt", HxDisabledElt);
        }

        if (!string.IsNullOrWhiteSpace(HxDisinherit))
        {
            builder.AddAttribute(18, "hx-disinherit", HxDisinherit);
        }

        if (!string.IsNullOrWhiteSpace(HxEncoding))
        {
            builder.AddAttribute(19, "hx-encoding", HxEncoding);
        }

        if (!string.IsNullOrWhiteSpace(HxExt))
        {
            builder.AddAttribute(20, "hx-ext", HxExt);
        }

        if (!string.IsNullOrWhiteSpace(HxHeaders))
        {
            builder.AddAttribute(21, "hx-headers", HxHeaders);
        }

        if (!string.IsNullOrWhiteSpace(HxHistory))
        {
            builder.AddAttribute(22, "hx-history", HxHistory);
        }

        if (!string.IsNullOrWhiteSpace(HxHistoryElt))
        {
            builder.AddAttribute(23, "hx-history-elt", HxHistoryElt);
        }

        if (!string.IsNullOrWhiteSpace(HxInclude))
        {
            builder.AddAttribute(24, "hx-include", HxInclude);
        }

        if (!string.IsNullOrWhiteSpace(HxIndicator))
        {
            builder.AddAttribute(25, "hx-indicator", HxIndicator);
        }

        if (!string.IsNullOrWhiteSpace(HxParams))
        {
            builder.AddAttribute(26, "hx-params", HxParams);
        }

        if (!string.IsNullOrWhiteSpace(HxPatch))
        {
            builder.AddAttribute(27, "hx-patch", HxPatch);
        }

        if (!string.IsNullOrWhiteSpace(HxPreserve))
        {
            builder.AddAttribute(28, "hx-preserve", HxPreserve);
        }

        if (!string.IsNullOrWhiteSpace(HxPrompt))
        {
            builder.AddAttribute(29, "hx-prompt", HxPrompt);
        }

        if (!string.IsNullOrWhiteSpace(HxPut))
        {
            builder.AddAttribute(30, "hx-put", HxPut);
        }

        if (!string.IsNullOrWhiteSpace(HxReplaceUrl))
        {
            builder.AddAttribute(31, "hx-replace-url", HxReplaceUrl);
        }

        if (!string.IsNullOrWhiteSpace(HxRequest))
        {
            builder.AddAttribute(32, "hx-request", HxRequest);
        }

        if (!string.IsNullOrWhiteSpace(HxSync))
        {
            builder.AddAttribute(33, "hx-sync", HxSync);
        }

        if (!string.IsNullOrWhiteSpace(HxValidate))
        {
            builder.AddAttribute(34, "hx-validate", HxValidate);
        }

        // Class names
        if (!string.IsNullOrWhiteSpace(ClassName))
        {
            builder.AddAttribute(14, "class", ClassName);
        }

        // Child content
        builder.AddContent(15, ChildContent);
        builder.CloseElement();
    }
}
