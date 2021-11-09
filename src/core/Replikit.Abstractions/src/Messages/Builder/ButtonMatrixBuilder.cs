using Replikit.Abstractions.Messages.Builder.Exceptions;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Abstractions.Messages.Builder;

public class ButtonMatrixBuilder<TButton> : ButtonMatrixBuilder<ButtonMatrixBuilder<TButton>, TButton> { }

public class ButtonMatrixBuilder<TBuilder, TButton> where TBuilder : ButtonMatrixBuilder<TBuilder, TButton>
{
    private readonly List<List<TButton>> _rows = new();

    public IReadOnlyList<IReadOnlyList<TButton>> Rows => _rows;

    public TBuilder InsertButton(int row, int column, TButton button)
    {
        CheckButtonIndexes(row, column);

        if (row > _rows.Count)
        {
            throw new ButtonIndexOverflowException(row, nameof(row));
        }

        if (row == _rows.Count)
        {
            if (column > 0)
            {
                throw new ButtonIndexOverflowException(column, nameof(column));
            }

            _rows.Add(new List<TButton> { button });
            return (TBuilder) this;
        }

        if (column > _rows[row].Count)
        {
            throw new ButtonIndexOverflowException(column, nameof(column));
        }

        _rows[row].Insert(column, button);

        return (TBuilder) this;
    }

    public TBuilder AddButtonRow()
    {
        if (_rows.Count == 0 || _rows[^1].Count > 1)
        {
            _rows.Add(new List<TButton>());
        }

        return (TBuilder) this;
    }

    public TBuilder RemoveButton(int row, int column)
    {
        CheckButtonIndexes(row, column);

        if (row > _rows.Count - 1 || _rows[row].Count > column - 1)
        {
            throw new ButtonDoesNotExistException(row, column);
        }

        _rows[row].RemoveAt(column);

        return (TBuilder) this;
    }

    private static void CheckButtonIndexes(int row, int column)
    {
        if (row < 0)
        {
            throw new NegativeButtonIndexException(row, nameof(row));
        }

        if (column < 0)
        {
            throw new NegativeButtonIndexException(column, nameof(column));
        }
    }

    public ButtonMatrix<TButton> Build() => new(_rows);
}
