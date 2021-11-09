namespace Replikit.Adapters.Common.Models;

public readonly record struct ResolvedMessage<TAttachmentSource>(
    string? Text,
    IReadOnlyList<ResolvedAttachment<TAttachmentSource>> Attachments
);
