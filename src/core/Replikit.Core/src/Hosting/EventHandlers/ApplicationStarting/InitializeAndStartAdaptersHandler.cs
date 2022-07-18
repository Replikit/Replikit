using Kantaiko.Hosting.Lifecycle.Events;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Hosting.Adapters;
using Replikit.Core.Hosting.Events;

namespace Replikit.Core.Hosting.EventHandlers.ApplicationStarting;

internal class InitializeAndStartAdaptersHandler : AsyncEventHandlerBase<ApplicationStartingEvent>
{
    private readonly IAdapterEventHandler _adapterEventHandler;
    private readonly AdapterLoader _adapterLoader;
    private readonly ReplikitCoreLifecycle _replikitCoreLifecycle;
    private readonly AdapterCollection _adapterCollection;

    public InitializeAndStartAdaptersHandler(
        IAdapterEventHandler adapterEventHandler,
        AdapterLoader adapterLoader, ReplikitCoreLifecycle replikitCoreLifecycle,
        AdapterCollection adapterCollection)
    {
        _adapterEventHandler = adapterEventHandler;
        _adapterLoader = adapterLoader;
        _replikitCoreLifecycle = replikitCoreLifecycle;
        _adapterCollection = adapterCollection;
    }

    protected override async Task HandleAsync(IAsyncEventContext<ApplicationStartingEvent> context)
    {
        var adapterContext = new AdapterFactoryContext(_adapterEventHandler);

        await _adapterLoader.LoadAdapters(adapterContext, context.CancellationToken);

        await using var scope = context.ServiceProvider.CreateAsyncScope();

        var eventContext = new AsyncEventContext<AdaptersInitializedEvent>(
            new AdaptersInitializedEvent(),
            scope.ServiceProvider,
            context.CancellationToken
        );

        await _replikitCoreLifecycle.OnAdaptersInitialized(eventContext);

        await _adapterCollection.StartAsync(context.CancellationToken);
    }
}
