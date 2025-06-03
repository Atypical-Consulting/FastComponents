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
/// HTMX component that supports WebSocket attributes.
/// </summary>
public class HtmxWsTag : HtmxComponentBase, IHxWsAttributes
{
    /// <inheritdoc />
    [Parameter]
    public string? WsConnect { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? WsSend { get; set; }
    
    /// <summary>
    /// Gets or sets the child content to be rendered inside the tag.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc />
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, Element);
        
        // Add class attribute if present
        if (!string.IsNullOrWhiteSpace(ClassName))
        {
            builder.AddAttribute(1, "class", ClassName);
        }
        
        // Add all HTMX core attributes
        AddHtmxCoreAttributes(builder, 100);
        
        // Add all HTMX additional attributes
        AddHtmxAdditionalAttributes(builder, 200);
        
        // Add WebSocket-specific attributes
        AddAttribute(builder, 300, "ws-connect", WsConnect);
        AddAttribute(builder, 301, "ws-send", WsSend);
        
        // Add custom attributes
        if (CustomAttributes != null)
        {
            builder.AddMultipleAttributes(400, CustomAttributes);
        }
        
        // Add child content
        if (ChildContent != null)
        {
            builder.AddContent(500, ChildContent);
        }
        
        builder.CloseElement();
    }
    
    private void AddHtmxCoreAttributes(RenderTreeBuilder builder, int sequence)
    {
        AddAttribute(builder, sequence++, "hx-boost", HxBoost);
        AddAttribute(builder, sequence++, "hx-get", HxGet);
        AddAttribute(builder, sequence++, "hx-post", HxPost);
        AddAttribute(builder, sequence++, "hx-on", HxOn);
        AddAttribute(builder, sequence++, "hx-push-url", HxPushUrl);
        AddAttribute(builder, sequence++, "hx-select", HxSelect);
        AddAttribute(builder, sequence++, "hx-select-oob", HxSelectOob);
        AddAttribute(builder, sequence++, "hx-swap", HxSwap);
        AddAttribute(builder, sequence++, "hx-swap-oob", HxSwapOob);
        AddAttribute(builder, sequence++, "hx-target", HxTarget);
        AddAttribute(builder, sequence++, "hx-trigger", HxTrigger);
        AddAttribute(builder, sequence++, "hx-vals", HxVals);
        AddAttribute(builder, sequence++, "hx-vars", HxVars);
    }
    
    private void AddHtmxAdditionalAttributes(RenderTreeBuilder builder, int sequence)
    {
        AddAttribute(builder, sequence++, "hx-confirm", HxConfirm);
        AddAttribute(builder, sequence++, "hx-delete", HxDelete);
        AddAttribute(builder, sequence++, "hx-disable", HxDisable);
        AddAttribute(builder, sequence++, "hx-disabled-elt", HxDisabledElt);
        AddAttribute(builder, sequence++, "hx-disinherit", HxDisinherit);
        AddAttribute(builder, sequence++, "hx-encoding", HxEncoding);
        AddAttribute(builder, sequence++, "hx-ext", HxExt);
        AddAttribute(builder, sequence++, "hx-headers", HxHeaders);
        AddAttribute(builder, sequence++, "hx-history", HxHistory);
        AddAttribute(builder, sequence++, "hx-history-elt", HxHistoryElt);
        AddAttribute(builder, sequence++, "hx-include", HxInclude);
        AddAttribute(builder, sequence++, "hx-indicator", HxIndicator);
        AddAttribute(builder, sequence++, "hx-params", HxParams);
        AddAttribute(builder, sequence++, "hx-patch", HxPatch);
        AddAttribute(builder, sequence++, "hx-preserve", HxPreserve);
        AddAttribute(builder, sequence++, "hx-prompt", HxPrompt);
        AddAttribute(builder, sequence++, "hx-put", HxPut);
        AddAttribute(builder, sequence++, "hx-replace-url", HxReplaceUrl);
        AddAttribute(builder, sequence++, "hx-request", HxRequest);
        AddAttribute(builder, sequence++, "hx-sync", HxSync);
        AddAttribute(builder, sequence++, "hx-validate", HxValidate);
    }
    
    private static void AddAttribute(RenderTreeBuilder builder, int sequence, string name, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            builder.AddAttribute(sequence, name, value);
        }
    }
}