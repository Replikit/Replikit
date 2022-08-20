namespace Replikit.Abstractions.Messages.Models.TextTokens;

/// <summary>
/// Represents a special <see cref="TextToken"/> for a link.
/// </summary>
/// <param name="Text">A text to be displayed for the link.</param>
/// <param name="Url">The URL of the link.</param>
/// <param name="Modifiers">A union of <see cref="TextTokenModifiers"/> flags.</param>
public record LinkTextToken(
    string Text,
    Uri? Url = null,
    TextTokenModifiers Modifiers = TextTokenModifiers.None
) : TextToken(Text, Modifiers)
{
    /// <summary>
    /// The URL of the link.
    /// </summary>
    public Uri? Url { get; } = Url;
}
