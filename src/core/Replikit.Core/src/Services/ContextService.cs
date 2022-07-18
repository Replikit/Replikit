using Kantaiko.Routing.Context;
using Replikit.Abstractions.Events;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Services;

public abstract class ContextService : ContextService<IAdapterEvent>
{
    protected ContextService(ContextAccessor<IAdapterEventContext<IAdapterEvent>> contextAccessor) : base(
        contextAccessor) { }
}

public abstract class ContextService<TEvent> where TEvent : IAdapterEvent
{
    protected IAdapterEventContext<TEvent> Context { get; }

    protected TEvent Event => Context.Event;
    protected IServiceProvider ServiceProvider => Context.ServiceProvider;
    protected CancellationToken CancellationToken => Context.CancellationToken;

    protected ContextService(ContextAccessor<IAdapterEventContext<TEvent>> contextAccessor)
    {
        Context = contextAccessor.Context ??
                  throw new InvalidOperationException("Context service was constructed outside of an event context");
    }
}
