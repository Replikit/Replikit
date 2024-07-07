using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Messages.Models.Keyboard;

/// <summary>
/// Represents a button used in keyboards.
/// </summary>
/// <param name="Text">A text of the button.</param>
public record KeyboardButton(string Text)
{
    /// <summary>
    /// The text of the button.
    /// </summary>
    public string Text { get; } = Check.NotNull(Text);

    /// <summary>
    /// Creates a new <see cref="KeyboardButton"/> with the specified text.
    /// </summary>
    /// <param name="text">A text of the button.</param>
    /// <returns>A new <see cref="KeyboardButton"/> with the specified text.</returns>
    public static implicit operator KeyboardButton(string text) => new(text);
}
