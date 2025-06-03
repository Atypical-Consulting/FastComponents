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
using Shouldly;

namespace FastComponents.UnitTests;

public class MainExtensionsTests
{
    [Fact]
    public void AddFastComponents_ShouldRegisterRequiredServices()
    {
        // Arrange
        ServiceCollection services = [];
        services.AddLogging(); // Add required logging dependencies

        // Act
        IServiceCollection result = services.AddFastComponents();

        // Assert
        result.ShouldBe(services);

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetService<HtmlRenderer>().ShouldNotBeNull();
        serviceProvider.GetService<ComponentHtmlResponseService>().ShouldNotBeNull();
    }

    [Fact]
    public void AddFastComponents_ShouldReturnSameServiceCollection()
    {
        // Arrange
        ServiceCollection services = [];

        // Act
        IServiceCollection result = services.AddFastComponents();

        // Assert
        result.ShouldBeSameAs(services);
    }

    [Fact]
    public void AddFastComponents_WithExistingServices_ShouldNotThrow()
    {
        // Arrange
        ServiceCollection services = [];
        services.AddScoped<HtmlRenderer>();

        // Act & Assert
        Should.NotThrow(() => services.AddFastComponents());
    }

    [Fact]
    public void UseFastComponents_ShouldReturnSameWebApplication()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        WebApplication app = builder.Build();

        // Act
        WebApplication result = app.UseFastComponents();

        // Assert
        result.ShouldBeSameAs(app);
    }

    [Fact]
    public void UseFastComponents_ShouldNotThrow()
    {
        // Arrange
        WebApplicationBuilder builder = WebApplication.CreateBuilder();
        WebApplication app = builder.Build();

        // Act & Assert
        Should.NotThrow(() => app.UseFastComponents());
    }
}
