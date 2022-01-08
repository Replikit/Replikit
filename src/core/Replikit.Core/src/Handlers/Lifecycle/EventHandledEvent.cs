using Kantaiko.Routing.Events;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers.Lifecycle;

public class EventHandledEvent
{
    public EventHandledEvent(IEventContext<IEvent> eventContext)
    {
        EventContext = eventContext;
    }

    public IEventContext<IEvent> EventContext { get; }
}
