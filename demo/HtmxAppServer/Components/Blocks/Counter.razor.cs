namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record CounterParameters : HtmxComponentParameters
{
    public int Count { get; init; } = 10;

    // this method is not used by example
    public string Decrement()
    {
        CounterParameters parameters = new() { Count = Count - 1 };
        return parameters.ToComponentUrl(HtmxRoutes.RouteCounter);
    }

    // but this one is
    public string Increment()
    {
        CounterParameters parameters = new() { Count = Count + 1 };
        return parameters.ToComponentUrl(HtmxRoutes.RouteCounter);
    }
}
