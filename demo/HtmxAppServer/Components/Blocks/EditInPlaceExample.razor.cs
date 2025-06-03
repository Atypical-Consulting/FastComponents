using FastComponents;

namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record EditInPlaceParameters : HtmxComponentParameters
{
    public int CharacterId { get; init; } = 0;
    public string EditingField { get; init; } = "";
    public string TempValue { get; init; } = "";
    public string Action { get; init; } = "";
    public string Field { get; init; } = "";
    public string ValidationError { get; init; } = "";
}