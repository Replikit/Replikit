using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Abstractions.Messages.Models.InlineButtons;
using Replikit.Abstractions.Messages.Models.Tokens;

namespace Replikit.Abstractions.Messages.Builder;

public class MessageBuilder : MessageBuilder<MessageBuilder> { }

public class MessageBuilder<TBuilder> where TBuilder : MessageBuilder<TBuilder>
{
    private List<TextToken>? _textTokens;
    private List<OutAttachment>? _attachments;
    private List<GlobalMessageIdentifier>? _forwardedMessages;
    private MessageIdentifier? _reply;

    private ButtonMatrixBuilder<IInlineButton>? _inlineButtonBuilder;

    public ButtonMatrixBuilder<IInlineButton> InlineButtonBuilder =>
        _inlineButtonBuilder ??= new ButtonMatrixBuilder<IInlineButton>();

    private MessageKeyboardBuilder? _keyboardBuilder;
    public MessageKeyboardBuilder KeyboardBuilder => _keyboardBuilder ??= new MessageKeyboardBuilder();

    public TBuilder WithReply(MessageIdentifier identifier)
    {
        _reply = identifier;

        return (TBuilder) this;
    }

    public TBuilder WithText(TextToken textToken)
    {
        _textTokens ??= new List<TextToken>();
        _textTokens.Add(textToken);

        return (TBuilder) this;
    }

    public TBuilder WithText(IEnumerable<TextToken> textTokens)
    {
        _textTokens ??= new List<TextToken>();
        _textTokens.AddRange(textTokens);

        return (TBuilder) this;
    }

    public TBuilder WithAttachment(OutAttachment attachment)
    {
        _attachments ??= new List<OutAttachment>();
        _attachments.Add(attachment);

        return (TBuilder) this;
    }

    public TBuilder WithAttachments(IEnumerable<OutAttachment> attachments)
    {
        _attachments ??= new List<OutAttachment>();
        _attachments.AddRange(attachments);

        return (TBuilder) this;
    }

    public TBuilder WithForwardedMessage(GlobalMessageIdentifier messageIdentifier)
    {
        _forwardedMessages ??= new List<GlobalMessageIdentifier>();
        _forwardedMessages.Add(messageIdentifier);

        return (TBuilder) this;
    }

    public TBuilder WithForwardedMessages(IEnumerable<GlobalMessageIdentifier> messageIdentifiers)
    {
        _forwardedMessages ??= new List<GlobalMessageIdentifier>();
        _forwardedMessages.AddRange(messageIdentifiers);

        return (TBuilder) this;
    }

    public OutMessage Build()
    {
        return new OutMessage(_textTokens,
            _attachments, _forwardedMessages, _reply,
            _inlineButtonBuilder?.Build(),
            _keyboardBuilder?.Build());
    }

    public static implicit operator OutMessage(MessageBuilder<TBuilder> builder)
    {
        return builder.Build();
    }
}
