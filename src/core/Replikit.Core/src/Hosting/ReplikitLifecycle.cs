using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.Introspection;
using Kantaiko.Routing;
using Kantaiko.Routing.AutoRegistration;
using Kantaiko.Routing.Events;
using Replikit.Core.Hosting.Events;

namespace Replikit.Core.Hosting;

internal class ReplikitLifecycle : IReplikitLifecycle
{
    private IHandler<IEventContext<AdaptersInitializedEvent>, Task<Unit>> _adaptersInitialized;

    public ReplikitLifecycle(HostInfo hostInfo)
    {
        var types = AutoRegistrationUtils.MaterializeCollection(hostInfo.Assemblies.SelectMany(x => x.GetTypes()));

        _adaptersInitialized = EventHandlerFactory
            .CreateSequentialEventHandler<AdaptersInitializedEvent>(types, ServiceHandlerFactory.Instance);
    }

    public IHandler<IEventContext<AdaptersInitializedEvent>, Task<Unit>> AdaptersInitialized
    {
        get => _adaptersInitialized;
        set
        {
            ArgumentNullException.ThrowIfNull(value);
            _adaptersInitialized = value;
        }
    }
}
