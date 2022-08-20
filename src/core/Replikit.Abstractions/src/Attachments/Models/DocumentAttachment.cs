using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// The document attachment.
/// </summary>
public class DocumentAttachment : Attachment
{
    /// <summary>
    /// Creates a new instance of <see cref="DocumentAttachment"/>.
    /// </summary>
    /// <param name="id">An identifier of the attachment.</param>
    public DocumentAttachment(GlobalIdentifier id) : base(id, AttachmentType.Document) { }
}
