using Replikit.Abstractions.Repositories.Events;
using Replikit.Abstractions.Repositories.Models;
using Replikit.Core.EntityCollections;
using Replikit.Core.Handlers.Extensions;

namespace Replikit.Core.Handlers;

public abstract class ChannelEventHandler<TEvent> : AdapterEventHandler<TEvent> where TEvent : IChannelEvent
{
    private IMessageCollection? _messageCollection;
    protected IMessageCollection MessageCollection => _messageCollection ??= Context.GetRequiredMessageCollection();

    protected ChannelInfo Channel => Event.Channel;
}
