using Kantaiko.Routing.Events;
using Replikit.Abstractions.Repositories.Events;
using Replikit.Core.EntityCollections;

namespace Replikit.Core.Handlers.Extensions;

public static class ChannelContextExtensions
{
    public static IMessageCollection GetMessageCollection<TEvent>(this IEventContext<TEvent> context)
        where TEvent : IChannelEvent
    {
        ArgumentNullException.ThrowIfNull(context);

        if (AdapterEventProperties.Of(context)?.Adapter is not { } adapter)
        {
            throw new InvalidOperationException("Failed to access adapter instance");
        }

        return new MessageCollection(context.Event.Channel.Id, adapter.MessageService);
    }
}
