using System.Drawing;
using Kantaiko.ConsoleFormatting;
using Kantaiko.Modularity.Introspection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Replikit.Abstractions.Adapters.Factory;
using Replikit.Abstractions.Events;
using Replikit.Core.Hosting.Adapters;

namespace Replikit.Core.Hosting;

internal class ReplikitHostedService : IHostedService
{
    private readonly IAdapterEventDispatcher _adapterEventDispatcher;
    private readonly AdapterLoader _adapterLoader;
    private readonly ReplikitCoreLifetime _replikitCoreLifetime;
    private readonly AdapterCollection _adapterCollection;
    private readonly HostInfo _hostInfo;
    private readonly ILogger<ModuleLoader> _moduleLoaderLogger;

    public ReplikitHostedService(
        IAdapterEventDispatcher adapterEventDispatcher,
        AdapterLoader adapterLoader,
        ReplikitCoreLifetime replikitCoreLifetime,
        AdapterCollection adapterCollection,
        HostInfo hostInfo,
        // ReSharper disable once ContextualLoggerProblem
        ILogger<ModuleLoader> moduleLoaderLogger)
    {
        _adapterEventDispatcher = adapterEventDispatcher;
        _adapterLoader = adapterLoader;
        _replikitCoreLifetime = replikitCoreLifetime;
        _adapterCollection = adapterCollection;
        _hostInfo = hostInfo;
        _moduleLoaderLogger = moduleLoaderLogger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var adapterContext = new AdapterFactoryContext(_adapterEventDispatcher);

        await _adapterLoader.LoadAdapters(adapterContext, cancellationToken);

        await _replikitCoreLifetime.OnAdaptersInitializedAsync(cancellationToken);

        await _adapterCollection.StartAsync(cancellationToken);

        LogLoadedModules();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return _adapterCollection.StopAsync(cancellationToken);
    }

    private void LogLoadedModules()
    {
        foreach (var moduleInfo in _hostInfo.Modules)
        {
            if (moduleInfo.DisplayName == "ReplikitCore")
            {
                continue;
            }

            if (moduleInfo.Flags.HasFlag(ModuleFlags.Library) || moduleInfo.Flags.HasFlag(ModuleFlags.Hidden))
            {
                continue;
            }

            var isImplicit = moduleInfo.Dependents.Count > 0;
            var implicitBrand = isImplicit ? " [implicit]" : "";

            _moduleLoaderLogger.LogInformation(
                "Loaded module {Name} {Version}{ImplicitBrand}",
                Colors.FgColor(moduleInfo.DisplayName, Color.Cyan),
                Colors.FgColor(moduleInfo.Version.ToString(), Color.LightCyan),
                implicitBrand
            );
        }
    }
}
