using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Messages.Models.TextTokens;

/// <summary>
/// Represents a part of a text message containing formatting or other special information.
/// </summary>
/// <param name="Text">A text message.</param>
/// <param name="Modifiers">A union of <see cref="TextTokenModifiers"/> flags.</param>
public record TextToken(string Text, TextTokenModifiers Modifiers = TextTokenModifiers.None)
{
    /// <summary>
    /// The text of the token.
    /// </summary>
    public string Text { get; } = Check.NotNull(Text);

    /// <summary>
    /// The union of <see cref="TextTokenModifiers"/> flags.
    /// </summary>
    public TextTokenModifiers Modifiers { get; } = Modifiers;

    /// <summary>
    /// Creates a new <see cref="TextToken"/> with the specified text and bold modifier.
    /// </summary>
    /// <param name="text">A text of the token.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static TextToken Bold(string text) => new(text, TextTokenModifiers.Bold);

    /// <summary>
    /// Creates a new <see cref="TextToken"/> with the specified text and code modifier.
    /// </summary>
    /// <param name="text">A text of the token.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static TextToken Code(string text) => new(text, TextTokenModifiers.Code);

    /// <summary>
    /// Creates a new <see cref="TextToken"/> with the specified text and italic modifier.
    /// </summary>
    /// <param name="text">A text of the token.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static TextToken Italic(string text) => new(text, TextTokenModifiers.Italic);

    /// <summary>
    /// Creates a new <see cref="TextToken"/> with the specified text and strikethrough modifier.
    /// </summary>
    /// <param name="text">A text of the token.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static TextToken Strike(string text) => new(text, TextTokenModifiers.Strike);

    /// <summary>
    /// Creates a new <see cref="TextToken"/> with the specified text and underline modifier.
    /// </summary>
    /// <param name="text">A text of the token.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static TextToken Underline(string text) => new(text, TextTokenModifiers.Underline);

    /// <summary>
    /// Creates a new <see cref="TextToken"/> with the specified text and spoiler modifier.
    /// </summary>
    /// <param name="text">A text of the token.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static TextToken Spoiler(string text) => new(text, TextTokenModifiers.Spoiler);

    /// <summary>
    /// Creates a new <see cref="TextToken"/> with the specified text and inline code modifier.
    /// </summary>
    /// <param name="text">A text of the token.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static TextToken InlineCode(string text) => new(text, TextTokenModifiers.InlineCode);

    /// <summary>
    /// Creates a new <see cref="TextToken"/> with the specified text with appended newline and specified modifiers.
    /// </summary>
    /// <param name="text">A text of the token.</param>
    /// <param name="modifiers">A union of <see cref="TextTokenModifiers"/> flags.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static TextToken Line(string? text = null, TextTokenModifiers modifiers = TextTokenModifiers.None) =>
        new(text + "\n", modifiers);

    /// <summary>
    /// Creates a new <see cref="LinkTextToken"/>
    /// </summary>
    /// <param name="text">A text of the link.</param>
    /// <param name="url"></param>
    /// <param name="modifiers">A union of <see cref="TextTokenModifiers"/> flags.</param>
    /// <returns>The created <see cref="LinkTextToken"/></returns>
    public static TextToken Link(string text, Uri? url = null,
        TextTokenModifiers modifiers = TextTokenModifiers.None) =>
        new LinkTextToken(text, url, modifiers);

    /// <summary>
    /// Creates a new <see cref="TextToken"/> from the plain text.
    /// </summary>
    /// <param name="text">A plain text.</param>
    /// <returns>The created <see cref="TextToken"/>.</returns>
    public static implicit operator TextToken(string text) => new(text);
}
