using Replikit.Abstractions.Events;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.Handlers;

public abstract class BotEventHandler
{
    public abstract Task HandleAsync(IBotEventContext context);
}

public abstract class BotEventHandler<TEvent> : BotEventHandler where TEvent : IBotEvent
{
    public abstract Task HandleAsync(IBotEventContext<TEvent> context);

    public override Task HandleAsync(IBotEventContext context)
    {
        if (context is IBotEventContext<TEvent> typedContext)
        {
            return HandleAsync(typedContext);
        }

        return Task.CompletedTask;
    }
}
