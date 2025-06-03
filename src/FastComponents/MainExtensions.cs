using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace FastComponents;

/// <summary>
/// Main extensions for FastComponents
/// </summary>
public static class MainExtensions
{
    /// <summary>
    /// Add FastComponents to the service collection
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection</returns>
    public static IServiceCollection AddFastComponents(this IServiceCollection services)
    {
        services.AddScoped<HtmlRenderer>();
        services.AddScoped<HtmlBeautifier>();
        services.AddScoped<ComponentHtmlResponseService>();
        return services;
    }
    
    /// <summary>
    /// Use FastComponents in the application
    /// </summary>
    /// <param name="app">The web application</param>
    /// <returns>The web application</returns>
    public static WebApplication UseFastComponents(this WebApplication app)
    {
        // No specific middleware needed for minimal API approach
        return app;
    }
}