# Builders API

FastComponents provides fluent builder APIs for constructing HTMX elements programmatically.

## HtmxBuilder

A fluent builder for creating HTMX-enabled HTML elements.

### Creating Builders

```C#
// Generic builder
var builder = HtmxBuilder.Create();

// Pre-configured builders
var button = HtmxBuilder.Button();
var form = HtmxBuilder.Form();
var div = HtmxBuilder.Div();
```

### Setting Attributes

#### Element Type

```C#
builder.Element("span")
       .Text("Hello World");
```

#### HTMX Attributes

```C#
builder
    .Get("/api/data")
    .Target("#result")
    .Swap("innerHTML")
    .Trigger("click");
```

#### CSS Classes

```C#
builder
    .Class("btn btn-primary")
    .Class("active");
```

#### Custom Attributes

```C#
builder
    .Attr("id", "my-button")
    .Attr("data-value", "123")
    .Attr("disabled", true);
```

### Content

#### Text Content

```C#
builder.Text("Click me!");
```

#### Render Fragment

```C#
builder.Content(@<div>Complex content here</div>);
```

### Common Patterns

#### Self-Updating Button

```C#
HtmxBuilder.Button()
    .GetSelf("/htmx/counter", "counter-1")
    .Text("Count: 0");
```

#### Search Input

```C#
HtmxBuilder.Create()
    .Element("input")
    .Search("/api/search", "#results", 300)
    .Attr("placeholder", "Search...");
```

#### Load Once Container

```C#
HtmxBuilder.Div()
    .LoadOnce("/api/expensive-data")
    .Attr("id", "data-container");
```

### Rendering

```C#
@code {
    void BuildContent(RenderTreeBuilder builder)
    {
        HtmxBuilder.Button()
            .Post("/api/submit")
            .Target("#result")
            .Text("Submit")
            .Render(builder);
    }
}
```

## HtmxBuilderExtensions

Extension methods for common HTMX patterns.

### Button

Creates a complete button with text, URL, and target.

```C#
HtmxBuilder.Button("Click Me", "/api/action", "#result");
```

### SearchInput

Creates a search input with debouncing.

```C#
HtmxBuilder.SearchInput("/api/search", "#results", "Search products...");
```

### LoadContainer

Creates a container that loads content on page load.

```C#
HtmxBuilder.LoadContainer("/api/dashboard", "#dashboard");
```

## HtmxPatterns

Static helper methods for common HTMX patterns.

### SelfUpdatingButton

```C#
var attrs = HtmxPatterns.SelfUpdatingButton("/htmx/counter", "counter-1");
```

### SearchInput

```csharp
var attrs = HtmxPatterns.SearchInput("/api/search", "#results");
```

### LoadOnce

```csharp
var attrs = HtmxPatterns.LoadOnce("/api/expensive-operation");
```

## Complete Example

```csharp
@page "/builder-demo"
@using FastComponents

<h3>HtmxBuilder Demo</h3>

@{
    void RenderSearchForm(RenderTreeBuilder builder)
    {
        HtmxBuilder.Form()
            .Post("/api/search")
            .Target("#search-results")
            .Class("search-form")
            .Content(@<text>
                <input type="text" name="query" placeholder="Search..." />
                @{
                    HtmxBuilder.Button()
                        .Class("btn btn-primary")
                        .Text("Search")
                        .Render(builder);
                }
            </text>)
            .Render(builder);
    }
}

@{ RenderSearchForm(__builder); }

<div id="search-results">
    @{
        HtmxBuilder.Div()
            .LoadOnce("/api/initial-results")
            .Class("results-container")
            .Text("Loading...")
            .Render(__builder);
    }
</div>
```

## See Also

- [Components API](Components-API.md)
- [HTMX Attributes](HTMX-Attributes.md)
- [Component Development](Component-Development.md)