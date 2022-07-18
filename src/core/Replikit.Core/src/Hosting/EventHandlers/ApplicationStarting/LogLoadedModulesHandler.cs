using System.Drawing;
using Kantaiko.ConsoleFormatting;
using Kantaiko.Hosting.Lifecycle.Events;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.Logging;

namespace Replikit.Core.Hosting.EventHandlers.ApplicationStarting;

public class LogLoadedModulesHandler : AsyncEventHandlerBase<ApplicationStartingEvent>
{
    private readonly HostInfo _hostInfo;
    private readonly ILogger<ModuleLoader> _logger;

    // ReSharper disable once ContextualLoggerProblem
    public LogLoadedModulesHandler(HostInfo hostInfo, ILogger<ModuleLoader> logger)
    {
        _hostInfo = hostInfo;
        _logger = logger;
    }

    protected override Task HandleAsync(IAsyncEventContext<ApplicationStartingEvent> context)
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

            _logger.LogInformation(
                "Loaded module {Name} {Version}{ImplicitBrand}",
                Colors.FgColor(moduleInfo.DisplayName, Color.Cyan),
                Colors.FgColor(moduleInfo.Version.ToString(), Color.LightCyan),
                implicitBrand
            );
        }

        return Task.CompletedTask;
    }
}
