using Kantaiko.Routing.Events;

namespace Replikit.Core.Handlers.Lifecycle;

public interface IHandlerLifecycle
{
    event AsyncEventHandler<IAsyncEventContext<EventHandlerCreatedEvent>> EventHandlerCreated;
    event AsyncEventHandler<IAsyncEventContext<EventHandledEvent>> EventHandled;
}
