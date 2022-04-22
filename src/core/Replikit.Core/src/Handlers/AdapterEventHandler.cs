using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Extensions;
using Replikit.Core.Handlers.Lifecycle;

namespace Replikit.Core.Handlers;

public abstract class AdapterEventHandler : AdapterEventHandler<Event> { }

public abstract class AdapterEventHandler<TEvent> : ChainedEventHandler<TEvent> where TEvent : IEvent
{
    private IAdapter? _adapter;

    protected IAdapter Adapter => _adapter ??= Context.GetRequiredAdapter();

    protected override async Task BeforeHandleAsync(IEventContext<TEvent> context)
    {
        var lifecycle = context.ServiceProvider.GetRequiredService<IHandlerLifecycle>();

        await using var scope = ServiceProvider.CreateAsyncScope();

        var eventContext = new EventContext<EventHandlerCreatedEvent>(
            new EventHandlerCreatedEvent((IEventContext<IEvent>) context),
            scope.ServiceProvider, cancellationToken: CancellationToken);

        await lifecycle.EventHandlerCreated.Handle(eventContext);
    }
}
