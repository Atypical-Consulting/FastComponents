using Microsoft.AspNetCore.Components;

namespace FastComponents;

/// <summary>
/// Base class for all components that are rendered on the server.
/// </summary>
public abstract class HtmxComponentBase
    : ComponentBase, IHxAttributes
{
    /// <summary>
    /// Gets or sets a custom tag name for the component.
    /// </summary>
    [Parameter]
    public virtual string As { get; set; } = "div";
    
    /////////////////////
    // Core Attributes //
    /////////////////////
    
    /// <inheritdoc />
    [Parameter]
    public virtual string? HxBoost { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public virtual string? HxGet { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public virtual string? HxPost { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxOn { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxPushUrl { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxSelect { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxSelectOob { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxSwap { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxSwapOob { get; set; }
    
    /// <inheritdoc />
    [Parameter] 
    public string? HxTarget { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxTrigger { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxVals { get; set; }
    
    ///////////////////////////
    // Additional Attributes //
    ///////////////////////////
    
    /// <inheritdoc />
    [Parameter]
    public string? HxConfirm { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxDelete { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxDisable { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxDisabledElt { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxDisinherit { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxEncoding { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxExt { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxHeaders { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxHistory { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxHistoryElt { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxInclude { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxIndicator { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxParams { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxPatch { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxPreserve { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxPrompt { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxPut { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxReplaceUrl { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxRequest { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxSync { get; set; }
    
    /// <inheritdoc />
    [Parameter]
    public string? HxValidate { get; set; }
    
    // Custom Attributes
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object>? CustomAttributes { get; set; }

    protected string? ClassName { get; private set; }

    /// <inheritdoc />
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