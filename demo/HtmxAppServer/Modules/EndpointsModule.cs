using HtmxAppServer.Components;
using HtmxAppServer.Components.Blocks;
using TheAppManager.Modules;
using static HtmxAppServer.Components.HtmxRoutes;

namespace HtmxAppServer.Modules;

/// <summary>
/// Configures HTMX endpoint routes for the application.
/// </summary>
public class EndpointsModule : IAppModule
{
    public void ConfigureMiddleware(WebApplication app)
    {
        // Map HTMX endpoints using convention-based registration
        // This single line replaces ~15 manual MapHtmxGet/MapHtmxPost calls for example components
#pragma warning disable IL2026, IL3050 // HTMX endpoints require reflection and dynamic code
        app.UseFastComponentsAuto(
            routePrefix: "/ui/examples",
            predicate: t => t.Name.EndsWith("Example", StringComparison.Ordinal),
            typeof(CounterExample).Assembly);
#pragma warning restore IL2026, IL3050
    }

    public void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
    {
        // Explicitly register components that don't follow the convention pattern
#pragma warning disable IL2026, IL3050 // HTMX endpoints require reflection and dynamic code
        endpoints.MapHtmxGet<App, AppParameters>(RouteApp)
            .AllowAnonymous();

        endpoints.MapHtmxGet<ExamplesDashboard>(RouteExamples)
            .AllowAnonymous();

        endpoints.MapHtmxGet<DebugDashboard, DebugDashboardParameters>(RouteDebug)
            .AllowAnonymous();
#pragma warning restore IL2026, IL3050
    }
}
