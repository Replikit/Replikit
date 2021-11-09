using Kantaiko.Hosting.Hooks;
using Replikit.Core.Hosting.Adapters;
using Replikit.Core.Hosting.Hooks;

namespace Replikit.Core.Hosting.HookHandlers.AdaptersInitialized;

internal class StartAdaptersHandler : IAsyncHookHandler<AdaptersInitializedHook>
{
    private readonly AdapterCollection _adapterCollection;

    public StartAdaptersHandler(AdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    public async Task HandleAsync(AdaptersInitializedHook payload, CancellationToken cancellationToken = default)
    {
        await _adapterCollection.StartAsync(cancellationToken);
    }
}
