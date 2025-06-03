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
    public static RouteHandlerBuilder MapHtmxGet<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase
    {
        return endpoints.MapGet(pattern, async (ComponentHtmlResponseService service) =>
        {
            return await service.RenderAsHtmlContent<TComponent>();
        });
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent,
        TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapGet(pattern, async (
            ComponentHtmlResponseService service,
            HttpContext context) =>
        {
            // Create new parameters and bind from query
            TParameters baseParams = new();
            HtmxComponentParameters boundParams = baseParams.BindFromQuery(context.Request.Query);
            
            Dictionary<string, object?> componentParameters = new()
            { 
                [nameof(HtmxComponentBase<TParameters>.Parameters)] = boundParams 
            };
            
            return await service.RenderAsHtmlContent<TComponent>(componentParameters);
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
    public static RouteHandlerBuilder MapHtmxPost<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase
    {
        return endpoints.MapPost(pattern, async (ComponentHtmlResponseService service) =>
        {
            return await service.RenderAsHtmlContent<TComponent>();
        });
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent,
        TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapPost(pattern, async (
            ComponentHtmlResponseService service,
            HttpContext context,
            TParameters parameters) =>
        {
            // For POST, parameters come from body, but also check query
            HtmxComponentParameters boundParams = parameters.BindFromQuery(context.Request.Query);
            
            Dictionary<string, object?> componentParameters = new()
            { 
                [nameof(HtmxComponentBase<TParameters>.Parameters)] = boundParams 
            };
            
            return await service.RenderAsHtmlContent<TComponent>(componentParameters);
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent,
        TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapPut(pattern, async (
            ComponentHtmlResponseService service,
            TParameters parameters) =>
        {
            Dictionary<string, object?> componentParameters = new()
            { 
                [nameof(HtmxComponentBase<TParameters>.Parameters)] = parameters 
            };
            
            return await service.RenderAsHtmlContent<TComponent>(componentParameters);
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent,
        TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapDelete(pattern, async (
            ComponentHtmlResponseService service,
            TParameters parameters) =>
        {
            Dictionary<string, object?> componentParameters = new()
            { 
                [nameof(HtmxComponentBase<TParameters>.Parameters)] = parameters 
            };
            
            return await service.RenderAsHtmlContent<TComponent>(componentParameters);
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] TComponent,
        TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : HtmxComponentParameters, new()
    {
        return endpoints.MapPatch(pattern, async (
            ComponentHtmlResponseService service,
            TParameters parameters) =>
        {
            Dictionary<string, object?> componentParameters = new()
            { 
                [nameof(HtmxComponentBase<TParameters>.Parameters)] = parameters 
            };
            
            return await service.RenderAsHtmlContent<TComponent>(componentParameters);
        });
    }
}