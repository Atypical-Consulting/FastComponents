namespace HtmxAppServer.Components.Blocks;

public class CounterEndpoint
    : HtmxComponentEndpoint<Counter, CounterEndpoint.CounterParameters>
{
    public override void Configure()
    {
        Get(RouteCounter);
        AllowAnonymous();
    }
    
    public record CounterParameters : HtmxComponentParameters
    {
        public int Count { get; init; } = 10;
    
        // this method is not used by example
        public string Decrement()
        {
            var parameters = this with { Count = Count - 1 }; 
            return parameters.ToComponentUrl(RouteCounter);
        }
    
        // but this one is
        public string Increment()
        {
            var parameters = this with { Count = Count + 1 }; 
            return parameters.ToComponentUrl(RouteCounter);
        }
    }
}