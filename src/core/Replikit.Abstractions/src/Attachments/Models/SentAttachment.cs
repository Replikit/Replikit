namespace Replikit.Abstractions.Attachments.Models;

public sealed record SentAttachment(Attachment Attachment, Attachment? Original = null)
{
    /// <summary>
    /// Indicates that sent attachment was uploaded rather than used from the cache.
    /// </summary>
    public bool WasUploaded => Original is not null && Attachment.Id != Original.Id;
}
