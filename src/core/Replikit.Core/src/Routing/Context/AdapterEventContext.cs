using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Events;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Routing.Context;

internal class AdapterEventContext<TEvent> : IAdapterEventContext<TEvent>
    where TEvent : IBotEvent
{
    public AdapterEventContext(TEvent @event, IAdapter adapter, IServiceProvider serviceProvider,
        CancellationToken cancellationToken)
    {
        Event = @event;
        Adapter = adapter;
        ServiceProvider = serviceProvider;
        CancellationToken = cancellationToken;
    }

    public TEvent Event { get; }
    public IAdapter Adapter { get; }
    public IServiceProvider ServiceProvider { get; }
    public CancellationToken CancellationToken { get; }

    public IMessageCollection MessageCollection => ServiceProvider.GetRequiredService<IMessageCollection>();
    public bool IsHandled { get; set; }
}
