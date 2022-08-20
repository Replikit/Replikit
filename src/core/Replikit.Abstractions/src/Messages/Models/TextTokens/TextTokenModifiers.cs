namespace Replikit.Abstractions.Messages.Models.TextTokens;

/// <summary>
/// The flags that can be used to format a text token.
/// </summary>
[Flags]
public enum TextTokenModifiers
{
    /// <summary>
    /// No formatting.
    /// </summary>
    None = 0,

    /// <summary>
    /// The text is bold.
    /// </summary>
    Bold = 1 << 0,

    /// <summary>
    /// The text is italic.
    /// </summary>
    Italic = 1 << 1,

    /// <summary>
    /// The text is underlined.
    /// </summary>
    Underline = 1 << 2,

    /// <summary>
    /// The text is strikethrough.
    /// </summary>
    Strike = 1 << 3,

    /// <summary>
    /// The text is monospace (multiline code).
    /// </summary>
    Code = 1 << 4,

    /// <summary>
    /// The text is a single line code.
    /// </summary>
    InlineCode = 1 << 5,

    /// <summary>
    /// The text is a spoiler.
    /// </summary>
    Spoiler = 1 << 6,
}
