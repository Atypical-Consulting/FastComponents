using FastEndpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace FastComponents;

public static class MainExtensions
{
    public static IServiceCollection AddFastComponents(this IServiceCollection services)
    {
        services.AddFastEndpoints();
        
        services.AddScoped<HtmlRenderer>();
        services.AddScoped<HtmlBeautifier>();
        services.AddScoped<ComponentHtmlResponseService>();
        return services;
    }
    
    public static IApplicationBuilder UseFastComponents(this IApplicationBuilder app)
    {
        app.UseFastEndpoints();
        return app;
    }
}