using Kantaiko.Routing;
using Kantaiko.Routing.AutoRegistration;
using Kantaiko.Routing.Events;
using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Context;
using Replikit.Core.Handlers.Lifecycle;

namespace Replikit.Core.Handlers;

public abstract class AdapterEventHandler : AdapterEventHandler<AdapterEvent> { }

public abstract class AdapterEventHandler<TEvent> : AdapterEventHandler<TEvent, IAdapterEventContext<TEvent>>
    where TEvent : IAdapterEvent { }

public abstract class AdapterEventHandler<TEvent, TContext> : AsyncChainedEventHandlerBase<TEvent, TContext>
    where TEvent : IAdapterEvent
    where TContext : IAdapterEventContext<TEvent>
{
    protected delegate Task<Unit> NextAction();

    protected override Task<Unit> HandleAsync(TContext context, Func<Task<Unit>> next)
    {
        return HandleAsync(context, () => next());
    }

    protected abstract Task<Unit> HandleAsync(TContext context, NextAction next);

    protected IAdapter Adapter => Context.Adapter;

    protected override Task BeforeHandleAsync(TContext context)
    {
        var lifecycle = context.ServiceProvider.GetRequiredService<HandlerLifecycle>();

        return lifecycle.OnEventHandlerCreatedAsync((IAdapterEventContext<IAdapterEvent>) context, CancellationToken);
    }
}
