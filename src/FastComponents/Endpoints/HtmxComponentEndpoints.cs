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
    public static RouteHandlerBuilder MapHtmxGet<TComponent>(
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
    public static RouteHandlerBuilder MapHtmxGet<TComponent, TParameters>(
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
            await context.Request.BindAsync(parameters);
            
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
    public static RouteHandlerBuilder MapHtmxPost<TComponent>(
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
    public static RouteHandlerBuilder MapHtmxPost<TComponent, TParameters>(
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
    public static RouteHandlerBuilder MapHtmxPut<TComponent, TParameters>(
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
    public static RouteHandlerBuilder MapHtmxDelete<TComponent, TParameters>(
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
    public static RouteHandlerBuilder MapHtmxPatch<TComponent, TParameters>(
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
    private static async Task BindAsync<T>(this HttpRequest request, T target) where T : class
    {
        await Task.CompletedTask; // Make async to match signature pattern
        
        PropertyInfo[] properties = typeof(T).GetProperties();
        foreach (PropertyInfo property in properties)
        {
            if (request.Query.TryGetValue(property.Name, out StringValues value))
            {
                try
                {
                    object convertedValue = Convert.ChangeType(value.ToString(), property.PropertyType);
                    property.SetValue(target, convertedValue);
                }
                catch
                {
                    // Ignore conversion errors for now
                }
            }
        }
    }
}
