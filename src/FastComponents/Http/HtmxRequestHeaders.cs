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

using Microsoft.AspNetCore.Http;

namespace FastComponents.Http;

/// <summary>
/// Provides access to HTMX request headers from an HTTP request.
/// </summary>
public class HtmxRequestHeaders
{
    /// <summary>
    /// Header names for HTMX requests
    /// </summary>
    public static class Names
    {
        /// <summary>Indicates that the request is via an element using hx-boost</summary>
        public const string HxBoosted = "HX-Boosted";
        
        /// <summary>The current URL of the browser</summary>
        public const string HxCurrentUrl = "HX-Current-URL";
        
        /// <summary>True if the request is for history restoration after a miss in the local history cache</summary>
        public const string HxHistoryRestoreRequest = "HX-History-Restore-Request";
        
        /// <summary>The user response to an hx-prompt</summary>
        public const string HxPrompt = "HX-Prompt";
        
        /// <summary>Always true for HTMX requests</summary>
        public const string HxRequest = "HX-Request";
        
        /// <summary>The id of the target element if it exists</summary>
        public const string HxTarget = "HX-Target";
        
        /// <summary>The name of the triggered element if it exists</summary>
        public const string HxTriggerName = "HX-Trigger-Name";
        
        /// <summary>The id of the triggered element if it exists</summary>
        public const string HxTrigger = "HX-Trigger";
    }
    
    private readonly IHeaderDictionary _headers;
    
    /// <summary>
    /// Creates a new instance of HtmxRequestHeaders from the HTTP context
    /// </summary>
    public HtmxRequestHeaders(HttpContext context)
    {
        _headers = context.Request.Headers;
    }
    
    /// <summary>
    /// Creates a new instance of HtmxRequestHeaders from the request headers
    /// </summary>
    public HtmxRequestHeaders(IHeaderDictionary headers)
    {
        _headers = headers;
    }
    
    /// <summary>
    /// Indicates that the request is via an element using hx-boost
    /// </summary>
    public bool IsBoosted => GetBoolHeader(Names.HxBoosted);
    
    /// <summary>
    /// The current URL of the browser
    /// </summary>
    public string? CurrentUrl => GetHeader(Names.HxCurrentUrl);
    
    /// <summary>
    /// True if the request is for history restoration after a miss in the local history cache
    /// </summary>
    public bool IsHistoryRestoreRequest => GetBoolHeader(Names.HxHistoryRestoreRequest);
    
    /// <summary>
    /// The user response to an hx-prompt
    /// </summary>
    public string? Prompt => GetHeader(Names.HxPrompt);
    
    /// <summary>
    /// True if this is an HTMX request
    /// </summary>
    public bool IsHtmxRequest => GetBoolHeader(Names.HxRequest);
    
    /// <summary>
    /// The id of the target element if it exists
    /// </summary>
    public string? Target => GetHeader(Names.HxTarget);
    
    /// <summary>
    /// The name of the triggered element if it exists
    /// </summary>
    public string? TriggerName => GetHeader(Names.HxTriggerName);
    
    /// <summary>
    /// The id of the triggered element if it exists
    /// </summary>
    public string? Trigger => GetHeader(Names.HxTrigger);
    
    private string? GetHeader(string name)
    {
        return _headers.TryGetValue(name, out var value) && !string.IsNullOrEmpty(value) 
            ? value.ToString() 
            : null;
    }
    
    private bool GetBoolHeader(string name)
    {
        var value = GetHeader(name);
        return string.Equals(value, "true", StringComparison.OrdinalIgnoreCase);
    }
}