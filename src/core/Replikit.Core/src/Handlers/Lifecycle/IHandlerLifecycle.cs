using Kantaiko.Routing;
using Kantaiko.Routing.Events;

namespace Replikit.Core.Handlers.Lifecycle;

public interface IHandlerLifecycle
{
    IHandler<IEventContext<EventHandlerCreatedEvent>, Task> EventHandlerCreated { get; set; }
    IHandler<IEventContext<EventHandledEvent>, Task> EventHandled { get; set; }
}
