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

namespace FastComponents.Events;

/// <summary>
/// Constants for HTMX JavaScript events
/// </summary>
public static class HtmxEvents
{
    /// <summary>
    /// Fired when an AJAX request is aborted
    /// </summary>
    public const string Abort = "htmx:abort";

    /// <summary>
    /// Fired after an AJAX request has completed
    /// </summary>
    public const string AfterOnLoad = "htmx:afterOnLoad";

    /// <summary>
    /// Fired after content has been processed by htmx
    /// </summary>
    public const string AfterProcessNode = "htmx:afterProcessNode";

    /// <summary>
    /// Fired after an AJAX request has been made
    /// </summary>
    public const string AfterRequest = "htmx:afterRequest";

    /// <summary>
    /// Fired after content has been settled
    /// </summary>
    public const string AfterSettle = "htmx:afterSettle";

    /// <summary>
    /// Fired after content has been swapped
    /// </summary>
    public const string AfterSwap = "htmx:afterSwap";

    /// <summary>
    /// Fired before htmx cleans up an element
    /// </summary>
    public const string BeforeCleanupElement = "htmx:beforeCleanupElement";

    /// <summary>
    /// Fired before any content swapping occurs
    /// </summary>
    public const string BeforeOnLoad = "htmx:beforeOnLoad";

    /// <summary>
    /// Fired before htmx processes a node
    /// </summary>
    public const string BeforeProcessNode = "htmx:beforeProcessNode";

    /// <summary>
    /// Fired before an AJAX request is made
    /// </summary>
    public const string BeforeRequest = "htmx:beforeRequest";

    /// <summary>
    /// Fired before content is swapped
    /// </summary>
    public const string BeforeSwap = "htmx:beforeSwap";

    /// <summary>
    /// Fired before a new node is added to the DOM
    /// </summary>
    public const string BeforeTransition = "htmx:beforeTransition";

    /// <summary>
    /// Fired when a request is cancelled
    /// </summary>
    public const string Cancel = "htmx:cancel";

    /// <summary>
    /// Fired when htmx is configured
    /// </summary>
    public const string ConfigRequest = "htmx:configRequest";

    /// <summary>
    /// Fired when a user confirms an action
    /// </summary>
    public const string Confirm = "htmx:confirm";

    /// <summary>
    /// Fired when the history cache is cleared
    /// </summary>
    public const string HistoryCacheMiss = "htmx:historyCacheMiss";

    /// <summary>
    /// Fired when the history cache is populated
    /// </summary>
    public const string HistoryCacheMissError = "htmx:historyCacheMissError";

    /// <summary>
    /// Fired when the history cache is loaded
    /// </summary>
    public const string HistoryCacheMissLoad = "htmx:historyCacheMissLoad";

    /// <summary>
    /// Fired when history is restored
    /// </summary>
    public const string HistoryRestore = "htmx:historyRestore";

    /// <summary>
    /// Fired when the history cache is replaced
    /// </summary>
    public const string BeforeHistorySave = "htmx:beforeHistorySave";

    /// <summary>
    /// Fired when htmx is loaded
    /// </summary>
    public const string Load = "htmx:load";

    /// <summary>
    /// Fired when an out of band swap occurs
    /// </summary>
    public const string NoSseSourceError = "htmx:noSSESourceError";

    /// <summary>
    /// Fired when htmx swaps out of band content
    /// </summary>
    public const string OobAfterSwap = "htmx:oobAfterSwap";

    /// <summary>
    /// Fired before htmx swaps out of band content
    /// </summary>
    public const string OobBeforeSwap = "htmx:oobBeforeSwap";

    /// <summary>
    /// Fired when htmx handles an error swapping out of band content
    /// </summary>
    public const string OobErrorNoTarget = "htmx:oobErrorNoTarget";

    /// <summary>
    /// Fired when a prompt is shown
    /// </summary>
    public const string Prompt = "htmx:prompt";

    /// <summary>
    /// Fired after URL has been pushed to history
    /// </summary>
    public const string PushedIntoHistory = "htmx:pushedIntoHistory";

    /// <summary>
    /// Fired after URL has been replaced in history
    /// </summary>
    public const string ReplacedInHistory = "htmx:replacedInHistory";

    /// <summary>
    /// Fired when a response error occurs
    /// </summary>
    public const string ResponseError = "htmx:responseError";

    /// <summary>
    /// Fired when an error sending an AJAX request occurs
    /// </summary>
    public const string SendError = "htmx:sendError";

    /// <summary>
    /// Fired when a server sent event error occurs
    /// </summary>
    public const string SseError = "htmx:sseError";

    /// <summary>
    /// Fired when a server sent event is received
    /// </summary>
    public const string SseMessage = "htmx:sseMessage";

    /// <summary>
    /// Fired when a server sent event connection is opened
    /// </summary>
    public const string SseOpen = "htmx:sseOpen";

    /// <summary>
    /// Fired when content is swapped into the DOM
    /// </summary>
    public const string SwapError = "htmx:swapError";

    /// <summary>
    /// Fired when a target error occurs
    /// </summary>
    public const string TargetError = "htmx:targetError";

    /// <summary>
    /// Fired when a request timeout occurs
    /// </summary>
    public const string Timeout = "htmx:timeout";

    /// <summary>
    /// Fired when validation fails
    /// </summary>
    public const string ValidationValidate = "htmx:validation:validate";

    /// <summary>
    /// Fired when validation fails
    /// </summary>
    public const string ValidationFailed = "htmx:validation:failed";

    /// <summary>
    /// Fired when validation is halted
    /// </summary>
    public const string ValidationHalted = "htmx:validation:halted";

    /// <summary>
    /// Fired when an XHR request is about to be made
    /// </summary>
    public const string XhrAbort = "htmx:xhr:abort";

    /// <summary>
    /// Fired when an XHR request has loaded
    /// </summary>
    public const string XhrLoadEnd = "htmx:xhr:loadend";

    /// <summary>
    /// Fired when an XHR request starts loading
    /// </summary>
    public const string XhrLoadStart = "htmx:xhr:loadstart";

    /// <summary>
    /// Fired during XHR request progress
    /// </summary>
    public const string XhrProgress = "htmx:xhr:progress";
}
