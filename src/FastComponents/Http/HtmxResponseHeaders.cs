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

using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace FastComponents.Http;

/// <summary>
/// Provides methods to set HTMX response headers on an HTTP response.
/// </summary>
public class HtmxResponseHeaders
{
    /// <summary>
    /// Header names for HTMX responses
    /// </summary>
    public static class Names
    {
        /// <summary>Allows you to do a client-side redirect that does not do a full page reload</summary>
        public const string HxLocation = "HX-Location";
        
        /// <summary>Pushes a new url into the history stack</summary>
        public const string HxPushUrl = "HX-Push-Url";
        
        /// <summary>Can be used to do a client-side redirect to a new location</summary>
        public const string HxRedirect = "HX-Redirect";
        
        /// <summary>If set to "true" the client-side will do a full refresh of the page</summary>
        public const string HxRefresh = "HX-Refresh";
        
        /// <summary>Replaces the current URL in the location bar</summary>
        public const string HxReplaceUrl = "HX-Replace-Url";
        
        /// <summary>Allows you to specify how the response will be swapped</summary>
        public const string HxReswap = "HX-Reswap";
        
        /// <summary>A CSS selector that updates the target of the content update to a different element on the page</summary>
        public const string HxRetarget = "HX-Retarget";
        
        /// <summary>A CSS selector that allows you to choose which part of the response is used to be swapped in</summary>
        public const string HxReselect = "HX-Reselect";
        
        /// <summary>Allows you to trigger client-side events</summary>
        public const string HxTrigger = "HX-Trigger";
        
        /// <summary>Allows you to trigger client-side events after the settle step</summary>
        public const string HxTriggerAfterSettle = "HX-Trigger-After-Settle";
        
        /// <summary>Allows you to trigger client-side events after the swap step</summary>
        public const string HxTriggerAfterSwap = "HX-Trigger-After-Swap";
    }
    
    private readonly IHeaderDictionary _headers;
    
    /// <summary>
    /// Creates a new instance of HtmxResponseHeaders from the HTTP context
    /// </summary>
    public HtmxResponseHeaders(HttpContext context)
    {
        _headers = context.Response.Headers;
    }
    
    /// <summary>
    /// Creates a new instance of HtmxResponseHeaders from the response headers
    /// </summary>
    public HtmxResponseHeaders(IHeaderDictionary headers)
    {
        _headers = headers;
    }
    
    /// <summary>
    /// Allows you to do a client-side redirect that does not do a full page reload
    /// </summary>
    public HtmxResponseHeaders Location(string url)
    {
        _headers[Names.HxLocation] = url;
        return this;
    }
    
    /// <summary>
    /// Allows you to do a client-side redirect with additional options
    /// </summary>
    public HtmxResponseHeaders Location(object locationData)
    {
        _headers[Names.HxLocation] = JsonSerializer.Serialize(locationData);
        return this;
    }
    
    /// <summary>
    /// Pushes a new url into the history stack
    /// </summary>
    public HtmxResponseHeaders PushUrl(string url)
    {
        _headers[Names.HxPushUrl] = url;
        return this;
    }
    
    /// <summary>
    /// Prevent pushing the url into the history stack
    /// </summary>
    public HtmxResponseHeaders PreventPushUrl()
    {
        _headers[Names.HxPushUrl] = "false";
        return this;
    }
    
    /// <summary>
    /// Can be used to do a client-side redirect to a new location
    /// </summary>
    public HtmxResponseHeaders Redirect(string url)
    {
        _headers[Names.HxRedirect] = url;
        return this;
    }
    
    /// <summary>
    /// If set to "true" the client-side will do a full refresh of the page
    /// </summary>
    public HtmxResponseHeaders Refresh()
    {
        _headers[Names.HxRefresh] = "true";
        return this;
    }
    
    /// <summary>
    /// Replaces the current URL in the location bar
    /// </summary>
    public HtmxResponseHeaders ReplaceUrl(string url)
    {
        _headers[Names.HxReplaceUrl] = url;
        return this;
    }
    
    /// <summary>
    /// Prevent replacing the current URL in the location bar
    /// </summary>
    public HtmxResponseHeaders PreventReplaceUrl()
    {
        _headers[Names.HxReplaceUrl] = "false";
        return this;
    }
    
    /// <summary>
    /// Allows you to specify how the response will be swapped
    /// </summary>
    public HtmxResponseHeaders Reswap(string swapStrategy)
    {
        _headers[Names.HxReswap] = swapStrategy;
        return this;
    }
    
    /// <summary>
    /// A CSS selector that updates the target of the content update to a different element on the page
    /// </summary>
    public HtmxResponseHeaders Retarget(string selector)
    {
        _headers[Names.HxRetarget] = selector;
        return this;
    }
    
    /// <summary>
    /// A CSS selector that allows you to choose which part of the response is used to be swapped in
    /// </summary>
    public HtmxResponseHeaders Reselect(string selector)
    {
        _headers[Names.HxReselect] = selector;
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger a client-side event
    /// </summary>
    public HtmxResponseHeaders Trigger(string eventName)
    {
        _headers[Names.HxTrigger] = eventName;
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger multiple client-side events
    /// </summary>
    public HtmxResponseHeaders Trigger(params string[] eventNames)
    {
        _headers[Names.HxTrigger] = string.Join(",", eventNames);
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger client-side events with details
    /// </summary>
    public HtmxResponseHeaders TriggerWithDetails(object eventDetails)
    {
        _headers[Names.HxTrigger] = JsonSerializer.Serialize(eventDetails);
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger a client-side event after the settle step
    /// </summary>
    public HtmxResponseHeaders TriggerAfterSettle(string eventName)
    {
        _headers[Names.HxTriggerAfterSettle] = eventName;
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger multiple client-side events after the settle step
    /// </summary>
    public HtmxResponseHeaders TriggerAfterSettle(params string[] eventNames)
    {
        _headers[Names.HxTriggerAfterSettle] = string.Join(",", eventNames);
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger client-side events with details after the settle step
    /// </summary>
    public HtmxResponseHeaders TriggerAfterSettleWithDetails(object eventDetails)
    {
        _headers[Names.HxTriggerAfterSettle] = JsonSerializer.Serialize(eventDetails);
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger a client-side event after the swap step
    /// </summary>
    public HtmxResponseHeaders TriggerAfterSwap(string eventName)
    {
        _headers[Names.HxTriggerAfterSwap] = eventName;
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger multiple client-side events after the swap step
    /// </summary>
    public HtmxResponseHeaders TriggerAfterSwap(params string[] eventNames)
    {
        _headers[Names.HxTriggerAfterSwap] = string.Join(",", eventNames);
        return this;
    }
    
    /// <summary>
    /// Allows you to trigger client-side events with details after the swap step
    /// </summary>
    public HtmxResponseHeaders TriggerAfterSwapWithDetails(object eventDetails)
    {
        _headers[Names.HxTriggerAfterSwap] = JsonSerializer.Serialize(eventDetails);
        return this;
    }
}