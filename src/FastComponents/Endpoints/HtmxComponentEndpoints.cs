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
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FastComponents;

/// <summary>
/// Extension methods for registering HTMX component endpoints with ASP.NET Minimal APIs
/// </summary>
public static class HtmxComponentEndpoints
{
    /// <summary>
    /// Maps a GET endpoint for an HTMX component without parameters
    /// </summary>
    /// <typeparam name="TComponent">The component type to render</typeparam>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <param name="pattern">The route pattern</param>
    /// <returns>The route handler builder for further configuration</returns>
    [RequiresUnreferencedCode("Component endpoints may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("Component endpoints may require runtime code generation.")]
    public static RouteHandlerBuilder MapHtmxGet<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
            this IEndpointRouteBuilder endpoints, string pattern)
        where TComponent : HtmxComponentBase
    {
        return endpoints.MapGet(
            pattern,
            (ComponentHtmlResponseService service)
                => service.RenderAsHtmlContentAsync<TComponent>());
    }

    /// <summary>
    /// Maps a GET endpoint for an HTMX component with parameters
    /// </summary>
    /// <typeparam name="TComponent">The component type to render</typeparam>
    /// <typeparam name="TParameters">The parameters type</typeparam>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <param name="pattern">The route pattern</param>
    /// <returns>The route handler builder for further configuration</returns>
    [RequiresUnreferencedCode("Component endpoints may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("Component endpoints may require runtime code generation.")]
    public static RouteHandlerBuilder MapHtmxGet<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent, TParameters>(
            this IEndpointRouteBuilder endpoints, string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapGet(
            pattern,
            (ComponentHtmlResponseService service, HttpContext context) =>
            {
                // Create new parameters and bind from query
                TParameters baseParams = new();
                HtmxComponentParameters boundParams = baseParams.BindFromQuery(context.Request.Query);
                Dictionary<string, object?> componentParameters = new()
                    { [nameof(HtmxComponentBase<TParameters>.Parameters)] = boundParams };
                return service.RenderAsHtmlContentAsync<TComponent>(componentParameters);
            });
    }

    /// <summary>
    /// Maps a POST endpoint for an HTMX component without parameters
    /// </summary>
    /// <typeparam name="TComponent">The component type to render</typeparam>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <param name="pattern">The route pattern</param>
    /// <returns>The route handler builder for further configuration</returns>
    [RequiresUnreferencedCode("Component endpoints may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("Component endpoints may require runtime code generation.")]
    public static RouteHandlerBuilder MapHtmxPost<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
            this IEndpointRouteBuilder endpoints, string pattern)
        where TComponent : HtmxComponentBase
    {
        return endpoints.MapPost(
            pattern,
            (ComponentHtmlResponseService service) => service.RenderAsHtmlContentAsync<TComponent>());
    }

    /// <summary>
    /// Maps a POST endpoint for an HTMX component with parameters
    /// </summary>
    /// <typeparam name="TComponent">The component type to render</typeparam>
    /// <typeparam name="TParameters">The parameters type</typeparam>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <param name="pattern">The route pattern</param>
    /// <returns>The route handler builder for further configuration</returns>
    [RequiresUnreferencedCode("Component endpoints may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("Component endpoints may require runtime code generation.")]
    public static RouteHandlerBuilder MapHtmxPost<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent, TParameters>(
            this IEndpointRouteBuilder endpoints, string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapPost(
            pattern,
            (
                ComponentHtmlResponseService service,
                HttpContext context,
                TParameters parameters) =>
            {
                // For POST, parameters come from body, but also check query
                HtmxComponentParameters boundParams = parameters.BindFromQuery(context.Request.Query);

                const string key = nameof(HtmxComponentBase<TParameters>.Parameters);
                Dictionary<string, object?> componentParameters = new() { [key] = boundParams };

                return service.RenderAsHtmlContentAsync<TComponent>(componentParameters);
            });
    }

    /// <summary>
    /// Maps a PUT endpoint for an HTMX component with parameters
    /// </summary>
    /// <typeparam name="TComponent">The component type to render</typeparam>
    /// <typeparam name="TParameters">The parameters type</typeparam>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <param name="pattern">The route pattern</param>
    /// <returns>The route handler builder for further configuration</returns>
    [RequiresUnreferencedCode("Component endpoints may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("Component endpoints may require runtime code generation.")]
    public static RouteHandlerBuilder MapHtmxPut<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent, TParameters>(
            this IEndpointRouteBuilder endpoints, string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapPut(
            pattern,
            (
                ComponentHtmlResponseService service,
                TParameters parameters) =>
            {
                const string key = nameof(HtmxComponentBase<TParameters>.Parameters);
                Dictionary<string, object?> componentParameters = new() { [key] = parameters };

                return service.RenderAsHtmlContentAsync<TComponent>(componentParameters);
            });
    }

    /// <summary>
    /// Maps a DELETE endpoint for an HTMX component with parameters
    /// </summary>
    /// <typeparam name="TComponent">The component type to render</typeparam>
    /// <typeparam name="TParameters">The parameters type</typeparam>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <param name="pattern">The route pattern</param>
    /// <returns>The route handler builder for further configuration</returns>
    [RequiresUnreferencedCode("Component endpoints may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("Component endpoints may require runtime code generation.")]
    public static RouteHandlerBuilder MapHtmxDelete<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent, TParameters>(
            this IEndpointRouteBuilder endpoints, string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapDelete(
            pattern,
            (ComponentHtmlResponseService service, TParameters parameters) =>
            {
                Dictionary<string, object?> componentParameters =
                    new() { [nameof(HtmxComponentBase<TParameters>.Parameters)] = parameters };
                return service.RenderAsHtmlContentAsync<TComponent>(componentParameters);
            });
    }

    /// <summary>
    /// Maps a PATCH endpoint for an HTMX component with parameters
    /// </summary>
    /// <typeparam name="TComponent">The component type to render</typeparam>
    /// <typeparam name="TParameters">The parameters type</typeparam>
    /// <param name="endpoints">The endpoint route builder</param>
    /// <param name="pattern">The route pattern</param>
    /// <returns>The route handler builder for further configuration</returns>
    [RequiresUnreferencedCode("Component endpoints may require types that cannot be statically analyzed.")]
    [RequiresDynamicCode("Component endpoints may require runtime code generation.")]
    public static RouteHandlerBuilder MapHtmxPatch<
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent, TParameters>(
            this IEndpointRouteBuilder endpoints, string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapPatch(
            pattern,
            (
                ComponentHtmlResponseService service,
                TParameters parameters) =>
            {
                const string key = nameof(HtmxComponentBase<TParameters>.Parameters);
                Dictionary<string, object?> componentParameters = new() { [key] = parameters };

                return service.RenderAsHtmlContentAsync<TComponent>(componentParameters);
            });
    }
}
