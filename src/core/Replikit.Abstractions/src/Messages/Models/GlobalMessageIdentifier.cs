using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models;

public record GlobalMessageIdentifier : MessageIdentifier
{
    public GlobalMessageIdentifier(GlobalIdentifier channelId, Identifier identifier) : base(identifier)
    {
        ChannelId = channelId;
    }

    public GlobalMessageIdentifier(GlobalIdentifier channelId, IEnumerable<Identifier> identifiers) :
        base(identifiers)
    {
        ChannelId = channelId;
    }

    public GlobalMessageIdentifier(GlobalIdentifier channelId, IImmutableList<Identifier> identifiers) :
        base(identifiers)
    {
        ChannelId = channelId;
    }

    public GlobalIdentifier ChannelId { get; }

    public virtual bool Equals(GlobalMessageIdentifier? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(ChannelId, other.ChannelId) && Identifiers.SequenceEqual(other.Identifiers);
    }

    public override int GetHashCode()
    {
        return Identifiers.Aggregate(HashCode.Combine(ChannelId), HashCode.Combine);
    }
}
