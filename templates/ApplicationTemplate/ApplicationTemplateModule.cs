using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters.Loader;
using Replikit.Core.Modules;

namespace ApplicationTemplate;

public class ApplicationTemplateModule : ReplikitModule
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        // Here you can register services and add dependency modules
        // services.AddSingleton<MyDependency>();
        // services.AddModule<ScenesModule>();
    }

    protected override void ConfigureAdapters(IAdapterLoaderOptions options)
    {
        // Here you can register adapters
        // options.AddTelegram();
    }
}
