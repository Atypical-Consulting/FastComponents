namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record TabsParameters : HtmxComponentParameters
{
    public string ActiveTab { get; init; } = "overview";
    public bool EnableDebug { get; init; }
    public bool EnableCaching { get; init; }
    public string Theme { get; init; } = "auto";
}
