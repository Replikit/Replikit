namespace Replikit.Abstractions.Messages.Models.Keyboard;

public record KeyboardButton(string Text)
{
    public static implicit operator KeyboardButton(string text) => new(text);
}
