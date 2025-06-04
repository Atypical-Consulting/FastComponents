# Components API

This section covers the core component APIs provided by FastComponents.

<chapter title="HtmxComponentBase" id="htmx-component-base">

The foundation for all HTMX-enabled components.

<chapter title="Core HTMX Attributes" id="core-attributes">

HxGet
: URL for GET requests

HxPost  
: URL for POST requests

HxPut
: URL for PUT requests

HxPatch
: URL for PATCH requests

HxDelete
: URL for DELETE requests

HxTrigger
: Event that triggers the request

HxTarget
: Target element for swapping content

HxSwap
: How to swap the content

</chapter>

<chapter title="Additional Attributes" id="additional-attributes">

HxBoost
: Progressively enhance links and forms

HxConfirm
: Confirmation dialog before request

HxDisable
: Disable element during request

HxIndicator
: Loading indicator element

HxParams
: Parameters to include/exclude

HxHeaders
: Additional headers to send

</chapter>

</chapter>

### Example {#htmx-component-example}

<code-block lang="html">
@inherits HtmxComponentBase

&lt;div @attributes="@HtmxAttributes"&gt;
    &lt;button hx-get="@HxGet" 
            hx-target="@HxTarget"
            hx-swap="@HxSwap"&gt;
        Click Me
    &lt;/button&gt;
&lt;/div&gt;

@code {
    protected override void OnInitialized()
    {
        HxGet = "/api/data";
        HxTarget = "#result";
        HxSwap = "innerHTML";
    }
}
</code-block>

## SimpleHtmxComponent

A simplified base class that automatically generates routes based on component name.

### Properties

State
: The component's state object

Url
: Auto-generated URL for the component

### Example {#simple-component-example}

<code-block lang="html">
@inherits SimpleHtmxComponent&lt;CounterState&gt;

&lt;div&gt;
    &lt;p&gt;Count: @State.Count&lt;/p&gt;
    &lt;button hx-get="@Url" hx-target="this" hx-swap="outerHTML"&gt;
        Increment
    &lt;/button&gt;
&lt;/div&gt;

@code {
    public class CounterState : HtmxComponentParameters
    {
        public int Count { get; set; }
    }
}
</code-block>

## HtmxComponentParameters

Base class for component state/parameters that can be bound from query strings.

### Methods {#parameters-methods}

BindFromQuery(IQueryCollection)
: Bind parameters from query string

BuildQueryString()
: Convert parameters to query string

ToComponentUrl(string)
: Create URL with parameters

### Example {#parameters-example}

<code-block lang="c#">
public class SearchParameters : HtmxComponentParameters
{
    public string Query { get; set; } = "";
    public int Page { get; set; } = 1;
    
    public override void BindFromQuery(IQueryCollection query)
    {
        Query = GetQueryValue(query, nameof(Query)) ?? "";
        Page = GetQueryInt(query, nameof(Page)) ?? 1;
    }
}
</code-block>

## ClassNamesBuilder

Fluent API for building CSS class names dynamically.

### Methods {#classnames-methods}

AddClass(string, bool)
: Add class conditionally

AddRawValue(string)
: Add raw CSS class string

Build()
: Build final class string

### Example {#classnames-example}

<code-block lang="c#">
@code {
    protected override void OnBuildClassNames(
        ClassNamesBuilder builder)
    {
        builder
            .AddClass("active", IsActive)
            .AddClass("disabled", IsDisabled)
            .AddClass("primary", IsPrimary);
    }
}
</code-block>

## See Also

- [Core Concepts](Core-Concepts.md)
- [Component Development](Component-Development.md)
- [HTMX Attributes](HTMX-Attributes.md)