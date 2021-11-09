namespace Replikit.Abstractions.Messages.Models.Tokens;

public record TextToken(
    string Text,
    TextTokenModifiers Modifiers = TextTokenModifiers.None
)
{
    public static TextToken Empty { get; } = new(string.Empty);
    public static TextToken WhiteSpace { get; } = new(" ");
    public static TextToken NewLine { get; } = new("\n");
}
