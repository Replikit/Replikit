using Kantaiko.Hosting.Lifecycle.TypeRegistration;
using Kantaiko.Routing.Events;
using Replikit.Core.Hosting.Events;

namespace Replikit.Core.Hosting.TypeRegistration;

internal class ReplikitCoreLifecycleRegistrationHandler : EventHandlerTypeRegistrationHandler
{
    private readonly IReplikitCoreLifecycle _lifecycle;

    public ReplikitCoreLifecycleRegistrationHandler(IReplikitCoreLifecycle lifecycle)
    {
        _lifecycle = lifecycle;
    }

    protected override bool RegisterHandler(Type contextType, Type handlerType)
    {
        if (contextType == typeof(IAsyncEventContext<AdaptersInitializedEvent>))
        {
            _lifecycle.AdaptersInitialized +=
                CreateAsyncHandler<IAsyncEventContext<AdaptersInitializedEvent>>(handlerType);

            return true;
        }

        return false;
    }
}
