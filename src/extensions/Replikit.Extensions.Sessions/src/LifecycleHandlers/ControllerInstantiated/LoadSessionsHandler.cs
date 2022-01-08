using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Lifecycle;
using Replikit.Extensions.Sessions.Services;

namespace Replikit.Extensions.Sessions.LifecycleHandlers.ControllerInstantiated;

internal class LoadSessionsHandler : Kantaiko.Routing.Events.EventHandler<ControllerInstantiatedEvent>
{
    protected override async Task<Unit> HandleAsync(IEventContext<ControllerInstantiatedEvent> context)
    {
        var sessionManager = context.Event.EventContext.ServiceProvider.GetRequiredService<ISessionManager>();

        await sessionManager.LoadAsync(context.CancellationToken);

        return default;
    }
}
