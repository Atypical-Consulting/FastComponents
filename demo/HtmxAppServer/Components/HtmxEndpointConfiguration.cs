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
        app.MapHtmxGet<App, AppEndpoint.AppParameters>(RouteApp)
            .AllowAnonymous();

        // Map the Counter component
        app.MapHtmxGet<Counter, CounterEndpoint.CounterParameters>(RouteCounter)
            .AllowAnonymous();

        // Map the MovieCharacters component (no parameters)
        app.MapHtmxGet<MovieCharacters>(RouteMovieCharacters)
            .AllowAnonymous();

        // Map the MovieCharactersRows component
        app.MapHtmxGet<MovieCharactersRows, MovieCharactersRowsEndpoint.MovieCharactersRowsParameters>(RouteMovieCharactersRows)
            .AllowAnonymous();
    }
}