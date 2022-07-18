using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Handlers.Lifecycle;

public class EventHandledEvent
{
    public EventHandledEvent(IAdapterEventContext<IAdapterEvent> eventContext)
    {
        EventContext = eventContext;
    }

    public IAdapterEventContext<IAdapterEvent> EventContext { get; }
}
