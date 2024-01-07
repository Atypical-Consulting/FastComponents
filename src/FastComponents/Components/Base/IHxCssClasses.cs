namespace FastComponents;

public interface IHxCssClasses
{
    /// <summary>
    /// CSS CLASS<br/>
    /// ------------------------------<br/>
    /// Applied to a new piece of content before it is swapped, removed after it is settled.
    /// </summary>
    string? HxCssAdded { get; set; }
    
    /// <summary>
    /// CSS CLASS<br/>
    /// ------------------------------<br/>
    /// A dynamically generated class that will toggle visible (opacity:1) when a htmx-request class is present
    /// </summary>
    string? HxCssIndicator { get; set; }
    
    /// <summary>
    /// CSS CLASS<br/>
    /// ------------------------------<br/>
    /// Applied to either the element or the element specified with hx-indicator while a request is ongoing
    /// </summary>
    string? HxCssRequest { get; set; }    
    
    /// <summary>
    /// CSS CLASS<br/>
    /// ------------------------------<br/>
    /// Applied to a target after content is swapped, removed after it is settled. The duration can be modified via hx-swap.
    /// </summary>
    string? HxCssSettling { get; set; }
    
    /// <summary>
    /// CSS CLASS<br/>
    /// ------------------------------<br/>
    /// Applied to a target before any content is swapped, removed after it is swapped. The duration can be modified via hx-swap.
    /// </summary>
    string? HxCssSwapping { get; set; }
}