using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Services;

/// <summary>
/// The service that provides methods to work with messages.
/// </summary>
public interface IMessageService : IHasFeatures<MessageServiceFeatures>
{
    /// <summary>
    /// Sends a message.
    /// <br/>
    /// Note, that depending on the adapter and specified message, it might be split into multiple real messages.
    /// </summary>
    /// <param name="channelId">An identifier of the channel to send the message to.</param>
    /// <param name="message">A message to send.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Message"/> that represents the sent message.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<Message> SendAsync(Identifier channelId, OutMessage message, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Send);
    }

    /// <summary>
    /// Edits message with specified identifier, replacing it's content with specified message.
    /// <br/>
    /// Note, that depending on the adapter and specified message, it might be partially edited.
    /// <br/>
    /// You can pass an <paramref name="oldMessage"/> parameter to help adapter decide which real messages should be edited.
    /// Otherwise, it will have to try to edit all messages or additionally fetch messages.
    /// <br/>
    /// Also note, that in some adapters message attachments can't be replaced with attachments of another type.
    /// </summary>
    /// <param name="channelId">An identifier of the channel to edit the message in.</param>
    /// <param name="messageId">An identifier of the message to edit.</param>
    /// <param name="message">A message to replace the old message with.</param>
    /// <param name="oldMessage">An old message to help adapter decide which real message should be edited.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Message"/> that represents the edited message.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<Message> EditAsync(Identifier channelId, MessageIdentifier messageId, OutMessage message,
        OutMessage? oldMessage = null, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Edit);
    }

    /// <summary>
    /// Deletes the single message part with the specified identifier.
    /// </summary>
    /// <param name="channelId">An identifier of the channel to delete the message from.</param>
    /// <param name="messagePartId">An identifier of the message part to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task DeleteSingleAsync(Identifier channelId, Identifier messagePartId,
        CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.DeleteSingle);
    }

    /// <summary>
    /// Deletes multiple message part with the specified identifiers.
    /// </summary>
    /// <param name="channelId">An identifier of the channel to delete the message from.</param>
    /// <param name="messagePartIds">An identifiers of the message parts to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task DeleteManyAsync(Identifier channelId, IReadOnlyCollection<Identifier> messagePartIds,
        CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.DeleteMany);
    }

    /// <summary>
    /// Gets the message with specified identifier.
    /// </summary>
    /// <param name="channelId">An identifier of the channel to get the message from.</param>
    /// <param name="messageId">An identifier of the message to get.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="Message"/> that represents the message or null if message was not found.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<Message?> GetAsync(Identifier channelId, MessageIdentifier messageId,
        CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Get);
    }

    /// <summary>
    /// Gets multiple messages with specified identifiers.
    /// <br/>
    /// Can return less messages that ids specified, if some messages could not be found.
    /// </summary>
    /// <param name="channelId">An identifier of the channel to get the messages from.</param>
    /// <param name="messageIds">An identifiers of the messages to get.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a collection of <see cref="Message"/> that represents the found messages.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task<IReadOnlyList<Message>> GetManyAsync(Identifier channelId, IReadOnlyCollection<MessageIdentifier> messageIds,
        CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.GetMany);
    }

    /// <summary>
    /// Pins the message with specified identifier in the channel.
    /// </summary>
    /// <param name="channelId">An identifier of the channel to pin the message in.</param>
    /// <param name="messageId">An identifier of the message to pin.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task PinAsync(Identifier channelId, MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Pin);
    }

    /// <summary>
    /// Unpins the message with specified identifier in the channel.
    /// </summary>
    /// <param name="channelId">An identifier of the channel to unpin the message in.</param>
    /// <param name="messageId">An identifier of the message to unpin.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The method is not supported by the service implementation.
    /// </exception>
    Task UnpinAsync(Identifier channelId, MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        throw HasFeaturesHelper.CreateUnsupportedException(this, MessageServiceFeatures.Unpin);
    }
}
