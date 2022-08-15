namespace Replikit.Abstractions.Messages.Models.Buttons;

public class ButtonMatrix<TButton> : List<IList<TButton>>, IButtonMatrix<TButton>
{
    public TButton this[int row, int column]
    {
        get => this[row][column];
        set => this[row][column] = value;
    }

    public void Add(TButton button)
    {
        Add(new List<TButton> { button });
    }

    public void Add(params TButton[] buttonRow)
    {
        base.Add(buttonRow.ToList());
    }
}
