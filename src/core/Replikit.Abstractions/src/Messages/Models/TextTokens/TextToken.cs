namespace Replikit.Abstractions.Messages.Models.TextTokens;

public record TextToken(string Text, TextTokenModifiers Modifiers = TextTokenModifiers.None)
{
    public static implicit operator TextToken(string text) => new(text);

    public static TextToken Bold(string text) => new(text, TextTokenModifiers.Bold);

    public static TextToken Code(string text) => new(text, TextTokenModifiers.Code);

    public static TextToken Italic(string text) => new(text, TextTokenModifiers.Italic);

    public static TextToken Strike(string text) => new(text, TextTokenModifiers.Strike);

    public static TextToken Underline(string text) => new(text, TextTokenModifiers.Underline);

    public static TextToken Spoiler(string text) => new(text, TextTokenModifiers.Spoiler);

    public static TextToken InlineCode(string text) => new(text, TextTokenModifiers.InlineCode);

    public static TextToken Line(string text, TextTokenModifiers modifiers = TextTokenModifiers.None) =>
        new(text + "\n", modifiers);
}
