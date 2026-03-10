namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record EditInPlaceParameters : HtmxComponentParameters
{
    public int CharacterId { get; init; }
    public string EditingField { get; init; } = string.Empty;
    public string TempValue { get; init; } = string.Empty;
    public string Action { get; init; } = string.Empty;
    public string Field { get; init; } = string.Empty;
    public string ValidationError { get; init; } = string.Empty;
}
