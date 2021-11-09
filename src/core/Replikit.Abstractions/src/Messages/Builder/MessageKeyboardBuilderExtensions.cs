using Replikit.Abstractions.Messages.Models.Keyboard;

namespace Replikit.Abstractions.Messages.Builder;

public static class MessageKeyboardBuilderExtensions
{
    public static TBuilder AddButton<TBuilder>(this TBuilder builder, int row, KeyboardButton button)
        where TBuilder : MessageKeyboardBuilder<TBuilder>
    {
        builder.ButtonMatrixBuilder.AddButton(row, button);
        return builder;
    }

    public static TBuilder AddButton<TBuilder>(this TBuilder builder, KeyboardButton button)
        where TBuilder : MessageKeyboardBuilder<TBuilder>
    {
        builder.ButtonMatrixBuilder.AddButton(button);
        return builder;
    }

    public static TBuilder AddButton<TBuilder>(this TBuilder builder, int row, string buttonText)
        where TBuilder : MessageKeyboardBuilder<TBuilder>
    {
        return builder.AddButton(row, new KeyboardButton(buttonText));
    }

    public static TBuilder AddButton<TBuilder>(this TBuilder builder, string buttonText)
        where TBuilder : MessageKeyboardBuilder<TBuilder>
    {
        return builder.AddButton(new KeyboardButton(buttonText));
    }

    public static TBuilder AddButtonRow<TBuilder>(this TBuilder builder)
        where TBuilder : MessageKeyboardBuilder<TBuilder>
    {
        builder.ButtonMatrixBuilder.AddButtonRow(Array.Empty<KeyboardButton>());
        return builder;
    }

    public static TBuilder AddButtonRow<TBuilder>(this TBuilder builder, params KeyboardButton[] buttons)
        where TBuilder : MessageKeyboardBuilder<TBuilder>
    {
        builder.ButtonMatrixBuilder.AddButtonRow(buttons);
        return builder;
    }

    public static TBuilder AddButtonRow<TBuilder>(this TBuilder builder, params string[] buttons)
        where TBuilder : MessageKeyboardBuilder<TBuilder>
    {
        builder.ButtonMatrixBuilder.AddButtonRow(buttons.Select(x => new KeyboardButton(x)).ToArray());
        return builder;
    }
}
