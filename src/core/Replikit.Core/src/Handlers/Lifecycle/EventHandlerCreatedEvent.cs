using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Handlers.Lifecycle;

public class EventHandlerCreatedEvent
{
    public EventHandlerCreatedEvent(IAdapterEventContext<IAdapterEvent> eventContext)
    {
        EventContext = eventContext;
    }

    public IAdapterEventContext<IAdapterEvent> EventContext { get; }
}
