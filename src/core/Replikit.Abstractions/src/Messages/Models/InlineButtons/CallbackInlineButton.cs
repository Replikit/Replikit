using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Messages.Models.InlineButtons;

/// <summary>
/// The inline button that sends a payload back to the bot when pressed.
/// </summary>
/// <param name="Text">A text of the button.</param>
/// <param name="Payload">A payload of the button.</param>
public record CallbackInlineButton(string Text, string Payload) : IInlineButton
{
    /// <summary>
    /// <inheritdoc cref="IInlineButton.Text"/>
    /// </summary>
    public string Text { get; } = Check.NotNull(Text);

    /// <summary>
    /// The string-based payload that is sent back to the bot when the button is pressed.
    /// </summary>
    public string Payload { get; } = Check.NotNull(Payload);
}
