using System;

namespace FastComponents;

/// <summary>
/// Indicates that the parameter methods (BuildQueryString and BindFromQuery) should be automatically generated.
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public sealed class GenerateParameterMethodsAttribute : Attribute
{
    /// <summary>
    /// Gets or sets whether to generate code that skips default values in query strings.
    /// </summary>
    public bool SkipDefaults { get; set; } = true;
}