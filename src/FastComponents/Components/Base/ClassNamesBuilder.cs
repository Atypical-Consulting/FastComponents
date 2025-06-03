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

using System.Text;

namespace FastComponents;

/// <summary>
/// A helper class for building a string of CSS class names.
/// </summary>
/// <param name="value">The initial value.</param>
/// <param name="prefix">A optional prefix to add to each class name.</param>
/// <param name="suffix">A optional suffix to add to each class name.</param>
public readonly struct ClassNamesBuilder(
    string value,
    string prefix = "",
    string suffix = "")
{
    private readonly StringBuilder _stringBuffer = new(value);

    /// <summary>
    /// Creates a new instance of <see cref="ClassNamesBuilder"/>.
    /// </summary>
    /// <param name="value">The initial value.</param>
    /// <returns>A new instance of <see cref="ClassNamesBuilder"/>.</returns>
    public static ClassNamesBuilder Default(string value)
        => new(value);

    /// <summary>
    /// Creates a new instance of <see cref="ClassNamesBuilder"/> with a prefix.
    /// </summary>
    /// <returns>A new instance of <see cref="ClassNamesBuilder"/>.</returns>
    public static ClassNamesBuilder Empty()
        => new(string.Empty);

    private static bool Invoke(Func<bool>? when)
        => when?.Invoke() ?? true;

    /// <summary>
    /// Adds a raw value to the builder.
    /// </summary>
    /// <param name="value">The value to add.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddRawValue(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            _ = _stringBuffer.Append(value);
        }

        return this;
    }

    /// <summary>
    /// Adds a class name to the builder.
    /// </summary>
    /// <param name="value">The class name to add.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddClass(string value)
        => AddRawValue($" {prefix}{value}{suffix}");

    /// <summary>
    /// Adds a class name to the builder if the condition is true.
    /// </summary>
    /// <param name="value">The class name to add.</param>
    /// <param name="when">The condition to check.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddClass(string value, bool when)
        => when ? AddClass(value) : this;

    /// <summary>
    /// Adds a class name to the builder if the result of the function is true.
    /// </summary>
    /// <param name="value">The class name to add.</param>
    /// <param name="when">A function to check the condition.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddClass(string value, Func<bool>? when)
        => Invoke(when) ? AddClass(value) : this;

    /// <summary>
    /// Adds a class name to the builder if the condition is true.
    /// </summary>
    /// <param name="value">A function that returns the class name to add.</param>
    /// <param name="when">The condition to check.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddClass(Func<string> value, bool when)
        => when ? AddClass(value()) : this;

    /// <summary>
    /// Adds a class name to the builder if the result of the function is true.
    /// </summary>
    /// <param name="value">A function that returns the class name to add.</param>
    /// <param name="when">A function to check the condition.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddClass(Func<string> value, Func<bool>? when)
        => Invoke(when) ? AddClass(value()) : this;

    /// <summary>
    /// Adds classes from another builder if the condition is true.
    /// </summary>
    /// <param name="builder">The builder whose classes to add.</param>
    /// <param name="when">The condition to check.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddClass(in ClassNamesBuilder builder, bool when)
        => when ? AddClass(builder.Build()) : this;

    /// <summary>
    /// Adds classes from another builder if the result of the function is true.
    /// </summary>
    /// <param name="builder">The builder whose classes to add.</param>
    /// <param name="when">A function to check the condition.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddClass(in ClassNamesBuilder builder, Func<bool>? when)
        => Invoke(when) ? AddClass(builder.Build()) : this;

    /// <summary>
    /// Adds class names from the "class" attribute in additional attributes.
    /// </summary>
    /// <param name="additionalAttributes">The additional attributes dictionary.</param>
    /// <returns>The current instance of <see cref="ClassNamesBuilder"/>.</returns>
    public ClassNamesBuilder AddClassFromAttributes(IReadOnlyDictionary<string, object>? additionalAttributes)
        => additionalAttributes is not null
            && additionalAttributes.TryGetValue("class", out object? c)
            && c is string classes
            ? AddClass(classes)
            : this;

    /// <summary>
    /// Builds the string of class names.
    /// </summary>
    /// <returns>The string of class names.</returns>
    public string Build()
        => _stringBuffer?.Replace("  ", " ").ToString().Trim() ?? string.Empty;

    /// <summary>
    /// Builds the string of class names.
    /// </summary>
    /// <returns>The string of class names.</returns>
    public override string ToString()
        => Build();
}
