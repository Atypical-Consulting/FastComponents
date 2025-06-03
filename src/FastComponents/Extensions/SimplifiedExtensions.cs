/*
 * Copyright 2025 Atypical Consulting SRL
 * Licensed under the Apache License, Version 2.0
 */

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FastComponents;

/// <summary>
/// Simplified extension methods for the easiest possible setup
/// </summary>
public static class SimplifiedExtensions
{
    /// <summary>
    /// Adds FastComponents with automatic component discovery and registration
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddFastComponentsAuto(this IServiceCollection services)
    {
        // Add core FastComponents services
        services.AddFastComponents();
        
        // TODO: Add automatic JSON context generation for discovered component states
        
        return services;
    }

    /// <summary>
    /// Maps all HTMX components automatically using conventions
    /// One line replaces all the manual MapHtmx* calls
    /// </summary>
    /// <param name="app">The web application</param>
    /// <returns>The web application for chaining</returns>
    public static WebApplication UseFastComponentsAuto(this WebApplication app)
    {
        // Auto-discover and register all HTMX components
        app.MapHtmxComponentsByConvention();
        
        return app;
    }
}

/// <summary>
/// Smart defaults for common HTMX patterns
/// </summary>
public static class HtmxDefaults
{
    /// <summary>
    /// Default swap mode for most updates
    /// </summary>
    public static string Swap => "outerHTML";
    
    /// <summary>
    /// Default trigger for search inputs
    /// </summary>
    public static string SearchTrigger => "keyup changed delay:300ms, search";
    
    /// <summary>
    /// Default trigger for load-once content
    /// </summary>
    public static string LoadOnceTrigger => "load once";
}

/// <summary>
/// Common HTMX patterns as static methods
/// </summary>
public static class HtmxPatterns
{
    /// <summary>
    /// Creates attributes for a self-updating button
    /// </summary>
    public static Dictionary<string, object> SelfUpdatingButton(string url, string id) => new()
    {
        ["hx-get"] = url,
        ["hx-target"] = $"#{id}",
        ["hx-swap"] = HtmxDefaults.Swap,
        ["id"] = id
    };

    /// <summary>
    /// Creates attributes for a search input
    /// </summary>
    public static Dictionary<string, object> SearchInput(string url, string target) => new()
    {
        ["hx-get"] = url,
        ["hx-target"] = target,
        ["hx-trigger"] = HtmxDefaults.SearchTrigger,
        ["hx-indicator"] = "#loading"
    };

    /// <summary>
    /// Creates attributes for load-once content
    /// </summary>
    public static Dictionary<string, object> LoadOnce(string url) => new()
    {
        ["hx-get"] = url,
        ["hx-trigger"] = HtmxDefaults.LoadOnceTrigger
    };
}