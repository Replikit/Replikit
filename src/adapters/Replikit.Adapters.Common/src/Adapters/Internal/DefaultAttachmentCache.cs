using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Attachments.Services;

namespace Replikit.Adapters.Common.Adapters.Internal;

internal class DefaultAttachmentCache : IAttachmentCache
{
    public Task SaveAsync(IReadOnlyList<SentAttachment> attachments, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<OutAttachment>> ResolveAsync(IEnumerable<OutAttachment> attachments,
        CancellationToken cancellationToken)
    {
        return Task.FromResult<IReadOnlyList<OutAttachment>>(attachments.ToList());
    }

    public static DefaultAttachmentCache Instance { get; } = new();
}
