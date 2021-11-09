using Kantaiko.Routing;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Internal;

namespace Replikit.Core.Handlers;

public abstract class AdapterEventHandler : AdapterEventHandler<Event> { }

public abstract class AdapterEventHandler<TEvent> :
    IChainedHandler<IEventContext, Task<Unit>>,
    IAutoRegistrableHandler<TEvent>
    where TEvent : IEvent
{
    protected IEventContext<TEvent> Context { get; private set; } = null!;

    protected TEvent Event => Context.Event;
    protected IServiceProvider ServiceProvider => Context.ServiceProvider;
    protected IAdapter Adapter => Context.Adapter;
    protected CancellationToken CancellationToken => Context.CancellationToken;

    public delegate Task<Unit> NextAction(IEventContext<TEvent>? context = default);

    Task<Unit> IChainedHandler<IEventContext, Task<Unit>>.Handle(IEventContext input,
        Func<IEventContext, Task<Unit>> next)
    {
        var context = (IEventContext<TEvent>) input;

        BeforeHandle(context);
        return HandleAsync(context, x => next(x ?? context));
    }

    protected virtual void BeforeHandle(IEventContext<TEvent> context)
    {
        Context = context;
    }

    public abstract Task<Unit> HandleAsync(IEventContext<TEvent> context, NextAction next);
}
