global using FastComponents;
global using static HtmxAppServer.Components.HtmxRoutes;

using HtmxAppServer.Services;

// TODO: enable AOT compilation
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

// Map endpoints to components
app.UseFastComponents();

app.Run();