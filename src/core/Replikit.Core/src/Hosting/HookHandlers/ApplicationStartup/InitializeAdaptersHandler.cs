using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Hooks.ApplicationHooks;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Hosting.Adapters;
using Replikit.Core.Hosting.Hooks;

namespace Replikit.Core.Hosting.HookHandlers.ApplicationStartup;

internal class InitializeAdaptersHandler : IAsyncHookHandler<ApplicationStartupHook>
{
    private readonly IHookDispatcher _hookDispatcher;
    private readonly IAdapterEventHandler _adapterEventHandler;
    private readonly AdapterLoader _adapterLoader;

    public InitializeAdaptersHandler(IHookDispatcher hookDispatcher,
        IAdapterEventHandler adapterEventHandler,
        AdapterLoader adapterLoader)
    {
        _hookDispatcher = hookDispatcher;
        _adapterEventHandler = adapterEventHandler;
        _adapterLoader = adapterLoader;
    }

    public async Task HandleAsync(ApplicationStartupHook payload, CancellationToken cancellationToken = default)
    {
        var context = new AdapterContext(_adapterEventHandler);
        await _adapterLoader.LoadAdapters(context, cancellationToken);

        var adaptersInitializedHook = new AdaptersInitializedHook();
        await _hookDispatcher.DispatchAsync(adaptersInitializedHook, cancellationToken);
    }
}
