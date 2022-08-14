using Kantaiko.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Routing;

namespace Replikit.Core.Modularity;

public abstract class ReplikitModule : Module, IModule
{
    protected virtual bool ConfigureDefaults => true;

    protected virtual void Configure(IApplicationBuilder app) { }

    protected virtual void PostConfigure(IApplicationBuilder app) { }

    void IModule.ConfigureServices(IServiceCollection services, ModuleMetadata metadata)
    {
        services.AddModule<ReplikitCoreModule>();

        ConfigureServices(services);

        services.ConfigureReplikitApplication(app => Configure(app, GetType(), metadata));
        services.PostConfigureReplikitApplication(app => PostConfigure(app, GetType(), metadata));
    }

    private void Configure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata)
    {
        if (!ConfigureDefaults)
        {
            return;
        }

        var routingContributors = app.ApplicationServices.GetServices<IModuleRoutingContributor>();

        foreach (var routingContributor in routingContributors)
        {
            routingContributor.Configure(app, moduleType, moduleMetadata);
        }
    }

    private void PostConfigure(IApplicationBuilder app, Type moduleType, ModuleMetadata moduleMetadata)
    {
        if (!ConfigureDefaults)
        {
            return;
        }

        var routingContributors = app.ApplicationServices.GetServices<IModuleRoutingContributor>();

        foreach (var routingContributor in routingContributors)
        {
            routingContributor.PostConfigure(app, moduleType, moduleMetadata);
        }
    }
}
