using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Replikit.Abstractions.Adapters.Loader;
using Replikit.Core.Hosting.Options;

namespace Replikit.Core.Hosting;

public class ReplikitHostingBuilder
{
    public ReplikitHostingBuilder(HostBuilderContext context, IServiceCollection services)
    {
        Context = context;
        Services = services;
    }

    public HostBuilderContext Context { get; }
    public IServiceCollection Services { get; }

    public ReplikitHostingBuilder ConfigureAdapters(Action<IAdapterLoaderOptions> configureDelegate)
    {
        Services.Configure<AdapterLoaderOptions>(configureDelegate);

        return this;
    }
}
