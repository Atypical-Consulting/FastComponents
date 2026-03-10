/*
 * Copyright 2025 Atypical Consulting SRL
 * Licensed under the Apache License, Version 2.0
 */

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.AspNetCore.Routing;

namespace FastComponents;

/// <summary>
/// Extension methods for convention-based component registration
/// </summary>
public static class ConventionBasedRegistration
{
    /// <summary>
    /// Automatically maps all HTMX components in the specified assemblies using conventions
    /// </summary>
    /// <param name="app">The endpoint route builder</param>
    /// <param name="assemblies">Assemblies to scan for components (defaults to calling assembly)</param>
    /// <returns>The endpoint route builder for chaining</returns>
    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2072:UnrecognizedReflectionPattern",
        Justification = "Convention-based registration requires reflection")]
    public static IEndpointRouteBuilder MapHtmxComponentsByConvention(
        this IEndpointRouteBuilder app,
        params Assembly[] assemblies)
    {
        if (assemblies.Length == 0)
        {
            assemblies = [Assembly.GetCallingAssembly()];
        }

        foreach (Assembly assembly in assemblies)
        {
            Type[] componentTypes = [.. assembly.GetTypes()
                .Where(t => t is { IsClass: true, IsAbstract: false } && IsHtmxComponent(t))];

            foreach (Type componentType in componentTypes)
            {
                RegisterComponent(app, componentType);
            }
        }

        return app;
    }

    private static bool IsHtmxComponent(Type type)
    {
        return type.IsSubclassOfGeneric(typeof(SimpleHtmxComponent<>))
            || type.IsSubclassOf(typeof(SimpleHtmxComponent))
            || type.IsSubclassOfGeneric(typeof(HtmxComponentBase<>))
            || type.IsSubclassOf(typeof(HtmxComponentBase));
    }

    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2060:MakeGenericMethod",
        Justification = "Convention-based registration requires reflection")]
    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2075:UnrecognizedReflectionPattern",
        Justification = "Convention-based registration requires reflection")]
    private static void RegisterComponent(
        IEndpointRouteBuilder app,
        [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type componentType)
    {
        string route = GetConventionalRoute(componentType);

        // Support both GET and POST by default
        MethodInfo? getMethod = typeof(HtmxComponentEndpoints)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "MapHtmxGet" && m.GetParameters().Length == 2);

        MethodInfo? postMethod = typeof(HtmxComponentEndpoints)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "MapHtmxPost" && m.GetParameters().Length == 2);

        if (getMethod is null)
        {
            return;
        }

        Type? stateType = GetStateType(componentType);
        if (stateType is null)
        {
            return;
        }

        MethodInfo genericGetMethod = getMethod.MakeGenericMethod(componentType, stateType);
        object? getEndpoint = genericGetMethod.Invoke(null, [app, route]);

        // Add AllowAnonymous if available
        if (getEndpoint is not null)
        {
            MethodInfo? allowAnonymousMethod = getEndpoint.GetType().GetMethod("AllowAnonymous");
            _ = allowAnonymousMethod?.Invoke(getEndpoint, null);
        }

        // Also register POST if component has actions
        if (HasActions(componentType))
        {
            MethodInfo? genericPostMethod = postMethod?.MakeGenericMethod(componentType, stateType);
            object? postEndpoint = genericPostMethod?.Invoke(null, [app, route]);
            if (postEndpoint is not null)
            {
                MethodInfo? allowAnonymousMethod = postEndpoint.GetType().GetMethod("AllowAnonymous");
                _ = allowAnonymousMethod?.Invoke(postEndpoint, null);
            }
        }
    }

    private static string GetConventionalRoute(Type componentType)
    {
        string name = componentType.Name;

        // Remove common suffixes
        if (name.EndsWith("Component", StringComparison.Ordinal))
        {
            name = name[..^9]; // Remove "Component"
        }

        if (name.EndsWith("Example", StringComparison.Ordinal))
        {
            name = name[..^7]; // Remove "Example"
        }

        // Convert to kebab-case
        string kebabCase = string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x : x.ToString()))
            .ToLowerInvariant();

        return $"/htmx/{kebabCase}";
    }

    private static Type? GetStateType(Type componentType)
    {
        // For SimpleHtmxComponent<T>, get T
        Type? baseType = componentType.BaseType;
        while (baseType is not null)
        {
            if (baseType.IsGenericType)
            {
                Type genericDef = baseType.GetGenericTypeDefinition();
                if (genericDef == typeof(SimpleHtmxComponent<>)
                    || genericDef == typeof(HtmxComponentBase<>))
                {
                    return baseType.GetGenericArguments()[0];
                }
            }

            baseType = baseType.BaseType;
        }

        return null;
    }

    private static bool HasActions([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] Type componentType)
    {
        // Check if the component has methods that look like actions
        return componentType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Any(m => m.Name.StartsWith("On", StringComparison.Ordinal)
                || m.Name.EndsWith("Action", StringComparison.Ordinal)
                || m.ReturnType == typeof(string));
    }

    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2075:UnrecognizedReflectionPattern",
        Justification = "Convention-based registration requires reflection")]
    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2067:UnrecognizedReflectionPattern",
        Justification = "Convention-based registration requires reflection")]
    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2111:RequiresDynamicCode",
        Justification = "Convention-based registration requires reflection")]
    private static bool IsSubclassOfGeneric(this Type child, Type parent)
    {
        if (child == parent)
        {
            return false;
        }

        if (child.IsSubclassOf(parent))
        {
            return true;
        }

        Type[] parameters = parent.GetGenericArguments();
        bool isParameterLessGeneric = !(parameters.Length > 0
            && ((parameters[0].Attributes & TypeAttributes.BeforeFieldInit) == TypeAttributes.BeforeFieldInit));

        while (child is not null && child != typeof(object))
        {
            Type cur = GetFullTypeDefinition(child);
            if (parent == cur
                || (isParameterLessGeneric
                    && cur.GetInterfaces()
                        .Select(GetFullTypeDefinition)
                        .Contains(GetFullTypeDefinition(parent))))
            {
                return true;
            }
            else if (!isParameterLessGeneric)
            {
                if (GetFullTypeDefinition(parent) == cur && !cur.IsInterface)
                {
                    if (VerifyGenericArguments(GetFullTypeDefinition(parent), cur))
                    {
                        if (VerifyGenericArguments(parent, child))
                        {
                            return true;
                        }
                    }
                }
            }

            child = child.BaseType!;
        }

        return false;
    }

    [UnconditionalSuppressMessage(
        "Trimming",
        "IL2075:UnrecognizedReflectionPattern",
        Justification = "Convention-based registration requires reflection")]
    private static Type GetFullTypeDefinition([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type type)
    {
        return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
    }

    private static bool VerifyGenericArguments(Type parent, Type child)
    {
        Type[] childArguments = child.GetGenericArguments();
        Type[] parentArguments = parent.GetGenericArguments();
        if (childArguments.Length != parentArguments.Length)
        {
            return true;
        }

        return !childArguments
            .Where((t, i) =>
                (t.Assembly != parentArguments[i].Assembly
                    || t.Name != parentArguments[i].Name
                    || t.Namespace != parentArguments[i].Namespace)
                    && !t.IsSubclassOf(parentArguments[i]))
            .Any();
    }
}
