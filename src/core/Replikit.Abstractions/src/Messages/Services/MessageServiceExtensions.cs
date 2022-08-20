using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Services;

/// <summary>
/// The extensions of <see cref="IMessageService"/>.
/// </summary>
public static class MessageServiceExtensions
{
    /// <summary>
    /// Deletes a message.
    /// <br/>
    /// If the message consists of a single part, the <see cref="IMessageService.DeleteSingleAsync"/> method is used.
    /// <br/>
    /// Otherwise, this method tries to use <see cref="IMessageService.DeleteManyAsync"/> and if it is not supported,
    /// it falls back to multiple calls of the <see cref="IMessageService.DeleteSingleAsync"/> method.
    /// </summary>
    /// <param name="messageService">The message service.</param>
    /// <param name="channelId">An identifier of the channel to delete the message from.</param>
    /// <param name="messageId">An identifier of the message to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The both <see cref="IMessageService.DeleteSingleAsync"/> and <see cref="IMessageService.DeleteManyAsync"/>
    /// methods are not supported by the service implementation.
    /// </exception>
    public static async Task DeleteAsync(this IMessageService messageService, Identifier channelId,
        MessageIdentifier messageId, CancellationToken cancellationToken = default)
    {
        Check.NotNull(messageService);
        Check.NotDefault(messageId);

        if (messageId.PartIdentifiers.Count == 1)
        {
            await messageService.DeleteSingleAsync(channelId, messageId.PartIdentifiers[0], cancellationToken);
            return;
        }

        if (messageService.Supports(MessageServiceFeatures.DeleteMany))
        {
            await messageService.DeleteManyAsync(channelId, messageId.PartIdentifiers, cancellationToken);
            return;
        }

        foreach (var partIdentifier in messageId.PartIdentifiers)
        {
            await messageService.DeleteSingleAsync(channelId, partIdentifier, cancellationToken);
        }
    }

    /// <summary>
    /// Deletes many messages.
    /// <br/>
    /// Tries to use <see cref="IMessageService.DeleteManyAsync"/> and if it is not supported,
    /// it falls back to multiple calls of the <see cref="IMessageService.DeleteSingleAsync"/> method.
    /// </summary>
    /// <param name="messageService">The message service.</param>
    /// <param name="channelId">An identifier of the channel to delete the message from.</param>
    /// <param name="messageIds">An identifiers of the messages to delete.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken"/> to cancel the operation.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    /// <exception cref="UnsupportedFeatureException">
    /// The both <see cref="IMessageService.DeleteSingleAsync"/> and <see cref="IMessageService.DeleteManyAsync"/>
    /// methods are not supported by the service implementation.
    /// </exception>
    public static async Task DeleteMany(this IMessageService messageService, Identifier channelId,
        IReadOnlyCollection<MessageIdentifier> messageIds, CancellationToken cancellationToken = default)
    {
        Check.NotNull(messageService);
        Check.NotNull(messageIds);

        var messagePartIds = messageIds.SelectMany(messageId => messageId.PartIdentifiers).ToArray();

        if (messageService.Supports(MessageServiceFeatures.DeleteMany))
        {
            await messageService.DeleteManyAsync(channelId, messagePartIds, cancellationToken);
            return;
        }

        foreach (var messagePartId in messagePartIds)
        {
            await messageService.DeleteSingleAsync(channelId, messagePartId, cancellationToken);
        }
    }
}
