namespace HtmxAppServer.Components.Blocks;

[GenerateParameterMethods]
public partial record ModalParameters : HtmxComponentParameters
{
    public string ModalType { get; init; } = string.Empty;
    public string Title { get; init; } = string.Empty;
    public string Message { get; init; } = string.Empty;
    public bool Confirmed { get; init; }
    public string FormName { get; init; } = string.Empty;
    public string FormEmail { get; init; } = string.Empty;
    public string FormBio { get; init; } = string.Empty;
}
