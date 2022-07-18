using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Handlers.Lifecycle;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State.EventHandlers.EventHandlerCreated;

internal class LoadStateHandler : AsyncEventHandlerBase<EventHandlerCreatedEvent>
{
    protected override Task HandleAsync(IAsyncEventContext<EventHandlerCreatedEvent> context)
    {
        var stateLoader = Event.EventContext.ServiceProvider.GetRequiredService<IStateLoader>();

        return stateLoader.LoadAsync(CancellationToken);
    }
}
