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
        
        protected override string BuildQueryString()
        {
            var parts = new List<string>();
            
            if (Page != 1)
                parts.Add($"Page={Page}");
                
            if (PageSize != 5)
                parts.Add($"PageSize={PageSize}");
                
            return string.Join("&", parts);
        }
        
        public override HtmxComponentParameters BindFromQuery(IQueryCollection query)
        {
            return this with
            {
                Page = GetQueryInt(query, "Page") ?? Page,
                PageSize = GetQueryInt(query, "PageSize") ?? PageSize
            };
        }
    }
}