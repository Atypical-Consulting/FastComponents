# FastComponents Overview

FastComponents is a modern .NET library that seamlessly integrates HTMX with Blazor components for building dynamic web applications with server-side rendering. It provides a type-safe, component-based approach to creating interactive web experiences without writing JavaScript.

## What is FastComponents?

FastComponents bridges the gap between ASP.NET Core's server-side capabilities and HTMX's dynamic HTML approach. It allows you to:

- Build interactive web applications using familiar Blazor component syntax
- Leverage HTMX attributes through strongly-typed C# properties
- Create reusable components that render as pure HTML
- Handle dynamic updates without full page reloads
- Maintain type safety throughout your application

## Key Features

### 🚀 Type-Safe HTMX Integration
All HTMX attributes are exposed as strongly-typed C# properties, providing IntelliSense support and compile-time validation.

### 🎯 Component-Based Architecture
Build your UI using reusable Blazor components that render to clean, semantic HTML enhanced with HTMX attributes.

### ⚡ Server-Side Rendering
Components are rendered on the server and sent as HTML fragments, ensuring fast initial page loads and SEO-friendly content.

### 🔧 ASP.NET Core Integration
Seamlessly integrates with ASP.NET Core Minimal APIs for endpoint routing and request handling.

### 📦 Zero JavaScript Required
Create fully interactive web applications without writing any client-side JavaScript code.

### 🎨 Flexible Styling
Works with any CSS framework - components render clean HTML that you can style however you prefer.

## Architecture Overview

FastComponents follows a Multiple Resources Application (MRA) architecture:

1. **Server-Side Components**: Blazor components inherit from `HtmxComponentBase` to gain access to HTMX attributes
2. **HTML Rendering**: Components are rendered as HTML fragments on the server
3. **HTMX Enhancement**: The rendered HTML includes HTMX attributes for dynamic behavior
4. **Minimal API Endpoints**: ASP.NET Core endpoints handle HTMX requests and return rendered components

## Core Concepts

### HTMX Components
Components that inherit from `HtmxComponentBase` automatically gain access to all HTMX attributes as C# properties:

```csharp
@inherits HtmxComponentBase

<div @attributes="AdditionalAttributes">
    <button hx-get="/counter" hx-target="#result">
        Click me!
    </button>
    <div id="result"></div>
</div>
```

### Component Parameters
Create strongly-typed parameters for your components using records that inherit from `HtmxComponentParameters`:

```csharp
[GenerateParameterMethods]
public partial record CounterState : HtmxComponentParameters
{
    public int Count { get; init; } = 0;
}
```

### Endpoint Mapping
Map components to HTTP endpoints using the fluent API:

```csharp
app.MapHtmxGet<CounterComponent, CounterState>("/counter");
```

## Why Choose FastComponents?

- **Familiar Development Model**: If you know Blazor, you already know how to use FastComponents
- **Progressive Enhancement**: Start with server-rendered HTML and enhance with HTMX
- **Type Safety**: Catch errors at compile-time rather than runtime
- **Performance**: Minimal JavaScript payload and efficient server-side rendering
- **Maintainability**: Component-based architecture promotes code reuse and organization

## Next Steps

- [Getting Started](Getting-Started.md) - Set up your first FastComponents project
- [Core Concepts](Core-Concepts.md) - Deep dive into the architecture
- [Component Development](Component-Development.md) - Learn how to build components
- [API Reference](API-Reference.md) - Detailed API documentation