# Component Development

This guide covers advanced techniques for developing FastComponents, from basic patterns to complex scenarios.

## Component Structure

### Basic Component Template

```razor
@inherits HtmxComponentBase
@* or @inherits SimpleHtmxComponent<TState> *@

<div @attributes="AdditionalAttributes" class="@ClassName">
    <!-- Component content -->
</div>

@code {
    // Component logic
}
```

### Component Organization

Organize your components for maintainability:

```
Components/
├── Shared/
│   ├── Button.razor
│   ├── Modal.razor
│   └── LoadingSpinner.razor
├── Features/
│   ├── Products/
│   │   ├── ProductList.razor
│   │   ├── ProductCard.razor
│   │   └── ProductDetail.razor
│   └── Cart/
│       ├── CartSummary.razor
│       └── CartItem.razor
└── _Imports.razor
```

## Component Patterns

### 1. Self-Updating Components

Components that update themselves without full page interaction:

```razor
@inherits SimpleHtmxComponent<TimerState>

<div class="timer" hx-get="@Url" hx-trigger="every 1s">
    <h3>Current Time: @State.CurrentTime</h3>
</div>

@code {
    protected override void OnGet(TimerState state)
    {
        State = state with { CurrentTime = DateTime.Now };
    }
}
```

### 2. Form Components

Handle form submissions with validation:

```razor
@inherits SimpleHtmxComponent<ContactFormState>

<form hx-post="@Url" hx-target="this" hx-swap="outerHTML">
    @if (!string.IsNullOrEmpty(State.SuccessMessage))
    {
        <div class="alert success">@State.SuccessMessage</div>
    }
    
    @if (State.Errors.Any())
    {
        <div class="alert error">
            @foreach (var error in State.Errors)
            {
                <p>@error</p>
            }
        </div>
    }
    
    <input type="text" name="name" value="@State.Name" 
           placeholder="Your name" required />
    
    <input type="email" name="email" value="@State.Email" 
           placeholder="Your email" required />
    
    <textarea name="message" placeholder="Your message" required>
        @State.Message
    </textarea>
    
    <button type="submit">Send Message</button>
</form>

@code {
    protected override ContactFormState OnPost(ContactFormState state)
    {
        var errors = ValidateForm(state);
        if (errors.Any())
        {
            return state with { Errors = errors };
        }
        
        // Process form...
        return state with 
        { 
            SuccessMessage = "Message sent successfully!",
            Name = "",
            Email = "",
            Message = "",
            Errors = []
        };
    }
    
    private List<string> ValidateForm(ContactFormState state)
    {
        var errors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(state.Name))
            errors.Add("Name is required");
            
        if (string.IsNullOrWhiteSpace(state.Email))
            errors.Add("Email is required");
        else if (!IsValidEmail(state.Email))
            errors.Add("Invalid email format");
            
        if (string.IsNullOrWhiteSpace(state.Message))
            errors.Add("Message is required");
            
        return errors;
    }
}
```

### 3. List Components with Actions

Interactive lists with inline actions:

```razor
@inherits SimpleHtmxComponent<TodoListState>

<div class="todo-list">
    <h2>Todo List (@State.Items.Count(i => !i.IsCompleted) remaining)</h2>
    
    <form hx-post="@Url" hx-target="closest .todo-list" hx-swap="outerHTML">
        <input type="text" name="newItem" placeholder="Add new item..." />
        <button type="submit" name="action" value="add">Add</button>
    </form>
    
    <ul>
        @foreach (var item in State.Items)
        {
            <li class="@(item.IsCompleted ? "completed" : "")">
                <form hx-post="@Url" hx-target="closest .todo-list" hx-swap="outerHTML">
                    <input type="hidden" name="itemId" value="@item.Id" />
                    
                    <span>@item.Text</span>
                    
                    @if (!item.IsCompleted)
                    {
                        <button name="action" value="complete">✓</button>
                    }
                    else
                    {
                        <button name="action" value="uncomplete">↺</button>
                    }
                    
                    <button name="action" value="delete" 
                            hx-confirm="Delete this item?">🗑</button>
                </form>
            </li>
        }
    </ul>
</div>

@code {
    protected override TodoListState OnPost(TodoListState state)
    {
        var form = Request.Form;
        var action = form["action"].ToString();
        
        return action switch
        {
            "add" => AddItem(state, form["newItem"]),
            "complete" => ToggleItem(state, form["itemId"], true),
            "uncomplete" => ToggleItem(state, form["itemId"], false),
            "delete" => DeleteItem(state, form["itemId"]),
            _ => state
        };
    }
}
```

### 4. Modal Components

Reusable modal pattern:

```razor
@inherits HtmxComponentBase

<div class="modal-backdrop" 
     hx-on:click="if(event.target === this) htmx.remove(this)">
    <div class="modal-content">
        <button class="modal-close" onclick="htmx.remove(closest('.modal-backdrop'))">
            ×
        </button>
        
        @ChildContent
    </div>
</div>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
```

Usage:

```razor
<button hx-get="/modal/edit-profile" hx-target="body" hx-swap="beforeend">
    Edit Profile
</button>
```

### 5. Infinite Scroll Components

Load more content as user scrolls:

```razor
@inherits SimpleHtmxComponent<ArticleListState>

<div class="article-list">
    @foreach (var article in State.Articles)
    {
        <article>
            <h3>@article.Title</h3>
            <p>@article.Summary</p>
        </article>
    }
    
    @if (State.HasMore)
    {
        <div hx-get="@Url?page=@(State.Page + 1)" 
             hx-trigger="revealed" 
             hx-swap="outerHTML"
             class="loading-trigger">
            Loading more...
        </div>
    }
</div>

@code {
    protected override void OnGet(ArticleListState state)
    {
        var newArticles = LoadArticles(state.Page);
        State = state with 
        { 
            Articles = state.Articles.Concat(newArticles).ToList(),
            Page = state.Page + 1,
            HasMore = newArticles.Any()
        };
    }
}
```

## Advanced Techniques

### Dynamic Attribute Building

Build attributes conditionally:

```razor
@inherits HtmxComponentBase

@code {
    protected override void OnInitialized()
    {
        if (ShouldAutoRefresh)
        {
            HxGet = "/api/status";
            HxTrigger = "every 5s";
            HxSwap = "innerHTML";
        }
        
        if (RequiresConfirmation)
        {
            HxConfirm = "Are you sure?";
        }
    }
}
```

### Component Composition

Compose complex components from simpler ones:

```razor
@* DataTable.razor *@
@inherits HtmxComponentBase
@typeparam TItem

<table class="data-table">
    <thead>
        <tr>
            @HeaderTemplate
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Items)
        {
            <tr>
                @RowTemplate(item)
            </tr>
        }
    </tbody>
</table>

@if (ShowPagination)
{
    <Pagination CurrentPage="@CurrentPage" 
                TotalPages="@TotalPages" 
                OnPageChange="@OnPageChange" />
}

@code {
    [Parameter] public IEnumerable<TItem> Items { get; set; } = [];
    [Parameter] public RenderFragment? HeaderTemplate { get; set; }
    [Parameter] public RenderFragment<TItem>? RowTemplate { get; set; }
    [Parameter] public bool ShowPagination { get; set; }
    [Parameter] public int CurrentPage { get; set; }
    [Parameter] public int TotalPages { get; set; }
    [Parameter] public EventCallback<int> OnPageChange { get; set; }
}
```

### Event Coordination

Coordinate events between components:

```razor
@inherits SimpleHtmxComponent<DashboardState>

<div class="dashboard">
    <!-- Sidebar triggers refresh of main content -->
    <aside>
        <button hx-post="@Url?filter=active" 
                hx-target="#main-content">
            Active Items
        </button>
        <button hx-post="@Url?filter=completed" 
                hx-target="#main-content">
            Completed Items
        </button>
    </aside>
    
    <main id="main-content">
        @RenderContent()
    </main>
</div>

@code {
    private RenderFragment RenderContent()
    {
        return builder =>
        {
            builder.OpenComponent<ItemList>(0);
            builder.AddAttribute(1, "Filter", State.Filter);
            builder.CloseComponent();
        };
    }
    
    protected override DashboardState OnPost(DashboardState state)
    {
        var filter = Request.Query["filter"].ToString();
        return state with { Filter = filter };
    }
}
```

### Lazy Loading

Load expensive content on demand:

```razor
@inherits HtmxComponentBase

<div class="lazy-content">
    @if (!IsLoaded)
    {
        <div hx-get="@LoadUrl" 
             hx-trigger="@LoadTrigger" 
             hx-swap="outerHTML">
            <LoadingSpinner />
        </div>
    }
    else
    {
        @ChildContent
    }
</div>

@code {
    [Parameter] public string LoadUrl { get; set; } = "";
    [Parameter] public string LoadTrigger { get; set; } = "revealed";
    [Parameter] public bool IsLoaded { get; set; }
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
```

### Error Handling

Graceful error handling in components:

```razor
@inherits SimpleHtmxComponent<DataState>

<div class="data-component">
    @if (State.IsLoading)
    {
        <LoadingSpinner />
    }
    else if (State.Error != null)
    {
        <ErrorDisplay Message="@State.Error" 
                     RetryUrl="@Url" />
    }
    else
    {
        @RenderData()
    }
</div>

@code {
    protected override async Task OnGetAsync(DataState state)
    {
        try
        {
            State = state with { IsLoading = true, Error = null };
            
            var data = await LoadDataAsync();
            State = state with 
            { 
                Data = data, 
                IsLoading = false 
            };
        }
        catch (Exception ex)
        {
            State = state with 
            { 
                Error = "Failed to load data", 
                IsLoading = false 
            };
            Logger.LogError(ex, "Error loading data");
        }
    }
}
```

## Performance Optimization

### 1. Component Output Caching

Cache rendered components:

```csharp
app.MapHtmxGet<ExpensiveComponent, ComponentState>("/expensive")
   .CacheOutput(policy => policy.Expire(TimeSpan.FromMinutes(5)));
```

### 2. Conditional Rendering

Only render what's necessary:

```razor
@if (ShouldRender)
{
    <ExpensiveChildComponent />
}
```

### 3. Debounced Inputs

Prevent excessive requests:

```razor
<input type="search" 
       hx-get="/search" 
       hx-trigger="keyup changed delay:500ms" 
       hx-target="#results" />
```

## Testing Components

### Unit Testing

Test component logic:

```csharp
[Fact]
public void Counter_Increments_On_Post()
{
    // Arrange
    var state = new CounterState { Count = 5 };
    var component = new CounterComponent();
    
    // Act
    var newState = component.OnPost(state);
    
    // Assert
    Assert.Equal(6, newState.Count);
}
```

### Integration Testing

Test component rendering:

```csharp
[Fact]
public async Task Component_Renders_Correctly()
{
    // Arrange
    using var ctx = new TestContext();
    var state = new ProductState { Name = "Test Product" };
    
    // Act
    var component = ctx.RenderComponent<ProductCard>(
        parameters => parameters.Add(p => p.State, state));
    
    // Assert
    Assert.Contains("Test Product", component.Markup);
}
```

## Best Practices

1. **Keep Components Focused**: Each component should have a single, clear purpose
2. **Use Meaningful Names**: Component names should describe what they display or do
3. **Minimize State**: Only include essential data in component state
4. **Handle Edge Cases**: Always handle loading, error, and empty states
5. **Test Thoroughly**: Write tests for component logic and rendering
6. **Document Complex Logic**: Add comments for non-obvious behavior
7. **Optimize Selectively**: Only optimize when you have measured performance issues

## Next Steps

- [HTMX Attributes](HTMX-Attributes.md) - Complete attribute reference
- [State Management](State-Management.md) - Advanced state patterns
- [Examples](Examples.md) - Real-world examples
- [Performance](Performance.md) - Optimization guide