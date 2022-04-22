using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.Options;

namespace Replikit.Abstractions.Messages.Services;

/// <summary>
/// Provides methods to work with messages.
/// </summary>
public interface IMessageService : IHasFeatures<MessageServiceFeatures>
{
    /// <summary>
    /// Sends a message.
    /// Note, that depending on the adapter and specified message, it might be split into multiple real messages.
    /// </summary>
    Task<Message> SendAsync(Identifier channelId, OutMessage message, SendMessageOptions? options = null,
        CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Send);

    /// <summary>
    /// Edits message with specified identifier, replacing it's content with specified message.
    /// Note, that depending on the adapter and specified message, it might be partially edited.
    /// You can pass an <paramref name="oldMessage"/> parameter to help adapter decide which real messages should be edited.
    /// Otherwise, it will have to try to edit all messages or additionally fetch messages.
    /// Also note, that in some adapters message attachments can't be replaced with attachments of another type.
    /// </summary>
    Task<Message> EditAsync(Identifier channelId, MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage = null, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Edit);

    /// <summary>
    /// Deletes message with specified identifier.
    /// Note, that depending on the adapter and specified message, it might delete multiple real messages.
    /// </summary>
    Task DeleteAsync(Identifier channelId, Identifier messageId,
        CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Delete);

    /// <summary>
    /// Deletes multiple messages with specified identifiers as quickly as possible.
    /// Note, that depending on the adapter and specified message, it might delete more real messages than ids specified.
    /// </summary>
    Task DeleteManyAsync(Identifier channelId, IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.DeleteMany);

    /// <summary>
    /// Finds message with specified identifier.
    /// Returns null if message could not be found.
    /// </summary>
    Task<Message?> GetAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Get);

    /// <summary>
    /// Finds multiple messages with specified identifiers.
    /// Can return less messages that ids specified, if some messages could not be found.
    /// </summary>
    Task<IReadOnlyList<Message>> GetManyAsync(Identifier channelId, IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.GetMany);

    /// <summary>
    /// Finds multiple messages matching specified condition.
    /// Returns as many messages as could find according to specified take and skip options.
    /// </summary>
    Task<IReadOnlyList<Message>> FindAsync(Identifier channelId, string? query = null, int? take = null,
        int? skip = null, CancellationToken cancellationToken = default) =>
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Find);

    /// <summary>
    /// Adds a message with specified identifier to the list of pinned messages in a chat.
    /// </summary>
    Task PinAsync(Identifier channelId, MessageIdentifier messageId, CancellationToken cancellationToken = default)
        => throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Pin);

    /// <summary>
    /// Removes a message with specified identifier from the list of pinned messages in a chat.
    /// </summary>
    Task UnpinAsync(Identifier channelId, MessageIdentifier messageId, CancellationToken cancellationToken = default)
        => throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Unpin);

    /// <summary>
    /// Answers to pressing of an inline button.
    /// </summary>
    public Task AnswerInlineButtonRequestAsync(Identifier requestId, string message,
        CancellationToken cancellationToken = default)
        => throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.AnswerInlineButtonRequest);
}
