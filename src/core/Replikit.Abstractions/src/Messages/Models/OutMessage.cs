using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Abstractions.Messages.Models.Buttons;
using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Abstractions.Messages.Models.Keyboard;
using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Abstractions.Messages.Models;

/// <summary>
/// Represents the model of an outgoing message which should be sent by the bot.
/// </summary>
public record OutMessage
{
    private OutMessageText _text = new();
    private IList<OutAttachment> _attachments = new List<OutAttachment>();
    private IList<ChannelMessageIdentifier> _forwardedMessages = new List<ChannelMessageIdentifier>();
    private ButtonMatrix<IInlineButton> _inlineButtons = new();
    private Identifier? _reply;
    private MessageKeyboard _keyboard = new();

    /// <summary>
    /// The collection of text tokens to send.
    /// </summary>
    public OutMessageText Text
    {
        get => _text;
        set => _text = Check.NotNull(value);
    }

    /// <summary>
    /// The collection of attachments to send.
    /// </summary>
    public IList<OutAttachment> Attachments
    {
        get => _attachments;
        set => _attachments = Check.NotNull(value);
    }

    /// <summary>
    /// The collection of message identifiers to forward.
    /// </summary>
    public IList<ChannelMessageIdentifier> ForwardedMessages
    {
        get => _forwardedMessages;
        set => _forwardedMessages = Check.NotNull(value);
    }

    /// <summary>
    /// The matrix of inline buttons to send.
    /// </summary>
    public ButtonMatrix<IInlineButton> InlineButtons
    {
        get => _inlineButtons;
        set => _inlineButtons = Check.NotNull(value);
    }

    /// <summary>
    /// The identifier of the message part to reply to.
    /// </summary>
    public Identifier? Reply
    {
        get => _reply;
        set => _reply = Check.NullOrNotDefault(value);
    }

    /// <summary>
    /// The keyboard to send.
    /// </summary>
    public MessageKeyboard Keyboard
    {
        get => _keyboard;
        set => _keyboard = Check.NotNull(value);
    }

    /// <summary>
    /// Creates a new <see cref="OutMessage"/> containing the specified <see cref="TextToken"/>.
    /// </summary>
    /// <param name="textToken">Text token to send.</param>
    /// <returns>The created <see cref="OutMessage"/>.</returns>
    public static implicit operator OutMessage(TextToken textToken)
    {
        return new OutMessage { Text = textToken };
    }

    /// <summary>
    /// Creates a new <see cref="OutMessage"/> containing the specified <seealso cref="OutAttachment"/>.
    /// </summary>
    /// <param name="attachment">Attachment to send.</param>
    /// <returns>The created <see cref="OutMessage"/>.</returns>
    public static implicit operator OutMessage(OutAttachment attachment)
    {
        return new OutMessage { Attachments = { attachment } };
    }

    /// <summary>
    /// Creates a new <see cref="OutMessage"/> containing the specified text.
    /// </summary>
    /// <param name="text">Text to send.</param>
    /// <returns>The created <see cref="OutMessage"/>.</returns>
    public static implicit operator OutMessage(string text)
    {
        return new OutMessage { Text = text };
    }

    /// <summary>
    /// Creates a new <see cref="OutMessage"/> containing the specified text.
    /// </summary>
    /// <param name="text">Text to send.</param>
    /// <returns>The created <see cref="OutMessage"/>.</returns>
    public static implicit operator OutMessage(OutMessageText text)
    {
        return new OutMessage { Text = text };
    }
}
