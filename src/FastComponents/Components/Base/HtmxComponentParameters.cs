namespace FastComponents;

public abstract record HtmxComponentParameters
{
    protected virtual string ToComponentUrl(string route)
        => $"{route}?{ToQueryString()}";
    
    protected virtual string ToQueryString()
    {
        IEnumerable<string> properties = GetType().GetProperties()
            .Where(p => p.GetValue(this) != null)
            .Select(p => $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(p.GetValue(this)!.ToString()!)}");
        
        return string.Join("&", properties);
    }
}
