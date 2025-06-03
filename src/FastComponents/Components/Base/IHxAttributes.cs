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
/// Htmx attributes (core and additional)
/// </summary>
public interface IHxAttributes
    : IHxCoreAttributes, IHxAdditionalAttributes;
    
    
/// <summary>
/// HTMX SSE (Server-Sent Events) attributes interface.
/// </summary>
public interface IHxSseAttributes
    : IHxAttributes
{
    /// <summary>
    /// Establishes a Server-Sent Events connection to the specified URL
    /// </summary>
    string? SseConnect { get; set; }
    
    /// <summary>
    /// Swaps content based on SSE messages with the given event name
    /// </summary>
    string? SseSwap { get; set; }
}

/// <summary>
/// HTMX WebSocket attributes interface.
/// </summary>
public interface IHxWsAttributes
    : IHxAttributes
{
    /// <summary>
    /// Establishes a WebSocket connection to the specified URL
    /// </summary>
    string? WsConnect { get; set; }
    
    /// <summary>
    /// Sends WebSocket messages based on events
    /// </summary>
    string? WsSend { get; set; }
}