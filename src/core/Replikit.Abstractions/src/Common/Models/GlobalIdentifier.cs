using System.Diagnostics;

namespace Replikit.Abstractions.Common.Models;

[DebuggerDisplay("[Value = {Value} AdapterId = {AdapterId}]")]
public record GlobalIdentifier : Identifier
{
    public AdapterIdentifier AdapterId { get; }

    public GlobalIdentifier(Identifier id, AdapterIdentifier adapterId) : base(id) => AdapterId = adapterId;

    public static bool operator ==(GlobalIdentifier? globalIdentifier, Identifier? identifier)
    {
        return Equals(identifier?.Value, globalIdentifier?.Value);
    }

    public static bool operator !=(GlobalIdentifier? globalIdentifier, Identifier? identifier)
    {
        return !Equals(identifier?.Value, globalIdentifier?.Value);
    }

    public static bool operator ==(Identifier? identifier, GlobalIdentifier? globalIdentifier)
    {
        return Equals(identifier?.Value, globalIdentifier?.Value);
    }

    public static bool operator !=(Identifier? identifier, GlobalIdentifier? globalIdentifier)
    {
        return !Equals(identifier?.Value, globalIdentifier?.Value);
    }
}
