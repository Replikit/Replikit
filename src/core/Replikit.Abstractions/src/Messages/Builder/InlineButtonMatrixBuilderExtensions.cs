using Replikit.Abstractions.Messages.Models.InlineButtons;

namespace Replikit.Abstractions.Messages.Builder;

public static class InlineButtonMatrixBuilderExtensions
{
    public static TBuilder AddButton<TBuilder>(this TBuilder builder, int row, IInlineButton button)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return ButtonMatrixBuilderExtensions.AddButton(builder, row, button);
    }

    public static TBuilder AddButton<TBuilder>(this TBuilder builder, IInlineButton button)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return ButtonMatrixBuilderExtensions.AddButton(builder, button);
    }

    public static TBuilder AddCallbackButton<TBuilder>(this TBuilder builder, int row, string text, string data)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return builder.AddButton(row, new CallbackInlineButton(text, data));
    }

    public static TBuilder AddCallbackButton<TBuilder>(this TBuilder builder, string text, string data)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return builder.AddButton(new CallbackInlineButton(text, data));
    }

    public static TBuilder AddLinkButton<TBuilder>(this TBuilder builder, int row, string text, Uri url)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return builder.AddButton(row, new LinkInlineButton(text, url));
    }

    public static TBuilder AddLinkButton<TBuilder>(this TBuilder builder, int row, string text, string url)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return builder.AddButton(row, new LinkInlineButton(text, new Uri(url)));
    }

    public static TBuilder AddLinkButton<TBuilder>(this TBuilder builder, string text, Uri url)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return builder.AddButton(new LinkInlineButton(text, url));
    }

    public static TBuilder AddLinkButton<TBuilder>(this TBuilder builder, string text, string url)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return builder.AddButton(new LinkInlineButton(text, new Uri(url)));
    }

    public static TBuilder AddButtonRow<TBuilder>(this TBuilder builder, params IInlineButton[] buttons)
        where TBuilder : ButtonMatrixBuilder<TBuilder, IInlineButton>
    {
        return ButtonMatrixBuilderExtensions.AddButtonRow(builder, buttons);
    }
}
