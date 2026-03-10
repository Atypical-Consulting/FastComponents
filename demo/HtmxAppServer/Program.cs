global using FastComponents;
global using static HtmxAppServer.Components.HtmxRoutes;

using HtmxAppServer;
using HtmxAppServer.Components;
using HtmxAppServer.Components.Blocks;
using HtmxAppServer.Services;
using HtmxAppServer.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

// Configure JSON serialization for AOT
services.ConfigureHttpJsonOptions(options => options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default));

// Add FastComponents services (auto mode includes core services)
services.AddFastComponentsAuto();

// Add business services
services.AddSingleton<MovieService>();

// Add debugging services
services.AddSingleton<HtmxRequestTracker>();

WebApplication app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add HTMX debugging middleware
app.UseMiddleware<HtmxDebuggingMiddleware>();

// Map HTMX endpoints using convention-based registration
// This single line replaces ~15 manual MapHtmxGet/MapHtmxPost calls for example components
#pragma warning disable IL2026, IL3050 // HTMX endpoints require reflection and dynamic code
app.UseFastComponentsAuto(
    routePrefix: "/ui/examples",
    predicate: t => t.Name.EndsWith("Example", StringComparison.Ordinal),
    typeof(CounterExample).Assembly);

// Explicitly register components that don't follow the convention pattern
app.MapHtmxGet<App, AppParameters>(RouteApp)
    .AllowAnonymous();

app.MapHtmxGet<ExamplesDashboard>(RouteExamples)
    .AllowAnonymous();

app.MapHtmxGet<DebugDashboard, DebugDashboardParameters>(RouteDebug)
    .AllowAnonymous();
#pragma warning restore IL2026, IL3050

app.Run();
