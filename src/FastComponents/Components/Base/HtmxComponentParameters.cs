using System.Diagnostics.CodeAnalysis;

namespace FastComponents;

/// <summary>
/// Base class for HTMX component parameters
/// </summary>
public abstract record HtmxComponentParameters
{
    /// <summary>
    /// Converts the parameters to a component URL with query string
    /// </summary>
    /// <param name="route">The route to append parameters to</param>
    /// <returns>The full URL with query string</returns>
    protected virtual string ToComponentUrl(string route)
        => $"{route}?{ToQueryString()}";
    
    /// <summary>
    /// Converts the parameters to a query string
    /// </summary>
    /// <returns>The query string representation</returns>
    [RequiresUnreferencedCode("Query string generation uses reflection to access parameter properties.")]
    [RequiresDynamicCode("Query string generation may require runtime code generation.")]
    [UnconditionalSuppressMessage("Trimming", "IL2075", Justification = "Parameters are designed to be serialized via reflection")]
    protected virtual string ToQueryString()
    {
        IEnumerable<string> properties = GetType().GetProperties()
            .Where(p => p.GetValue(this) != null)
            .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(this)!.ToString()!)}");
        
        return string.Join("&", properties);
    }
}