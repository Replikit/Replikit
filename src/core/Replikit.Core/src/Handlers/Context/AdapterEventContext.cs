using Kantaiko.Routing.Events;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;

namespace Replikit.Core.Handlers.Context;

internal class AdapterEventContext<TEvent> : AsyncEventContext<TEvent>, IAdapterEventContext<TEvent>
    where TEvent : IAdapterEvent
{
    public AdapterEventContext(TEvent @event,
        IAdapter adapter,
        IServiceProvider? serviceProvider = null,
        CancellationToken cancellationToken = default
    ) : base(@event, serviceProvider, cancellationToken)
    {
        Adapter = adapter;
    }

    public IAdapter Adapter { get; }
}
