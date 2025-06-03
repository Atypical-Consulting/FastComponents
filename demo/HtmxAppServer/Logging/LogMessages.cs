namespace HtmxAppServer.Logging;

/// <summary>
/// High-performance logging messages using LoggerMessage.Define pattern
/// </summary>
internal static partial class LogMessages
{
    // HtmxDebuggingMiddleware messages
    [LoggerMessage(
        EventId = 1001,
        Level = LogLevel.Error,
        Message = "Error in HTMX request {RequestId}: {Message}")]
    public static partial void HtmxRequestError(
        this ILogger logger,
        Exception exception,
        string requestId,
        string message);

    // HtmxRequestTracker messages
    [LoggerMessage(
        EventId = 2001,
        Level = LogLevel.Information,
        Message = "HTMX Request: {RequestId} | Path: {Path} | Target: {Target} | Trigger: {Trigger}")]
    public static partial void HtmxRequestStarted(
        this ILogger logger,
        string requestId,
        string path,
        string? target,
        string? trigger);

    [LoggerMessage(
        EventId = 2002,
        Level = LogLevel.Information,
        Message = "HTMX Request Completed: {RequestId} | Duration: {Duration}ms")]
    public static partial void HtmxRequestCompleted(
        this ILogger logger,
        string requestId,
        double duration);

    [LoggerMessage(
        EventId = 2003,
        Level = LogLevel.Warning,
        Message = "🔄 POTENTIAL RECURSIVE REQUESTS DETECTED!")]
    public static partial void RecursiveRequestsDetected(
        this ILogger logger);

    [LoggerMessage(
        EventId = 2004,
        Level = LogLevel.Warning,
        Message = "Path: {Path} | Target: {Target} | Active Count: {Count}")]
    public static partial void RecursiveRequestDetails(
        this ILogger logger,
        string path,
        string? target,
        int count);

    [LoggerMessage(
        EventId = 2005,
        Level = LogLevel.Warning,
        Message = "  - Active Request: {RequestId} (Started: {StartTime})")]
    public static partial void ActiveRequestInfo(
        this ILogger logger,
        string requestId,
        DateTime startTime);

    [LoggerMessage(
        EventId = 2006,
        Level = LogLevel.Error,
        Message = "⚡ RAPID-FIRE REQUESTS DETECTED for target: {Target} | Count: {Count}")]
    public static partial void RapidFireRequestsDetected(
        this ILogger logger,
        string? target,
        int count);
}
