using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Handlers;

public abstract class AdapterEventHandler
{
    public abstract Task HandleAsync(IAdapterEventContext<IAdapterEvent> context);
}

public abstract class AdapterEventHandler<TEvent> : AdapterEventHandler where TEvent : IAdapterEvent
{
    public abstract Task HandleAsync(IAdapterEventContext<TEvent> context);

    public override Task HandleAsync(IAdapterEventContext<IAdapterEvent> context)
    {
        if (context is IAdapterEventContext<TEvent> typedContext)
        {
            return HandleAsync(typedContext);
        }

        return Task.CompletedTask;
    }
}
