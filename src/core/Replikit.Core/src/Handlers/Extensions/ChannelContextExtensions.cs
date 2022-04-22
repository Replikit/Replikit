using Kantaiko.Routing.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Handlers.Extensions;

public static class ChannelContextExtensions
{
    public static IMessageCollection GetRequiredMessageCollection<TEvent>(this IEventContext<TEvent> context)
        where TEvent : IChannelEvent
    {
        var adapter = context.GetRequiredAdapter();

        return new MessageCollection(context.Event.Channel.Id, adapter.MessageService);
    }
}
