using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Handlers.Lifecycle;
using Replikit.Extensions.Sessions.Internal;
using Replikit.Extensions.Sessions.Services;

namespace Replikit.Extensions.Sessions.LifecycleHandlers.EventHandled;

internal class SaveSessionsHandler : Kantaiko.Routing.Events.EventHandler<EventHandledEvent>
{
    protected override async Task<Unit> HandleAsync(IEventContext<EventHandledEvent> context)
    {
        var sessionManager = context.Event.EventContext.ServiceProvider.GetRequiredService<ISessionManager>();

        await sessionManager.SaveAsync(context.CancellationToken);

        return default;
    }
}
