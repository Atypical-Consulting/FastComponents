namespace HtmxAppServer.Components.Blocks;

public class CounterEndpoint
{
    public record CounterParameters : HtmxComponentParameters
    {
        public int Count { get; init; } = 10;
    
        // this method is not used by example
        public string Decrement()
        {
            var parameters = this with { Count = Count - 1 }; 
            return parameters.ToComponentUrl(HtmxRoutes.RouteCounter);
        }
    
        // but this one is
        public string Increment()
        {
            var parameters = this with { Count = Count + 1 }; 
            return parameters.ToComponentUrl(HtmxRoutes.RouteCounter);
        }
        
        protected override string BuildQueryString()
        {
            return Count != 10 ? $"Count={Count}" : "";
        }
        
        public override HtmxComponentParameters BindFromQuery(IQueryCollection query)
        {
            return this with
            {
                Count = GetQueryInt(query, "Count") ?? Count
            };
        }
    }
}