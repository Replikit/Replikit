using Microsoft.Extensions.DependencyInjection;
using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Options;
using Replikit.Abstractions.Messages.Services;

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

    public Task<Message> SendAsync(OutMessage message, SendMessageOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return _messageService.SendAsync(ChannelId, message, options, cancellationToken);
    }

    public Task<Message> EditAsync(MessageIdentifier messageId, OutMessage message, OutMessage? oldMessage = null,
        CancellationToken cancellationToken = default)
    {
        return _messageService.EditAsync(ChannelId, messageId, message, oldMessage, cancellationToken);
    }

    public Task DeleteAsync(Identifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.DeleteAsync(ChannelId, messageId, cancellationToken);
    }

    public Task DeleteManyAsync(IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        return _messageService.DeleteManyAsync(ChannelId, messageIds, cancellationToken);
    }

    public Task<Message?> GetAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.GetAsync(ChannelId, messageId, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> GetManyAsync(IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        return _messageService.GetManyAsync(ChannelId, messageIds, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> FindAsync(string? query = null, int? take = null, int? skip = null,
        CancellationToken cancellationToken = default)
    {
        return _messageService.FindAsync(ChannelId, query, take, skip, cancellationToken);
    }

    public Task PinAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.PinAsync(ChannelId, messageId, cancellationToken);
    }

    public Task UnpinAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.UnpinAsync(ChannelId, messageId, cancellationToken);
    }

    public static IMessageCollection Create(GlobalIdentifier channelId, IServiceProvider serviceProvider)
    {
        var adapterCollection = serviceProvider.GetRequiredService<IAdapterCollection>();
        var adapter = adapterCollection.ResolveRequired(channelId);

        return new MessageCollection(channelId, adapter.MessageService);
    }
}
