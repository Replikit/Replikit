using Kantaiko.Hosting;
using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Managed;
using Kantaiko.Hosting.Modularity;
using Microsoft.Extensions.Hosting;
using Replikit.Core.Modules;

namespace Replikit.Core.Hosting;

public static class ReplikitHost
{
    public static void RunModule<TModule>(string[]? args = null) where TModule : ReplikitModule
    {
        static void ConfigureHost(IHostBuilder hostBuilder)
        {
            hostBuilder.AddModule<ReplikitCoreModule>();
            hostBuilder.AddModule<TModule>();
            hostBuilder.CompleteModularityConfiguration();

            hostBuilder.ConfigureServices(x => x.AddModularLifecycleEvents());
            hostBuilder.ConfigureServices(x => x.AddManagedHostLifecycle());

            hostBuilder.AddDevelopmentUserSecrets<TModule>();
        }

        ManagedHost.CreateDefaultBuilder(args)
            .UseManagedHostHandler(new LifecycleManagedHostHandler())
            .ConfigureHostBuilder(ConfigureHost)
            .Build().Run();
    }
}
