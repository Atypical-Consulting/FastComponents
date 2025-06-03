using FastComponents;

namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record TabsParameters : HtmxComponentParameters
{
    public string ActiveTab { get; init; } = "overview";
    public bool EnableDebug { get; init; } = false;
    public bool EnableCaching { get; init; } = false;
    public string Theme { get; init; } = "auto";
}