using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters.Loader;
using Replikit.Core.Options;

namespace Replikit.Core.Modularity;

public abstract class ReplikitModule : Module, IModule
{
    protected virtual void ConfigureAdapters(IAdapterLoaderOptions options) { }

    void IModule.ConfigureServices(IServiceCollection services)
    {
        services.AddModule<ReplikitCoreModule>();

        ConfigureServices(services);
        services.Configure<AdapterLoaderOptions>(ConfigureAdapters);
    }
}
