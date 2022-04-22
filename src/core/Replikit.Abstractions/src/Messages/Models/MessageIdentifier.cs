using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models;

public readonly record struct MessageIdentifier
{
    public MessageIdentifier(Identifier identifier)
    {
        Identifiers = ImmutableArray.Create(identifier);
    }

    public MessageIdentifier(IEnumerable<Identifier> identifiers)
    {
        Identifiers = ImmutableArray.CreateRange(identifiers);
    }

    public MessageIdentifier(IImmutableList<Identifier> identifiers)
    {
        Identifiers = identifiers;
    }

    public IImmutableList<Identifier> Identifiers { get; }

    public bool Equals(MessageIdentifier other)
    {
        return Identifiers.SequenceEqual(other.Identifiers);
    }

    public override int GetHashCode() => Identifiers.Aggregate(0, HashCode.Combine);

    public static implicit operator MessageIdentifier(Identifier identifier) => new(identifier);
}
