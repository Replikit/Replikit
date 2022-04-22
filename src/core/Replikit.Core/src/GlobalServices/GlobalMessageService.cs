using Replikit.Abstractions.Adapters;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Options;
using Replikit.Abstractions.Messages.Services;

namespace Replikit.Core.GlobalServices;

internal class GlobalMessageService : IGlobalMessageService
{
    private readonly IAdapterCollection _adapterCollection;

    public GlobalMessageService(IAdapterCollection adapterCollection)
    {
        _adapterCollection = adapterCollection;
    }

    public MessageServiceFeatures GetFeatures(AdapterIdentifier adapterId)
    {
        return _adapterCollection.ResolveRequired(adapterId).MessageService.Features;
    }

    private IMessageService ResolveMessageService(GlobalIdentifier channelId)
    {
        return _adapterCollection.ResolveRequired(channelId).MessageService;
    }

    public Task<Message> SendAsync(GlobalIdentifier channelId, OutMessage message, SendMessageOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).SendAsync(channelId, message, options, cancellationToken);
    }

    public Task<Message> EditAsync(GlobalIdentifier channelId, MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage = null, CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).EditAsync(channelId, messageId, message, oldMessage, cancellationToken);
    }

    public Task DeleteAsync(GlobalIdentifier channelId, Identifier messageId,
        CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).DeleteAsync(channelId, messageId, cancellationToken);
    }

    public Task DeleteManyAsync(GlobalIdentifier channelId, IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).DeleteManyAsync(channelId, messageIds, cancellationToken);
    }

    public Task<Message?> GetAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).GetAsync(channelId, messageId, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> GetManyAsync(GlobalIdentifier channelId,
        IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).GetManyAsync(channelId, messageIds, cancellationToken);
    }

    public Task<IReadOnlyList<Message>> FindAsync(GlobalIdentifier channelId, string? query = null, int? take = null,
        int? skip = null,
        CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).FindAsync(channelId, query, take, skip, cancellationToken);
    }

    public Task PinAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).PinAsync(channelId, messageId, cancellationToken);
    }

    public Task UnpinAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        return ResolveMessageService(channelId).UnpinAsync(channelId, messageId, cancellationToken);
    }
}
