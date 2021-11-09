using Replikit.Abstractions.Common.Models;

namespace Replikit.Abstractions.Attachments.Models;

public record StickerAttachment(
    GlobalIdentifier? Id = null,
    string? Caption = null,
    string? Url = null,
    string? FileName = null,
    Stream? Content = null,
    int? Size = null
) : AttachmentBase<StickerAttachment>(Id, Caption, Url, FileName, Content)
{
    public StickerAttachment() : this(Id: null) { }
}
