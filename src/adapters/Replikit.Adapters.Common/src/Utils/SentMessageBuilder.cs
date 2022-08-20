using System.Collections.Immutable;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;
using Replikit.Abstractions.Messages.Models;
using Replikit.Adapters.Common.Models;

namespace Replikit.Adapters.Common.Utils;

public class SentMessageBuilder
{
    private readonly GlobalIdentifier _accountId;
    private readonly GlobalIdentifier _channelId;

    private readonly List<Identifier> _identifiers = new();
    private readonly List<SentAttachment> _attachments = new();
    private readonly List<object> _customData = new();

    private Identifier? _replyId;
    private string? _text;

    public SentMessageBuilder(BotIdentifier botId, Identifier channelId)
    {
        _accountId = new GlobalIdentifier(botId, botId.Value);
        _channelId = new GlobalIdentifier(botId, channelId);
    }

    public SentMessageBuilder SetText(string text)
    {
        Check.NotNull(text);

        _text = text;
        return this;
    }

    public SentMessageBuilder SetReplyId(Identifier replyId)
    {
        Check.NotDefault(replyId);

        _replyId = replyId;
        return this;
    }

    public SentMessageBuilder AddCustomData(object customData)
    {
        Check.NotNull(customData);

        _customData.Add(customData);
        return this;
    }

    public SentMessageBuilder AddPartIdentifier(Identifier identifier)
    {
        Check.NotDefault(identifier);

        _identifiers.Add(identifier);
        return this;
    }

    public SentMessageBuilder AddAttachment(Attachment attachment, OutAttachment outAttachment)
    {
        Check.NotNull(attachment);
        Check.NotNull(outAttachment);

        _attachments.Add(new SentAttachment(attachment, outAttachment));
        return this;
    }

    public Message Build()
    {
        var messageId = new GlobalMessageIdentifier(_channelId, _identifiers);

        _customData.Add(new SentMessageCustomData(_attachments));

        return new Message(messageId)
        {
            Text = _text,
            Attachments = _attachments.Select(x => x.Attachment).ToImmutableArray(),
            CustomData = _customData,
            ReplyId = _replyId,
            ChannelId = _channelId,
            AccountId = _accountId
        };
    }
}
