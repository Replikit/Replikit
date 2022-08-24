using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.TextTokens;
using Replikit.Extensions.Views.Actions;

namespace Replikit.Extensions.Views;

public class ViewMessage : OutMessage
{
    public ViewActionMatrix Actions { get; set; } = new();

    /// <summary>
    /// Creates a new <see cref="ViewMessage"/> containing the specified <see cref="TextToken"/>.
    /// </summary>
    /// <param name="textToken">Text token to send.</param>
    /// <returns>The created <see cref="ViewMessage"/>.</returns>
    public static implicit operator ViewMessage(TextToken textToken)
    {
        return new ViewMessage { Text = textToken };
    }

    /// <summary>
    /// Creates a new <see cref="ViewMessage"/> containing the specified <seealso cref="OutAttachment"/>.
    /// </summary>
    /// <param name="attachment">Attachment to send.</param>
    /// <returns>The created <see cref="ViewMessage"/>.</returns>
    public static implicit operator ViewMessage(OutAttachment attachment)
    {
        return new ViewMessage() { Attachments = { attachment } };
    }

    /// <summary>
    /// Creates a new <see cref="ViewMessage"/> containing the specified text.
    /// </summary>
    /// <param name="text">Text to send.</param>
    /// <returns>The created <see cref="ViewMessage"/>.</returns>
    public static implicit operator ViewMessage(string text)
    {
        return new ViewMessage { Text = text };
    }
}
