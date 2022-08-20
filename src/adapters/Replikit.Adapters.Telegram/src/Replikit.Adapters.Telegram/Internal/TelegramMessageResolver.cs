using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Services;
using Replikit.Adapters.Common.Utils;
using Telegram.Bot.Types;

namespace Replikit.Adapters.Telegram.Internal;

internal class TelegramMessageResolver : MessageResolver<InputMedia>
{
    public TelegramMessageResolver(ITextFormatter textFormatter, IAttachmentCache attachmentCache) : base(textFormatter,
        attachmentCache) { }

    public override Task<InputMedia> ResolveSourceAsync(OutAttachment attachment,
        CancellationToken cancellationToken)
    {
        var source = attachment.Source switch
        {
            string url => new InputMedia(url),
            Identifier uploadId => new InputMedia(uploadId),
            Stream content => new InputMedia(content, attachment.FileName ?? "file"),
            FileInfo file => new InputMedia(file.OpenRead(), file.Name),
            _ => throw new InvalidOperationException("Unsupported attachment source")
        };

        return Task.FromResult(source);
    }
}
