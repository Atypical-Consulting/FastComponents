# Security

FastComponents follows security best practices for building secure web applications with HTMX and ASP.NET Core.

<warning>
Security is critical for web applications. Always validate input, authenticate users, and follow the principle of least privilege.
</warning>

<chapter title="CSRF Protection" id="csrf-protection">

### Automatic CSRF Protection

ASP.NET Core provides built-in CSRF protection. FastComponents integrates seamlessly:

```C#
// Enable antiforgery services
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-CSRF-TOKEN";
});

// Components automatically include CSRF tokens
@inherits HtmxComponentBase

<form hx-post="/htmx/submit">
    @Html.AntiForgeryToken()
    <input type="text" name="data" />
    <button type="submit">Submit</button>
</form>
```

### HTMX CSRF Configuration

Configure HTMX to send CSRF tokens:

```javascript
// In your layout or app initialization
document.body.addEventListener('htmx:configRequest', 
    (evt) => {
    let token = document.querySelector(
        '[name=__RequestVerificationToken]').value;
    evt.detail.headers['X-CSRF-TOKEN'] = token;
});
```

</chapter>

## XSS Prevention

### Content Encoding

FastComponents automatically encodes output to prevent XSS:

```Razor
@inherits HtmxComponentBase<UserState>

<!-- Safe: Content is automatically encoded -->
<div>@Parameters.UserInput</div>

<!-- Unsafe: Only use with trusted content -->
<div>@((MarkupString)Parameters.TrustedHtml)</div>
```

### Input Validation

```C#
public class SecureFormState : HtmxComponentParameters
{
    private string _email = "";
    
    [Required]
    [EmailAddress]
    public string Email 
    { 
        get => _email;
        set => _email = HtmlEncoder.Default.Encode(value);
    }
    
    [Required]
    [StringLength(100, MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$")]
    public string Name { get; set; } = "";
}
```

## Authentication & Authorization

### Component-Level Authorization

```Razor
@inherits HtmxComponentBase
@attribute [Authorize]

<div>
    <h3>Authorized Content Only</h3>
    <p>Welcome, @User.Identity.Name!</p>
</div>
```

### Endpoint Authorization

```C#
app.MapHtmxGet<AdminComponent, AdminState>("/htmx/admin")
   .RequireAuthorization("AdminPolicy");

app.MapHtmxPost<UserComponent, UserState>("/htmx/user")
   .RequireAuthorization();
```

### Role-Based Access

```Razor
@inherits HtmxComponentBase

@if (User.IsInRole("Admin"))
{
    <button hx-delete="/api/user/@userId" 
            hx-confirm="Are you sure?">
        Delete User
    </button>
}
```

## Secure Headers

### Configure Security Headers

```C#
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
    
    await next();
});

// Or use a security headers middleware
app.UseSecurityHeaders(policies =>
{
    policies.AddFrameOptionsDeny()
        .AddXssProtectionBlock()
        .AddContentTypeOptionsNoSniff()
        .AddReferrerPolicyStrictOriginWhenCrossOrigin()
        .AddContentSecurityPolicy(builder =>
        {
            builder.AddDefaultSrc().Self();
            builder.AddScriptSrc().Self().UnsafeInline();
            builder.AddStyleSrc().Self().UnsafeInline();
        });
});
```

## Input Sanitization

### Server-Side Sanitization

```C#
using Ganss.XSS;

public class SanitizedComponent : HtmxComponentBase<ContentState>
{
    private readonly HtmlSanitizer _sanitizer;
    
    public SanitizedComponent()
    {
        _sanitizer = new HtmlSanitizer();
        _sanitizer.AllowedTags.Add("em");
        _sanitizer.AllowedTags.Add("strong");
    }
    
    protected override void OnParametersSet()
    {
        // Sanitize user content
        Parameters.Content = _sanitizer.Sanitize(Parameters.Content);
    }
}
```

### Validation Attributes

```C#
public class SecureInputState : HtmxComponentParameters
{
    [Required]
    [SqlInjectionValidator] // Custom validator
    public string Query { get; set; } = "";
    
    [Required]
    [FileExtensions(Extensions = "jpg,png,gif")]
    public string FileName { get; set; } = "";
    
    [Url]
    [AllowedDomains("example.com", "trusted.com")]
    public string? ExternalUrl { get; set; }
}
```

## Rate Limiting

### Configure Rate Limiting

```C#
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("htmx", options =>
    {
        options.PermitLimit = 100;
        options.Window = TimeSpan.FromMinutes(1);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 50;
    });
});

// Apply to HTMX endpoints
app.MapHtmxPost<SearchComponent, SearchState>("/htmx/search")
   .RequireRateLimiting("htmx");
```

### Component-Specific Rate Limiting

```C#
[RateLimit("search", PermitLimit = 10, Window = "00:01:00")]
public class SearchComponent : HtmxComponentBase<SearchState>
{
    // Component implementation
}
```

## Secure Communication

### HTTPS Enforcement

```C#
// Redirect HTTP to HTTPS
app.UseHttpsRedirection();

// Enforce HTTPS in production
if (app.Environment.IsProduction())
{
    app.UseHsts();
}
```

### Secure WebSocket/SSE

```Razor
<HtmxWsTag WsConnect="wss://secure.example.com/ws">
    <!-- Only use WSS in production -->
</HtmxWsTag>

<HtmxSseTag SseConnect="https://secure.example.com/sse">
    <!-- Only use HTTPS for SSE -->
</HtmxSseTag>
```

## Data Protection

### Encrypt Sensitive Data

```C#
public class ProtectedComponent : HtmxComponentBase<ProtectedState>
{
    private readonly IDataProtector _protector;
    
    public ProtectedComponent(IDataProtectionProvider provider)
    {
        _protector = provider.CreateProtector("ComponentData");
    }
    
    protected override void OnParametersSet()
    {
        // Decrypt sensitive data
        if (!string.IsNullOrEmpty(Parameters.EncryptedId))
        {
            Parameters.ActualId = _protector.Unprotect(Parameters.EncryptedId);
        }
    }
}
```

### Secure State Management

```C#
public class SecureState : HtmxComponentParameters
{
    [JsonIgnore] // Don't serialize sensitive data
    public string? ApiKey { get; set; }
    
    [Encrypted] // Custom attribute for encryption
    public string? SensitiveData { get; set; }
    
    public override void BindFromQuery(IQueryCollection query)
    {
        // Validate before binding
        if (!IsValidRequest(query))
        {
            throw new SecurityException("Invalid request");
        }
        
        base.BindFromQuery(query);
    }
}
```

## Security Checklist

### Development

- [ ] Enable CSRF protection
- [ ] Validate all user input
- [ ] Encode output properly
- [ ] Use parameterized queries
- [ ] Implement proper authentication
- [ ] Apply authorization checks

### Deployment

- [ ] Use HTTPS everywhere
- [ ] Set security headers
- [ ] Enable HSTS
- [ ] Configure rate limiting
- [ ] Disable detailed errors
- [ ] Remove debug endpoints

### Monitoring

- [ ] Log security events
- [ ] Monitor failed auth attempts
- [ ] Track rate limit violations
- [ ] Alert on suspicious patterns
- [ ] Regular security audits

## Common Vulnerabilities

### 1. HTMX-Specific XSS

```Razor
// ❌ Vulnerable: Unescaped attribute
<div hx-get="/api/data?q=@Parameters.Query">

// ✅ Safe: Properly encoded
<div hx-get="@($"/api/data?q={Uri.EscapeDataString(Parameters.Query)}")">
```

### 2. Insecure Direct Object References

```C#
// ❌ Vulnerable: No authorization check
app.MapHtmxGet<UserProfile, UserProfileState>("/htmx/user/{id}");

// ✅ Safe: Verify user access
app.MapHtmxGet<UserProfile, UserProfileState>("/htmx/user/{id}")
   .RequireAuthorization()
   .Add(endpointBuilder =>
   {
       endpointBuilder.Metadata.Add(new AuthorizeUserAccessAttribute());
   });
```

### 3. Mass Assignment

```C#
// ❌ Vulnerable: Binding all properties
public void UpdateUser(User user) { }

// ✅ Safe: Use specific DTOs
public void UpdateUser(UserUpdateDto dto) { }
```

## See Also

- [](Deployment.md)
- [](Testing.md)
- [](Advanced-Features.md)