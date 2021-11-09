using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Abstractions.Messages.Models.Tokens;

namespace Replikit.Abstractions.Messages.Builder;

public static class MessageBuilderExtensions
{
    public static TBuilder WithKeyboard<TBuilder>(this TBuilder builder, Action<MessageKeyboardBuilder> buildDelegate)
        where TBuilder : MessageBuilder<TBuilder>
    {
        buildDelegate.Invoke(builder.KeyboardBuilder);
        return builder;
    }

    public static TBuilder WithInlineButtons<TBuilder>(this TBuilder builder,
        Action<ButtonMatrixBuilder<IInlineButton>> buildDelegate)
        where TBuilder : MessageBuilder<TBuilder>
    {
        buildDelegate.Invoke(builder.InlineButtonBuilder);
        return builder;
    }

    public static TBuilder AddText<TBuilder>(this TBuilder builder, string text,
        TextTokenModifiers modifiers = TextTokenModifiers.None)
        where TBuilder : MessageBuilder<TBuilder>
    {
        return builder.WithText(new TextToken(text, modifiers));
    }

    public static TBuilder AddTextLine<TBuilder>(this TBuilder builder, string? line = null,
        TextTokenModifiers modifiers = TextTokenModifiers.None)
        where TBuilder : MessageBuilder<TBuilder>
    {
        return builder.AddText(line + "\n", modifiers);
    }

    public static TBuilder AddTextLines<TBuilder>(this TBuilder builder, IEnumerable<string> lines,
        TextTokenModifiers modifiers = TextTokenModifiers.None)
        where TBuilder : MessageBuilder<TBuilder>
    {
        return builder.AddText(string.Join("\n", lines), modifiers);
    }

    public static TBuilder AddCodeLine<TBuilder>(this TBuilder builder, string? line = null)
        where TBuilder : MessageBuilder<TBuilder>
    {
        return builder.AddText(line + "\n", TextTokenModifiers.Code);
    }

    public static TBuilder AddCodeLines<TBuilder>(this TBuilder builder, IEnumerable<string> lines)
        where TBuilder : MessageBuilder<TBuilder>
    {
        return builder.AddTextLines(lines, TextTokenModifiers.Code);
    }
}
