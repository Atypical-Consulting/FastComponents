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

using Bunit;
using Shouldly;

namespace FastComponents.UnitTests;

public class HtmxTagTests : Bunit.TestContext
{
    [Fact]
    public void HtmxTag_WithDefaultProperties_ShouldRenderDivElement()
    {
        // Act
        var component = RenderComponent<HtmxTag>();

        // Assert
        component.Markup.ShouldBe("<div></div>");
    }

    [Fact]
    public void HtmxTag_WithCustomElement_ShouldRenderCorrectElement()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.Element, "span"));

        // Assert
        component.Markup.ShouldBe("<span></span>");
    }

    [Fact]
    public void HtmxTag_WithChildContent_ShouldRenderContent()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .AddChildContent("Test Content"));

        // Assert
        component.Markup.ShouldBe("<div>Test Content</div>");
    }

    [Fact]
    public void HtmxTag_WithHxGet_ShouldRenderHxGetAttribute()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxGet, "/api/test"));

        // Assert
        component.Markup.ShouldBe("<div hx-get=\"/api/test\"></div>");
    }

    [Fact]
    public void HtmxTag_WithHxPost_ShouldRenderHxPostAttribute()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxPost, "/api/submit"));

        // Assert
        component.Markup.ShouldBe("<div hx-post=\"/api/submit\"></div>");
    }

    [Fact]
    public void HtmxTag_WithHxTrigger_ShouldRenderHxTriggerAttribute()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxTrigger, "click"));

        // Assert
        component.Markup.ShouldBe("<div hx-trigger=\"click\"></div>");
    }

    [Fact]
    public void HtmxTag_WithHxTarget_ShouldRenderHxTargetAttribute()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxTarget, "#result"));

        // Assert
        component.Markup.ShouldBe("<div hx-target=\"#result\"></div>");
    }

    [Fact]
    public void HtmxTag_WithHxSwap_ShouldRenderHxSwapAttribute()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxSwap, "innerHTML"));

        // Assert
        component.Markup.ShouldBe("<div hx-swap=\"innerHTML\"></div>");
    }

    [Fact]
    public void HtmxTag_WithHxConfirm_ShouldRenderHxConfirmAttribute()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxConfirm, "Are you sure?"));

        // Assert
        component.Markup.ShouldBe("<div hx-confirm=\"Are you sure?\"></div>");
    }

    [Fact]
    public void HtmxTag_WithMultipleHxAttributes_ShouldRenderAllAttributes()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxGet, "/api/test")
            .Add(p => p.HxTrigger, "click")
            .Add(p => p.HxTarget, "#result")
            .Add(p => p.HxSwap, "innerHTML"));

        // Assert
        var markup = component.Markup;
        markup.ShouldContain("hx-get=\"/api/test\"");
        markup.ShouldContain("hx-trigger=\"click\"");
        markup.ShouldContain("hx-target=\"#result\"");
        markup.ShouldContain("hx-swap=\"innerHTML\"");
    }

    [Fact]
    public void HtmxTag_WithCustomAttributes_ShouldRenderCustomAttributes()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .AddUnmatched("data-test", "value")
            .AddUnmatched("id", "test-id"));

        // Assert
        var markup = component.Markup;
        markup.ShouldContain("data-test=\"value\"");
        markup.ShouldContain("id=\"test-id\"");
    }

    [Fact]
    public void HtmxTag_WithClassAttribute_ShouldRenderClassAttribute()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .AddUnmatched("class", "test-class"));

        // Assert
        component.Markup.ShouldContain("class=\"test-class\"");
    }

    [Fact]
    public void HtmxTag_WithAllCoreAttributes_ShouldRenderAllCoreAttributes()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxBoost, "true")
            .Add(p => p.HxGet, "/api/get")
            .Add(p => p.HxPost, "/api/post")
            .Add(p => p.HxOn, "click")
            .Add(p => p.HxPushUrl, "true")
            .Add(p => p.HxSelect, ".content")
            .Add(p => p.HxSelectOob, ".sidebar")
            .Add(p => p.HxSwap, "innerHTML")
            .Add(p => p.HxSwapOob, "true")
            .Add(p => p.HxTarget, "#target")
            .Add(p => p.HxTrigger, "click")
            .Add(p => p.HxVals, "{}"));

        // Assert
        var markup = component.Markup;
        markup.ShouldContain("hx-boost=\"true\"");
        markup.ShouldContain("hx-get=\"/api/get\"");
        markup.ShouldContain("hx-post=\"/api/post\"");
        markup.ShouldContain("hx-on=\"click\"");
        markup.ShouldContain("hx-push-url=\"true\"");
        markup.ShouldContain("hx-select=\".content\"");
        markup.ShouldContain("hx-select-oob=\".sidebar\"");
        markup.ShouldContain("hx-swap=\"innerHTML\"");
        markup.ShouldContain("hx-swap-oob=\"true\"");
        markup.ShouldContain("hx-target=\"#target\"");
        markup.ShouldContain("hx-trigger=\"click\"");
        markup.ShouldContain("hx-vals=\"{}\"");
    }

    [Fact]
    public void HtmxTag_WithAllAdditionalAttributes_ShouldRenderAllAdditionalAttributes()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxConfirm, "Are you sure?")
            .Add(p => p.HxDelete, "/api/delete")
            .Add(p => p.HxDisable, "true")
            .Add(p => p.HxDisabledElt, "#btn")
            .Add(p => p.HxDisinherit, "*")
            .Add(p => p.HxEncoding, "multipart/form-data")
            .Add(p => p.HxExt, "json-enc")
            .Add(p => p.HxHeaders, "{}")
            .Add(p => p.HxHistory, "false")
            .Add(p => p.HxHistoryElt, "#content")
            .Add(p => p.HxInclude, "[name='csrf']")
            .Add(p => p.HxIndicator, "#spinner")
            .Add(p => p.HxParams, "*")
            .Add(p => p.HxPatch, "/api/patch")
            .Add(p => p.HxPreserve, "true")
            .Add(p => p.HxPrompt, "Enter value")
            .Add(p => p.HxPut, "/api/put")
            .Add(p => p.HxReplaceUrl, "true")
            .Add(p => p.HxRequest, "{}")
            .Add(p => p.HxSync, "closest form:abort")
            .Add(p => p.HxValidate, "true"));

        // Assert
        var markup = component.Markup;
        markup.ShouldContain("hx-confirm=\"Are you sure?\"");
        markup.ShouldContain("hx-delete=\"/api/delete\"");
        markup.ShouldContain("hx-disable=\"true\"");
        markup.ShouldContain("hx-disabled-elt=\"#btn\"");
        markup.ShouldContain("hx-disinherit=\"*\"");
        markup.ShouldContain("hx-encoding=\"multipart/form-data\"");
        markup.ShouldContain("hx-ext=\"json-enc\"");
        markup.ShouldContain("hx-headers=\"{}\"");
        markup.ShouldContain("hx-history=\"false\"");
        markup.ShouldContain("hx-history-elt=\"#content\"");
        markup.ShouldContain("hx-include=\"[name='csrf']\"");
        markup.ShouldContain("hx-indicator=\"#spinner\"");
        markup.ShouldContain("hx-params=\"*\"");
        markup.ShouldContain("hx-patch=\"/api/patch\"");
        markup.ShouldContain("hx-preserve=\"true\"");
        markup.ShouldContain("hx-prompt=\"Enter value\"");
        markup.ShouldContain("hx-put=\"/api/put\"");
        markup.ShouldContain("hx-replace-url=\"true\"");
        markup.ShouldContain("hx-request=\"{}\"");
        markup.ShouldContain("hx-sync=\"closest form:abort\"");
        markup.ShouldContain("hx-validate=\"true\"");
    }

    [Fact]
    public void HtmxTag_WithEmptyStringAttributes_ShouldNotRenderThoseAttributes()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.HxGet, "")
            .Add(p => p.HxPost, " ")
            .Add(p => p.HxTarget, "/api/valid"));

        // Assert
        var markup = component.Markup;
        markup.ShouldNotContain("hx-get=");
        markup.ShouldNotContain("hx-post=");
        markup.ShouldContain("hx-target=\"/api/valid\"");
    }

    [Fact]
    public void HtmxTag_WithComplexChildContent_ShouldRenderCorrectly()
    {
        // Act
        var component = RenderComponent<HtmxTag>(parameters => parameters
            .Add(p => p.Element, "button")
            .Add(p => p.HxPost, "/api/submit")
            .AddChildContent("<span>Click me</span>"));

        // Assert
        component.Markup.ShouldContain("<button");
        component.Markup.ShouldContain("hx-post=\"/api/submit\"");
        component.Markup.ShouldContain("<span>Click me</span>");
        component.Markup.ShouldContain("</button>");
    }
}