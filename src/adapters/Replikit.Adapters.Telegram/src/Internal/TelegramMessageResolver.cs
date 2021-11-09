using Replikit.Abstractions.Attachments.Features;
using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Messages.Features;
using Replikit.Abstractions.Repositories.Features;
using Replikit.Adapters.Common.Utils;
using Replikit.Adapters.Telegram.Exceptions;
using Replikit.Adapters.Telegram.Models;
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

    public override async Task<InputMedia> ResolveSourceAsync(Attachment attachment,
        CancellationToken cancellationToken)
    {
        if (attachment.Id is null)
        {
            if (attachment.Content is not null)
            {
                return new InputMedia(attachment.Content, attachment.FileName);
            }

            attachment = await _repository.ResolveAttachmentUrlAsync(attachment, cancellationToken);
            return new InputMedia(attachment.Url);
        }

        if (attachment.Id.AdapterId != _adapterIdentifier)
        {
            throw new TelegramAttachmentResolutionException(
                "Adapter identifiers of attachment does not match: " +
                $"Expected {_adapterIdentifier}, got {attachment.Id.AdapterId}");
        }

        if (attachment.Id is not TelegramAttachmentIdentifier telegramIdentifier)
        {
            throw new TelegramAttachmentResolutionException(
                "Cannot reuse attachment whose identifier does not inherit from TelegramAttachmentIdentifier");
        }

        return new InputMedia(telegramIdentifier.UploadId);
    }
}
