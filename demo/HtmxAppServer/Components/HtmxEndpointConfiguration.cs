using HtmxAppServer.Components.Blocks;

namespace HtmxAppServer.Components;

public static class HtmxEndpointConfiguration
{
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