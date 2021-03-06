using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Replikit.Abstractions.Adapters;
using Replikit.Core.Hosting.Adapters;
using Replikit.Core.Options;

namespace Replikit.Core.Hosting;

internal static class ServiceCollectionExtensions
{
    public static void AddReplikitHosting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<AdapterLoader>();

        services.PostConfigure<AdapterLoaderOptions>(options =>
        {
            AdapterConfigurationLoader.LoadAdaptersFromConfiguration(options, configuration);
        });

        services.TryAddSingleton<AdapterCollection>();
        services.TryAddSingleton<IAdapterCollection>(sp => sp.GetRequiredService<AdapterCollection>());

        services.AddSingleton<IReplikitLifecycle, ReplikitLifecycle>();
    }
}
