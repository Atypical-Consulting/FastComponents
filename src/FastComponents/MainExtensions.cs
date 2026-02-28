// Copyright (c) Atypical Consulting SRL. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using FastEndpoints;
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
        _ = services.AddFastEndpoints();

        _ = services.AddScoped<HtmlRenderer>();
        _ = services.AddScoped<HtmlBeautifier>();
        _ = services.AddScoped<ComponentHtmlResponseService>();
        return services;
    }
    
    /// <summary>
    /// Use FastComponents in the application
    /// </summary>
    /// <param name="app">The application builder</param>
    /// <returns>The application builder</returns>
    public static IApplicationBuilder UseFastComponents(this IApplicationBuilder app)
    {
        _ = app.UseFastEndpoints();
        return app;
    }
}