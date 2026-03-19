using HtmxAppServer.Middleware;
using TheAppManager.Modules;

namespace HtmxAppServer.Modules;

/// <summary>
/// Configures the HTTP middleware pipeline for the HTMX application.
/// </summary>
public class MiddlewareModule : IAppModule
{
    public void ConfigureMiddleware(WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        // Add HTMX debugging middleware
        app.UseMiddleware<HtmxDebuggingMiddleware>();
    }
}
