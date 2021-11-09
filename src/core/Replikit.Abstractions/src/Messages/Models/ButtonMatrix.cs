namespace Replikit.Abstractions.Messages.Models;

public sealed record ButtonMatrix<TButton>(IReadOnlyList<IReadOnlyList<TButton>> Rows)
{
    public static ButtonMatrix<TButton> Empty { get; } = new(Array.Empty<IReadOnlyList<TButton>>());
}
