namespace Replikit.Abstractions.Messages.Models.TextTokens;

[Flags]
public enum TextTokenModifiers
{
    None = 0,
    Bold = 1 << 0,
    Italic = 1 << 1,
    Underline = 1 << 2,
    Strike = 1 << 3,
    Code = 1 << 4,
    InlineCode = 1 << 5,
    Spoiler = 1 << 6,
}
