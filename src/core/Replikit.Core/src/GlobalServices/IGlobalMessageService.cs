using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Options;
using Replikit.Abstractions.Messages.Services;

namespace Replikit.Core.GlobalServices;

public interface IGlobalMessageService : IGlobalHasFeatures<MessageServiceFeatures>
{
    /// <inheritdoc cref="IMessageService.SendAsync" />
    Task<Message> SendAsync(GlobalIdentifier channelId, OutMessage message, SendMessageOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMessageService.EditAsync" />
    Task<Message> EditAsync(GlobalIdentifier channelId, MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMessageService.DeleteAsync" />
    Task DeleteAsync(GlobalIdentifier channelId, Identifier messageId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMessageService.DeleteManyAsync" />
    Task DeleteManyAsync(GlobalIdentifier channelId, IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMessageService.GetAsync" />
    Task<Message?> GetAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMessageService.GetManyAsync" />
    Task<IReadOnlyList<Message>> GetManyAsync(GlobalIdentifier channelId,
        IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMessageService.FindAsync" />
    Task<IReadOnlyList<Message>> FindAsync(GlobalIdentifier channelId, string? query = null, int? take = null,
        int? skip = null, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMessageService.PinAsync" />
    Task PinAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default);

    /// <inheritdoc cref="IMessageService.UnpinAsync" />
    Task UnpinAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default);
}
