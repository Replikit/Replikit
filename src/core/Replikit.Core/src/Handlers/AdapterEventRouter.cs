using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing;
using Kantaiko.Routing.AutoRegistration;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers;

internal class AdapterEventRouter : IAdapterEventRouter
{
    private IHandler<IEventContext<Event>, Task<Unit>> _handler;

    public AdapterEventRouter(HostInfo hostInfo)
    {
        var types = hostInfo.Assemblies.Append(typeof(IEvent).Assembly).SelectMany(x => x.GetTypes());
        _handler = EventHandlerFactory.CreateChainedEventHandler<IEvent>(types, ServiceHandlerFactory.Instance);
    }

    public IHandler<IEventContext<Event>, Task<Unit>> Handler
    {
        get => _handler;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _handler = value;
        }
    }
}
