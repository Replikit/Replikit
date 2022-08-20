using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Services;

namespace Replikit.Core.EntityCollections;

public interface IMessageCollection : IHasFeatures<MessageServiceFeatures>
{
    GlobalIdentifier ChannelId { get; }

    /// <inheritdoc cref="IMessageService.SendAsync"/>
    Task<Message> SendAsync(OutMessage message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Edits message with specified identifier, replacing it's content with specified message.
    /// Note, that depending on the adapter and specified message, it might be partially edited.
    /// You can pass an <paramref name="oldMessage"/> parameter to help adapter decide which real messages should be edited.
    /// Otherwise, it will have to try to edit all messages or additionally fetch messages.
    /// Also note, that in some adapters message attachments can't be replaced with attachments of another type.
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="message"></param>
    /// <param name="oldMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Message> EditAsync(MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes message with specified identifier.
    /// Note, that depending on the adapter and specified message, it might delete multiple real messages.
    /// </summary>
    /// <param name="messagePartId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteSingleAsync(Identifier messagePartId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple messages with specified identifiers as quickly as possible.
    /// Note, that depending on the adapter and specified message, it might delete more real messages than ids specified.
    /// </summary>
    /// <param name="messagePartIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteManyAsync(IReadOnlyCollection<Identifier> messagePartIds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds message with specified identifier.
    /// Returns null if message could not be found.
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Message?> GetAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds multiple messages with specified identifiers.
    /// Can return less messages that ids specified, if some messages could not be found.
    /// </summary>
    /// <param name="messageIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Message>> GetManyAsync(IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a message with specified identifier to the list of pinned messages in a chat.
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PinAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a message with specified identifier from the list of pinned messages in a chat.
    /// </summary>
    /// <param name="messageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UnpinAsync(MessageIdentifier messageId, CancellationToken cancellationToken = default);
}
