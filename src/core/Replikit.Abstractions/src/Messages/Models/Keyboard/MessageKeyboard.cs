namespace Replikit.Abstractions.Messages.Models.Keyboard;

public record MessageKeyboard(bool ShouldRemove, ButtonMatrix<KeyboardButton> ButtonMatrix)
{
    public static MessageKeyboard Remove { get; } = new(true, ButtonMatrix<KeyboardButton>.Empty);
}
