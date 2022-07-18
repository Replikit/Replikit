using Kantaiko.Routing.Events;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Handlers.Lifecycle;

internal class HandlerLifecycle : IHandlerLifecycle
{
    public event AsyncEventHandler<IAsyncEventContext<EventHandlerCreatedEvent>>? EventHandlerCreated;
    public event AsyncEventHandler<IAsyncEventContext<EventHandledEvent>>? EventHandled;

    public Task OnEventHandlerCreatedAsync(IAdapterEventContext<IAdapterEvent> eventContext,
        CancellationToken cancellationToken)
    {
        var context = new AsyncEventContext<EventHandlerCreatedEvent>(
            new EventHandlerCreatedEvent(eventContext),
            cancellationToken: cancellationToken
        );

        return EventHandlerCreated.InvokeAsync(context);
    }

    public Task OnEventHandledAsync(IAdapterEventContext<IAdapterEvent> eventContext,
        CancellationToken cancellationToken)
    {
        var context = new AsyncEventContext<EventHandledEvent>(
            new EventHandledEvent(eventContext),
            cancellationToken: cancellationToken
        );

        return EventHandled.InvokeAsync(context);
    }
}
