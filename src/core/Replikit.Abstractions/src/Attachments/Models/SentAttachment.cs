using Replikit.Abstractions.Common.Utilities;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// Represents an attachment sent by the adapter.
/// </summary>
/// <param name="Attachment">An attachment returned by the adapter.</param>
/// <param name="OutAttachment">An attachment that was requested to send.</param>
public sealed record SentAttachment(Attachment Attachment, OutAttachment OutAttachment)
{
    /// <summary>
    /// The attachment returned by the adapter.
    /// </summary>
    public Attachment Attachment { get; } = Check.NotNull(Attachment);

    /// <summary>
    /// The attachment that was requested to send.
    /// </summary>
    public OutAttachment OutAttachment { get; } = Check.NotNull(OutAttachment);
}
