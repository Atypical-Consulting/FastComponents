// Copyright (c) Atypical Consulting SRL. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace FastComponents;

public static class Hx
{
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
