using Kantaiko.Routing;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Controllers.Lifecycle;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State.LifecycleHandlers.ControllerInstantiated;

internal class LoadStateHandler : Kantaiko.Routing.Events.EventHandler<ControllerInstantiatedEvent>
{
    protected override async Task<Unit> HandleAsync(IEventContext<ControllerInstantiatedEvent> context)
    {
        var stateLoader = Event.EventContext.ServiceProvider.GetRequiredService<IStateLoader>();

        await stateLoader.LoadAsync(CancellationToken);
        return default;
    }
}
