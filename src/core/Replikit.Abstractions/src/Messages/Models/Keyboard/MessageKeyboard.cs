using Replikit.Abstractions.Messages.Models.Buttons;

namespace Replikit.Abstractions.Messages.Models.Keyboard;

/// <summary>
/// Represents a keyboard with buttons.
/// </summary>
public class MessageKeyboard : ButtonMatrix<KeyboardButton>
{
    /// <summary>
    /// Creates an empty <see cref="MessageKeyboard"/>.
    /// </summary>
    public MessageKeyboard() { }

    /// <summary>
    /// Creates a new <see cref="MessageKeyboard"/> with the specified capacity.
    /// </summary>
    /// <param name="capacity">The number of button rows that the new keyboard can initially store.</param>
    public MessageKeyboard(int capacity) : base(capacity) { }

    /// <summary>
    /// Indicates that the previous keyboard should be removed.
    /// </summary>
    public bool RemoveKeyboard { get; private init; }

    /// <summary>
    /// The intent to remove the previous keyboard.
    /// </summary>
    public new static MessageKeyboard Remove { get; } = new() { RemoveKeyboard = true };
}
