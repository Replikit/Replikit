namespace Replikit.Abstractions.Common.Models;

public readonly record struct GlobalIdentifier(
    AdapterIdentifier AdapterId,
    Identifier Identifier
) : IEquatable<Identifier>
{
    public static implicit operator Identifier(GlobalIdentifier identifier) => identifier.Identifier;

    public static implicit operator long(GlobalIdentifier identifier) => identifier.Identifier;
    public static implicit operator int(GlobalIdentifier identifier) => identifier.Identifier;
    public static implicit operator string(GlobalIdentifier identifier) => identifier.Identifier;
    public static implicit operator Guid(GlobalIdentifier identifier) => identifier.Identifier;

    public bool Equals(Identifier other)
    {
        return Identifier.Equals(other);
    }

    public static bool operator ==(Identifier first, GlobalIdentifier second)
    {
        return second.Equals(first);
    }

    public static bool operator !=(Identifier first, GlobalIdentifier second)
    {
        return !second.Equals(first);
    }

    public static bool operator ==(GlobalIdentifier first, Identifier second)
    {
        return first.Equals(second);
    }

    public static bool operator !=(GlobalIdentifier first, Identifier second)
    {
        return !first.Equals(second);
    }
}
