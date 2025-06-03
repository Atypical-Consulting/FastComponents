using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;

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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.PublicConstructors)] TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : class, new()
    {
        return endpoints.MapGet(pattern, async (
            ComponentHtmlResponseService service,
            HttpContext context) =>
        {
            // Bind query parameters to the parameters type
            TParameters parameters = new();
            await BindQueryParameters(context.Request, parameters);
            
            Dictionary<string, object?> componentParameters = new()
            { 
                [nameof(HtmxComponentBase<TParameters>.Parameters)] = parameters 
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : class, new()
    {
        return endpoints.MapPost(pattern, async (
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : class, new()
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : class, new()
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
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] TParameters>(
        this IEndpointRouteBuilder endpoints,
        string pattern)
        where TComponent : HtmxComponentBase<TParameters>
        where TParameters : class, new()
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

    // Helper method to bind query parameters to an object
    [UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Properties are preserved by DynamicallyAccessedMembers attribute")]
    private static Task BindQueryParameters<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(
        HttpRequest request, 
        T target) where T : class
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (request.Query.TryGetValue(property.Name, out StringValues value) && !string.IsNullOrEmpty(value))
            {
                try
                {
                    object? convertedValue = Convert.ChangeType(value.ToString(), property.PropertyType);
                    property.SetValue(target, convertedValue);
                }
                catch
                {
                    // Ignore conversion errors - property will keep its default value
                }
            }
        }
        
        return Task.CompletedTask;
    }
}