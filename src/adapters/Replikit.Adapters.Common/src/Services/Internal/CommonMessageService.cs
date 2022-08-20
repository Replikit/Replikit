using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Common.CustomData;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Services;
using Replikit.Adapters.Common.Models;

namespace Replikit.Adapters.Common.Services.Internal;

internal class CommonMessageService : AdapterService, IMessageService
{
    private readonly IMessageService _messageService;
    private readonly IAttachmentCache _attachmentCache;

    public CommonMessageService(IAdapter adapter, IMessageService messageService, IAttachmentCache attachmentCache) :
        base(adapter)
    {
        _messageService = messageService;
        _attachmentCache = attachmentCache;
    }

    public MessageServiceFeatures Features => _messageService.Features;

    public async Task<Message> SendAsync(Identifier channelId, OutMessage message,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotNull(message);

        var result = await _messageService.SendAsync(channelId, message, cancellationToken);

        return await SaveAttachments(result, cancellationToken);
    }

    public async Task<Message> EditAsync(Identifier channelId, MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotDefault(messageId);
        Check.NotNull(message);

        var result = await _messageService.EditAsync(channelId, messageId, message, oldMessage, cancellationToken);

        return await SaveAttachments(result, cancellationToken);
    }

    public Task DeleteSingleAsync(Identifier channelId, Identifier messagePartId,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotDefault(messagePartId);

        return _messageService.DeleteSingleAsync(channelId, messagePartId, cancellationToken);
    }

    public Task DeleteManyAsync(Identifier channelId, IReadOnlyCollection<Identifier> messagePartIds,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotNull(messagePartIds);

        return _messageService.DeleteManyAsync(channelId, messagePartIds, cancellationToken);
    }

    public Task<Message?> GetAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotDefault(messageId);

        // TODO caching
        return _messageService.GetAsync(channelId, messageId, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> GetManyAsync(Identifier channelId,
        IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotNull(messageIds);

        // TODO caching
        return _messageService.GetManyAsync(channelId, messageIds, cancellationToken);
    }

    public Task PinAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotDefault(messageId);

        return _messageService.PinAsync(channelId, messageId, cancellationToken);
    }

    public Task UnpinAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        Check.NotDefault(channelId);
        Check.NotDefault(messageId);

        return _messageService.UnpinAsync(channelId, messageId, cancellationToken);
    }

    private async Task<Message> SaveAttachments(Message message, CancellationToken cancellationToken)
    {
        if (message.GetCustomDataOrDefault<SentMessageCustomData>() is { } sentMessage)
        {
            await _attachmentCache.SaveAsync(sentMessage.SentAttachments, cancellationToken);
        }

        return message;
    }
}
