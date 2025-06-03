namespace HtmxAppServer.Components.Blocks;

public class MovieCharactersRowsEndpoint
{
    public record MovieCharactersRowsParameters : HtmxComponentParameters
    {
        public int Page { get; init; } = 1;
        public int PageSize { get; init; } = 5;
        
        public string NextPage()
        {
            var parameters = this with { Page = Page + 1 };
            return parameters.ToComponentUrl(HtmxRoutes.RouteMovieCharactersRows);
        }
    }
}