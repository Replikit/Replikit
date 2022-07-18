using Kantaiko.Hosting.Lifecycle.Events;
using Kantaiko.Routing.Events;
using Replikit.Core.Hosting.Adapters;

namespace Replikit.Core.Hosting.EventHandlers.ApplicationStopping;

internal class StopAdaptersHandler : AsyncEventHandlerBase<ApplicationStoppingEvent>
{
    private readonly AdapterCollection _adapterCollection;

    public StopAdaptersHandler(AdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    protected override Task HandleAsync(IAsyncEventContext<ApplicationStoppingEvent> context)
    {
        return _adapterCollection.StopAsync(context.CancellationToken);
    }
}
