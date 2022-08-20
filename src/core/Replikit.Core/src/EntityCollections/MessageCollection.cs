using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Channels.Events;
using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Events;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Services;
using Replikit.Core.Routing.Context;

namespace Replikit.Core.EntityCollections;

public class MessageCollection : IMessageCollection
{
    private readonly IMessageService _messageService;

    public MessageCollection(GlobalIdentifier channelId, IMessageService messageService)
    {
        ChannelId = channelId;
        _messageService = messageService;
    }

    public GlobalIdentifier ChannelId { get; }

    public MessageServiceFeatures Features => _messageService.Features;

    public Task<Message> SendAsync(OutMessage message, CancellationToken cancellationToken = default)
    {
        return _messageService.SendAsync(ChannelId.Value, message, cancellationToken);
    }

    public Task<Message> EditAsync(MessageIdentifier messageId, OutMessage message, OutMessage? oldMessage = null,
        CancellationToken cancellationToken = default)
    {
        return _messageService.EditAsync(ChannelId.Value, messageId, message, oldMessage, cancellationToken);
    }

    public Task DeleteSingleAsync(Identifier messagePartId, CancellationToken cancellationToken = default)
    {
        return _messageService.DeleteAsync(ChannelId.Value, messagePartId, cancellationToken);
    }

    public Task DeleteManyAsync(IReadOnlyCollection<Identifier> messagePartIds,
        CancellationToken cancellationToken = default)
    {
        return _messageService.DeleteManyAsync(ChannelId.Value, messagePartIds, cancellationToken);
    }

    public Task<Message?> GetAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.GetAsync(ChannelId.Value, messageId, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> GetManyAsync(IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        return _messageService.GetManyAsync(ChannelId.Value, messageIds, cancellationToken);
    }

    public Task PinAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.PinAsync(ChannelId.Value, messageId, cancellationToken);
    }

    public Task UnpinAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.UnpinAsync(ChannelId.Value, messageId, cancellationToken);
    }

    internal static MessageCollection Create(IAdapterEventContext<IBotEvent>? context)
    {
        if (context is null)
        {
            throw new ReplikitException("Message collection can be accessed only inside adapter event context.");
        }

        if (context.Event is not IChannelEvent channelEvent)
        {
            throw new ReplikitException("Message collection can be accessed only for channel events.");
        }

        if (!context.Adapter.TryGetService<IMessageService>(out var messageCollection))
        {
            throw new ReplikitException(
                "Message collection can be accessed only for adapters supporting message service.");
        }

        return new MessageCollection(channelEvent.Channel.Id, messageCollection);
    }
}
