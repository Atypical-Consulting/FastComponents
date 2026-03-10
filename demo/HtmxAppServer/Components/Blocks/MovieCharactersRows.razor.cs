namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record MovieCharactersRowsParameters : HtmxComponentParameters
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 5;
    
    public string NextPage()
    {
        MovieCharactersRowsParameters parameters = this with { Page = Page + 1 };
        return parameters.ToComponentUrl(HtmxRoutes.RouteMovieCharactersRows);
    }
}
