using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

/// <summary>
/// The sticker attachment.
/// </summary>
public class StickerAttachment : Attachment
{
    /// <summary>
    /// Creates a new instance of <see cref="StickerAttachment"/>.
    /// </summary>
    /// <param name="id">An identifier of the attachment.</param>
    public StickerAttachment(GlobalIdentifier id) : base(id, AttachmentType.Sticker) { }
}
