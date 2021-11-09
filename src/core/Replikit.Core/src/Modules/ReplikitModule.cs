using Kantaiko.Hosting.Modules;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Loader;
using Replikit.Core.Options;

namespace Replikit.Core.Modules;

public abstract class ReplikitModule : IModule
{
    public virtual void ConfigureModules(IReplikitModuleCollection modules) { }
    public virtual void ConfigureServices(IServiceCollection services) { }

    public virtual void ConfigureAdapters(IAdapterLoaderOptions options) { }

    void IModule.ConfigureModules(IModuleCollection modules)
    {
        ConfigureModules(new ReplikitModuleCollection(modules));
    }

    void IModule.ConfigureServices(IServiceCollection services)
    {
        ConfigureServices(services);
        services.Configure<AdapterLoaderOptions>(ConfigureAdapters);
    }
}
