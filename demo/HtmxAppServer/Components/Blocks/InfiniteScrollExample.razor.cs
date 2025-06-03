using FastComponents;

namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record InfiniteScrollParameters : HtmxComponentParameters
{
    public int Page { get; init; } = 1;
    public string LoadType { get; init; } = "auto";
}