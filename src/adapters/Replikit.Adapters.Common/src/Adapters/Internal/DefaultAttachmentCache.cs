using Replikit.Abstractions.Attachments.Features;
using Replikit.Abstractions.Attachments.Models;

namespace Replikit.Adapters.Common.Adapters.Internal;

internal class DefaultAttachmentCache : IAttachmentCache
{
    public Task SaveAsync(IReadOnlyList<SentAttachment> attachments, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Attachment>> ResolveAsync(IReadOnlyList<Attachment> attachments,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(attachments);
    }

    public static DefaultAttachmentCache Instance { get; } = new();
}
