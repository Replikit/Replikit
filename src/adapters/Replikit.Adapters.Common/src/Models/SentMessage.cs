using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Adapters.Common.Models;

public sealed record SentMessage : Message
{
    public SentMessage(MessageIdentifier identifier,
        IReadOnlyList<SentAttachment> resolvedAttachments,
        IReadOnlyList<object> originals,
        string? text = null
    ) : base(
        new GlobalMessageIdentifier(default, identifier),
        resolvedAttachments.Select(x => x.Attachment).ToArray(),
        originals, Text: text
    )
    {
        ResolvedAttachments = resolvedAttachments;
    }

    public IReadOnlyList<SentAttachment> ResolvedAttachments { get; init; }
}
