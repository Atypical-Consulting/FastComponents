using HtmxAppServer.Services;
using TheAppManager.Modules;

namespace HtmxAppServer.Modules;

/// <summary>
/// Configures dependency injection services for the HTMX application.
/// </summary>
public class ServicesModule : IAppModule
{
    public void ConfigureServices(WebApplicationBuilder builder)
    {
        IServiceCollection services = builder.Services;

        // Configure JSON serialization for AOT
        services.ConfigureHttpJsonOptions(options =>
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default));

        // Add FastComponents services (auto mode includes core services)
        services.AddFastComponentsAuto();

        // Add business services
        services.AddSingleton<MovieService>();

        // Add debugging services
        services.AddSingleton<HtmxRequestTracker>();
    }
}
