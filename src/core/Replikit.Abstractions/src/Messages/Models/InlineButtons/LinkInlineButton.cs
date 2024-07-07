using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Messages.Models.InlineButtons;

/// <summary>
/// The inline button that opens a link when pressed.
/// </summary>
/// <param name="Text">A text of the button.</param>
/// <param name="Url">A URL to open.</param>
public record LinkInlineButton(string Text, Uri Url) : IInlineButton
{
    /// <summary>
    /// <inheritdoc cref="IInlineButton.Text"/>
    /// </summary>
    public string Text { get; } = Check.NotNull(Text);

    /// <summary>
    /// The URL to open when the button is pressed.
    /// </summary>
    public Uri Url { get; } = Check.NotNull(Url);
}
