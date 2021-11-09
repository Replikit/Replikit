namespace Replikit.Adapters.Common.Models;

public readonly record struct NullableResolvedMessage<TAttachmentSource>(
    string? Text,
    IReadOnlyList<ResolvedAttachment<TAttachmentSource>>? Attachments
);
