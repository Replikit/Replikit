using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Handlers.Lifecycle;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State.LifecycleHandlers.EventHandlerCreated;

internal class LoadStateHandler : Kantaiko.Routing.Events.EventHandler<EventHandlerCreatedEvent>
{
    protected override async Task<Unit> HandleAsync(IEventContext<EventHandlerCreatedEvent> context)
    {
        var stateLoader = Event.EventContext.ServiceProvider.GetRequiredService<IStateLoader>();

        await stateLoader.LoadAsync(CancellationToken);
        return default;
    }
}
