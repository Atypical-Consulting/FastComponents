namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record LiveSearchParameters : HtmxComponentParameters
{
    public string Query { get; init; } = string.Empty;
}
