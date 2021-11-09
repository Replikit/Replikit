namespace Replikit.Abstractions.Messages.Models.Tokens;

[Flags]
public enum TextTokenModifiers
{
    None = 0,
    Bold = 1 << 0,
    Italic = 1 << 1,
    Underline = 1 << 2,
    Strikethrough = 1 << 3,
    Code = 1 << 4,
    InlineCode = 1 << 5
}
