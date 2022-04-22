using Replikit.Abstractions.Common.Exceptions;
using Replikit.Abstractions.Common.Features;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Services;

public static class MessageServiceExtensions
{
    /// <summary>
    /// Deletes many messages. Tries to use DeleteMany method, but if not supported, falls back to Delete.
    /// Throws an <see cref="UnsupportedFeatureException"/> if both methods are not supported.
    /// </summary>
    /// <param name="messageService"></param>
    /// <param name="channelId"></param>
    /// <param name="messageIdentifiers"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public static Task DeleteManyUnoptimized(this IMessageService messageService, Identifier channelId,
        IReadOnlyCollection<MessageIdentifier> messageIdentifiers, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(messageIdentifiers);

        if (messageService.Supports(MessageServiceFeatures.DeleteMany))
        {
            return messageService.DeleteManyAsync(channelId, messageIdentifiers, cancellationToken);
        }

        var identifiers = messageIdentifiers.SelectMany(x => x.Identifiers);
        return Task.WhenAll(identifiers.Select(x => messageService.DeleteAsync(channelId, x, cancellationToken)));
    }
}
