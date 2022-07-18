using Kantaiko.Hosting.Lifecycle;
using Kantaiko.Hosting.Modularity.TypeRegistration;
using Kantaiko.Routing.AutoRegistration;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Handlers.TypeRegistration;

internal class AdapterEventHandlerRegistrationHandler : ITypeRegistrationHandler
{
    private readonly List<Type> _types;
    private readonly AdapterEventRouter _router;

    public AdapterEventHandlerRegistrationHandler(AdapterEventRouter router)
    {
        _router = router;

        _types = new List<Type>(typeof(IAdapterEvent).Assembly.GetTypes());
    }

    public bool Handle(Type type)
    {
        foreach (var @interface in type.GetInterfaces())
        {
            if (@interface == typeof(IAutoRegistrableHandler))
            {
                _types.Add(type);
                return true;
            }

            if (@interface == typeof(IAdapterEvent))
            {
                _types.Add(type);
                return true;
            }

            if (@interface.IsGenericType && @interface.GetGenericTypeDefinition() == typeof(IAdapterEventContext<>))
            {
                _types.Add(type);
                return true;
            }
        }

        return false;
    }

    public void Complete()
    {
        _router.Handler = EventHandlerFactory
            .ChainedAsync<IAdapterEventContext<AdapterEvent>>(_types, ServiceHandlerFactory.Instance);
    }
}
