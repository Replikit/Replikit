using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models;

public readonly record struct ChannelMessageIdentifier(
    Identifier ChannelId,
    MessageIdentifier Value
)
{
    public ChannelMessageIdentifier(Identifier channelId, Identifier value) :
        this(channelId, new MessageIdentifier(value)) { }

    public ChannelMessageIdentifier(Identifier channelId, IEnumerable<Identifier> identifiers) :
        this(channelId, new MessageIdentifier(identifiers)) { }

    public ChannelMessageIdentifier(Identifier channelId, IImmutableList<Identifier> identifiers) :
        this(channelId, new MessageIdentifier(identifiers)) { }

    public static implicit operator MessageIdentifier(ChannelMessageIdentifier identifier) => identifier.Value;
}
