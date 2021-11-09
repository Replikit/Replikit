using Replikit.Abstractions.Attachments.Models;

namespace Replikit.Abstractions.Attachments.Features;

public interface IAttachmentCache
{
    public Task SaveAsync(IReadOnlyList<SentAttachment> attachments, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<Attachment>> ResolveAsync(IReadOnlyList<Attachment> attachments,
        CancellationToken cancellationToken = default);
}
