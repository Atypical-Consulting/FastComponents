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
/// Provides helper constants and methods for HTMX operations.
/// </summary>
public static class Hx
{
    /// <summary>
    /// Constants for HTMX swap strategies.
    /// </summary>
    public static class Swap
    {
        /// <summary>
        /// The innerHTML property sets or returns the HTML content (inner HTML) of an element.
        /// </summary>
        public const string InnerHtml = "innerHTML";

        /// <summary>
        /// The outerHTML property sets or returns the HTML content (inner HTML + HTML element itself) of an element.
        /// </summary>
        public const string OuterHtml = "outerHTML";

        /// <summary>
        /// The beforebegin position is before the element itself.
        /// </summary>
        public const string BeforeBegin = "beforebegin";

        /// <summary>
        /// The afterbegin position is just inside the element, before its first child.
        /// </summary>
        public const string AfterBegin = "afterbegin";

        /// <summary>
        /// The beforeend position is just inside the element, after its last child.
        /// </summary>
        public const string BeforeEnd = "beforeend";

        /// <summary>
        /// The afterend position is after the element itself.
        /// </summary>
        public const string AfterEnd = "afterend";

        /// <summary>
        /// The delete position removes the element.
        /// </summary>
        public const string Delete = "delete";

        /// <summary>
        /// The none position leaves the element intact.
        /// </summary>
        public const string None = "none";
    }

    /// <summary>
    /// Prepends the Id with a #.
    /// </summary>
    /// <param name="id">The Id to prepend.</param>
    /// <returns>The Id prepended with a #.</returns>
    public static string TargetId(string id)
    {
        return $"#{id}";
    }
}
