using Kantaiko.Hosting.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Modules;
using Replikit.Extensions.Common;
using Replikit.Extensions.Common.Views;
using Replikit.Extensions.Views.Internal;
using Replikit.Extensions.Views.Options;

namespace Replikit.Extensions.Views;

[ModuleFlags(ModuleFlags.Library)]
public class ViewsModule : ReplikitModule
{
    private readonly IConfiguration _configuration;

    public ViewsModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IViewManager, ViewManager>();

        services.AddSingleton<ViewRequestContextAccessor>();
        services.AddSingleton<ViewRequestHandlerAccessor>();

        services.AddSingleton<IViewStorageProvider, ViewStorageProvider>();
        services.AddSingleton<MemoryViewStorage>();
        services.AddSingleton<ViewExternalActivationDeterminant>();

        services.AddTransient(provider => provider.GetRequiredService<IViewStorageProvider>().Resolve());

        services.ConfigureStorage<IViewStorage, MemoryViewStorage>(MemoryViewStorage.Name);

        var viewsOptions = _configuration.GetSection("Replikit:Views").Get<ViewsOptions>();
        services.ConfigureSelectedStorage<IViewStorage>(viewsOptions?.Storage);
    }
}
