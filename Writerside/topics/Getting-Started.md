# Getting Started with FastComponents

This guide will help you set up your first FastComponents project and create a simple interactive component.

## Prerequisites

- .NET 9.0 SDK or later
- A code editor (Visual Studio, VS Code, or JetBrains Rider)
- Basic knowledge of C# and ASP.NET Core

## Installation

### 1. Create a New Project

Create a new ASP.NET Core Web Application:

```bash
dotnet new web -n MyFastComponentsApp
cd MyFastComponentsApp
```

### 2. Install FastComponents

Add the FastComponents NuGet package:

```bash
dotnet add package FastComponents
```

### 3. Configure Services

Update your `Program.cs` to register FastComponents services:

```C#
using FastComponents;

var builder = WebApplication.CreateBuilder(args);

// Add FastComponents services
builder.Services.AddFastComponents();

var app = builder.Build();

// Use FastComponents middleware
app.UseFastComponents();

// Your endpoints will go here

app.Run();
```

## Creating Your First Component

### 1. Create a Counter Component

Create a new file `Components/Counter.razor`:

```Razor
@inherits SimpleHtmxComponent<CounterState>

<div class="counter">
    <h2>Counter: @State.Count</h2>
    <button hx-post="@Url" 
            hx-target="closest .counter" 
            hx-swap="outerHTML">
        Increment
    </button>
</div>

@code {
    protected override CounterState OnPost(CounterState state)
    {
        return state with { Count = state.Count + 1 };
    }
}
```

### 2. Define the Component State

Create `Models/CounterState.cs`:

```C#
using FastComponents;

[GenerateParameterMethods]
public partial record CounterState : HtmxComponentParameters
{
    public int Count { get; init; } = 0;
}
```

### 3. Map the Component Endpoint

In your `Program.cs`, add the component mapping:

```C#
// Map the counter component
app.MapHtmxGet<Counter, CounterState>("/counter");
app.MapHtmxPost<Counter, CounterState>("/counter");
```

### 4. Create the Main Page

Create an `index.html` file in `wwwroot`:

```html
<!DOCTYPE html>
<html>
<head>
    <title>FastComponents Demo</title>
    <script src="https://unpkg.com/htmx.org@2.0.0"></script>
    <style>
        body {
            font-family: system-ui, -apple-system, sans-serif;
            max-width: 800px;
            margin: 0 auto;
            padding: 2rem;
        }
        .counter {
            border: 1px solid #ddd;
            border-radius: 8px;
            padding: 1rem;
            margin: 1rem 0;
        }
        button {
            padding: 0.5rem 1rem;
            font-size: 1rem;
            cursor: pointer;
        }
    </style>
</head>
<body>
    <h1>FastComponents Demo</h1>
    
    <!-- Load the counter component -->
    <div hx-get="/counter" hx-trigger="load"></div>
</body>
</html>
```

### 5. Serve Static Files

Update `Program.cs` to serve static files:

```C#
app.UseStaticFiles();
app.MapFallbackToFile("index.html");
```

## Running the Application

1. Run the application:
   ```bash
   dotnet run
   ```

2. Open your browser and navigate to `https://localhost:5001` (or the port shown in the console)

3. You should see a counter that increments when you click the button - all without page refreshes!

## What's Happening?

1. **Initial Load**: The `hx-get="/counter"` attribute on the div loads the counter component when the page loads
2. **Component Rendering**: The server renders the Counter component and returns it as HTML
3. **Interactions**: When you click the button, HTMX sends a POST request to `/counter`
4. **State Update**: The `OnPost` method increments the count and returns the updated state
5. **DOM Update**: HTMX replaces the counter div with the newly rendered component

## Using the Simplified API

For even quicker setup, use the auto-configuration methods:

```C#
var builder = WebApplication.CreateBuilder(args);

// Automatically configure everything
builder.Services.AddFastComponentsAuto();

var app = builder.Build();

// Automatically map all components by convention
app.UseFastComponentsAuto();

app.Run();
```

This will automatically discover and map all components in your assembly that inherit from `SimpleHtmxComponent` or `HtmxComponentBase`.

## Next Steps

Now that you have a working FastComponents application, explore:

- [Component Development](Component-Development.md) - Learn advanced component techniques
- [HTMX Attributes](HTMX-Attributes.md) - Understand all available HTMX attributes
- [State Management](State-Management.md) - Handle complex state scenarios
- [Examples](Examples.md) - See more complex examples