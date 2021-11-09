using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Options;

namespace Replikit.Core.EntityCollections;

internal class MessageCollection : IMessageCollection
{
    private readonly Identifier _channelId;
    private readonly IMessageService _messageService;

    public MessageCollection(Identifier channelId, IMessageService messageService)
    {
        _channelId = channelId;
        _messageService = messageService;
    }

    public MessageServiceFeatures Features => _messageService.Features;

    public Task<Message> SendAsync(OutMessage message, SendMessageOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return _messageService.SendAsync(_channelId, message, options, cancellationToken);
    }

    public Task<Message> EditAsync(MessageIdentifier messageId, OutMessage message, OutMessage? oldMessage = null,
        CancellationToken cancellationToken = default)
    {
        return _messageService.EditAsync(_channelId, messageId, message, oldMessage, cancellationToken);
    }

    public Task DeleteAsync(Identifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.DeleteAsync(_channelId, messageId, cancellationToken);
    }

    public Task DeleteManyAsync(IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        return _messageService.DeleteManyAsync(_channelId, messageIds, cancellationToken);
    }

    public Task<Message?> GetAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.GetAsync(_channelId, messageId, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> GetManyAsync(IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        return _messageService.GetManyAsync(_channelId, messageIds, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> FindAsync(string? query = null, int? take = null, int? skip = null,
        CancellationToken cancellationToken = default)
    {
        return _messageService.FindAsync(_channelId, query, take, skip, cancellationToken);
    }

    public Task PinAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.PinAsync(_channelId, messageId, cancellationToken);
    }

    public Task UnpinAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        return _messageService.UnpinAsync(_channelId, messageId, cancellationToken);
    }
}
