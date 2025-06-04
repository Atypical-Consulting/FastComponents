# Examples and Tutorials

This section provides practical examples of common patterns and real-world scenarios using FastComponents.

## Interactive Examples

### 1. Live Search

A search component that updates results as you type:

```razor
@* Components/LiveSearch.razor *@
@inherits SimpleHtmxComponent<SearchState>

<div class="search-container">
    <form hx-get="@Url" 
          hx-target="#search-results" 
          hx-trigger="keyup changed delay:500ms from:input">
        <input type="search" 
               name="query" 
               value="@State.Query"
               placeholder="Search products..." 
               autocomplete="off" />
    </form>
    
    <div id="search-results">
        @if (!string.IsNullOrEmpty(State.Query))
        {
            @if (State.IsLoading)
            {
                <div class="loading">Searching...</div>
            }
            else if (!State.Results.Any())
            {
                <div class="no-results">
                    No results found for "@State.Query"
                </div>
            }
            else
            {
                <div class="results">
                    @foreach (var product in State.Results)
                    {
                        <div class="result-item">
                            <h4>@product.Name</h4>
                            <p>@product.Description</p>
                            <span class="price">$@product.Price</span>
                        </div>
                    }
                </div>
            }
        }
    </div>
</div>

@code {
    [Inject] private IProductService ProductService { get; set; } = null!;
    
    protected override async Task OnGetAsync(SearchState state)
    {
        if (string.IsNullOrWhiteSpace(state.Query))
        {
            State = state with { Results = [], IsLoading = false };
            return;
        }
        
        State = state with { IsLoading = true };
        
        var results = await ProductService.SearchAsync(state.Query);
        State = state with { Results = results, IsLoading = false };
    }
}

// Models/SearchState.cs
[GenerateParameterMethods]
public partial record SearchState : HtmxComponentParameters
{
    public string Query { get; init; } = "";
    public List<Product> Results { get; init; } = [];
    public bool IsLoading { get; init; }
}
```

### 2. Inline Editing

Edit content in place without navigating away:

```razor
@* Components/EditableField.razor *@
@inherits SimpleHtmxComponent<EditableFieldState>

@if (State.IsEditing)
{
    <form hx-post="@Url" 
          hx-target="this" 
          hx-swap="outerHTML"
          class="inline-edit-form">
        <input type="text" 
               name="value" 
               value="@State.Value" 
               autofocus />
        <button type="submit" name="action" value="save">✓</button>
        <button type="button" 
                name="action" 
                value="cancel"
                hx-get="@Url">✗</button>
    </form>
}
else
{
    <div class="editable-field" 
         hx-get="@Url?edit=true" 
         hx-swap="outerHTML">
        <span>@State.Value</span>
        <button class="edit-btn">✏️</button>
    </div>
}

@code {
    protected override EditableFieldState OnGet(EditableFieldState state)
    {
        var isEditing = Request.Query["edit"] == "true";
        return state with { IsEditing = isEditing };
    }
    
    protected override async Task<EditableFieldState> OnPostAsync(EditableFieldState state)
    {
        var action = Request.Form["action"];
        
        if (action == "save")
        {
            var newValue = Request.Form["value"].ToString();
            // Save to database
            await SaveValueAsync(state.FieldId, newValue);
            return state with { Value = newValue, IsEditing = false };
        }
        
        return state with { IsEditing = false };
    }
}
```

### 3. Shopping Cart

A dynamic shopping cart with real-time updates:

```razor
@* Components/ShoppingCart.razor *@
@inherits SimpleHtmxComponent<CartState>

<div class="shopping-cart" id="cart">
    <h3>Shopping Cart (@State.Items.Count items)</h3>
    
    @if (!State.Items.Any())
    {
        <p class="empty-cart">Your cart is empty</p>
    }
    else
    {
        <div class="cart-items">
            @foreach (var item in State.Items)
            {
                <div class="cart-item">
                    <img src="@item.ImageUrl" alt="@item.Name" />
                    <div class="item-details">
                        <h4>@item.Name</h4>
                        <p class="price">$@item.Price</p>
                    </div>
                    <div class="quantity-controls">
                        <button hx-post="@Url" 
                                hx-vals='@($"{{\"action\":\"decrease\",\"productId\":{item.ProductId}}}")'
                                hx-target="#cart"
                                hx-swap="outerHTML">-</button>
                        <span>@item.Quantity</span>
                        <button hx-post="@Url" 
                                hx-vals='@($"{{\"action\":\"increase\",\"productId\":{item.ProductId}}}")'
                                hx-target="#cart"
                                hx-swap="outerHTML">+</button>
                    </div>
                    <button class="remove-btn"
                            hx-delete="@Url" 
                            hx-vals='@($"{{\"productId\":{item.ProductId}}}")'
                            hx-target="#cart"
                            hx-swap="outerHTML"
                            hx-confirm="Remove this item?">🗑</button>
                </div>
            }
        </div>
        
        <div class="cart-summary">
            <div class="subtotal">
                <span>Subtotal:</span>
                <span>$@State.Subtotal</span>
            </div>
            <div class="tax">
                <span>Tax:</span>
                <span>$@State.Tax</span>
            </div>
            <div class="total">
                <span>Total:</span>
                <span>$@State.Total</span>
            </div>
            <button class="checkout-btn">Proceed to Checkout</button>
        </div>
    }
</div>

@code {
    [Inject] private ICartService CartService { get; set; } = null!;
    
    protected override async Task<CartState> OnPostAsync(CartState state)
    {
        var action = Request.Form["action"].ToString();
        var productId = int.Parse(Request.Form["productId"]);
        
        switch (action)
        {
            case "increase":
                await CartService.IncreaseQuantityAsync(productId);
                break;
            case "decrease":
                await CartService.DecreaseQuantityAsync(productId);
                break;
        }
        
        return await LoadCartStateAsync();
    }
    
    protected override async Task<CartState> OnDeleteAsync(CartState state)
    {
        var productId = int.Parse(Request.Form["productId"]);
        await CartService.RemoveItemAsync(productId);
        return await LoadCartStateAsync();
    }
}
```

### 4. Tabbed Interface

Dynamic tabs with lazy-loaded content:

```razor
@* Components/TabbedContent.razor *@
@inherits SimpleHtmxComponent<TabState>

<div class="tabs-container">
    <div class="tab-buttons">
        @foreach (var tab in State.Tabs)
        {
            <button class="tab-btn @(State.ActiveTab == tab.Id ? "active" : "")"
                    hx-get="@Url?tab=@tab.Id"
                    hx-target="#tab-content"
                    hx-push-url="true">
                @tab.Title
            </button>
        }
    </div>
    
    <div id="tab-content" class="tab-panel">
        @if (State.IsLoading)
        {
            <div class="loading">Loading @State.ActiveTabTitle...</div>
        }
        else
        {
            @State.Content
        }
    </div>
</div>

@code {
    protected override async Task OnGetAsync(TabState state)
    {
        var tabId = Request.Query["tab"].ToString();
        if (string.IsNullOrEmpty(tabId))
            tabId = state.Tabs.First().Id;
            
        State = state with { ActiveTab = tabId, IsLoading = true };
        
        // Load tab content
        var content = await LoadTabContentAsync(tabId);
        State = state with 
        { 
            Content = content, 
            IsLoading = false,
            ActiveTabTitle = state.Tabs.First(t => t.Id == tabId).Title
        };
    }
}
```

### 5. Data Table with Sorting and Filtering

A feature-rich data table:

```razor
@* Components/DataTable.razor *@
@inherits SimpleHtmxComponent<DataTableState>

<div class="data-table-container">
    <!-- Filters -->
    <div class="filters">
        <input type="search" 
               placeholder="Search..." 
               value="@State.SearchTerm"
               hx-get="@Url" 
               hx-trigger="keyup changed delay:500ms"
               hx-target=".data-table-container"
               hx-swap="outerHTML"
               name="search" />
               
        <select hx-get="@Url" 
                hx-target=".data-table-container"
                hx-swap="outerHTML"
                name="status">
            <option value="">All Status</option>
            <option value="active" selected="@(State.StatusFilter == "active")">Active</option>
            <option value="inactive" selected="@(State.StatusFilter == "inactive")">Inactive</option>
        </select>
    </div>
    
    <!-- Table -->
    <table class="data-table">
        <thead>
            <tr>
                <th>
                    <a hx-get="@GetSortUrl("name")"
                       hx-target=".data-table-container"
                       hx-swap="outerHTML">
                        Name @GetSortIcon("name")
                    </a>
                </th>
                <th>
                    <a hx-get="@GetSortUrl("email")"
                       hx-target=".data-table-container"
                       hx-swap="outerHTML">
                        Email @GetSortIcon("email")
                    </a>
                </th>
                <th>
                    <a hx-get="@GetSortUrl("status")"
                       hx-target=".data-table-container"
                       hx-swap="outerHTML">
                        Status @GetSortIcon("status")
                    </a>
                </th>
                <th>Actions</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var user in State.Users)
            {
                <tr>
                    <td>@user.Name</td>
                    <td>@user.Email</td>
                    <td>
                        <span class="status @user.Status">
                            @user.Status
                        </span>
                    </td>
                    <td>
                        <button hx-get="/users/@user.Id/edit"
                                hx-target="#modal"
                                hx-swap="innerHTML">
                            Edit
                        </button>
                        <button hx-delete="/users/@user.Id"
                                hx-target="closest tr"
                                hx-swap="outerHTML"
                                hx-confirm="Delete this user?">
                            Delete
                        </button>
                    </td>
                </tr>
            }
        </tbody>
    </table>
    
    <!-- Pagination -->
    <div class="pagination">
        @if (State.CurrentPage > 1)
        {
            <a hx-get="@Url?page=@(State.CurrentPage - 1)"
               hx-target=".data-table-container"
               hx-swap="outerHTML">
                Previous
            </a>
        }
        
        @for (int i = 1; i <= State.TotalPages; i++)
        {
            <a hx-get="@Url?page=@i"
               hx-target=".data-table-container"
               hx-swap="outerHTML"
               class="@(i == State.CurrentPage ? "active" : "")">
                @i
            </a>
        }
        
        @if (State.CurrentPage < State.TotalPages)
        {
            <a hx-get="@Url?page=@(State.CurrentPage + 1)"
               hx-target=".data-table-container"
               hx-swap="outerHTML">
                Next
            </a>
        }
    </div>
</div>

@code {
    private string GetSortUrl(string column)
    {
        var direction = State.SortColumn == column && State.SortDirection == "asc" 
            ? "desc" : "asc";
        return $"{Url}?sort={column}&dir={direction}";
    }
    
    private string GetSortIcon(string column)
    {
        if (State.SortColumn != column) return "↕";
        return State.SortDirection == "asc" ? "↑" : "↓";
    }
}
```

## Complete Application Examples

### Multi-Step Form Wizard

A complete wizard implementation:

```razor
@* Components/FormWizard.razor *@
@inherits SimpleHtmxComponent<WizardState>

<div class="wizard">
    <!-- Progress -->
    <div class="wizard-progress">
        @for (int i = 1; i <= State.TotalSteps; i++)
        {
            <div class="step @(i <= State.CurrentStep ? "completed" : "") 
                           @(i == State.CurrentStep ? "active" : "")">
                <span class="step-number">@i</span>
                <span class="step-title">@GetStepTitle(i)</span>
            </div>
        }
    </div>
    
    <!-- Content -->
    <div class="wizard-content">
        @switch (State.CurrentStep)
        {
            case 1:
                <PersonalInfoStep Data="@State.PersonalInfo" />
                break;
            case 2:
                <AddressStep Data="@State.Address" />
                break;
            case 3:
                <ReviewStep PersonalInfo="@State.PersonalInfo" 
                           Address="@State.Address" />
                break;
        }
    </div>
    
    <!-- Navigation -->
    <div class="wizard-nav">
        @if (State.CurrentStep > 1)
        {
            <button hx-post="@Url" 
                    hx-vals='{"action": "previous"}'
                    hx-target=".wizard"
                    hx-swap="outerHTML">
                Previous
            </button>
        }
        
        @if (State.CurrentStep < State.TotalSteps)
        {
            <button hx-post="@Url" 
                    hx-vals='{"action": "next"}'
                    hx-target=".wizard"
                    hx-swap="outerHTML"
                    hx-validate="true">
                Next
            </button>
        }
        else
        {
            <button hx-post="@Url" 
                    hx-vals='{"action": "submit"}'
                    hx-target=".wizard"
                    hx-swap="outerHTML">
                Submit
            </button>
        }
    </div>
</div>

@code {
    protected override WizardState OnPost(WizardState state)
    {
        var action = Request.Form["action"].ToString();
        
        return action switch
        {
            "next" => NextStep(state),
            "previous" => state with { CurrentStep = state.CurrentStep - 1 },
            "submit" => SubmitWizard(state),
            _ => state
        };
    }
    
    private WizardState NextStep(WizardState state)
    {
        // Validate current step
        if (!ValidateStep(state.CurrentStep, state))
        {
            return state with { ShowErrors = true };
        }
        
        // Save form data based on current step
        return state.CurrentStep switch
        {
            1 => state with 
            { 
                PersonalInfo = GetPersonalInfoFromForm(), 
                CurrentStep = 2 
            },
            2 => state with 
            { 
                Address = GetAddressFromForm(), 
                CurrentStep = 3 
            },
            _ => state
        };
    }
}
```

### Real-Time Dashboard

A dashboard with live updates:

```razor
@* Components/Dashboard.razor *@
@inherits SimpleHtmxComponent<DashboardState>

<div class="dashboard" hx-ext="sse" sse-connect="/dashboard/stream">
    <!-- Metrics -->
    <div class="metrics-grid">
        <div class="metric-card" 
             hx-get="/metrics/users" 
             hx-trigger="load, sse:users-updated">
            <h3>Total Users</h3>
            <div class="metric-value">@State.TotalUsers</div>
        </div>
        
        <div class="metric-card" 
             hx-get="/metrics/revenue" 
             hx-trigger="load, sse:revenue-updated">
            <h3>Revenue</h3>
            <div class="metric-value">$@State.Revenue</div>
        </div>
        
        <div class="metric-card" 
             hx-get="/metrics/orders" 
             hx-trigger="load, sse:orders-updated">
            <h3>Orders Today</h3>
            <div class="metric-value">@State.OrdersToday</div>
        </div>
    </div>
    
    <!-- Activity Feed -->
    <div class="activity-feed" 
         hx-get="/activity" 
         hx-trigger="load, every 30s">
        <h3>Recent Activity</h3>
        <div class="feed-items">
            @foreach (var activity in State.RecentActivities)
            {
                <div class="feed-item">
                    <span class="time">@activity.Time</span>
                    <span class="action">@activity.Description</span>
                </div>
            }
        </div>
    </div>
    
    <!-- Chart -->
    <div class="chart-container" 
         hx-get="/charts/sales" 
         hx-trigger="load"
         hx-swap="innerHTML">
        <!-- Chart rendered here -->
    </div>
</div>
```

## Best Practices from Examples

### 1. User Feedback
Always provide visual feedback during operations:
- Loading states
- Success/error messages
- Progress indicators
- Confirmation dialogs

### 2. Progressive Enhancement
Start with a working non-JavaScript version when possible:
- Forms should work without HTMX
- Links should have valid href attributes
- Enhance with HTMX attributes

### 3. State Management
Keep component state minimal and focused:
- Only store what's needed for rendering
- Derive computed values
- Use URL state for shareable views

### 4. Error Handling
Handle errors gracefully:
- Show user-friendly error messages
- Provide retry mechanisms
- Log errors for debugging

### 5. Performance
Optimize for performance:
- Debounce rapid triggers
- Use appropriate swap strategies
- Implement caching where beneficial
- Lazy load expensive content

## Next Steps

- [State Management](State-Management.md) - Advanced state patterns
- [Performance](Performance.md) - Optimization techniques
- [Testing](Testing.md) - Testing strategies
- [Deployment](Deployment.md) - Production deployment