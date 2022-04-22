using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Lifecycle.Events;
using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Hosting.Adapters;
using Replikit.Core.Hosting.Events;

namespace Replikit.Core.Hosting.EventHandlers.ApplicationStarting;

internal class InitializeAndStartAdaptersHandler : LifecycleEventHandler<ApplicationStartingEvent>
{
    private readonly IAdapterEventHandler _adapterEventHandler;
    private readonly AdapterLoader _adapterLoader;
    private readonly IReplikitLifecycle _replikitLifecycle;
    private readonly AdapterCollection _adapterCollection;

    public InitializeAndStartAdaptersHandler(
        IAdapterEventHandler adapterEventHandler,
        AdapterLoader adapterLoader, IReplikitLifecycle replikitLifecycle, AdapterCollection adapterCollection)
    {
        _adapterEventHandler = adapterEventHandler;
        _adapterLoader = adapterLoader;
        _replikitLifecycle = replikitLifecycle;
        _adapterCollection = adapterCollection;
    }

    protected override async Task<Unit> HandleAsync(IEventContext<ApplicationStartingEvent> context)
    {
        var adapterContext = new AdapterFactoryContext(_adapterEventHandler);
        await _adapterLoader.LoadAdapters(adapterContext, context.CancellationToken);

        var eventContext = new EventContext<AdaptersInitializedEvent>(new AdaptersInitializedEvent(),
            context.ServiceProvider, cancellationToken: context.CancellationToken);

        await _replikitLifecycle.AdaptersInitialized.Handle(eventContext);

        await _adapterCollection.StartAsync(context.CancellationToken);

        return default;
    }
}
