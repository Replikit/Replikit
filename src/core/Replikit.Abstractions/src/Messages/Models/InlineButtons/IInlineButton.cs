namespace Replikit.Abstractions.Messages.Models.InlineButtons;

/// <summary>
/// Represents a button which can be embedded in a message.
/// </summary>
public interface IInlineButton
{
    /// <summary>
    /// The text of the button.
    /// </summary>
    string Text { get; }
}
