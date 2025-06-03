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

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.HtmlRendering;
using Microsoft.AspNetCore.Http;

namespace FastComponents;

/// <summary>
/// Service responsible for rendering components as HTML and returning them as HTTP responses.
/// </summary>
public class ComponentHtmlResponseService(HtmlRenderer htmlRenderer)
{
    /// <summary>
    /// Renders a component as HTML and returns it as an HTTP content result.
    /// </summary>
    /// <typeparam name="TComponent">The type of the Blazor component to render.</typeparam>
    /// <param name="parameters">Optional parameters to pass to the component during rendering.</param>
    /// <returns>An <see cref="IResult"/> representing the HTTP content result of the rendered HTML.</returns>
    public async Task<IResult> RenderAsHtmlContentAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
        Dictionary<string, object?>? parameters = null)
        where TComponent : HtmxComponentBase
    {
        string html = await RenderComponentAsync<TComponent>(parameters).ConfigureAwait(false);
        string beautified = HtmlBeautifier.BeautifyHtml(html);
        return Results.Content(beautified, "text/html", Encoding.UTF8);
    }

    /// <summary>
    /// Renders a component T
    /// </summary>
    /// <param name="dictionary">The dictionary of parameters to pass to the component</param>
    /// <typeparam name="TComponent">The component to render</typeparam>
    /// <returns>The rendered component as a string</returns>
    public Task<string> RenderComponentAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
        Dictionary<string, object?>? dictionary = null)
        where TComponent : HtmxComponentBase
    {
        ParameterView parameters = dictionary is null
            ? ParameterView.Empty
            : ParameterView.FromDictionary(dictionary);

        return RenderComponentAsync<TComponent>(parameters);
    }

    /// <summary>
    /// Use the default dispatcher to invoke actions in the context of the 
    /// static HTML renderer and return as a string
    /// </summary>
    /// <param name="parameters">The parameters to pass to the component</param>
    /// <typeparam name="TComponent">The component to render</typeparam>
    /// <returns>The rendered component as a string</returns>
    private Task<string> RenderComponentAsync<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
        ParameterView parameters)
        where TComponent : HtmxComponentBase
    {
        return htmlRenderer.Dispatcher.InvokeAsync(Callback);

        async Task<string> Callback()
        {
            HtmlRootComponent output = await htmlRenderer
                .RenderComponentAsync<TComponent>(parameters)
                .ConfigureAwait(false);

            return output.ToHtmlString();
        }
    }
}
