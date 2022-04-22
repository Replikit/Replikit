using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Services;
using Replikit.Abstractions.Repositories.Services;
using Replikit.Adapters.Common.Utils;
using Telegram.Bot.Types;

namespace Replikit.Adapters.Telegram.Internal;

internal class TelegramMessageResolver : MessageResolver<InputMedia>
{
    private readonly AdapterIdentifier _adapterIdentifier;
    private readonly IAdapterRepository _repository;

    public TelegramMessageResolver(AdapterIdentifier adapterIdentifier, ITextFormatter textFormatter,
        IAdapterRepository repository, IAttachmentCache attachmentCache) :
        base(textFormatter, attachmentCache)
    {
        _adapterIdentifier = adapterIdentifier;
        _repository = repository;
    }

    public override Task<InputMedia> ResolveSourceAsync(OutAttachment attachment,
        CancellationToken cancellationToken)
    {
        var source = attachment.Source switch
        {
            string url => new InputMedia(url),
            Identifier uploadId => new InputMedia(uploadId),
            Stream content => new InputMedia(content, attachment.FileName ?? "file"),
            FileInfo file => new InputMedia(file.OpenRead(), file.Name),
            _ => throw new InvalidOperationException("Unsupported operation source")
        };

        return Task.FromResult(source);
    }
}
