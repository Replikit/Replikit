using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// The global identifier of the message.
/// Consists of the global channel identifier and the message identifier.
/// </summary>
public readonly record struct GlobalMessageIdentifier(
    GlobalIdentifier ChannelId,
    MessageIdentifier Value
)
{
    public GlobalMessageIdentifier(GlobalIdentifier channelId, Identifier value) :
        this(channelId, new MessageIdentifier(value)) { }

    public GlobalMessageIdentifier(GlobalIdentifier channelId, IEnumerable<Identifier> identifiers) :
        this(channelId, new MessageIdentifier(identifiers)) { }

    public GlobalMessageIdentifier(GlobalIdentifier channelId, IImmutableList<Identifier> identifiers) :
        this(channelId, new MessageIdentifier(identifiers)) { }

    public static implicit operator MessageIdentifier(GlobalMessageIdentifier identifier) => identifier.Value;
}
