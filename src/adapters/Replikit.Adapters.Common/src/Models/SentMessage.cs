using Replikit.Abstractions.Attachments.Models;
using Replikit.Abstractions.Messages.Models;

namespace Replikit.Adapters.Common.Models;

public sealed record SentMessage(
    MessageIdentifier Identifier,
    IReadOnlyList<SentAttachment> ResolvedAttachments,
    IReadOnlyList<object> Originals,
    string? Text = null
) : Message(new GlobalMessageIdentifier(null!, Identifier.Identifiers),
    ResolvedAttachments.Select(x => x.Attachment).ToArray(),
    Originals, Text: Text);
