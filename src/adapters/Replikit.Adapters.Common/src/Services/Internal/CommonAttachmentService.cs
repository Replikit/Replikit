using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Attachments.Services;
using Replikit.Abstractions.Common.Models;
using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Adapters.Common.Services.Internal;

internal class CommonAttachmentService : IAttachmentService
{
    private readonly IAttachmentService _attachmentService;

    public CommonAttachmentService(IAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }

    public AttachmentServiceFeatures Features => _attachmentService.Features;

    public Task<Attachment?> GetAsync(Identifier attachmentId, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(attachmentId);

        return _attachmentService.GetAsync(attachmentId, cancellationToken);
    }

    public Task<Stream?> GetContentAsync(Identifier attachmentId, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(attachmentId);

        return _attachmentService.GetContentAsync(attachmentId, cancellationToken);
    }

    public Task<Uri?> GetUrlAsync(Identifier attachmentId, CancellationToken cancellationToken = default)
    {
        Check.NotDefault(attachmentId);

        return _attachmentService.GetUrlAsync(attachmentId, cancellationToken);
    }
}
