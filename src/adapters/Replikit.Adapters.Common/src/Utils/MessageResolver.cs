using Replikit.Abstractions.Attachments.Features;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Messages.Models;
using Replikit.Adapters.Common.Models;

namespace Replikit.Adapters.Common.Utils;

public abstract class MessageResolver<TAttachmentSource>
{
    private readonly ITextFormatter _textFormatter;
    private readonly IAttachmentCache _attachmentCache;

    protected MessageResolver(ITextFormatter textFormatter, IAttachmentCache attachmentCache)
    {
        _textFormatter = textFormatter;
        _attachmentCache = attachmentCache;
    }

    public abstract Task<TAttachmentSource> ResolveSourceAsync(Attachment attachment,
        CancellationToken cancellationToken);

    public async Task<ResolvedAttachment<TAttachmentSource>> ResolveAttachmentAsync(Attachment attachment,
        CancellationToken cancellationToken = default)
    {
        return new ResolvedAttachment<TAttachmentSource>(attachment,
            await ResolveSourceAsync(attachment, cancellationToken));
    }

    public async ValueTask<ResolvedMessage<TAttachmentSource>> ResolveMessageAsync(OutMessage message,
        CancellationToken cancellationToken = default)
    {
        var text = _textFormatter.FormatText(message.Tokens);

        var attachments = await _attachmentCache.ResolveAsync(message.Attachments, cancellationToken);

        var tasks = attachments.Select(x => ResolveAttachmentAsync(x, cancellationToken));
        var resolvedAttachments = await Task.WhenAll(tasks);

        return new ResolvedMessage<TAttachmentSource>(text, resolvedAttachments);
    }

    public async ValueTask<NullableResolvedMessage<TAttachmentSource>> ResolveNullableMessageAsync(OutMessage? message,
        CancellationToken cancellationToken = default)
    {
        if (message is null) return new NullableResolvedMessage<TAttachmentSource>();

        var (text, attachments) = await ResolveMessageAsync(message, cancellationToken);
        return new NullableResolvedMessage<TAttachmentSource>(text, attachments);
    }
}
