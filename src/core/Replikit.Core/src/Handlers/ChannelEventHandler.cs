using Replikit.Abstractions.Repositories.Events;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers.Context;

namespace Replikit.Core.Handlers;

public abstract class ChannelEventHandler<TEvent> : AdapterEventHandler<TEvent, IChannelEventContext<TEvent>>
    where TEvent : IChannelEvent
{
    protected IMessageCollection MessageCollection => new MessageCollection(Channel.Id, Adapter.MessageService);

    protected ChannelInfo Channel => Event.Channel;
}
