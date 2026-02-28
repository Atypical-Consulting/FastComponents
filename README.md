# FastComponents

> **Server-side Blazor components rendered as HTMX-powered HTML fragments -- build interactive web UIs with .NET 10 and zero client-side Blazor runtime.**

<!-- Badges: Row 1 — Identity -->
[![Atypical-Consulting - FastComponents](https://img.shields.io/static/v1?label=Atypical-Consulting&message=FastComponents&color=blue&logo=github)](https://github.com/Atypical-Consulting/FastComponents)
[![License: Apache-2.0](https://img.shields.io/badge/License-Apache--2.0-blue.svg)](LICENSE)
[![.NET 10](https://img.shields.io/badge/.NET-10.0-purple?logo=dotnet)](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
[![stars - FastComponents](https://img.shields.io/github/stars/Atypical-Consulting/FastComponents?style=social)](https://github.com/Atypical-Consulting/FastComponents)
[![forks - FastComponents](https://img.shields.io/github/forks/Atypical-Consulting/FastComponents?style=social)](https://github.com/Atypical-Consulting/FastComponents)

<!-- Badges: Row 2 — Activity -->
[![GitHub tag](https://img.shields.io/github/tag/Atypical-Consulting/FastComponents?include_prereleases=&sort=semver&color=blue)](https://github.com/Atypical-Consulting/FastComponents/releases/)
[![issues - FastComponents](https://img.shields.io/github/issues/Atypical-Consulting/FastComponents)](https://github.com/Atypical-Consulting/FastComponents/issues)
[![GitHub pull requests](https://img.shields.io/github/issues-pr/Atypical-Consulting/FastComponents)](https://github.com/Atypical-Consulting/FastComponents/pulls)
[![GitHub last commit](https://img.shields.io/github/last-commit/Atypical-Consulting/FastComponents)](https://github.com/Atypical-Consulting/FastComponents/commits/main)

<!-- Badges: Row 3 — Quality -->
[![Build](https://github.com/Atypical-Consulting/FastComponents/actions/workflows/main.yml/badge.svg)](https://github.com/Atypical-Consulting/FastComponents/actions/workflows/main.yml)

<!-- Badges: Row 4 — Distribution -->
[![NuGet](https://img.shields.io/nuget/v/FastComponents.svg)](https://www.nuget.org/packages/FastComponents)

---

## Table of Contents

- [The Problem](#the-problem)
- [The Solution](#the-solution)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [License](#license)

## The Problem

Building interactive web UIs with .NET typically means choosing between the full Blazor Server runtime (with WebSocket overhead and connection state) or Blazor WASM (with large download sizes). If you just want lightweight, server-rendered HTML fragments that respond to user interactions -- the HTMX model -- there is no first-class Blazor integration. You end up writing raw HTML strings or abandoning the Razor component model entirely.

## The Solution

**FastComponents** bridges Blazor's server-side Razor component model with HTMX. You author components using familiar `.razor` syntax with full C# support, and FastComponents renders them as plain HTML fragments served via FastEndpoints. HTMX on the client handles partial page updates -- no WebSocket connections, no WASM downloads, just HTTP requests and HTML responses.

```csharp
// Define a component with HTMX attributes using the HtmxTag helper
<HtmxTag
    As="button"
    HxGet="/api/counter?Count=1"
    HxSwap="outerHTML"
    HxTarget="#counter">
    Increment
</HtmxTag>
```

## Features

- [x] `HtmxComponentBase` -- base class with all htmx attributes as Blazor `[Parameter]` properties
- [x] `HtmxTag` -- generic Razor component that renders any HTML element with htmx attributes
- [x] `HtmxComponentEndpoint<TComponent>` -- serve components as HTML via FastEndpoints routes
- [x] `HtmxComponentEndpoint<TComponent, TParameters>` -- typed parameter binding from query strings
- [x] `HtmxComponentParameters` -- immutable record base with automatic query string serialization
- [x] `ComponentHtmlResponseService` -- render any Blazor component to an HTML string on the server
- [x] `ClassNamesBuilder` -- fluent, conditional CSS class builder (similar to `classnames` in JS)
- [x] `Hx.Swap` constants and `Hx.TargetId()` helper for type-safe htmx attribute values
- [x] Bundled `htmx.min.js` as a static web asset
- [x] NuGet package with auto-generated API documentation
- [ ] HTML beautifier for formatted output *(stub implemented)*
- [ ] AOT compilation support *(planned)*
- [ ] Project template for `dotnet new` *(planned)*

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Runtime | .NET 10.0 / C# 14 |
| Component model | Blazor SSR (`Microsoft.AspNetCore.Components.Web` 10.0) |
| Endpoint routing | [FastEndpoints](https://fast-endpoints.com/) 8.0 |
| HTML parsing | [AngleSharp](https://anglesharp.github.io/) 1.4 |
| Client interactivity | [htmx](https://htmx.org/) (bundled) |
| Versioning | [MinVer](https://github.com/adamralph/minver) (git-tag based) |
| CI/CD | GitHub Actions (build, NuGet pack, validate, publish) |

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) >= 10.0

### Installation

**Option 1 -- NuGet** *(recommended)*

```bash
dotnet add package FastComponents
```

**Option 2 -- From Source**

```bash
git clone https://github.com/Atypical-Consulting/FastComponents.git
cd FastComponents
dotnet build
```

### Setup

Register FastComponents in your `Program.cs`:

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add FastComponents services (registers FastEndpoints + HTML renderer)
builder.Services.AddFastComponents();

var app = builder.Build();

app.UseStaticFiles();

// Map component endpoints
app.UseFastComponents();

app.Run();
```

## Usage

### Define a component

Create a Razor component that inherits from `HtmxComponentBase`:

```razor
@* Counter.razor *@
@inherits HtmxComponentBase<CounterEndpoint.CounterParameters>

<section id="block-counter">
    <HtmxTag
        As="button"
        HxGet="@Parameters.Increment()"
        HxSwap="@Hx.Swap.OuterHtml"
        HxTarget="@Hx.TargetId("block-counter")">
        Increment
    </HtmxTag>

    <span>Count: @Parameters.Count</span>
</section>
```

### Wire it to an endpoint

```csharp
// Counter.razor.cs
public class CounterEndpoint
    : HtmxComponentEndpoint<Counter, CounterEndpoint.CounterParameters>
{
    public override void Configure()
    {
        Get("/ui/blocks/counter");
        AllowAnonymous();
    }

    public record CounterParameters : HtmxComponentParameters
    {
        public int Count { get; init; } = 0;

        public string Increment()
        {
            var next = this with { Count = Count + 1 };
            return next.ToComponentUrl("/ui/blocks/counter");
        }
    }
}
```

When the button is clicked, htmx sends a GET request to `/ui/blocks/counter?Count=1`, FastComponents renders the Blazor component server-side, and the HTML fragment replaces the target element -- no JavaScript framework required.

## Architecture

```
Browser (htmx)                    ASP.NET Server
+-----------------+                +-----------------------------------+
| HTML + htmx.js  | -- HTTP GET -> | FastEndpoints route               |
|                  |                |   -> HtmxComponentEndpoint        |
|                  |                |     -> ComponentHtmlResponseService|
|                  |                |       -> HtmlRenderer (Blazor SSR)|
| <-- HTML frag -- |                |     <- Rendered HTML fragment     |
+-----------------+                +-----------------------------------+
```

### Project Structure

```
FastComponents/
├── src/
│   └── FastComponents/           # Core library (NuGet package)
│       ├── Components/
│       │   ├── Base/             # HtmxComponentBase, ClassNamesBuilder, interfaces
│       │   └── HtmxTag/         # Generic htmx-aware Razor component
│       ├── Endpoints/            # HtmxComponentEndpoint base classes
│       ├── Services/             # ComponentHtmlResponseService, HtmlBeautifier
│       ├── Utilities/            # Hx helper (swap constants, target helpers)
│       └── wwwroot/              # Bundled htmx.min.js
├── demo/
│   └── HtmxAppServer/           # Demo app (Counter, MovieCharacters examples)
├── docs/                         # Auto-generated API documentation
├── build/                        # Build scripts (versioning)
└── .github/workflows/            # CI pipeline (build, pack, validate, deploy)
```

## Roadmap

- [ ] Implement HTML beautifier for formatted debug output
- [ ] Add AOT compilation support
- [ ] Publish a `dotnet new` project template
- [ ] Add unit and integration tests
- [ ] Expand component library with common UI patterns

> Want to contribute? Pick any roadmap item and open a PR!

## Contributing

Contributions are welcome! Please read [CONTRIBUTING.md](CONTRIBUTING.md) first.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit using [conventional commits](https://www.conventionalcommits.org/) (`git commit -m 'feat: add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## License

[Apache-2.0](LICENSE) (c) 2020-2026 [Atypical Consulting](https://atypical.garry-ai.cloud)

---

Built with care by [Atypical Consulting](https://atypical.garry-ai.cloud) -- opinionated, production-grade open source.

[![Contributors](https://contrib.rocks/image?repo=Atypical-Consulting/FastComponents)](https://github.com/Atypical-Consulting/FastComponents/graphs/contributors)
