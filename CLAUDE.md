# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

FastComponents is a .NET 9.0 Razor Class Library that enables building HTMX-powered web applications using Blazor components with server-side rendering. It provides type-safe C# properties for all HTMX attributes and integrates with ASP.NET Minimal APIs for endpoint routing.

## Common Development Commands

```bash
# Build the solution
dotnet build

# Build in Release mode
dotnet build --configuration Release

# Pack NuGet package
dotnet pack --configuration Release

# Run the demo application
dotnet run --project demo/HtmxAppServer/HtmxAppServer.csproj

# Clean build artifacts
dotnet clean
```

## Architecture

### Core Components

- **HtmxComponentBase** (`src/FastComponents/Components/Base/HtmxComponentBase.cs`) - Base class providing HTMX attribute properties for all components
- **ComponentHtmlResponseService** (`src/FastComponents/Services/ComponentHtmlResponseService.cs`) - Renders Blazor components as HTML responses for HTMX requests
- **HtmxComponentEndpoints** (`src/FastComponents/Endpoints/HtmxComponentEndpoints.cs`) - ASP.NET Minimal API integration for component routing
- **ClassNamesBuilder** (`src/FastComponents/Components/Base/ClassNamesBuilder.cs`) - Fluent API for building CSS class names

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

Currently no test projects exist. When adding tests, follow the .NET testing conventions and consider:
- Unit tests for ClassNamesBuilder and utility classes
- Integration tests for ComponentHtmlResponseService
- Component rendering tests using bUnit

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