using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Models;
using Replikit.Adapters.Common.Models;

namespace Replikit.Adapters.Common.Utils;

public class SentMessageBuilder
{
    private readonly List<Identifier> _identifiers = new();
    private readonly List<SentAttachment> _attachments = new();
    private readonly List<object> _originals = new();
    private string? _text;

    public SentMessageBuilder SetText(string text)
    {
        _text = text;
        return this;
    }

    public SentMessageBuilder AddOriginal(object original)
    {
        _originals.Add(original);
        return this;
    }

    public SentMessageBuilder AddOriginals(IEnumerable<object> originals)
    {
        _originals.AddRange(originals);
        return this;
    }

    public SentMessageBuilder AddIdentifier(Identifier identifier)
    {
        _identifiers.Add(identifier);
        return this;
    }

    public SentMessageBuilder AddAttachment(Attachment attachment, Attachment? original = null)
    {
        _attachments.Add(new SentAttachment(attachment, original));
        return this;
    }

    public SentMessage Build()
    {
        var messageId = new MessageIdentifier(_identifiers);
        return new SentMessage(messageId, _attachments, _originals, _text);
    }
}
