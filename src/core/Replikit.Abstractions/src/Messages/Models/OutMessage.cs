using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models.Buttons;
using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Abstractions.Messages.Models.Keyboard;
using Replikit.Abstractions.Messages.Models.TextTokens;

namespace Replikit.Abstractions.Messages.Models;

public class OutMessage
{
    public TextTokenList Text { get; set; } = new();
    public IList<OutAttachment> Attachments { get; set; } = new List<OutAttachment>();
    public IList<ChannelMessageIdentifier> ForwardedMessages { get; set; } = new List<ChannelMessageIdentifier>();
    public IButtonMatrix<IInlineButton> InlineButtons { get; set; } = new ButtonMatrix<IInlineButton>();
    public Identifier? Reply { get; set; }
    public MessageKeyboard Keyboard { get; set; } = new();

    public static OutMessage FromText(string text, TextTokenModifiers modifiers = TextTokenModifiers.None)
    {
        return new OutMessage { Text = new TextToken(text, modifiers) };
    }

    public static OutMessage FromCode(string text)
    {
        return new OutMessage { Text = new TextToken(text, TextTokenModifiers.Code) };
    }

    public static implicit operator OutMessage(TextToken textToken)
    {
        return new OutMessage { Text = textToken };
    }

    public static implicit operator OutMessage(OutAttachment attachment)
    {
        return new OutMessage { Attachments = { attachment } };
    }

    public static implicit operator OutMessage(string text)
    {
        return new OutMessage { Text = text };
    }
}
