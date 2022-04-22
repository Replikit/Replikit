using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models;

public readonly record struct GlobalMessageIdentifier(
    GlobalIdentifier ChannelId,
    MessageIdentifier Identifier
) : IEquatable<MessageIdentifier>
{
    public GlobalMessageIdentifier(GlobalIdentifier channelId, Identifier identifier) :
        this(channelId, new MessageIdentifier(identifier)) { }

    public GlobalMessageIdentifier(GlobalIdentifier channelId, IEnumerable<Identifier> identifiers) :
        this(channelId, new MessageIdentifier(identifiers)) { }

    public GlobalMessageIdentifier(GlobalIdentifier channelId, IImmutableList<Identifier> identifiers) :
        this(channelId, new MessageIdentifier(identifiers)) { }

    public static implicit operator MessageIdentifier(GlobalMessageIdentifier identifier) => identifier.Identifier;

    public bool Equals(MessageIdentifier other)
    {
        return Identifier.Equals(other);
    }

    public static bool operator ==(MessageIdentifier first, GlobalMessageIdentifier second)
    {
        return second.Equals(first);
    }

    public static bool operator !=(MessageIdentifier first, GlobalMessageIdentifier second)
    {
        return !second.Equals(first);
    }

    public static bool operator ==(GlobalMessageIdentifier first, MessageIdentifier second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(GlobalMessageIdentifier first, MessageIdentifier second)
    {
        return !first.Equals(second);
    }
}
