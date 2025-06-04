# Deployment

This guide covers deploying FastComponents applications to various hosting platforms and environments.

## Production Configuration

### Environment Settings

```C#
var builder = WebApplication.CreateBuilder(args);

// Configure for production
if (builder.Environment.IsProduction())
{
    builder.Services.AddHsts(options =>
    {
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromDays(365);
    });
}

// Add FastComponents
builder.Services.AddFastComponents();

var app = builder.Build();

// Production middleware
if (app.Environment.IsProduction())
{
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseFastComponents();
app.Run();
```

### Static File Optimization

```C#
// Configure static files
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        // Cache static assets for 30 days
        if (ctx.File.Name.EndsWith(".js") || 
            ctx.File.Name.EndsWith(".css") ||
            ctx.File.Name.EndsWith(".woff2"))
        {
            ctx.Context.Response.Headers.CacheControl = 
                "public,max-age=2592000"; // 30 days
        }
    }
});
```

## Docker Deployment

### Dockerfile

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["YourApp/YourApp.csproj", "YourApp/"]
COPY ["FastComponents/FastComponents.csproj", "FastComponents/"]
RUN dotnet restore "YourApp/YourApp.csproj"
COPY . .
WORKDIR "/src/YourApp"
RUN dotnet build "YourApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "YourApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "YourApp.dll"]
```

### Docker Compose

```yaml
version: '3.8'
services:
  app:
    build: .
    ports:
      - "8080:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
      - ASPNETCORE_URLS=http://+:8080
      - ConnectionStrings__DefaultConnection=Server=db;Database=AppDB;User Id=sa;Password=YourPassword;
    depends_on:
      - db
    volumes:
      - ./logs:/app/logs

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - SA_PASSWORD=YourPassword
    volumes:
      - db_data:/var/opt/mssql

volumes:
  db_data:
```

## Cloud Deployment

### Azure App Service

#### Deploy with Azure CLI

```bash
# Create resource group
az group create --name myResourceGroup --location "East US"

# Create App Service plan
az appservice plan create --name myAppServicePlan --resource-group myResourceGroup --sku B1 --is-linux

# Create web app
az webapp create --resource-group myResourceGroup --plan myAppServicePlan --name myFastComponentsApp --runtime "DOTNETCORE:9.0"

# Deploy from ZIP
az webapp deploy --resource-group myResourceGroup --name myFastComponentsApp --src-path ./publish.zip --type zip
```

#### App Service Configuration

```json
{
  "ASPNETCORE_ENVIRONMENT": "Production",
  "WEBSITE_RUN_FROM_PACKAGE": "1",
  "ConnectionStrings__DefaultConnection": "your-connection-string"
}
```

### AWS Elastic Beanstalk

#### Configuration File (.ebextensions/01-aspnetcore.config)

```yaml
option_settings:
  aws:elasticbeanstalk:container:dotnet:
    Target: "/aws-eb-reqs.txt"
  aws:elasticbeanstalk:application:environment:
    ASPNETCORE_ENVIRONMENT: Production
    ConnectionStrings__DefaultConnection: "your-connection-string"
```

### Google Cloud Run

#### Deploy Script

```bash
# Build and push container
gcloud builds submit --tag gcr.io/PROJECT_ID/fastcomponents-app

# Deploy to Cloud Run
gcloud run deploy fastcomponents-app \
  --image gcr.io/PROJECT_ID/fastcomponents-app \
  --platform managed \
  --region us-central1 \
  --allow-unauthenticated \
  --set-env-vars ASPNETCORE_ENVIRONMENT=Production
```

## Performance Optimization

### Response Compression

```C#
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/javascript", "text/css", "text/html" });
});

app.UseResponseCompression();
```

### Output Caching

```C#
builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Cache());
    options.AddPolicy("StaticContent", builder => 
        builder.Cache().Expire(TimeSpan.FromHours(1)));
});

app.UseOutputCache();

// Apply to endpoints
app.MapHtmxGet<StaticComponent, EmptyState>("/htmx/static")
   .CacheOutput("StaticContent");
```

### CDN Configuration

For static assets, configure a CDN:

```C#
builder.Services.Configure<StaticFileOptions>(options =>
{
    if (builder.Environment.IsProduction())
    {
        options.FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.WebRootPath, "static"));
        options.RequestPath = "/static";
    }
});
```

## Database Deployment

### Entity Framework Migrations

```C#
// In Program.cs for automatic migrations
if (app.Environment.IsProduction())
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}
```

### Migration Scripts

```bash
# Generate migration script
dotnet ef migrations script --startup-project YourApp --project YourApp.Data --output migration.sql

# Apply in production
sqlcmd -S server -d database -i migration.sql
```

## Security Configuration

### HTTPS Configuration

```C#
builder.Services.AddHttpsRedirection(options =>
{
    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
    options.HttpsPort = 443;
});

// Certificate configuration
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ConfigureHttpsDefaults(httpsOptions =>
    {
        httpsOptions.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
    });
});
```

### Security Headers

```C#
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
    context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
    
    await next();
});
```

## Environment-Specific Configuration

### appsettings.Production.json

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "yourdomain.com",
  "ConnectionStrings": {
    "DefaultConnection": "Production-Connection-String"
  },
  "FastComponents": {
    "EnableDebugHeaders": false,
    "CacheComponentsInProduction": true
  }
}
```

### Secrets Management

```bash
# Azure Key Vault
dotnet add package Azure.Extensions.AspNetCore.Configuration.Secrets

# AWS Systems Manager
dotnet add package Amazon.Extensions.Configuration.SystemsManager

# Google Secret Manager
dotnet add package Google.Cloud.SecretManager.V1
```

```C#
// Configure secrets
if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        vaultUri: "https://yourvault.vault.azure.net/",
        credential: new DefaultAzureCredential());
}
```

## Monitoring and Logging

### Application Insights

```csharp
builder.Services.AddApplicationInsightsTelemetry();

// Custom telemetry
builder.Services.AddSingleton<ITelemetryInitializer, CustomTelemetryInitializer>();
```

### Structured Logging

```csharp
builder.Services.AddLogging(builder =>
{
    builder.AddConsole();
    builder.AddApplicationInsights();
    
    if (builder.Environment.IsProduction())
    {
        builder.AddEventLog();
    }
});
```

## Health Checks

### Configure Health Checks

```csharp
builder.Services.AddHealthChecks()
    .AddDbContext<AppDbContext>()
    .AddCheck("htmx-endpoints", () =>
    {
        // Custom health check for HTMX endpoints
        return HealthCheckResult.Healthy();
    });

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
```

## Deployment Checklist

### Pre-Deployment

- [ ] Environment variables configured
- [ ] Connection strings updated
- [ ] HTTPS certificates installed
- [ ] Database migrations prepared
- [ ] Static files optimized
- [ ] Security headers configured

### Post-Deployment

- [ ] Health checks passing
- [ ] Logs are being generated
- [ ] Performance metrics baseline
- [ ] Error tracking configured
- [ ] Backup procedures tested
- [ ] Rollback plan ready

### Monitoring

- [ ] Application performance monitoring
- [ ] Error rate monitoring  
- [ ] Database performance monitoring
- [ ] Infrastructure monitoring
- [ ] Security monitoring
- [ ] User experience monitoring

## Troubleshooting

### Common Issues

#### HTMX Requests Failing

```csharp
// Enable detailed errors temporarily
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}
```

#### Static Files Not Loading

```csharp
// Verify static file configuration
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot")),
    RequestPath = "/static"
});
```

#### Performance Issues

1. Enable response compression
2. Configure output caching
3. Optimize database queries
4. Use CDN for static assets
5. Monitor with APM tools

## See Also

- [Performance](Performance.md)
- [Security](Security.md)
- [AOT Support](AOT-Support.md)