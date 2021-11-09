using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Adapters.Loader;

namespace Replikit.Adapters.Common.Adapters;

public static class AdapterLoaderOptionsExtensions
{
    public static void EnsureAdapterFactoryConfigured(this IAdapterLoaderOptions loaderOptions, string name,
        Func<IAdapterFactory> factoryResolver)
    {
        if (!loaderOptions.AdapterFactories.ContainsKey(name))
        {
            loaderOptions.AdapterFactories[name] = factoryResolver();
        }
    }
}
