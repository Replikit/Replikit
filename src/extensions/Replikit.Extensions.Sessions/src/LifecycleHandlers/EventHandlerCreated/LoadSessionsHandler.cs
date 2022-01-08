using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Handlers.Lifecycle;
using Replikit.Extensions.Sessions.Internal;
using Replikit.Extensions.Sessions.Services;

namespace Replikit.Extensions.Sessions.LifecycleHandlers.EventHandlerCreated;

internal class LoadSessionsHandler : Kantaiko.Routing.Events.EventHandler<EventHandlerCreatedEvent>
{
    protected override async Task<Unit> HandleAsync(IEventContext<EventHandlerCreatedEvent> context)
    {
        var sessionManager = context.Event.EventContext.ServiceProvider.GetRequiredService<ISessionManager>();

        await sessionManager.LoadAsync(context.CancellationToken);

        return default;
    }
}
