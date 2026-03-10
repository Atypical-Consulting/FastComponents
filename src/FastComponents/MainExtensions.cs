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
        _ = services.AddScoped<HtmlRenderer>();
        _ = services.AddScoped<ComponentHtmlResponseService>();
        return services;
    }

    /// <summary>
    /// Use FastComponents in the application
    /// </summary>
    /// <param name="app">The web application</param>
    /// <returns>The web application</returns>
    public static WebApplication UseFastComponents(this WebApplication app)
    {
        // Currently no middleware needed, but this provides extension point
        return app;
    }
}
