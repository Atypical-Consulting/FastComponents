/*
 * Copyright 2025 Atypical Consulting SRL
 * Licensed under the Apache License, Version 2.0
 */

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Components;

namespace FastComponents;

/// <summary>
/// Simplified base class for HTMX components with convention-based routing
/// </summary>
/// <typeparam name="TState">The state/parameters type</typeparam>
public abstract class SimpleHtmxComponent<TState> : ComponentBase 
    where TState : class, new()
{
    /// <summary>
    /// The current state/parameters for this component
    /// </summary>
    [Parameter] public TState State { get; set; } = new();

    /// <summary>
    /// Creates a URL for this component with updated state
    /// </summary>
    public string Url(TState? newState = null) 
    {
        TState state = newState ?? State;
        string componentName = GetType().Name.Replace("Component", "").ToLowerInvariant();
        return $"/htmx/{componentName}?" + ToQueryString(state);
    }

    /// <summary>
    /// Creates a URL for this component with a state update function
    /// </summary>
    public string Url(Func<TState, TState> updateState)
    {
        return Url(updateState(State));
    }

    /// <summary>
    /// Gets the route for this component based on convention
    /// </summary>
    public string GetRoute() => $"/htmx/{GetType().Name.Replace("Component", "").ToLowerInvariant()}";

    [RequiresUnreferencedCode("Uses reflection to access object properties")]
    private static string ToQueryString(object obj)
    {
        string[] properties = [.. obj.GetType().GetProperties()
            .Where(p => p.CanRead && p.GetValue(obj) != null)
            .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(obj)?.ToString() ?? "")}")];
        return string.Join("&", properties);
    }
}

/// <summary>
/// Simplified component for stateless scenarios
/// </summary>
public abstract class SimpleHtmxComponent : ComponentBase
{
    /// <summary>
    /// Gets the route for this component based on convention
    /// </summary>
    public static string GetRoute<T>() where T : SimpleHtmxComponent => $"/htmx/{nameof(T).Replace("Component", "").ToLowerInvariant()}";
}