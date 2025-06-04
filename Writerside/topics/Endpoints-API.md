# Endpoints API

FastComponents provides extension methods for mapping HTMX components to ASP.NET Core endpoints.

## HtmxComponentEndpoints

Extension methods for registering HTMX components with the ASP.NET Core routing system.

### Methods

#### MapHtmxGet

Maps a component to handle GET requests.

```C#
app.MapHtmxGet<CounterComponent, CounterState>("/htmx/counter")
   .AllowAnonymous();
```

#### MapHtmxPost

Maps a component to handle POST requests.

```C#
app.MapHtmxPost<FormComponent, FormState>("/htmx/form")
   .RequireAuthorization();
```

#### MapHtmxPut

Maps a component to handle PUT requests.

```C#
app.MapHtmxPut<UpdateComponent, UpdateState>("/htmx/update/{id}")
   .RequireAuthorization("Admin");
```

#### MapHtmxDelete

Maps a component to handle DELETE requests.

```C#
app.MapHtmxDelete<DeleteComponent, DeleteState>("/htmx/delete/{id}")
   .RequireAuthorization();
```

#### MapHtmxPatch

Maps a component to handle PATCH requests.

```C#
app.MapHtmxPatch<PatchComponent, PatchState>("/htmx/patch/{id}")
   .RequireAuthorization();
```

## Convention-Based Registration

Automatically discover and register components based on naming conventions.

### MapHtmxComponentsByConvention

```C#
// Register all components in the calling assembly
app.MapHtmxComponentsByConvention();

// Register components from specific assemblies
app.MapHtmxComponentsByConvention(
    typeof(Program).Assembly,
    typeof(SharedComponents).Assembly
);
```

### Naming Conventions

- `CounterComponent` → `/htmx/counter`
- `MovieCharactersExample` → `/htmx/movie-characters`
- `UserProfileComponent` → `/htmx/user-profile`

Common suffixes like "Component" and "Example" are automatically removed, and names are converted to kebab-case.

## Simplified API

Use the auto-registration methods for the simplest setup:

```C#
// In Program.cs
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastComponentsAuto();

var app = builder.Build();
app.UseFastComponentsAuto();
```

## Route Parameters

Components can accept route parameters through their state objects:

```C#
public class ProductState : HtmxComponentParameters
{
    [FromRoute]
    public int Id { get; set; }
    
    [FromQuery]
    public string? Category { get; set; }
}

// Register with route template
app.MapHtmxGet<ProductComponent, ProductState>("/htmx/product/{id}");
```

## See Also

- [Getting Started](Getting-Started.md)
- [Components API](Components-API.md)
- [State Management](State-Management.md)