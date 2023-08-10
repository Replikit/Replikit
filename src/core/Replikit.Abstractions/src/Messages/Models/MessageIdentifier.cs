using System.Collections.Immutable;
using Replikit.Abstractions.Common.Models;

// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// The compound identifier of the message.
/// <br/>
/// Depending on the message and the platform each Replikit message can consists of multiple messages called parts.
/// This identifier is consists of all parts identifiers.
/// </summary>
public readonly record struct MessageIdentifier
{
    /// <summary>
    /// Creates a new instance of <see cref="MessageIdentifier"/> with
    /// the identifier of the single part (message itself).
    /// </summary>
    /// <param name="partIdentifier">An identifier of the message part.</param>
    public MessageIdentifier(Identifier partIdentifier)
    {
        ArgumentNullException.ThrowIfNull(partIdentifier);

        PartIdentifiers = ImmutableArray.Create(partIdentifier);
    }

    public MessageIdentifier(IEnumerable<Identifier> partIdentifiers)
    {
        ArgumentNullException.ThrowIfNull(partIdentifiers);

        PartIdentifiers = ImmutableArray.CreateRange(partIdentifiers);
    }

    /// <summary>
    /// The identifiers of the message parts.
    /// <br/>
    /// If the message is not split into parts, the collection contains only
    /// one element - the identifier of the message itself.
    /// </summary>
    public IReadOnlyList<Identifier> PartIdentifiers { get; }

    public bool Equals(MessageIdentifier other)
    {
        if (PartIdentifiers is null)
        {
            return other.PartIdentifiers is null;
        }

        if (other.PartIdentifiers is null)
        {
            return PartIdentifiers is null;
        }

        return PartIdentifiers.SequenceEqual(other.PartIdentifiers);
    }

    public override int GetHashCode()
    {
        return PartIdentifiers.Aggregate(0, HashCode.Combine);
    }

    /// <summary>
    /// Creates a new instance of <see cref="MessageIdentifier"/> with the identifier of the single part (message itself).
    /// </summary>
    /// <param name="partIdentifier">An identifier of the message part.</param>
    /// <returns>A new instance of <see cref="MessageIdentifier"/>.</returns>
    public static implicit operator MessageIdentifier(Identifier partIdentifier) => new(partIdentifier);

    /// <summary>
    /// Extracts the single part identifier from the compound identifier.
    /// </summary>
    /// <param name="identifier">The compound identifier.</param>
    /// <returns>The identifier of the single part.</returns>
    public static implicit operator Identifier(MessageIdentifier identifier) => identifier.PartIdentifiers[0];
}
