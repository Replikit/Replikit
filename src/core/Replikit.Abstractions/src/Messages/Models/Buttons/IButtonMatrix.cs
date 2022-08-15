namespace Replikit.Abstractions.Messages.Models.Buttons;

public interface IButtonMatrix<TButton> : IList<IList<TButton>>
{
    TButton this[int row, int column] { get; set; }

    void Add(TButton button);

    void Add(params TButton[] buttonRow);
}
