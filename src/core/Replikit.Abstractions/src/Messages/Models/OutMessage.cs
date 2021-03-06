using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Builder;
using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Abstractions.Messages.Models.Keyboard;
using Replikit.Abstractions.Messages.Models.Tokens;

namespace Replikit.Abstractions.Messages.Models;

public sealed record OutMessage
{
    public IReadOnlyList<TextToken> Tokens { get; init; }
    public IReadOnlyList<OutAttachment> Attachments { get; init; }
    public IReadOnlyList<GlobalMessageIdentifier> ForwardedMessages { get; init; }

    public ButtonMatrix<IInlineButton>? InlineButtonMatrix { get; init; }
    public MessageIdentifier? Reply { get; init; }
    public MessageKeyboard? MessageKeyboard { get; init; }

    public OutMessage(IReadOnlyList<TextToken>? tokens = null,
        IReadOnlyList<OutAttachment>? attachments = null,
        IReadOnlyList<GlobalMessageIdentifier>? forwardedMessages = null,
        MessageIdentifier? reply = null,
        ButtonMatrix<IInlineButton>? inlineButtonMatrix = null,
        MessageKeyboard? messageKeyboard = null)
    {
        Tokens = tokens ?? Array.Empty<TextToken>();
        Attachments = attachments ?? Array.Empty<OutAttachment>();
        ForwardedMessages = forwardedMessages ?? Array.Empty<GlobalMessageIdentifier>();
        Reply = reply;

        InlineButtonMatrix = inlineButtonMatrix;
        MessageKeyboard = messageKeyboard;
    }

    public static MessageBuilder CreateBuilder() => new();

    public static OutMessage FromToken(TextToken token)
    {
        return new MessageBuilder().WithText(token);
    }

    public static OutMessage FromText(object text, TextTokenModifiers modifiers = 0)
    {
        return FromToken(new TextToken(text.ToString() ?? string.Empty, modifiers));
    }

    public static OutMessage FromCode(object text)
    {
        return FromToken(new TextToken(text.ToString() ?? string.Empty, TextTokenModifiers.Code));
    }

    public static OutMessage FromAttachment(OutAttachment attachment)
    {
        return new OutMessage(attachments: new[] { attachment });
    }

    public static OutMessage FromAttachments(IEnumerable<OutAttachment> attachments)
    {
        return new OutMessage(attachments: attachments.ToArray());
    }

    public static implicit operator OutMessage(string text)
    {
        return new OutMessage(new[] { new TextToken(text) });
    }
}
