using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models.TextTokens;

public record MentionTextToken(
    string Text,
    string? Username = null,
    Identifier? AccountId = null,
    TextTokenModifiers Modifiers = TextTokenModifiers.None
) : TextToken(Text, Modifiers);
