using Kantaiko.Routing.Context;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers;

public class EventContext<TEvent> : ContextBase, IEventContext<TEvent> where TEvent : IEvent
{
    public EventContext(TEvent @event, IAdapter adapter, IServiceProvider serviceProvider,
        CancellationToken cancellationToken) : base(serviceProvider, cancellationToken)
    {
        Event = @event;
        Adapter = adapter;
    }

    public TEvent Event { get; }
    public IAdapter Adapter { get; }

    IEvent IEventContext.Event => Event;
}
