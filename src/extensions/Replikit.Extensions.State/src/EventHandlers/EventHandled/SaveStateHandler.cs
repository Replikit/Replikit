using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Core.Handlers.Lifecycle;
using Replikit.Extensions.State.Implementation;

namespace Replikit.Extensions.State.EventHandlers.EventHandled;

internal class SaveStateHandler : AsyncEventHandlerBase<EventHandledEvent>
{
    protected override Task HandleAsync(IAsyncEventContext<EventHandledEvent> context)
    {
        var stateLoader = Event.EventContext.ServiceProvider.GetRequiredService<IStateLoader>();

        return stateLoader.SaveAsync(CancellationToken);
    }
}
