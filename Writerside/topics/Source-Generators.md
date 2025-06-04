# Source Generators

FastComponents uses C# Source Generators to provide compile-time code generation for enhanced developer productivity.

## GenerateParameterMethodsAttribute

This attribute triggers source generation for component parameter helper methods.

### Basic Usage

```C#
[GenerateParameterMethods]
public partial class MyComponent : HtmxComponentBase<MyComponentState>
{
    // Source generator will create helper methods
}

public class MyComponentState : HtmxComponentParameters
{
    public string Title { get; set; } = "";
    public int Count { get; set; }
    public bool IsActive { get; set; }
}
```

### Generated Methods

The source generator creates fluent methods for each property:

```C#
// Generated methods:
public MyComponent WithTitle(string value) { ... }
public MyComponent WithCount(int value) { ... }
public MyComponent WithIsActive(bool value) { ... }
```

### Usage Example

```C#
@page "/products"

<MyComponent>
    @WithTitle("Featured Products")
    @WithCount(10)
    @WithIsActive(true)
</MyComponent>
```

## Configuration Options

### SkipDefaults

Skip generating methods for properties with default values:

```C#
[GenerateParameterMethods(SkipDefaults = true)]
public partial class OptimizedComponent : HtmxComponentBase<ComponentState>
{
    // Only generates methods for non-default property values
}
```

## How It Works

### Source Generation Process

1. **Discovery**: Analyzer finds classes with `[GenerateParameterMethods]`
2. **Analysis**: Examines the component's state/parameter type
3. **Generation**: Creates partial class with fluent methods
4. **Compilation**: Generated code is included in compilation

### Generated Code Structure

```csharp
// Generated file: MyComponent.g.cs
public partial class MyComponent
{
    public MyComponent WithTitle(string value)
    {
        Parameters.Title = value;
        return this;
    }
    
    public MyComponent WithCount(int value)
    {
        Parameters.Count = value;
        return this;
    }
}
```

## Benefits

### Type Safety

- Compile-time checking of parameter names
- IntelliSense support for all parameters
- Refactoring-safe parameter usage

### Developer Experience

- Fluent API for setting parameters
- No magic strings
- Clear, readable component configuration

### Performance

- Zero runtime overhead
- No reflection for parameter setting
- Optimized IL code generation

## Advanced Scenarios

### Custom Parameter Types

```csharp
public class ComplexState : HtmxComponentParameters
{
    public List<string> Tags { get; set; } = new();
    public Dictionary<string, object> Metadata { get; set; } = new();
    public ProductFilter? Filter { get; set; }
}

[GenerateParameterMethods]
public partial class ComplexComponent : HtmxComponentBase<ComplexState>
{
    // Generates methods for complex types too
}
```

### Nested Components

```csharp
[GenerateParameterMethods]
public partial class ParentComponent : HtmxComponentBase<ParentState>
{
    [GenerateParameterMethods]
    public partial class ChildComponent : HtmxComponentBase<ChildState>
    {
        // Nested components also get generated methods
    }
}
```

## Analyzer Diagnostics

The source generator provides helpful diagnostics:

### HTMX001: Missing Partial Modifier

```csharp
// ❌ Error: Class must be partial
[GenerateParameterMethods]
public class MyComponent : HtmxComponentBase<MyState> { }

// ✅ Correct: Partial class
[GenerateParameterMethods]
public partial class MyComponent : HtmxComponentBase<MyState> { }
```

### HTMX002: Invalid Base Class

```csharp
// ❌ Error: Must inherit from HtmxComponentBase
[GenerateParameterMethods]
public partial class MyComponent { }

// ✅ Correct: Proper inheritance
[GenerateParameterMethods]
public partial class MyComponent : HtmxComponentBase<MyState> { }
```

## Debugging Source Generators

### Enable Generated File Output

In your `.csproj`:

```xml
<PropertyGroup>
    <EmitCompilerGeneratedFiles>true</EmitCompilerGeneratedFiles>
    <CompilerGeneratedFilesOutputPath>$(BaseIntermediateOutputPath)Generated</CompilerGeneratedFilesOutputPath>
</PropertyGroup>
```

### View Generated Code

1. Build the project
2. Check `obj/Generated/FastComponents.Generators/` directory
3. Inspect generated `.g.cs` files

## Integration with IDEs

### Visual Studio

- Full IntelliSense support
- Go to Definition works with generated methods
- Refactoring includes generated code

### JetBrains Rider

- Source generator support enabled by default
- Generated code visible in "External Sources"

### VS Code

- Requires OmniSharp with source generator support
- Enable in settings: `"omnisharp.enableRoslynAnalyzers": true`

## Best Practices

### Component Design

1. Keep parameter types simple and serializable
2. Use immutable properties where possible
3. Provide sensible defaults

### Performance

1. Minimize the number of parameters
2. Use value types for simple data
3. Consider parameter grouping for related values

### Maintenance

1. Document parameter purposes
2. Use meaningful parameter names
3. Version parameter changes carefully

## Future Enhancements

Planned improvements for source generators:

- Automatic JSON serialization context generation
- Parameter validation method generation
- Component factory method generation
- Automatic route parameter binding

## See Also

- [Component Development](Component-Development.md)
- [AOT Support](AOT-Support.md)
- [API Reference](API-Reference.md)