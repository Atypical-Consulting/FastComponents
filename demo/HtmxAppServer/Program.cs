global using FastComponents;
global using static HtmxAppServer.Components.HtmxRoutes;

using HtmxAppServer;
using HtmxAppServer.Components;
using HtmxAppServer.Services;
using HtmxAppServer.Middleware;

// TODO: create a Template from this project
// TODO: complete README.md for this project

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Configure JSON serialization for AOT
services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

// Add FastComponents services
services.AddFastComponents();

// Add business services
services.AddSingleton<MovieService>();

// Add debugging services
services.AddSingleton<HtmxRequestTracker>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Add HTMX debugging middleware
app.UseMiddleware<HtmxDebuggingMiddleware>();

// Map HTMX endpoints
#pragma warning disable IL2026, IL3050 // HTMX endpoints require reflection and dynamic code
app.MapHtmxEndpoints();
#pragma warning restore IL2026, IL3050

app.Run();
