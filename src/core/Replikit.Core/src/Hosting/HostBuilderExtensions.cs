using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Host;
using Kantaiko.Hosting.Modules;
using Microsoft.Extensions.Hosting;

namespace Replikit.Core.Hosting;

public static class HostBuilderExtensions
{
    public static void ConfigureReplikitHosting(this IHostBuilder hostBuilder,
        Action<IModuleCollection>? configureDelegate = null)
    {
        ArgumentNullException.ThrowIfNull(hostBuilder);

        hostBuilder.ConfigureKantaikoHosting(services =>
        {
            services.Add<ReplikitCoreModule>();
            configureDelegate?.Invoke(services);
        });

        hostBuilder.ConfigureKantaikoHostingHooks();
    }
}
