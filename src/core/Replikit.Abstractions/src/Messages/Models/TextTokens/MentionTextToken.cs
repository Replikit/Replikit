using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Messages.Models.TextTokens;

/// <summary>
/// Represents a special <see cref="TextToken"/> for a mention.
/// </summary>
/// <param name="Text">A text to be displayed.</param>
/// <param name="Username">A username of the user to be mentioned.</param>
/// <param name="AccountId">An account id of the user to be mentioned.</param>
/// <param name="Modifiers">A union of <see cref="TextTokenModifiers"/> flags.</param>
public record MentionTextToken(
    string Text,
    string? Username = null,
    Identifier? AccountId = null,
    TextTokenModifiers Modifiers = TextTokenModifiers.None
) : TextToken(Text, Modifiers)
{
    /// <summary>
    /// The username of the user that was mentioned.
    /// </summary>
    public string? Username { get; } = Username;

    /// <summary>
    /// The account id of the user that was mentioned.
    /// </summary>
    public Identifier? AccountId { get; } = AccountId;
}
