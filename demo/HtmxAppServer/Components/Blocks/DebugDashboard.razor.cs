using FastComponents;

namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record DebugDashboardParameters : HtmxComponentParameters
{
    public bool AutoRefresh { get; init; } = false;
}