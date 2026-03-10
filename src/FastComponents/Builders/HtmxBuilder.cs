/*
 * Copyright 2025 Atypical Consulting SRL
 * Licensed under the Apache License, Version 2.0
 */

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace FastComponents;

/// <summary>
/// Fluent builder for creating HTMX elements with less boilerplate
/// </summary>
public class HtmxBuilder
{
    private readonly Dictionary<string, object?> _attributes = [];
    private string _element = "div";
    private string? _content;
    private RenderFragment? _childContent;

    private HtmxBuilder()
    {
    }

    /// <summary>
    /// Creates a new HTMX element builder
    /// </summary>
    public static HtmxBuilder Create() => new();

    /// <summary>
    /// Creates a button with HTMX attributes
    /// </summary>
    public static HtmxBuilder Button() => new() { _element = "button" };

    /// <summary>
    /// Creates a form with HTMX attributes
    /// </summary>
    public static HtmxBuilder Form() => new() { _element = "form" };

    /// <summary>
    /// Creates a div with HTMX attributes
    /// </summary>
    public static HtmxBuilder Div() => new() { _element = "div" };

    /// <summary>
    /// Sets the HTML element type
    /// </summary>
    public HtmxBuilder Element(string element)
    {
        _element = element;
        return this;
    }

    /// <summary>
    /// Sets hx-get attribute with URL
    /// </summary>
    public HtmxBuilder Get(string url)
    {
        _attributes["hx-get"] = url;
        return this;
    }

    /// <summary>
    /// Sets hx-post attribute with URL
    /// </summary>
    public HtmxBuilder Post(string url)
    {
        _attributes["hx-post"] = url;
        return this;
    }

    /// <summary>
    /// Sets hx-target attribute
    /// </summary>
    public HtmxBuilder Target(string target)
    {
        _attributes["hx-target"] = target;
        return this;
    }

    /// <summary>
    /// Sets hx-swap attribute
    /// </summary>
    public HtmxBuilder Swap(string swap)
    {
        _attributes["hx-swap"] = swap;
        return this;
    }

    /// <summary>
    /// Sets hx-trigger attribute
    /// </summary>
    public HtmxBuilder Trigger(string trigger)
    {
        _attributes["hx-trigger"] = trigger;
        return this;
    }

    /// <summary>
    /// Adds a CSS class
    /// </summary>
    public HtmxBuilder Class(string cssClass)
    {
        _attributes["class"] = _attributes.TryGetValue("class", out object? existing)
            ? $"{existing} {cssClass}"
            : cssClass;

        return this;
    }

    /// <summary>
    /// Adds a custom attribute
    /// </summary>
    public HtmxBuilder Attr(string name, object? value)
    {
        _attributes[name] = value;
        return this;
    }

    /// <summary>
    /// Sets the text content
    /// </summary>
    public HtmxBuilder Text(string content)
    {
        _content = content;
        return this;
    }

    /// <summary>
    /// Sets the child content
    /// </summary>
    public HtmxBuilder Content(RenderFragment content)
    {
        _childContent = content;
        return this;
    }

    /// <summary>
    /// Common pattern: Get request that updates self
    /// </summary>
    public HtmxBuilder GetSelf(string url, string id)
    {
        return Get(url).Target($"#{id}").Swap("outerHTML");
    }

    /// <summary>
    /// Common pattern: Post form that updates a target
    /// </summary>
    public HtmxBuilder PostTo(string url, string target)
    {
        return Post(url).Target(target).Swap("innerHTML");
    }

    /// <summary>
    /// Common pattern: Load content on page load
    /// </summary>
    public HtmxBuilder LoadOnce(string url)
    {
        return Get(url).Trigger("load once");
    }

    /// <summary>
    /// Common pattern: Search with debouncing
    /// </summary>
    public HtmxBuilder Search(string url, string target, int delayMs = 300)
    {
        return Get(url)
            .Target(target)
            .Trigger($"keyup changed delay:{delayMs}ms, search")
            .Attr("hx-indicator", "#loading");
    }

    /// <summary>
    /// Renders the HTMX element
    /// </summary>
    public void Render(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, _element);

        foreach ((string key, object? value) in _attributes)
        {
            builder.AddAttribute(1, key, value);
        }

        if (_content is not null)
        {
            builder.AddContent(2, _content);
        }
        else if (_childContent is not null)
        {
            builder.AddContent(2, _childContent);
        }

        builder.CloseElement();
    }
}

/// <summary>
/// Extension methods for easier HTMX building in components
/// </summary>
public static class HtmxBuilderExtensions
{
    /// <summary>
    /// Creates an HTMX button that updates itself
    /// </summary>
    public static RenderFragment Button(string text, string url, string id)
        => builder => HtmxBuilder.Button()
            .Text(text)
            .GetSelf(url, id)
            .Attr("id", id)
            .Render(builder);

    /// <summary>
    /// Creates an HTMX search input
    /// </summary>
    public static RenderFragment SearchInput(string url, string target, string placeholder = "Search...")
        => builder => HtmxBuilder.Create()
            .Element("input")
            .Attr("type", "search")
            .Attr("placeholder", placeholder)
            .Search(url, target)
            .Render(builder);

    /// <summary>
    /// Creates a loading container that loads content on page load
    /// </summary>
    public static RenderFragment LoadContainer(string url, string loadingText = "Loading...")
        => builder => HtmxBuilder.Div()
            .LoadOnce(url)
            .Text(loadingText)
            .Render(builder);
}
