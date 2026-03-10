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
/// Htmx core attributes
/// </summary>
public interface IHxCoreAttributes
{
    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Add or remove progressive enhancement for links and forms
    /// </summary>
    public string? HxBoost { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a GET to the specified URL
    /// </summary>
    public string? HxGet { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Issues a POST to the specified URL
    /// </summary>
    public string? HxPost { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Handle events with a inline scripts on elements
    /// </summary>
    public string? HxOn { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Pushes the URL into the browser location bar, creating a new history entry
    /// </summary>
    public string? HxPushUrl { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Select content to swap in from a response
    /// </summary>
    public string? HxSelect { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Select content to swap in from a response, out of band (somewhere other than the target)
    /// </summary>
    public string? HxSelectOob { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Controls how content is swapped in (outerHTML, beforeend, afterend, …)
    /// </summary>
    public string? HxSwap { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Marks content in a response to be out of band (should swap in somewhere other than the target)
    /// </summary>
    public string? HxSwapOob { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Specifies the target element to be swapped
    /// </summary>
    public string? HxTarget { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Specifies the event that triggers the request
    /// </summary>
    public string? HxTrigger { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Adds values to the parameters to submit with the request (JSON-formatted)
    /// </summary>
    public string? HxVals { get; set; }

    /// <summary>
    /// CORE ATTRIBUTE<br/>
    /// ------------------------------<br/>
    /// Adds values dynamically to the parameters to submit with the request (JavaScript expression)
    /// </summary>
    public string? HxVars { get; set; }
}
