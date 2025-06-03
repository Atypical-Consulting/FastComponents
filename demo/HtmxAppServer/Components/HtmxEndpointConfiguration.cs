using System.Diagnostics.CodeAnalysis;
using HtmxAppServer.Components.Blocks;

namespace HtmxAppServer.Components;

public static class HtmxEndpointConfiguration
{
    [RequiresUnreferencedCode("HTMX endpoints use reflection for component rendering.")]
    [RequiresDynamicCode("HTMX endpoints require runtime code generation.")]
    public static void MapHtmxEndpoints(this IEndpointRouteBuilder app)
    {
        // Map the main App component
        app.MapHtmxGet<App, AppParameters>(RouteApp)
            .AllowAnonymous();

        // Map the examples dashboard
        app.MapHtmxGet<ExamplesDashboard>(RouteExamples)
            .AllowAnonymous();

        // Map example components
        app.MapHtmxGet<CounterExample, CounterParameters>(RouteCounter)
            .AllowAnonymous();

        app.MapHtmxGet<LiveSearchExample, LiveSearchParameters>(RouteSearch)
            .AllowAnonymous();

        app.MapHtmxPost<FormValidationExample, FormValidationParameters>(RouteValidation)
            .AllowAnonymous();

        app.MapHtmxGet<ModalExample, ModalParameters>(RouteModal)
            .AllowAnonymous();

        app.MapHtmxPost<ModalExample, ModalParameters>(RouteModal)
            .AllowAnonymous();

        app.MapHtmxGet<TabsExample, TabsParameters>(RouteTabs)
            .AllowAnonymous();

        app.MapHtmxPost<TabsExample, TabsParameters>(RouteTabs)
            .AllowAnonymous();

        app.MapHtmxGet<InfiniteScrollExample, InfiniteScrollParameters>(RouteInfiniteScroll)
            .AllowAnonymous();

        app.MapHtmxGet<EditInPlaceExample, EditInPlaceParameters>(RouteEditInPlace)
            .AllowAnonymous();

        app.MapHtmxPost<EditInPlaceExample, EditInPlaceParameters>(RouteEditInPlace)
            .AllowAnonymous();

        // Debug dashboard
        app.MapHtmxGet<DebugDashboard, DebugDashboardParameters>(RouteDebug)
            .AllowAnonymous();

        // Legacy routes - keep for compatibility
        app.MapHtmxGet<Counter, CounterParameters>("/ui/blocks/counter")
            .AllowAnonymous();

        app.MapHtmxGet<MovieCharacters>(RouteMovieCharacters)
            .AllowAnonymous();

        app.MapHtmxGet<MovieCharactersRows, MovieCharactersRowsParameters>(RouteMovieCharactersRows)
            .AllowAnonymous();
    }
}