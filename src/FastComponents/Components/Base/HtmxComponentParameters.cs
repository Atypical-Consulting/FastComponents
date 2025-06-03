/*
 * Copyright 2025 Atypical Consulting SRL
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
    protected virtual string BuildQueryString()
    {
        throw new InvalidOperationException(
            $"BuildQueryString must be implemented in {GetType().Name}. "
                + $"Consider using the [GenerateParameterMethods] attribute to auto-generate this method.");
    }

    /// <summary>
    /// Creates a new instance of parameters with values bound from the query collection
    /// </summary>
    /// <param name="query">The query collection from the HTTP request</param>
    /// <returns>A new instance with bound values</returns>
    public virtual HtmxComponentParameters BindFromQuery(IQueryCollection query)
    {
        throw new InvalidOperationException(
            $"BindFromQuery must be implemented in {GetType().Name}. "
                + "Consider using the [GenerateParameterMethods] attribute to auto-generate this method.");
    }

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
        return value is not null && int.TryParse(value, out int result) ? result : null;
    }
}
