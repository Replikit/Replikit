using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing;
using Kantaiko.Routing.AutoRegistration;
using Kantaiko.Routing.Events;

namespace Replikit.Core.Controllers.Lifecycle;

internal class ControllerLifecycle : IControllerLifecycle
{
    private IHandler<IEventContext<ControllerInstantiatedEvent>, Task> _controllerInstantiated;

    public ControllerLifecycle(HostInfo hostInfo)
    {
        var types = hostInfo.Assemblies.SelectMany(x => x.GetTypes()).ToArray();

        _controllerInstantiated = EventHandlerFactory
            .CreateSequentialEventHandler<ControllerInstantiatedEvent>(types, ServiceHandlerFactory.Instance);
    }

    public IHandler<IEventContext<ControllerInstantiatedEvent>, Task> ControllerInstantiated
    {
        get => _controllerInstantiated;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _controllerInstantiated = value;
        }
    }
}
