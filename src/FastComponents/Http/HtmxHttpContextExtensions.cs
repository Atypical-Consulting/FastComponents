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

namespace FastComponents.Http;

/// <summary>
/// Extension methods for working with HTMX headers on HttpContext
/// </summary>
public static class HtmxHttpContextExtensions
{
    private const string HtmxRequestHeadersKey = "HtmxRequestHeaders";
    private const string HtmxResponseHeadersKey = "HtmxResponseHeaders";

    /// <summary>
    /// Gets the HTMX request headers from the current request
    /// </summary>
    public static HtmxRequestHeaders GetHtmxRequestHeaders(this HttpContext context)
    {
        if (!context.Items.TryGetValue(HtmxRequestHeadersKey, out object? headers))
        {
            headers = new HtmxRequestHeaders(context);
            context.Items[HtmxRequestHeadersKey] = headers;
        }

        return (HtmxRequestHeaders)headers!;
    }

    /// <summary>
    /// Gets the HTMX response headers for the current response
    /// </summary>
    public static HtmxResponseHeaders GetHtmxResponseHeaders(this HttpContext context)
    {
        if (!context.Items.TryGetValue(HtmxResponseHeadersKey, out object? headers))
        {
            headers = new HtmxResponseHeaders(context);
            context.Items[HtmxResponseHeadersKey] = headers;
        }

        return (HtmxResponseHeaders)headers!;
    }

    /// <summary>
    /// Checks if the current request is an HTMX request
    /// </summary>
    public static bool IsHtmxRequest(this HttpContext context)
    {
        return context.GetHtmxRequestHeaders().IsHtmxRequest;
    }

    /// <summary>
    /// Checks if the current request is an HTMX boosted request
    /// </summary>
    public static bool IsHtmxBoostedRequest(this HttpContext context)
    {
        return context.GetHtmxRequestHeaders().IsBoosted;
    }
}
