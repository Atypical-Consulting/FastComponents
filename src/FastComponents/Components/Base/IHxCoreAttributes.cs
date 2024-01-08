namespace FastComponents;

/// <summary>
/// Htmx core attributes
/// </summary>
public interface IHxCoreAttributes
{
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Add or remove progressive enhancement for links and forms
    /// </summary>
    string? HxBoost { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a GET to the specified URL
    /// </summary>
    string? HxGet { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a POST to the specified URL
    /// </summary>
    string? HxPost { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Handle events with a inline scripts on elements
    /// </summary>
    string? HxOn { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Pushes the URL into the browser location bar, creating a new history entry
    /// </summary>
    string? HxPushUrl { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Select content to swap in from a response
    /// </summary>
    string? HxSelect { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Select content to swap in from a response, out of band (somewhere other than the target)
    /// </summary>
    string? HxSelectOob { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Controls how content is swapped in (outerHTML, beforeend, afterend, …)
    /// </summary>
    string? HxSwap { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Marks content in a response to be out of band (should swap in somewhere other than the target)
    /// </summary>
    string? HxSwapOob { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Specifies the target element to be swapped
    /// </summary>
    string? HxTarget { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Specifies the event that triggers the request
    /// </summary>
    string? HxTrigger { get; set; }
    
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Adds values to the parameters to submit with the request (JSON-formatted)
    /// </summary>
    string? HxVals { get; set; }
}