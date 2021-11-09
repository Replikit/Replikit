using Kantaiko.Hosting.Hooks;
using Kantaiko.Hosting.Hooks.ApplicationHooks;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Internal;

namespace Replikit.Core.Handlers.HookHandlers.ApplicationInitialized;

internal class LoadHandlersHandler : IHookHandler<ApplicationInitializedHook>
{
    private readonly Internal.AdapterEventHandler _adapterEventHandler;

    public LoadHandlersHandler(Internal.AdapterEventHandler adapterEventHandler)
    {
        _adapterEventHandler = adapterEventHandler;
    }

    public void Handle(ApplicationInitializedHook payload)
    {
        var eventTypes = typeof(Event).Assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(Event)))
            .ToArray();

        var handlerTypes = payload.Assemblies
            .SelectMany(x => x.GetTypes())
            .Where(x => x.IsClass && !x.IsAbstract && x.IsAssignableTo(typeof(IAutoRegistrableHandler)))
            .ToArray();

        foreach (var eventType in eventTypes)
        {
            var targetType = typeof(IAutoRegistrableHandler<>).MakeGenericType(eventType);
            var specificHandlerTypes = handlerTypes.Where(x => x.IsAssignableTo(targetType)).ToArray();

            var childTypes = eventTypes.Where(x => x.IsAssignableTo(eventType));
            foreach (var type in childTypes)
            {
                _adapterEventHandler.HandlerTypes.AddRange(type, specificHandlerTypes);
            }
        }
    }
}
