using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Lifecycle;

namespace Replikit.Core.Handlers;

public abstract class AdapterEventHandler : AdapterEventHandler<Event> { }

public abstract class AdapterEventHandler<TEvent> : ChainedEventHandler<TEvent> where TEvent : IEvent
{
    private IAdapter? _adapter;

    protected IAdapter Adapter => _adapter ??=
        AdapterEventProperties.Of(Context)?.Adapter ??
        throw new InvalidOperationException("Failed to access adapter instance");

    protected override async Task BeforeHandleAsync(IEventContext<TEvent> context)
    {
        var lifecycle = context.ServiceProvider.GetRequiredService<IHandlerLifecycle>();

        using var scope = ServiceProvider.CreateScope();

        var eventContext = new EventContext<EventHandlerCreatedEvent>(
            new EventHandlerCreatedEvent((IEventContext<IEvent>) context),
            scope.ServiceProvider, cancellationToken: CancellationToken);

        await lifecycle.EventHandlerCreated.Handle(eventContext);
    }
}
