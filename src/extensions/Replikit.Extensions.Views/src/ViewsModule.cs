using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.State;
using Replikit.Extensions.Views.Internal;

namespace Replikit.Extensions.Views;

[Module(Flags = ModuleFlags.Library)]
public class ViewsModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddModule<StateModule>();

        services.AddScoped<IViewManager, ViewManager>();

        services.AddSingleton<ViewHandlerAccessor>();
        services.AddSingleton<IViewIntrospectionInfoAccessor>(sp => sp.GetRequiredService<ViewHandlerAccessor>());

        services.AddSingleton<ViewExternalActivationDeterminant>();
    }
}
