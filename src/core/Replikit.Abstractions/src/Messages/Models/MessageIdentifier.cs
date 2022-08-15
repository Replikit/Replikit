using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// The identifier of the message.
/// Consists of part message identifiers.
/// </summary>
public readonly record struct MessageIdentifier
{
    public MessageIdentifier(Identifier partIdentifier)
    {
        PartIdentifiers = ImmutableArray.Create(partIdentifier);
    }

    public MessageIdentifier(IEnumerable<Identifier> partIdentifiers)
    {
        PartIdentifiers = ImmutableArray.CreateRange(partIdentifiers);
    }

    public MessageIdentifier(IImmutableList<Identifier> partIdentifiers)
    {
        PartIdentifiers = partIdentifiers;
    }

    /// <summary>
    /// The part identifiers of the message.
    /// <br/>
    /// Depending on the adapter, it may contain either one identifier or several.
    /// </summary>
    public IReadOnlyList<Identifier> PartIdentifiers { get; }

    public bool Equals(MessageIdentifier other)
    {
        return PartIdentifiers.SequenceEqual(other.PartIdentifiers);
    }

    public override int GetHashCode() => PartIdentifiers.Aggregate(0, HashCode.Combine);

    public static implicit operator MessageIdentifier(Identifier partIdentifier) => new(partIdentifier);
}
