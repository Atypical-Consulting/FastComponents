global using FastComponents;
global using static HtmxAppServer.Components.HtmxRoutes;

using HtmxAppServer.Modules;
using TheAppManager.Startup;

AppManager.Start(
    args,
    modules =>
    {
        modules
            .Add<ServicesModule>()
            .Add<MiddlewareModule>()
            .Add<EndpointsModule>();
    });
