namespace Replikit.Abstractions.Messages.Models.TextTokens;

public record LinkTextToken(
    string Text,
    string? Url = null,
    TextTokenModifiers Modifiers = TextTokenModifiers.None
) : TextToken(Text, Modifiers);
