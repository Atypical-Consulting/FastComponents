/*
 * Copyright 2025 Atypical Consulting SRL
 * Licensed under the Apache License, Version 2.0
 */

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
            List<Type> componentTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && IsHtmxComponent(t))
                .ToList();

            foreach (Type componentType in componentTypes)
            {
                RegisterComponent(app, componentType);
            }
        }

        return app;
    }

    private static bool IsHtmxComponent(Type type)
    {
        return type.IsSubclassOfGeneric(typeof(SimpleHtmxComponent<>)) ||
               type.IsSubclassOf(typeof(SimpleHtmxComponent)) ||
               type.IsSubclassOfGeneric(typeof(HtmxComponentBase<>)) ||
               type.IsSubclassOf(typeof(HtmxComponentBase));
    }

    private static void RegisterComponent(IEndpointRouteBuilder app, Type componentType)
    {
        string route = GetConventionalRoute(componentType);
        
        // Support both GET and POST by default
        MethodInfo? getMethod = typeof(HtmxComponentEndpoints)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "MapHtmxGet" && m.GetParameters().Length == 2);
            
        MethodInfo? postMethod = typeof(HtmxComponentEndpoints)
            .GetMethods()
            .FirstOrDefault(m => m.Name == "MapHtmxPost" && m.GetParameters().Length == 2);

        if (getMethod != null)
        {
            Type? stateType = GetStateType(componentType);
            if (stateType != null)
            {
                MethodInfo genericGetMethod = getMethod.MakeGenericMethod(componentType, stateType);
                object? getEndpoint = genericGetMethod.Invoke(null, [app, route]);
                
                // Add AllowAnonymous if available
                MethodInfo? allowAnonymousMethod = getEndpoint?.GetType().GetMethod("AllowAnonymous");
                allowAnonymousMethod?.Invoke(getEndpoint, null);

                // Also register POST if component has actions
                if (HasActions(componentType))
                {
                    MethodInfo? genericPostMethod = postMethod?.MakeGenericMethod(componentType, stateType);
                    object? postEndpoint = genericPostMethod?.Invoke(null, [app, route]);
                    allowAnonymousMethod = postEndpoint?.GetType().GetMethod("AllowAnonymous");
                    allowAnonymousMethod?.Invoke(postEndpoint, null);
                }
            }
        }
    }

    private static string GetConventionalRoute(Type componentType)
    {
        string name = componentType.Name;
        
        // Remove common suffixes
        if (name.EndsWith("Component"))
            name = name[..^9]; // Remove "Component"
        if (name.EndsWith("Example"))
            name = name[..^7]; // Remove "Example"
            
        // Convert to kebab-case
        string kebabCase = string.Concat(name.Select((x, i) => i > 0 && char.IsUpper(x) ? "-" + x : x.ToString()))
            .ToLowerInvariant();
            
        return $"/htmx/{kebabCase}";
    }

    private static Type? GetStateType(Type componentType)
    {
        // For SimpleHtmxComponent<T>, get T
        Type? baseType = componentType.BaseType;
        while (baseType != null)
        {
            if (baseType.IsGenericType)
            {
                Type genericDef = baseType.GetGenericTypeDefinition();
                if (genericDef == typeof(SimpleHtmxComponent<>) || 
                    genericDef == typeof(HtmxComponentBase<>))
                {
                    return baseType.GetGenericArguments()[0];
                }
            }
            baseType = baseType.BaseType;
        }
        
        return null;
    }

    private static bool HasActions(Type componentType)
    {
        // Check if the component has methods that look like actions
        return componentType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Any(m => m.Name.StartsWith("On") || m.Name.EndsWith("Action") || m.ReturnType == typeof(string));
    }

    private static bool IsSubclassOfGeneric(this Type child, Type parent)
    {
        if (child == parent)
            return false;

        if (child.IsSubclassOf(parent))
            return true;

        Type[] parameters = parent.GetGenericArguments();
        bool isParameterLessGeneric = !(parameters.Length > 0 &&
                                        ((parameters[0].Attributes & TypeAttributes.BeforeFieldInit) == TypeAttributes.BeforeFieldInit));

        while (child != null && child != typeof(object))
        {
            Type cur = GetFullTypeDefinition(child);
            if (parent == cur || (isParameterLessGeneric && cur.GetInterfaces().Select(GetFullTypeDefinition).Contains(GetFullTypeDefinition(parent))))
                return true;
            else if (!isParameterLessGeneric)
                if (GetFullTypeDefinition(parent) == cur && !cur.IsInterface)
                {
                    if (VerifyGenericArguments(GetFullTypeDefinition(parent), cur))
                        if (VerifyGenericArguments(parent, child))
                            return true;
                }
            child = child.BaseType;
        }

        return false;
    }

    private static Type GetFullTypeDefinition(Type type)
    {
        return type.IsGenericType ? type.GetGenericTypeDefinition() : type;
    }

    private static bool VerifyGenericArguments(Type parent, Type child)
    {
        Type[] childArguments = child.GetGenericArguments();
        Type[] parentArguments = parent.GetGenericArguments();
        if (childArguments.Length != parentArguments.Length)
            return true;

        for (int i = 0; i < childArguments.Length; i++)
        {
            if (childArguments[i].Assembly != parentArguments[i].Assembly || childArguments[i].Name != parentArguments[i].Name || childArguments[i].Namespace != parentArguments[i].Namespace)
                if (!childArguments[i].IsSubclassOf(parentArguments[i]))
                    return false;
        }

        return true;
    }
}
