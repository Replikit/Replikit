using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Options;
using Replikit.Adapters.Common.Models;

namespace Replikit.Adapters.Common.Features.Internal;

internal class CommonMessageService : AdapterService, IMessageService
{
    private readonly IMessageService _messageService;
    private readonly IAttachmentCache _attachmentCache;

    public CommonMessageService(IAdapter adapter, IAttachmentCache attachmentCache, IMessageService messageService) :
        base(adapter)
    {
        _messageService = messageService;
        _attachmentCache = attachmentCache;
    }

    public MessageServiceFeatures Features => _messageService.Features;

    public async Task<Message> SendAsync(Identifier channelId, OutMessage message, SendMessageOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        CheckIdentifier(channelId);

        var result = await _messageService.SendAsync(channelId, message, options, cancellationToken);

        await SaveAttachments(result, cancellationToken);
        return UpdateMessage(result, channelId);
    }

    public async Task<Message> EditAsync(Identifier channelId, MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        CheckIdentifier(channelId);
        CheckIdentifier(messageId);

        var result = await _messageService.EditAsync(channelId, messageId, message, oldMessage, cancellationToken);

        await SaveAttachments(result, cancellationToken);
        return UpdateMessage(result, channelId);
    }

    public Task DeleteAsync(Identifier channelId, Identifier messageId,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifier(messageId);

        return _messageService.DeleteAsync(channelId, messageId, cancellationToken);
    }

    public Task DeleteManyAsync(Identifier channelId, IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifiers(messageIds);

        return _messageService.DeleteManyAsync(channelId, messageIds, cancellationToken);
    }

    public Task<Message?> GetAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifier(messageId);

        return _messageService.GetAsync(channelId, messageId, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> GetManyAsync(Identifier channelId,
        IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifiers(messageIds);

        return _messageService.GetManyAsync(channelId, messageIds, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> FindAsync(Identifier channelId, string? query = null, int? take = null,
        int? skip = null,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);

        return _messageService.FindAsync(channelId, query, take, skip, cancellationToken);
    }

    public Task PinAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifier(messageId);

        return _messageService.PinAsync(channelId, messageId, cancellationToken);
    }

    public Task UnpinAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        CheckIdentifier(channelId);
        CheckIdentifier(messageId);

        return _messageService.UnpinAsync(channelId, messageId, cancellationToken);
    }

    private async Task SaveAttachments(Message message, CancellationToken cancellationToken)
    {
        if (message is SentMessage sentMessage)
        {
            await _attachmentCache.SaveAsync(sentMessage.ResolvedAttachments, cancellationToken);
        }
    }

    private Message UpdateMessage(Message message, Identifier channelId)
    {
        var globalChannelId = CreateGlobalIdentifier(channelId);

        return message with
        {
            Id = new GlobalMessageIdentifier(globalChannelId, message.Id.Identifiers),
            AccountId = CreateGlobalIdentifier(Adapter.Id.BotId),
            ChannelId = CreateGlobalIdentifier(channelId)
        };
    }
}
