using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Handlers.Extensions;

public static class ChannelContextExtensions
{
    public static IMessageCollection GetMessageCollection<TEvent>(this IEventContext<TEvent> context)
        where TEvent : IChannelEvent
    {
        ArgumentNullException.ThrowIfNull(context);

        return new MessageCollection(context.Event.Channel.Id, context.Adapter.MessageService);
    }
}
