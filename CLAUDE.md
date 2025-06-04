# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

FastComponents is a .NET 9.0 Razor Class Library that enables building HTMX-powered web applications using Blazor components with server-side rendering. It provides type-safe C# properties for all HTMX attributes and integrates with ASP.NET Minimal APIs for endpoint routing.

The library simplifies building Multiple Resources Applications (MRA) by combining the component model of Blazor with the simplicity of HTMX, allowing developers to build dynamic web applications without writing JavaScript.

## Common Development Commands

```bash
# Build the solution
dotnet build

# Build in Release mode
dotnet build --configuration Release

# Run all tests
dotnet test

# Run tests with coverage and generate HTML report
./coverage.sh

# Run specific test project
dotnet test tests/FastComponents.UnitTests/FastComponents.UnitTests.csproj
dotnet test tests/FastComponents.Generators.UnitTests/FastComponents.Generators.UnitTests.csproj

# Run a single test
dotnet test --filter "FullyQualifiedName~TestMethodName"
dotnet test --filter "DisplayName~TestMethodName"

# Pack NuGet package
dotnet pack --configuration Release

# Run the demo application
dotnet run --project demo/HtmxAppServer/HtmxAppServer.csproj

# Watch mode for development
dotnet watch --project demo/HtmxAppServer/HtmxAppServer.csproj

# Clean build artifacts
dotnet clean
```

## Architecture

### Core Components

- **HtmxComponentBase** (`src/FastComponents/Components/Base/HtmxComponentBase.cs`) - Base class providing HTMX attribute properties for all components
- **SimpleHtmxComponent** (`src/FastComponents/Components/Base/SimpleHtmxComponent.cs`) - Simplified base class with automatic route generation
- **ComponentHtmlResponseService** (`src/FastComponents/Services/ComponentHtmlResponseService.cs`) - Renders Blazor components as HTML responses for HTMX requests
- **HtmxComponentEndpoints** (`src/FastComponents/Endpoints/HtmxComponentEndpoints.cs`) - ASP.NET Minimal API integration for component routing
- **ClassNamesBuilder** (`src/FastComponents/Components/Base/ClassNamesBuilder.cs`) - Fluent API for building CSS class names
- **HtmxBuilder** (`src/FastComponents/Builders/HtmxBuilder.cs`) - Fluent builder for creating HTMX elements programmatically
- **FastComponents.Generators** (`src/FastComponents.Generators/`) - Source generators for parameter method generation

### HTMX Integration Pattern

Components inherit from `HtmxComponentBase` to gain access to HTMX attributes:
- Core attributes: HxGet, HxPost, HxTrigger, HxTarget, HxSwap
- Additional attributes: HxConfirm, HxDisable, HxIndicator, etc.
- CSS classes: HxCssAdded, HxCssRequest, HxCssSwapping, etc.

### MRA (Multiple Resources Application) Architecture

The project follows an MRA pattern where:
1. Server renders Blazor components as HTML
2. HTMX handles dynamic updates without full page reloads
3. ASP.NET Minimal APIs provide endpoints for component requests

### Simplified API and Convention-Based Registration

FastComponents provides two API approaches:

1. **Explicit Registration** - Traditional approach with manual endpoint mapping:
   ```csharp
   app.MapHtmxGet<CounterComponent, CounterState>("/htmx/counter");
   ```

2. **Convention-Based Registration** - Automatic discovery and registration:
   ```csharp
   // In Program.cs
   builder.Services.AddFastComponentsAuto();
   app.UseFastComponentsAuto();
   ```
   
   Components are automatically discovered and mapped based on naming conventions:
   - `CounterComponent` → `/htmx/counter`
   - `MovieCharactersExample` → `/htmx/movie-characters`
   - Routes are kebab-cased with common suffixes removed

The convention-based approach is implemented in `ConventionBasedRegistration.cs` and `SimplifiedExtensions.cs`.

## Key Dependencies

- **.NET 9.0** - Target framework
- **AngleSharp 1.3.0** - HTML parsing and beautification
- **HTMX** - Client-side library for dynamic HTML (included in wwwroot)

## Code Quality

The project enforces strict code analysis:
- All .NET analyzers enabled with "All" mode
- EditorConfig for consistent formatting
- XML documentation required for public APIs
- Reproducible builds enabled

## Testing

The project uses xUnit for testing with comprehensive coverage:

- **FastComponents.UnitTests** - Main library unit tests covering all core components
- **FastComponents.Generators.UnitTests** - Source generator tests  
- **coverage.sh** - Automated coverage script that generates HTML reports using ReportGenerator

Test files are organized by component and follow the pattern `{ComponentName}Tests.cs`. All public APIs have corresponding test coverage.

## Demo Application

The `demo/HtmxAppServer` project demonstrates usage patterns:
- Component organization in `/Components/Blocks/`
- Service registration in `Program.cs`
- HTMX route configuration in `HtmxRoutes.cs`
- Example components: Counter, MovieCharacters

## NuGet Package

The project uses MinVer for automatic versioning based on Git tags. Package is published to NuGet.org on GitHub release creation.

## AOT Compilation

The project supports AOT (Ahead-Of-Time) compilation with the following considerations:
- The demo app has AOT enabled (`PublishAot=true`)
- The library is marked as AOT-compatible (`IsAotCompatible=true`)
- Component rendering and parameter binding use reflection, requiring `RequiresUnreferencedCode` and `RequiresDynamicCode` attributes
- AOT warnings are suppressed at the application level where HTMX endpoints are mapped