using Replikit.Abstractions.Repositories.Events;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Core.Handlers;

public abstract class ChannelAdapterEventHandler<TEvent> : AdapterEventHandler<TEvent> where TEvent : IChannelEvent
{
    protected IMessageCollection MessageCollection => Context.GetMessageCollection();
    protected ChannelInfo Channel => Event.Channel;
}
