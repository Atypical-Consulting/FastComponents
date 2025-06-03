namespace HtmxAppServer.Components;

public class AppEndpoint
{
    // Define the parameters with a public record (immutable)
    // because we use a record, it's like a reducer:
    //   we can use the old parameters to create some new ones
    public record AppParameters : HtmxComponentParameters
    {
        // define the parameters that we want to pass to the component
        // and their default values
        public string Theme { get; init; } = "dark";
        public string Language { get; init; } = "en";
    
        public string SetTheme(string theme)
        {
            // create a new set of parameters 
            var parameters = this with { Theme = theme };
        
            // return the url with the new parameters as query string
            return parameters.ToComponentUrl(HtmxRoutes.RouteApp);
        }
    }
}