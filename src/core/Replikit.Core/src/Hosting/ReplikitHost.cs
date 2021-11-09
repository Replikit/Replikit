using Kantaiko.Hosting.Host;
using Kantaiko.Hosting.Modules;
using Microsoft.Extensions.Hosting;
using Replikit.Core.Modules;

namespace Replikit.Core.Hosting;

public static class ReplikitHost
{
    public static void RunModule<TModule>(string[]? args = null) where TModule : ReplikitModule
    {
        var builder = new ReplikitHostBuilder(args);

        builder.Modules.Add<TModule>();
        builder.AddDevelopmentUserSecrets<TModule>();

        var managedHost = builder.Build();
        managedHost.Run();
    }

    public static IManagedHostBuilder CreateBuilder(string[]? args = null)
    {
        return new ReplikitHostBuilder(args);
    }

    public static IHostBuilder CreateSingleBuilder(string[]? args = null,
        Action<IModuleCollection>? configureDelegate = null)
    {
        var builder = Host.CreateDefaultBuilder();
        builder.ConfigureReplikitHosting(configureDelegate);

        return builder;
    }
}
