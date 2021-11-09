using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters.Loader;
using Replikit.Core.Modules;

namespace ModuleTemplate;

public class ModuleTemplateModule : ReplikitModule
{
    public override void ConfigureModules(IReplikitModuleCollection modules)
    {
        // Here you can add dependency modules
        // modules.Add<ScenesModule>();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        // Here you can register services inside the DI container
        // services.AddSingleton<MyDependency>();
    }

    public override void ConfigureAdapters(IAdapterLoaderOptions options)
    {
        // Here you can register adapters
        // options.AddTelegram();
    }
}
