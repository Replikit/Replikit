using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models;

public record MessageIdentifier
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

    public virtual bool Equals(MessageIdentifier? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Identifiers.SequenceEqual(other.Identifiers);
    }

    public override int GetHashCode() => HashCode.Combine(0, Identifiers);

    public static implicit operator MessageIdentifier(Identifier identifier) => new(identifier);
}
