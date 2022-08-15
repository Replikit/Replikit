using Replikit.Abstractions.Messages.Models.Buttons;

namespace Replikit.Abstractions.Messages.Models.Keyboard;

public class MessageKeyboard : ButtonMatrix<KeyboardButton>
{
    public bool RemoveKeyboard { get; private init; }

    public static MessageKeyboard Removed { get; } = new() { RemoveKeyboard = true };
}
