using Kantaiko.Modularity;
using Kantaiko.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modularity;
using Replikit.Extensions.State;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views;

[Module(Flags = ModuleFlags.Library)]
public class ReplikitViewsModule : ReplikitModule
{
    protected override bool ConfigureDefaults => false;

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<ReplikitStateModule>();

        services.AddSingleton<ViewHandlerFactory>();

        services.AddSingleton<ViewManager>();
        services.AddSingleton<IViewManager>(sp => sp.GetRequiredService<ViewManager>());

        services.AddSingleton<IModuleRoutingContributor, ViewModuleRoutingContributor>();
    }
}
