using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing;
using Kantaiko.Routing.AutoRegistration;
using Kantaiko.Routing.Events;

namespace Replikit.Core.Handlers.Lifecycle;

internal class HandlerLifecycle : IHandlerLifecycle
{
    private IHandler<IEventContext<EventHandlerCreatedEvent>, Task> _eventHandlerCreated;
    private IHandler<IEventContext<EventHandledEvent>, Task> _eventHandled;

    public HandlerLifecycle(HostInfo hostInfo)
    {
        var types = hostInfo.Assemblies.SelectMany(x => x.GetTypes()).ToArray();

        _eventHandlerCreated = EventHandlerFactory
            .CreateParallelEventHandler<EventHandlerCreatedEvent>(types, ServiceHandlerFactory.Instance);

        _eventHandled = EventHandlerFactory
            .CreateParallelEventHandler<EventHandledEvent>(types, ServiceHandlerFactory.Instance);
    }

    public IHandler<IEventContext<EventHandlerCreatedEvent>, Task> EventHandlerCreated
    {
        get => _eventHandlerCreated;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _eventHandlerCreated = value;
        }
    }

    public IHandler<IEventContext<EventHandledEvent>, Task> EventHandled
    {
        get => _eventHandled;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _eventHandled = value;
        }
    }
}
