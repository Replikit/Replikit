using Kantaiko.Hosting.Modularity;
using Kantaiko.Hosting.Modularity.Introspection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.Common;
using Replikit.Extensions.Common.Views;
using Replikit.Extensions.Views.Internal;
using Replikit.Extensions.Views.Options;

namespace Replikit.Extensions.Views;

[Module(Flags = ModuleFlags.Library)]
public class ViewsModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public ViewsModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IViewManager, ViewManager>();

        services.AddSingleton<ViewHandlerAccessor>();
        services.AddSingleton<IViewIntrospectionInfoAccessor>(sp => sp.GetRequiredService<ViewHandlerAccessor>());

        services.AddSingleton<IViewStorageProvider, ViewStorageProvider>();
        services.AddSingleton<MemoryViewStorage>();
        services.AddSingleton<ViewExternalActivationDeterminant>();

        services.AddTransient(provider => provider.GetRequiredService<IViewStorageProvider>().Resolve());

        services.ConfigureStorage<IViewStorage, MemoryViewStorage>(MemoryViewStorage.Name);

        var viewsOptions = _configuration.GetSection("Replikit:Views").Get<ViewsOptions>();
        services.ConfigureSelectedStorage<IViewStorage>(viewsOptions?.Storage);
    }
}
