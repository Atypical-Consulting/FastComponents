using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

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
    public string ToComponentUrl(string route)
    {
        string queryString = BuildQueryString();
        return string.IsNullOrEmpty(queryString) ? route : $"{route}?{queryString}";
    }
    
    /// <summary>
    /// Builds the query string from parameters. Override this method to provide custom query string generation.
    /// </summary>
    /// <returns>The query string representation</returns>
    protected abstract string BuildQueryString();
    
    /// <summary>
    /// Creates a new instance of parameters with values bound from the query collection
    /// </summary>
    /// <param name="query">The query collection from the HTTP request</param>
    /// <returns>A new instance with bound values</returns>
    public abstract HtmxComponentParameters BindFromQuery(IQueryCollection query);
    
    /// <summary>
    /// Helper method to get a string value from query collection
    /// </summary>
    protected static string? GetQueryValue(IQueryCollection query, string key)
    {
        return query.TryGetValue(key, out StringValues value) && !string.IsNullOrEmpty(value)
            ? value.ToString()
            : null;
    }
    
    /// <summary>
    /// Helper method to get an int value from query collection
    /// </summary>
    protected static int? GetQueryInt(IQueryCollection query, string key)
    {
        string? value = GetQueryValue(query, key);
        return value != null && int.TryParse(value, out int result) ? result : null;
    }
}