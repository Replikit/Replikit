using Replikit.Abstractions.Attachments.Models;

namespace Replikit.Abstractions.Attachments.Services;

public interface IAttachmentCache
{
    public Task SaveAsync(IReadOnlyList<SentAttachment> attachments, CancellationToken cancellationToken = default);

    public Task<IReadOnlyList<OutAttachment>> ResolveAsync(IEnumerable<OutAttachment> attachments,
        CancellationToken cancellationToken = default);
}
