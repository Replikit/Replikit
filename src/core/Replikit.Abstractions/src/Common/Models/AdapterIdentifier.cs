namespace Replikit.Abstractions.Common.Models;

public sealed record AdapterIdentifier(string Type, Identifier BotId)
{
    public static implicit operator string(AdapterIdentifier identifier) => identifier.ToString();

    public override string ToString() => $"{Type}:{BotId}";
}
