namespace HtmxAppServer.Models;

/// <summary>
/// State for the simplified counter component
/// </summary>
public record CounterState
{
    /// <summary>
    /// Current count value
    /// </summary>
    public int Count { get; init; }
}
