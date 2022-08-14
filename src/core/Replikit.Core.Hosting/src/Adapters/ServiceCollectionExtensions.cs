using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Core.Hosting.Options;

namespace Replikit.Core.Hosting.Adapters;

internal static class ServiceCollectionExtensions
{
    public static void AddAdaptersInternal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<AdapterCollection>();
        services.AddSingleton<IAdapterCollection>(sp => sp.GetRequiredService<AdapterCollection>());

        services.AddSingleton<AdapterLoader>();

        services.PostConfigure<AdapterLoaderOptions>(options =>
        {
            AdapterConfigurationLoader.LoadAdaptersFromConfiguration(options, configuration);
        });
    }
}
