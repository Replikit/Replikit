using Replikit.Abstractions.Messages.Builder.Exceptions;

namespace Replikit.Abstractions.Messages.Builder;

public static class ButtonMatrixBuilderExtensions
{
    public static TBuilder AddButton<TBuilder, TButton>(this TBuilder builder, int row, TButton button)
        where TBuilder : ButtonMatrixBuilder<TBuilder, TButton>
    {
        if (row > builder.Rows.Count)
        {
            throw new ButtonIndexOverflowException(row, nameof(row));
        }

        var column = row == builder.Rows.Count ? 0 : builder.Rows[row].Count;

        return builder.InsertButton(row, column, button);
    }

    public static TBuilder AddButton<TBuilder, TButton>(this TBuilder builder, TButton button)
        where TBuilder : ButtonMatrixBuilder<TBuilder, TButton>
    {
        int row, column;

        if (builder.Rows.Count > 0)
        {
            row = builder.Rows.Count - 1;
            column = builder.Rows[row].Count;
        }
        else
        {
            row = 0;
            column = 0;
        }

        return builder.InsertButton(row, column, button);
    }

    public static TBuilder AddButtons<TBuilder, TButton>(this TBuilder builder, IEnumerable<TButton> buttons)
        where TBuilder : ButtonMatrixBuilder<TBuilder, TButton>
    {
        var rowIndex = builder.Rows.Count;

        foreach (var button in buttons)
        {
            var column = builder.Rows.Count > 0 ? builder.Rows[^1].Count : 0;
            builder.InsertButton(rowIndex, column, button);
        }

        return builder;
    }

    public static TBuilder AddButtons<TBuilder, TButton>(this TBuilder builder, params TButton[] buttons)
        where TBuilder : ButtonMatrixBuilder<TBuilder, TButton>
    {
        return builder.AddButtons((IEnumerable<TButton>) buttons);
    }

    public static TBuilder AddButtonRow<TBuilder, TButton>(this TBuilder builder, IEnumerable<TButton> buttons)
        where TBuilder : ButtonMatrixBuilder<TBuilder, TButton>
    {
        builder.AddButtonRow();
        builder.AddButtons(buttons);

        return builder;
    }

    public static TBuilder AddButtonRow<TBuilder, TButton>(this TBuilder builder, params TButton[] buttons)
        where TBuilder : ButtonMatrixBuilder<TBuilder, TButton>
    {
        return builder.AddButtonRow((IEnumerable<TButton>) buttons);
    }
}
