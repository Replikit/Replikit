using Kantaiko.Properties;
using Kantaiko.Routing.Context;
using Kantaiko.Routing.Events;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Services;

public abstract class ContextService : ContextService<IEvent>
{
    protected ContextService(IContextAccessor<IEventContext<IEvent>> contextAccessor) : base(contextAccessor) { }
}

public abstract class ContextService<TEvent> where TEvent : IEvent
{
    protected IEventContext<TEvent> Context { get; }

    protected TEvent Event => Context.Event;
    protected CancellationToken CancellationToken => Context.CancellationToken;
    protected IReadOnlyPropertyCollection Properties => Context.Properties;

    protected ContextService(IContextAccessor<IEventContext<TEvent>> contextAccessor)
    {
        Context = contextAccessor.Context ??
                  throw new InvalidOperationException("Context service was constructed outside of an event context");
    }
}
