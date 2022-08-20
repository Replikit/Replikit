namespace Replikit.Abstractions.Messages.Models.Buttons;

/// <summary>
/// Represents the matrix of buttons.
/// </summary>
/// <typeparam name="TButton">The type of the button.</typeparam>
public class ButtonMatrix<TButton> : List<IList<TButton>> where TButton : notnull
{
    /// <summary>
    /// Gets the button at the specified position.
    /// </summary>
    /// <param name="row">A zero-based row index.</param>
    /// <param name="column">A zero-based column index.</param>
    /// <exception cref="IndexOutOfRangeException">The button row or column is out of range.</exception>
    public TButton this[int row, int column]
    {
        get => this[row][column];
        set => this[row][column] = value;
    }

    /// <summary>
    /// Adds a row of buttons containing the single button.
    /// </summary>
    /// <param name="button">A button to create a row with.</param>
    public void Add(TButton button)
    {
        ArgumentNullException.ThrowIfNull(button);

        Add(new List<TButton> { button });
    }

    /// <summary>
    /// Adds a row of buttons containing the specified buttons.
    /// </summary>
    /// <param name="buttonRow">A button row to add.</param>
    public void Add(params TButton[] buttonRow)
    {
        ArgumentNullException.ThrowIfNull(buttonRow);

        base.Add(buttonRow.ToList());
    }
}
