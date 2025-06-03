global using FastComponents;
global using static HtmxAppServer.Components.HtmxRoutes;

using HtmxAppServer.Components;
using HtmxAppServer.Services;

// TODO: create a Template from this project
// TODO: complete README.md for this project

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add FastComponents services
services.AddFastComponents();

// Add business services
services.AddSingleton<MovieService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

// Use FastComponents (currently no-op, but kept for compatibility)
app.UseFastComponents();

// Map HTMX endpoints
#pragma warning disable IL2026, IL3050 // HTMX endpoints require reflection and dynamic code
app.MapHtmxEndpoints();
#pragma warning restore IL2026, IL3050

app.Run();