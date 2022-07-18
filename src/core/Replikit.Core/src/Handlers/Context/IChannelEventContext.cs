using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Handlers.Context;

public interface IChannelEventContext<out TEvent> : IAdapterEventContext<TEvent> where TEvent : IChannelEvent
{
    IMessageCollection MessageCollection { get; }
}
