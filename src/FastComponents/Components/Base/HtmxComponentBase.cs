using Microsoft.AspNetCore.Components;

namespace FastComponents;

/// <summary>
/// Base class for all components that are rendered on the server.
/// </summary>
public abstract class HtmxComponentBase
    : ComponentBase, IHxAttributes
{
    [Parameter] public virtual string As { get; set; } = "div";
    
    // Core Attributes
    [Parameter] public virtual string? HxBoost { get; set; }
    [Parameter] public virtual string? HxGet { get; set; }
    [Parameter] public virtual string? HxPost { get; set; }
    [Parameter] public string? HxOn { get; set; }
    [Parameter] public string? HxPushUrl { get; set; }
    [Parameter] public string? HxSelect { get; set; }
    [Parameter] public string? HxSelectOob { get; set; }
    [Parameter] public string? HxSwap { get; set; }
    [Parameter] public string? HxSwapOob { get; set; }
    [Parameter] public string? HxTarget { get; set; }
    [Parameter] public string? HxTrigger { get; set; }
    [Parameter] public string? HxVals { get; set; }
    
    // Additional Attributes
    [Parameter] public string? HxConfirm { get; set; }
    [Parameter] public string? HxDelete { get; set; }
    [Parameter] public string? HxDisable { get; set; }
    [Parameter] public string? HxDisabledElt { get; set; }
    [Parameter] public string? HxDisinherit { get; set; }
    [Parameter] public string? HxEncoding { get; set; }
    [Parameter] public string? HxExt { get; set; }
    [Parameter] public string? HxHeaders { get; set; }
    [Parameter] public string? HxHistory { get; set; }
    [Parameter] public string? HxHistoryElt { get; set; }
    [Parameter] public string? HxInclude { get; set; }
    [Parameter] public string? HxIndicator { get; set; }
    [Parameter] public string? HxParams { get; set; }
    [Parameter] public string? HxPatch { get; set; }
    [Parameter] public string? HxPreserve { get; set; }
    [Parameter] public string? HxPrompt { get; set; }
    [Parameter] public string? HxPut { get; set; }
    [Parameter] public string? HxReplaceUrl { get; set; }
    [Parameter] public string? HxRequest { get; set; }
    [Parameter] public string? HxSync { get; set; }
    [Parameter] public string? HxValidate { get; set; }
    
    // Custom Attributes
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? CustomAttributes { get; set; }

    protected string? ClassName { get; private set; }

    protected override void OnParametersSet()
    {
        ClassName = BuildClassNames();
    }

    protected virtual ClassNamesBuilder OnBuildClassNames(ClassNamesBuilder builder)
    {
        return builder;
    }
    
    private string BuildClassNames()
    {
        ClassNamesBuilder builder = ClassNamesBuilder.Empty();
        
        // add base class names
        // builder
        //     .AddClass($"w-{W}", !string.IsNullOrWhiteSpace(W))
        //     .AddClass($"h-{H}", !string.IsNullOrWhiteSpace(H))
        //     .AddClass($"max-w-{MaxW}", !string.IsNullOrWhiteSpace(MaxW))
        //     .AddClass($"max-h-{MaxH}", !string.IsNullOrWhiteSpace(MaxH))
        //     .AddClass($"rounded-{Rounded}", !string.IsNullOrWhiteSpace(Rounded))
        //     .AddClass($"opacity-{Opacity}", !string.IsNullOrWhiteSpace(Opacity));
        
        // add additional class names
        builder = OnBuildClassNames(builder);

        // add class names from attributes
        builder.AddClassFromAttributes(CustomAttributes);
        
        return builder.Build();
    }
}

/// <summary>
/// Base class for all components that are rendered on the server.
/// </summary>
/// <typeparam name="TParameters">The type of the parameters.</typeparam>
public abstract class HtmxComponentBase<TParameters> : HtmxComponentBase
    where TParameters : class, new()
{
    /// <summary>
    /// Gets or sets the parameters.
    /// </summary>
    [Parameter]
    public TParameters Parameters { get; set; } = CreateDefaultParameters();

    /// <summary>
    /// Creates the default parameters.
    /// </summary>
    /// <returns>The default parameters.</returns>
    private static TParameters CreateDefaultParameters()
        => (TParameters)Activator.CreateInstance(typeof(TParameters))!;
}