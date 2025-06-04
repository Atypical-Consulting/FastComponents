# Migration Guide

This guide helps you migrate existing applications to FastComponents and upgrade between versions.

## Migrating from Blazor Server

### Component Migration

#### Before: Blazor Server Component

```C#
@page "/counter"
@using Microsoft.AspNetCore.Components

<PageTitle>Counter</PageTitle>

<h1>Counter</h1>

<p role="status">Current count: @currentCount</p>

<button class="btn btn-primary" @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

#### After: FastComponents

```C#
@inherits SimpleHtmxComponent<CounterState>

<div>
    <h1>Counter</h1>
    <p>Current count: @State.Count</p>
    <button hx-get="@Url" hx-target="this" hx-swap="outerHTML" 
            class="btn btn-primary">
        Click me
    </button>
</div>

@code {
    public class CounterState : HtmxComponentParameters
    {
        public int Count { get; set; }
        
        public override void BindFromQuery(IQueryCollection query)
        {
            Count = GetQueryInt(query, nameof(Count)) ?? 0;
            Count++; // Increment on each request
        }
    }
}
```

### Service Registration Migration

#### Before: Blazor Server

```C#
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
```

#### After: FastComponents

```C#
builder.Services.AddFastComponentsAuto();

var app = builder.Build();
app.UseFastComponentsAuto();
```

## Migrating from MVC/Razor Pages

### View to Component Migration

#### Before: MVC View

```html
@model ProductListViewModel

<h2>Products</h2>

<form method="get">
    <input type="text" name="search" value="@Model.Search" />
    <button type="submit">Search</button>
</form>

<div id="product-list">
    @foreach (var product in Model.Products)
    {
        <div class="product">
            <h3>@product.Name</h3>
            <p>@product.Description</p>
            <span class="price">$@product.Price</span>
        </div>
    }
</div>
```

#### After: FastComponents

```C#
@inherits HtmxComponentBase<ProductListState>

<div>
    <h2>Products</h2>
    
    <form hx-get="@GetComponentUrl()" hx-target="#product-list" 
          hx-trigger="submit, keyup delay:300ms from:input[name=search]">
        <input type="text" name="search" value="@Parameters.Search" />
        <button type="submit">Search</button>
    </form>
    
    <div id="product-list">
        @foreach (var product in Products)
        {
            <div class="product">
                <h3>@product.Name</h3>
                <p>@product.Description</p>
                <span class="price">$@product.Price</span>
            </div>
        }
    </div>
</div>

@code {
    [Inject] private IProductService ProductService { get; set; } = default!;
    
    private List<Product> Products { get; set; } = new();
    
    protected override async Task OnParametersSetAsync()
    {
        Products = await ProductService.SearchAsync(Parameters.Search ?? "");
    }
}
```

### Controller to Endpoint Migration

#### Before: MVC Controller

```csharp
public class ProductController : Controller
{
    private readonly IProductService _productService;
    
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
    
    public async Task<IActionResult> Index(string search = "")
    {
        var products = await _productService.SearchAsync(search);
        var model = new ProductListViewModel 
        { 
            Products = products, 
            Search = search 
        };
        
        return View(model);
    }
}
```

#### After: FastComponents Endpoint

```csharp
// Automatic registration with convention-based approach
app.UseFastComponentsAuto();

// Or explicit registration
app.MapHtmxGet<ProductListComponent, ProductListState>("/products");
```

## Migrating from SPA Frameworks

### React/Vue to FastComponents

#### Before: React Component

```jsx
import React, { useState, useEffect } from 'react';

function ProductSearch() {
    const [search, setSearch] = useState('');
    const [products, setProducts] = useState([]);
    const [loading, setLoading] = useState(false);
    
    useEffect(() => {
        const searchProducts = async () => {
            if (!search) return;
            
            setLoading(true);
            const response = await fetch(`/api/products?search=${search}`);
            const data = await response.json();
            setProducts(data);
            setLoading(false);
        };
        
        const debounce = setTimeout(searchProducts, 300);
        return () => clearTimeout(debounce);
    }, [search]);
    
    return (
        <div>
            <input 
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                placeholder="Search products..."
            />
            {loading && <div>Loading...</div>}
            <div>
                {products.map(product => (
                    <div key={product.id}>
                        <h3>{product.name}</h3>
                        <p>${product.price}</p>
                    </div>
                ))}
            </div>
        </div>
    );
}
```

#### After: FastComponents

```csharp
@inherits HtmxComponentBase<SearchState>

<div>
    <input type="text" name="search" value="@Parameters.Search"
           hx-get="@GetComponentUrl()" 
           hx-target="#results"
           hx-trigger="keyup changed delay:300ms"
           hx-indicator="#loading"
           placeholder="Search products..." />
           
    <div id="loading" class="htmx-indicator">Loading...</div>
    
    <div id="results">
        @foreach (var product in Products)
        {
            <div>
                <h3>@product.Name</h3>
                <p>$@product.Price</p>
            </div>
        }
    </div>
</div>

@code {
    [Inject] private IProductService ProductService { get; set; } = default!;
    
    private List<Product> Products { get; set; } = new();
    
    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrEmpty(Parameters.Search))
        {
            Products = await ProductService.SearchAsync(Parameters.Search);
        }
    }
}
```

## Version Migration

### From 0.x to 1.0

#### Breaking Changes

1. **Namespace Changes**
   ```csharp
   // Before
   using FastComponents.Core;
   using FastComponents.Extensions;
   
   // After
   using FastComponents;
   ```

2. **Component Base Class Changes**
   ```csharp
   // Before
   public class MyComponent : HtmxComponent<MyState>
   
   // After
   public class MyComponent : HtmxComponentBase<MyState>
   ```

3. **Service Registration Changes**
   ```csharp
   // Before
   services.AddFastComponents(options => {
       options.EnableConventionRouting = true;
   });
   
   // After
   services.AddFastComponentsAuto();
   ```

#### Migration Steps

1. **Update Package References**
   ```xml
   <PackageReference Include="FastComponents" Version="1.0.0" />
   ```

2. **Update Namespace Imports**
   ```bash
   # Use find and replace in your IDE
   Find: "using FastComponents.Core;"
   Replace: "using FastComponents;"
   ```

3. **Update Component Base Classes**
   ```csharp
   // Update all component base classes
   // HtmxComponent<T> → HtmxComponentBase<T>
   // SimpleComponent<T> → SimpleHtmxComponent<T>
   ```

4. **Update Service Registration**
   ```csharp
   // In Program.cs
   builder.Services.AddFastComponentsAuto();
   
   var app = builder.Build();
   app.UseFastComponentsAuto();
   ```

### From 1.0 to 1.1

#### New Features

1. **Enhanced Builder API**
   ```csharp
   // New fluent builder
   HtmxBuilder.Button()
       .Get("/api/action")
       .Target("#result")
       .Text("Click Me")
       .Render(builder);
   ```

2. **Improved Source Generators**
   ```csharp
   [GenerateParameterMethods(SkipDefaults = true)]
   public partial class MyComponent : HtmxComponentBase<MyState>
   {
       // Generated methods available
   }
   ```

## Common Migration Patterns

### State Management Migration

#### From Session State

```csharp
// Before: Session-based state
public class ProductController : Controller
{
    public IActionResult Index()
    {
        var filter = HttpContext.Session.GetString("ProductFilter") ?? "";
        // ...
    }
}

// After: Component state
public class ProductListState : HtmxComponentParameters
{
    public string Filter { get; set; } = "";
    
    public override void BindFromQuery(IQueryCollection query)
    {
        Filter = GetQueryValue(query, nameof(Filter)) ?? "";
    }
}
```

#### From ViewBag/ViewData

```csharp
// Before: ViewBag
public IActionResult Index()
{
    ViewBag.Title = "Products";
    ViewData["ShowFilter"] = true;
    return View();
}

// After: Strongly-typed state
public class ProductState : HtmxComponentParameters
{
    public string Title { get; set; } = "Products";
    public bool ShowFilter { get; set; } = true;
}
```

### Form Handling Migration

#### Before: MVC Form Handling

```csharp
[HttpPost]
public async Task<IActionResult> Create(ProductModel model)
{
    if (!ModelState.IsValid)
    {
        return View(model);
    }
    
    await _service.CreateAsync(model);
    return RedirectToAction("Index");
}
```

#### After: FastComponents Form Handling

```csharp
@inherits HtmxComponentBase<ProductFormState>

<form hx-post="@GetComponentUrl()" hx-target="this" hx-swap="outerHTML">
    <div asp-validation-summary="All"></div>
    
    <input asp-for="@Parameters.Name" />
    <span asp-validation-for="@Parameters.Name"></span>
    
    <button type="submit">Save</button>
</form>

@code {
    [Inject] private IProductService ProductService { get; set; } = default!;
    
    protected override async Task OnParametersSetAsync()
    {
        if (HttpContext.Request.Method == "POST")
        {
            if (ModelState.IsValid)
            {
                await ProductService.CreateAsync(Parameters);
                // Redirect or show success message
            }
        }
    }
}
```

## Migration Checklist

### Pre-Migration

- [ ] Audit existing components/views
- [ ] Identify shared state management
- [ ] Document current API endpoints
- [ ] Plan component hierarchy
- [ ] Set up development environment

### During Migration

- [ ] Migrate components incrementally
- [ ] Update service registrations
- [ ] Test each migrated component
- [ ] Update routing configuration
- [ ] Verify HTMX integration

### Post-Migration

- [ ] Remove obsolete code
- [ ] Update documentation
- [ ] Performance testing
- [ ] User acceptance testing
- [ ] Production deployment

## Migration Tools

### Automated Migration Script

```bash
#!/bin/bash
# migration-helper.sh

echo "FastComponents Migration Helper"

# Update namespace imports
find . -name "*.cs" -type f -exec sed -i 's/using FastComponents\.Core;/using FastComponents;/g' {} +
find . -name "*.cs" -type f -exec sed -i 's/using FastComponents\.Extensions;/using FastComponents;/g' {} +

# Update component base classes
find . -name "*.cs" -type f -exec sed -i 's/HtmxComponent</HtmxComponentBase</g' {} +
find . -name "*.cs" -type f -exec sed -i 's/SimpleComponent</SimpleHtmxComponent</g' {} +

echo "Migration completed. Please review changes and test thoroughly."
```

### Component Analyzer

```csharp
// Custom analyzer to help identify migration opportunities
public class MigrationAnalyzer : DiagnosticAnalyzer
{
    public static readonly DiagnosticDescriptor ViewComponentRule = new DiagnosticDescriptor(
        "FC0001",
        "Consider migrating to FastComponents",
        "This ViewComponent could be migrated to FastComponents",
        "Migration",
        DiagnosticSeverity.Info,
        true);
        
    // Implementation details...
}
```

## See Also

- [Getting Started](Getting-Started.md)
- [Component Development](Component-Development.md)
- [API Reference](API-Reference.md)