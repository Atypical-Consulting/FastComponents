using System.Text;

namespace FastComponents;

public readonly struct ClassNamesBuilder(
    string value, string prefix = "", string suffix = "")
{
    private readonly StringBuilder _stringBuffer = new(value);

    public static ClassNamesBuilder Default(string value)
        => new(value);
    
    public static ClassNamesBuilder Empty()
        => new("");
    
    private static bool Invoke(Func<bool>? when)
        => when?.Invoke() ?? true;

    public ClassNamesBuilder AddRawValue(string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            _stringBuffer.Append(value);
    
        return this;
    }

    public ClassNamesBuilder AddClass(string value)
        => AddRawValue($" {prefix}{value}{suffix}");

    public ClassNamesBuilder AddClass(string value, bool when)
        => when ? AddClass(value) : this;

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
           && additionalAttributes.TryGetValue("class", out var c)
           && c is string classes
            ? AddClass(classes)
            : this;

    public string Build()
        => _stringBuffer?.Replace("  ", " ").ToString().Trim() ?? "";

    public override string ToString()
        => Build();
}