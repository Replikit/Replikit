using Replikit.Abstractions.Messages.Models.Buttons;

namespace Replikit.Abstractions.Messages.Models.Keyboard;

/// <summary>
/// Represents a keyboard with buttons.
/// </summary>
public class MessageKeyboard : ButtonMatrix<KeyboardButton>
{
    /// <summary>
    /// Indicates that the previous keyboard should be removed.
    /// </summary>
    public bool RemoveKeyboard { get; private init; }

    /// <summary>
    /// The intent to remove the previous keyboard.
    /// </summary>
    public new static MessageKeyboard Remove { get; } = new() { RemoveKeyboard = true };
}
