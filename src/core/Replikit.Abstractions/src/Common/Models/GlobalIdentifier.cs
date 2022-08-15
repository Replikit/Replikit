namespace Replikit.Abstractions.Common.Models;

public readonly record struct GlobalIdentifier(
    AdapterIdentifier AdapterId,
    Identifier Value
)
{
    public static implicit operator Identifier(GlobalIdentifier identifier) => identifier.Value;
}
