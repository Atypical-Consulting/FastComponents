/*
 * Copyright 2025 Atypical Consulting SRL
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

namespace FastComponents;

/// <summary>
/// Htmx additional attributes
/// </summary>
public interface IHxAdditionalAttributes
{
    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Shows a confirm() dialog before issuing a request.
    /// </summary>
    public string? HxConfirm { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a DELETE to the specified URL.
    /// </summary>
    public string? HxDelete { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Disables htmx processing for the given node and any children nodes.
    /// </summary>
    public string? HxDisable { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Adds the disabled attribute to the specified elements while a request is in flight.
    /// </summary>
    public string? HxDisabledElt { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Control and disable automatic attribute inheritance for child nodes.
    /// </summary>
    public string? HxDisinherit { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Changes the request encoding type.
    /// </summary>
    public string? HxEncoding { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Extensions to use for this element.
    /// </summary>
    public string? HxExt { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Adds to the headers that will be submitted with the request.
    /// </summary>
    public string? HxHeaders { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Prevent sensitive data from being saved to the history cache.
    /// </summary>
    public string? HxHistory { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// The element to snapshot and restore during history navigation.
    /// </summary>
    public string? HxHistoryElt { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Include additional data in requests.
    /// </summary>
    public string? HxInclude { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// The element to put the htmx-request class on during the request.
    /// </summary>
    public string? HxIndicator { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Filters the parameters that will be submitted with a request.
    /// </summary>
    public string? HxParams { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a PATCH to the specified URL.
    /// </summary>
    public string? HxPatch { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Specifies elements to keep unchanged between requests.
    /// </summary>
    public string? HxPreserve { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Shows a prompt() dialog before submitting a request.
    /// </summary>
    public string? HxPrompt { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a PUT to the specified URL.
    /// </summary>
    public string? HxPut { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Replace the URL in the browser location bar.
    /// </summary>
    public string? HxReplaceUrl { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Configures various aspects of the request.
    /// </summary>
    public string? HxRequest { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Control how requests made by different elements are synchronized.
    /// </summary>
    public string? HxSync { get; set; }

    /// <summary>
    /// ADDITIONAL ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Force elements to validate themselves before a request.
    /// </summary>
    public string? HxValidate { get; set; }
}
