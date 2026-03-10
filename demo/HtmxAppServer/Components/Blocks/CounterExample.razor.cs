namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record CounterParameters : HtmxComponentParameters
{
    public int Count { get; init; } = 10;

    public string Decrement()
    {
        CounterParameters parameters = new() { Count = Count - 1 };
        return parameters.ToComponentUrl(RouteCounter);
    }

    public string Increment()
    {
        CounterParameters parameters = new() { Count = Count + 1 };
        return parameters.ToComponentUrl(RouteCounter);
    }
}
