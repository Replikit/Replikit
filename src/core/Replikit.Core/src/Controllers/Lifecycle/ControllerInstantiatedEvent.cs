using Kantaiko.Routing.Events;
using Replikit.Abstractions.Messages.Events;

namespace Replikit.Core.Controllers.Lifecycle;

public class ControllerInstantiatedEvent
{
    public ControllerInstantiatedEvent(IEventContext<MessageReceivedEvent> eventContext)
    {
        EventContext = eventContext;
    }

    public IEventContext<MessageReceivedEvent> EventContext { get; }
}
