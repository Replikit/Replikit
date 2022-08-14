using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Replikit.Core.Hosting.Abstractions;
using Replikit.Core.Hosting.Adapters;
using Replikit.Core.Hosting.Logging;

namespace Replikit.Core.Hosting;

public static class HostBuilderExtensions
{
    public static void ConfigureReplikit(this IHostBuilder hostBuilder, Action<ReplikitHostingBuilder>? builder = null)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);

        hostBuilder.ConfigureServices((context, services) =>
        {
            services.AddLoggingInternal();
            services.AddAdaptersInternal(context.Configuration);

            services.AddSingleton<ReplikitCoreLifetime>();
            services.AddSingleton<IReplikitCoreLifetime>(sp => sp.GetRequiredService<ReplikitCoreLifetime>());

            services.AddHostedService<ReplikitHostedService>();

            builder?.Invoke(new ReplikitHostingBuilder(context, services));
        });
    }

    public static void ConfigureDevelopmentUserSecrets<T>(this IHostBuilder hostBuilder) where T : class
    {
        hostBuilder.ConfigureAppConfiguration((context, configurationBuilder) =>
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                configurationBuilder.AddUserSecrets<T>();
            }
        });
    }
}
