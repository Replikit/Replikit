using Kantaiko.Routing.Events;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers.Lifecycle;

public class EventHandlerCreatedEvent
{
    public EventHandlerCreatedEvent(IEventContext<IEvent> eventContext)
    {
        EventContext = eventContext;
    }

    public IEventContext<IEvent> EventContext { get; }
}
