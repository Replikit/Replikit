using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Handlers.Context;

internal class ChannelEventContext<TEvent> : AdapterEventContext<TEvent>, IChannelEventContext<TEvent>
    where TEvent : IChannelEvent
{
    public ChannelEventContext(
        TEvent @event,
        IAdapter adapter,
        IServiceProvider? serviceProvider = null,
        CancellationToken cancellationToken = default
    ) : base(@event, adapter, serviceProvider, cancellationToken)
    {
        MessageCollection = new MessageCollection(@event.Channel.Id, adapter.MessageService);
    }

    public IMessageCollection MessageCollection { get; }
}
