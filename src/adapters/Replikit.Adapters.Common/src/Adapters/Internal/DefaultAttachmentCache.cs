using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Attachments.Services;

namespace Replikit.Adapters.Common.Adapters.Internal;

internal class DefaultAttachmentCache : IAttachmentCache
{
    public Task SaveAsync(IReadOnlyList<SentAttachment> attachments, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<OutAttachment>> ResolveAsync(IReadOnlyList<OutAttachment> attachments,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(attachments);
    }

    public static DefaultAttachmentCache Instance { get; } = new();
}
