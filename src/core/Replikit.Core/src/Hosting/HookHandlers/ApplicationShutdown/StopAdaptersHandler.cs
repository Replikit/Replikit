using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Hooks.ApplicationHooks;
using Replikit.Core.Hosting.Adapters;

namespace Replikit.Core.Hosting.HookHandlers.ApplicationShutdown;

internal class StopAdaptersHandler : IAsyncHookHandler<ApplicationShutdownHook>
{
    private readonly AdapterCollection _adapterCollection;

    public StopAdaptersHandler(AdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    public async Task HandleAsync(ApplicationShutdownHook payload, CancellationToken cancellationToken)
    {
        await _adapterCollection.StopAsync(cancellationToken);
    }
}
