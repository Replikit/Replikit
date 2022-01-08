using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Options;

namespace Replikit.Core.GlobalServices;

public interface IGlobalMessageService : IGlobalHasFeatures<MessageServiceFeatures>
{
    /// <summary>
    /// Sends message to collection.
    /// Note, that depending on the adapter and specified message, it might be split into multiple real messages.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="message"></param>
    /// <param name="options"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Message> SendAsync(GlobalIdentifier channelId, OutMessage message, SendMessageOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Edits message with specified identifier, replacing it's content with specified message.
    /// Note, that depending on the adapter and specified message, it might be partially edited.
    /// You can pass an <paramref name="oldMessage"/> parameter to help adapter decide which real messages should be edited.
    /// Otherwise, it will have to try to edit all messages or additionally fetch messages.
    /// Also note, that in some adapters message attachments can't be replaced with attachments of another type.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="messageId"></param>
    /// <param name="message"></param>
    /// <param name="oldMessage"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Message> EditAsync(GlobalIdentifier channelId, MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes message with specified identifier.
    /// Note, that depending on the adapter and specified message, it might delete multiple real messages.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="messageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(GlobalIdentifier channelId, Identifier messageId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes multiple messages with specified identifiers as quickly as possible.
    /// Note, that depending on the adapter and specified message, it might delete more real messages than ids specified.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="messageIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteManyAsync(GlobalIdentifier channelId, IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds message with specified identifier.
    /// Returns null if message could not be found.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="messageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Message?> GetAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds multiple messages with specified identifiers.
    /// Can return less messages that ids specified, if some messages could not be found.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="messageIds"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Message>> GetManyAsync(GlobalIdentifier channelId,
        IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds multiple messages matching specified condition.
    /// Returns as many messages as could find according to specified take and skip options.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="query"></param>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IReadOnlyList<Message>> FindAsync(GlobalIdentifier channelId, string? query = null, int? take = null,
        int? skip = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a message with specified identifier to the list of pinned messages in a chat.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="messageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PinAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a message with specified identifier from the list of pinned messages in a chat.
    /// </summary>
    /// <param name="channelId"></param>
    /// <param name="messageId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UnpinAsync(GlobalIdentifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default);
}