namespace Replikit.Abstractions.Messages.Models.Tokens;

public record LinkTextToken(
    string Text,
    string? Url = null,
    TextTokenModifiers Modifiers = TextTokenModifiers.None
) : TextToken(Text, Modifiers);
