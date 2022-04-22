using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Handlers.Lifecycle;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State.LifecycleHandlers.EventHandled;

internal class SaveStateHandler : Kantaiko.Routing.Events.EventHandler<EventHandledEvent>
{
    protected override async Task<Unit> HandleAsync(IEventContext<EventHandledEvent> context)
    {
        var stateLoader = Event.EventContext.ServiceProvider.GetRequiredService<IStateLoader>();

        await stateLoader.SaveAsync(CancellationToken);
        return default;
    }
}
