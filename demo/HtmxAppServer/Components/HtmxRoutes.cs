namespace HtmxAppServer.Components;

/// <summary>
/// Route constants for the demo application.
/// Convention-based routes are auto-generated from component names (kebab-case, suffix removed).
/// </summary>
public static class HtmxRoutes
{
    // Root and dashboard routes (registered explicitly)
    public const string RouteApp = "/";
    public const string RouteExamples = "/ui/examples";
    public const string RouteDebug = "/ui/debug";

    // Example component routes (auto-registered via convention-based registration)
    // Convention: ComponentName → remove "Example" suffix → kebab-case → prefix with /ui/examples
    public const string RouteCounter = "/ui/examples/counter";
    public const string RouteSearch = "/ui/examples/live-search";
    public const string RouteValidation = "/ui/examples/form-validation";
    public const string RouteModal = "/ui/examples/modal";
    public const string RouteTabs = "/ui/examples/tabs";
    public const string RouteInfiniteScroll = "/ui/examples/infinite-scroll";
    public const string RouteEditInPlace = "/ui/examples/edit-in-place";
}
