using Kantaiko.Routing.Events;
using Replikit.Core.Hosting.Events;

namespace Replikit.Core.Hosting;

internal class ReplikitCoreLifecycle : IReplikitCoreLifecycle
{
    public event AsyncEventHandler<IAsyncEventContext<AdaptersInitializedEvent>>? AdaptersInitialized;

    public Task OnAdaptersInitialized(IAsyncEventContext<AdaptersInitializedEvent> context)
    {
        return AdaptersInitialized.InvokeAsync(context);
    }
}
