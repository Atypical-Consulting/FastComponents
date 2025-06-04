# AOT Support

FastComponents is designed to work with .NET Ahead-Of-Time (AOT) compilation, enabling smaller deployments and faster startup times.

## Overview

AOT compilation pre-compiles your .NET code to native code, eliminating JIT compilation at runtime. FastComponents supports AOT with some considerations due to its use of reflection for component rendering.

## Enabling AOT

### Project Configuration

In your `.csproj` file:

```xml
<PropertyGroup>
    <PublishAot>true</PublishAot>
    <IsAotCompatible>true</IsAotCompatible>
</PropertyGroup>
```

### Required Packages

```xml
<ItemGroup>
    <PackageReference Include="FastComponents" Version="1.0.0" />
    <!-- AOT-friendly JSON serialization -->
    <PackageReference Include="System.Text.Json" Version="9.0.0" />
</ItemGroup>
```

## AOT Considerations

### Component Rendering

FastComponents uses reflection for component rendering, which requires special handling:

```C#
[RequiresUnreferencedCode("Component rendering uses reflection")]
[RequiresDynamicCode("Component parameter binding requires dynamic code")]
public static class HtmxComponentEndpoints
{
    // Extension methods for mapping components
}
```

### Suppressing Warnings

When mapping endpoints, suppress AOT warnings at the application level:

```C#
[UnconditionalSuppressMessage("Trimming", "IL2072", 
    Justification = "Component types are preserved")]
[UnconditionalSuppressMessage("AOT", "IL3050", 
    Justification = "Component rendering requires dynamic code")]
public static void MapComponents(this WebApplication app)
{
    app.MapHtmxGet<CounterComponent, CounterState>("/htmx/counter");
}
```

## JSON Serialization

### Source-Generated Serialization

Use System.Text.Json source generators for AOT-compatible JSON:

```C#
[JsonSerializable(typeof(CounterState))]
[JsonSerializable(typeof(List<Product>))]
[JsonSerializable(typeof(Dictionary<string, object>))]
public partial class AppJsonSerializerContext : JsonSerializerContext
{
}

// Configure in Program.cs
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
```

### Component State Serialization

Ensure all component state types are included:

```C#
public class CounterState : HtmxComponentParameters
{
    public int Count { get; set; }
    
    // Simple types work well with AOT
    public string? Message { get; set; }
    public DateTime LastUpdated { get; set; }
}
```

## Component Registration

### Explicit Registration (Recommended for AOT)

```C#
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddFastComponents();

var app = builder.Build();

// Explicitly register each component
app.MapHtmxGet<HomeComponent, HomeState>("/");
app.MapHtmxGet<CounterComponent, CounterState>("/htmx/counter");
app.MapHtmxPost<FormComponent, FormState>("/htmx/form");
```

### Convention-Based Registration

When using convention-based registration with AOT:

```C#
[UnconditionalSuppressMessage("AOT", "IL3050")]
[UnconditionalSuppressMessage("Trimming", "IL2026")]
public static void ConfigureApp(this WebApplication app)
{
    // AOT warnings are suppressed for this method
    app.UseFastComponentsAuto();
}
```

## Trimming Configuration

### Preserve Assemblies

In your `.csproj`:

```xml
<ItemGroup>
    <TrimmerRootAssembly Include="FastComponents" />
    <TrimmerRootAssembly Include="YourApp" />
</ItemGroup>
```

### Preserve Component Types

```xml
<ItemGroup>
    <TrimmerRootDescriptor Include="TrimmerRoots.xml" />
</ItemGroup>
```

`TrimmerRoots.xml`:

```xml
<linker>
    <assembly fullname="YourApp">
        <type fullname="YourApp.Components.*" preserve="all" />
    </assembly>
</linker>
```

## Testing AOT Builds

### Local AOT Publish

```bash
# Publish with AOT
dotnet publish -c Release -r linux-x64

# Test the native executable
./bin/Release/net9.0/linux-x64/publish/YourApp
```

### Size Comparison

Typical size improvements with AOT:

- Without AOT: ~80-150 MB
- With AOT: ~30-50 MB
- With trimming: ~15-30 MB

## Performance Benefits

### Startup Time

- JIT: 200-500ms
- AOT: 50-100ms

### Memory Usage

- Lower memory footprint
- No JIT compiler overhead
- Predictable performance

## Limitations

### Dynamic Features

Some features have limitations with AOT:

1. **Reflection**: Limited to pre-compiled types
2. **Dynamic Assembly Loading**: Not supported
3. **Expression Trees**: Limited support
4. **Runtime Code Generation**: Not available

### Workarounds

```C#
// Instead of dynamic creation
var component = Activator.CreateInstance(componentType);

// Use compile-time known types
var component = componentType switch
{
    Type t when t == typeof(CounterComponent) => new CounterComponent(),
    Type t when t == typeof(FormComponent) => new FormComponent(),
    _ => throw new NotSupportedException()
};
```

## Best Practices

### 1. Explicit Type Registration

```C#
// Register all component types explicitly
services.AddScoped<CounterComponent>();
services.AddScoped<FormComponent>();
```

### 2. Avoid Dynamic Types

```C#
// ❌ Avoid
object dynamicState = GetDynamicState();

// ✅ Prefer
CounterState typedState = GetCounterState();
```

### 3. Source Generators

Use source generators for compile-time code generation:

```C#
[GenerateParameterMethods]
public partial class MyComponent : HtmxComponentBase<MyState>
{
    // Methods generated at compile time
}
```

## Debugging AOT Issues

### Enable Detailed Warnings

```xml
<PropertyGroup>
    <IlcGenerateCompleteTypeMetadata>true</IlcGenerateCompleteTypeMetadata>
    <IlcGenerateStackTraceData>true</IlcGenerateStackTraceData>
</PropertyGroup>
```

### Common Issues

1. **Missing Metadata**: Add types to TrimmerRootAssembly
2. **Serialization Failures**: Include types in JsonSerializerContext
3. **Reflection Errors**: Use compile-time alternatives

## Example: AOT-Ready Application

```C#
using System.Text.Json.Serialization;
using FastComponents;

var builder = WebApplication.CreateBuilder(args);

// Configure AOT-friendly services
builder.Services.AddFastComponents();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonContext.Default);
});

var app = builder.Build();

// Map components with AOT suppressions
MapComponents(app);

app.Run();

[UnconditionalSuppressMessage("AOT", "IL3050")]
static void MapComponents(WebApplication app)
{
    app.MapHtmxGet<HomeComponent, EmptyState>("/");
    app.MapHtmxGet<CounterComponent, CounterState>("/htmx/counter");
}

// JSON serialization context
[JsonSerializable(typeof(CounterState))]
[JsonSerializable(typeof(EmptyState))]
partial class AppJsonContext : JsonSerializerContext { }
```

## See Also

- [Deployment](Deployment.md)
- [Performance](Performance.md)
- [Source Generators](Source-Generators.md)