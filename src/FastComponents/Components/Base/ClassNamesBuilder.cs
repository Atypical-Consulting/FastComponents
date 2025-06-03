using System.Text;

namespace FastComponents;

/// <summary>
/// A helper class for building a string of CSS class names.
/// </summary>
/// <param name="value">The initial value.</param>
/// <param name="prefix">A optional prefix to add to each class name.</param>
/// <param name="suffix">A optional suffix to add to each class name.</param>
public readonly struct ClassNamesBuilder(
    string value, string prefix = "", string suffix = "")
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
        => new("");
    
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
            _stringBuffer.Append(value);
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

    public ClassNamesBuilder AddClass(Func<string> value, bool when)
        => when ? AddClass(value()) : this;

    public ClassNamesBuilder AddClass(Func<string> value, Func<bool>? when)
        => Invoke(when) ? AddClass(value()) : this;

    public ClassNamesBuilder AddClass(ClassNamesBuilder builder, bool when)
        => when ? AddClass(builder.Build()) : this;

    public ClassNamesBuilder AddClass(ClassNamesBuilder builder, Func<bool>? when)
        => Invoke(when) ? AddClass(builder.Build()) : this;

    public ClassNamesBuilder AddClassFromAttributes(IReadOnlyDictionary<string, object>? additionalAttributes)
        => additionalAttributes != null
           && additionalAttributes.TryGetValue("class", out object? c)
           && c is string classes
            ? AddClass(classes)
            : this;

    /// <summary>
    /// Builds the string of class names.
    /// </summary>
    /// <returns>The string of class names.</returns>
    public string Build()
        => _stringBuffer?.Replace("  ", " ").ToString().Trim() ?? "";

    /// <summary>
    /// Builds the string of class names.
    /// </summary>
    /// <returns>The string of class names.</returns>
    public override string ToString()
        => Build();
}
