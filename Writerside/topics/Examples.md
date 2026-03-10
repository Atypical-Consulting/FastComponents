# Examples and Tutorials

<tldr>
<p>Practical examples and real-world patterns for building interactive web applications with FastComponents.</p>
<p>From simple forms to complex dashboards - learn by example.</p>
</tldr>

This section provides practical examples of common patterns and real-world scenarios using FastComponents.

## Interactive Examples

<tabs>
<tab title="Live Search" id="live-search-tab">
Build a responsive search component that updates results as you type
</tab>
<tab title="Inline Editing" id="inline-edit-tab">
Create seamless in-place editing without page navigation
</tab>
<tab title="Shopping Cart" id="cart-tab">
Implement a dynamic cart with real-time updates
</tab>
<tab title="Tabbed Interface" id="tabs-tab">
Create dynamic tabs with lazy-loaded content
</tab>
<tab title="Data Table" id="table-tab">
Build feature-rich tables with sorting and filtering
</tab>
</tabs>

<chapter title="Live Search Component" id="live-search-example">

A search component that updates results as you type, providing instant feedback to users.

<procedure title="Building a Live Search Component" id="build-live-search">

<step>Create the search state record:

<code-block lang="c#">
// Models/SearchState.cs
[GenerateParameterMethods]
public partial record SearchState : HtmxComponentParameters
{
    public string Query { get; init; } = "";
    public List&lt;Product&gt; Results { get; init; } = [];
    public bool IsLoading { get; init; }
}
</code-block>
</step>

<step>Implement the search component:

<code-block lang="c#">
// Components/LiveSearch.razor
@inherits SimpleHtmxComponent&lt;SearchState&gt;

&lt;div class="search-container"&gt;
    &lt;form hx-get="@Url" 
          hx-target="#search-results" 
          hx-trigger="keyup changed delay:500ms from:input"&gt;
        &lt;input type="search" 
               name="query" 
               value="@State.Query"
               placeholder="Search products..." 
               autocomplete="off" /&gt;
    &lt;/form&gt;
    
    &lt;div id="search-results"&gt;
        @if (!string.IsNullOrEmpty(State.Query))
        {
            @if (State.IsLoading)
            {
                &lt;div class="loading"&gt;Searching...&lt;/div&gt;
            }
            else if (!State.Results.Any())
            {
                &lt;div class="no-results"&gt;
                    No results found for "@State.Query"
                &lt;/div&gt;
            }
            else
            {
                &lt;div class="results"&gt;
                    @foreach (var product in State.Results)
                    {
                        &lt;div class="result-item"&gt;
                            &lt;h4&gt;@product.Name&lt;/h4&gt;
                            &lt;p&gt;@product.Description&lt;/p&gt;
                            &lt;span class="price"&gt;$@product.Price&lt;/span&gt;
                        &lt;/div&gt;
                    }
                &lt;/div&gt;
            }
        }
    &lt;/div&gt;
&lt;/div&gt;

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

</code-block>
</step>

</procedure>

<tip>
The `delay:500ms` trigger prevents excessive server requests while typing. Adjust the delay based on your application's needs.
</tip>

</chapter>

<chapter title="Inline Editing Component" id="inline-edit-example">

Edit content in place without navigating away, providing a seamless user experience.

<warning>
Always validate and sanitize user input on the server side, even for inline editing scenarios.
</warning>

<code-block lang="c#">
// Components/EditableField.razor
@inherits SimpleHtmxComponent&lt;EditableFieldState&gt;

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
</code-block>

</chapter>

<chapter title="Shopping Cart Component" id="shopping-cart-example">

A dynamic shopping cart with real-time updates that demonstrates complex state management.

<note>
This example shows how to handle multiple actions (add, remove, update quantities) within a single component.
</note>

<code-block lang="c#">
// Components/ShoppingCart.razor
@inherits SimpleHtmxComponent&lt;CartState&gt;

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
</code-block>

</chapter>

<chapter title="Tabbed Interface Component" id="tabs-example">

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

<cards>
<card title="📝 Multi-Step Form Wizard" id="wizard-card">
Learn how to build complex forms with validation and progressive disclosure.
</card>
<card title="📊 Real-Time Dashboard" id="dashboard-card">
Create live dashboards with Server-Sent Events and WebSocket integration.
</card>
</cards>

<chapter title="Multi-Step Form Wizard" id="form-wizard-example">

A complete wizard implementation demonstrating step management, validation, and data flow.

<procedure title="Creating a Form Wizard" id="build-wizard">

<step>Define the wizard state:

<code-block lang="c#">
[GenerateParameterMethods]
public partial record WizardState : HtmxComponentParameters
{
    public int CurrentStep { get; init; } = 1;
    public int TotalSteps { get; init; } = 3;
    public PersonalInfo PersonalInfo { get; init; } = new();
    public Address Address { get; init; } = new();
    public bool ShowErrors { get; init; }
}
</code-block>
</step>

<step>Implement the wizard component:

<code-block lang="c#">
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
</code-block>
</step>

</procedure>

</chapter>

<chapter title="Real-Time Dashboard" id="dashboard-example">

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
</code-block>
</step>

</procedure>

<tip>
Use SSE for one-way real-time updates and WebSockets for 
bidirectional communication.
</tip>

</chapter>

## Best Practices from Examples

<deflist>
<def title="User Feedback">
Always provide visual feedback during operations - loading states, 
success/error messages, progress indicators, and confirmation dialogs.
</def>
<def title="Progressive Enhancement">
Start with a working non-JavaScript version when possible - forms should 
work without HTMX, links should have valid href attributes.
</def>
<def title="State Management">
Keep component state minimal and focused - only store what's needed 
for rendering, derive computed values, use URL state for shareable views.
</def>
<def title="Error Handling">
Handle errors gracefully - show user-friendly error messages, 
provide retry mechanisms, log errors for debugging.
</def>
<def title="Performance">
Optimize for performance - debounce rapid triggers, use appropriate 
swap strategies, implement caching where beneficial, lazy load 
content.
</def>
</deflist>

## Next Steps

- [State Management](State-Management.md) - Advanced state patterns
- [Performance](Performance.md) - Optimization techniques
- [Testing](Testing.md) - Testing strategies
- [Deployment](Deployment.md) - Production deployment