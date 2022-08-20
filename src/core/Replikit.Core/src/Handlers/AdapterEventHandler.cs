using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Handlers;

public abstract class AdapterEventHandler
{
    public abstract Task HandleAsync(IAdapterEventContext<IBotEvent> context);
}

public abstract class AdapterEventHandler<TEvent> : AdapterEventHandler where TEvent : IBotEvent
{
    public abstract Task HandleAsync(IAdapterEventContext<TEvent> context);

    public override Task HandleAsync(IAdapterEventContext<IBotEvent> context)
    {
        if (context is IAdapterEventContext<TEvent> typedContext)
        {
            return HandleAsync(typedContext);
        }

        return Task.CompletedTask;
    }
}
