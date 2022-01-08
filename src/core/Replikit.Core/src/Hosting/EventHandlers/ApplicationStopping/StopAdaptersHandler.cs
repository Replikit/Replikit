using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Lifecycle.Events;
using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Replikit.Core.Hosting.Adapters;

namespace Replikit.Core.Hosting.EventHandlers.ApplicationStopping;

internal class StopAdaptersHandler : LifecycleEventHandler<ApplicationStoppingEvent>
{
    private readonly AdapterCollection _adapterCollection;

    public StopAdaptersHandler(AdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    protected override async Task<Unit> HandleAsync(IEventContext<ApplicationStoppingEvent> context)
    {
        await _adapterCollection.StopAsync(context.CancellationToken);
        return default;
    }
}
