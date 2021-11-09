using System.Drawing;
using Kantaiko.ConsoleFormatting;
using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Hooks.ApplicationHooks;
using Kantaiko.Hosting.Introspection;
using Kantaiko.Hosting.Modules;
using Microsoft.Extensions.Logging;

namespace Replikit.Core.Hosting.HookHandlers.ApplicationInitialized;

public class LogLoadedModulesHandler : IHookHandler<ApplicationInitializedHook>
{
    private readonly HostInfo _hostInfo;
    private readonly ILogger<ModuleLoader> _logger;

    // ReSharper disable once ContextualLoggerProblem
    public LogLoadedModulesHandler(HostInfo hostInfo, ILogger<ModuleLoader> logger)
    {
        _hostInfo = hostInfo;
        _logger = logger;
    }

    public void Handle(ApplicationInitializedHook payload)
    {
        foreach (var moduleInfo in _hostInfo.Modules)
        {
            if (moduleInfo.DisplayName == "ReplikitCore") continue;
            if (moduleInfo.Flags.HasFlag(ModuleFlags.Library)) continue;

            var isImplicit = moduleInfo.Dependents.Count > 0;
            var implicitBrand = isImplicit ? " [implicit]" : "";

            _logger.LogInformation("Loaded module {Name} {Version}{ImplicitBrand}",
                Colors.FgColor(moduleInfo.DisplayName, Color.Cyan),
                Colors.FgColor(moduleInfo.Version.ToString(), Color.LightCyan),
                implicitBrand);
        }
    }
}
