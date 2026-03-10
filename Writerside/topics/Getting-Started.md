# Getting Started with FastComponents

This guide will help you set up your first FastComponents project and create a simple interactive component.

<chapter title="Prerequisites" id="prerequisites">

Before you begin, ensure you have:

.NET 9.0 SDK or later
: Download from [Microsoft's official site](https://dotnet.microsoft.com/download)

Code Editor
: Visual Studio, VS Code, or JetBrains Rider

Basic Knowledge
: C# and ASP.NET Core fundamentals

</chapter>

<chapter title="Installation" id="installation">

<procedure title="Setting up FastComponents" id="setup-procedure">

<step>Create a new ASP.NET Core Web Application:

<code-block lang="bash">
dotnet new web -n MyFastComponentsApp
cd MyFastComponentsApp
</code-block>
</step>

<step>Install the FastComponents NuGet package:

<code-block lang="bash">
dotnet add package FastComponents
</code-block>
</step>

<step>Configure services in `Program.cs`:

<code-block lang="c#">
using FastComponents;

var builder = WebApplication.CreateBuilder(args);

// Add FastComponents services
builder.Services.AddFastComponents();

var app = builder.Build();

// Use FastComponents middleware
app.UseFastComponents();

// Your endpoints will go here

app.Run();
</code-block>
</step>

</procedure>

</chapter>

<chapter title="Creating Your First Component" id="first-component">

<procedure title="Building a Counter Component" id="counter-component">

<step>Create the component file `Components/Counter.razor`:

<code-block lang="razor">
@inherits SimpleHtmxComponent&lt;CounterState&gt;

&lt;div class="counter"&gt;
    &lt;h2&gt;Counter: @State.Count&lt;/h2&gt;
    &lt;button hx-post="@Url" 
            hx-target="closest .counter" 
            hx-swap="outerHTML"&gt;
        Increment
    &lt;/button&gt;
&lt;/div&gt;

@code {
    protected override CounterState OnPost(CounterState state)
    {
        return state with { Count = state.Count + 1 };
    }
}
</code-block>
</step>

<step>Define the component state in `Models/CounterState.cs`:

<code-block lang="c#">
using FastComponents;

[GenerateParameterMethods]
public partial record CounterState : HtmxComponentParameters
{
    public int Count { get; init; } = 0;
}
</code-block>
</step>

</procedure>

</chapter>

<chapter title="Component Registration" id="registration">

Choose your preferred registration method:

<tabs>
    <tab title="Manual Registration" id="manual-tab">
        <p>Explicit endpoint mapping in your <code>Program.cs</code>:</p>
        
        <code-block lang="c#">
        // Map the counter component
        app.MapHtmxGet&lt;Counter, CounterState&gt;("/counter");
        app.MapHtmxPost&lt;Counter, CounterState&gt;("/counter");
        </code-block>
    </tab>
    
    <tab title="Auto Registration">
        <p>Convention-based automatic discovery:</p>
        
        <code-block lang="c#">
        // Replace AddFastComponents() and UseFastComponents() with:
        builder.Services.AddFastComponentsAuto();
        
        var app = builder.Build();
        app.UseFastComponentsAuto();
        
        // Components are automatically mapped based on naming conventions
        // Counter component → /htmx/counter
        </code-block>
    </tab>
</tabs>

</chapter>

### 4. Create the Main Page

Create an `index.html` file in `wwwroot`:

<code-block lang="html">
&lt;!DOCTYPE html&gt;
&lt;html&gt;
&lt;head&gt;
    &lt;title&gt;FastComponents Demo&lt;/title&gt;
    &lt;script src="https://unpkg.com/htmx.org@2.0.0"&gt;
    &lt;/script&gt;
    &lt;style&gt;
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
    &lt;/style&gt;
&lt;/head&gt;
&lt;body&gt;
    &lt;h1&gt;FastComponents Demo&lt;/h1&gt;
    
    &lt;!-- Load the counter component --&gt;
    &lt;div hx-get="/counter" hx-trigger="load"&gt;&lt;/div&gt;
&lt;/body&gt;
&lt;/html&gt;
</code-block>

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