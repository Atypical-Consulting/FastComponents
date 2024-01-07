namespace FastComponents;

public interface IHxAdditionalAttributes
{
    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Shows a confirm() dialog before issuing a request.
    /// </summary>
    string? HxConfirm { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a DELETE to the specified URL.
    /// </summary>
    string? HxDelete { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Disables htmx processing for the given node and any children nodes.
    /// </summary>
    string? HxDisable { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Adds the disabled attribute to the specified elements while a request is in flight.
    /// </summary>
    string? HxDisabledElt { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Control and disable automatic attribute inheritance for child nodes.
    /// </summary>
    string? HxDisinherit { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Changes the request encoding type.
    /// </summary>
    string? HxEncoding { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Extensions to use for this element.
    /// </summary>
    string? HxExt { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Adds to the headers that will be submitted with the request.
    /// </summary>
    string? HxHeaders { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Prevent sensitive data from being saved to the history cache.
    /// </summary>
    string? HxHistory { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// The element to snapshot and restore during history navigation.
    /// </summary>
    string? HxHistoryElt { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Include additional data in requests.
    /// </summary>
    string? HxInclude { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// The element to put the htmx-request class on during the request.
    /// </summary>
    string? HxIndicator { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Filters the parameters that will be submitted with a request.
    /// </summary>
    string? HxParams { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a PATCH to the specified URL.
    /// </summary>
    string? HxPatch { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Specifies elements to keep unchanged between requests.
    /// </summary>
    string? HxPreserve { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Shows a prompt() dialog before submitting a request.
    /// </summary>
    string? HxPrompt { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a PUT to the specified URL.
    /// </summary>
    string? HxPut { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Replace the URL in the browser location bar.
    /// </summary>
    string? HxReplaceUrl { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Configures various aspects of the request.
    /// </summary>
    string? HxRequest { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Control how requests made by different elements are synchronized.
    /// </summary>
    string? HxSync { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Force elements to validate themselves before a request.
    /// </summary>
    string? HxValidate { get; set; }
}